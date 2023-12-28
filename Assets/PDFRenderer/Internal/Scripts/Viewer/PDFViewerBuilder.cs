/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using System.Collections.Generic;
using System.Reflection;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace Paroxe.PdfRenderer.Internal.Viewer
{
    [ExecuteInEditMode]
    public class PDFViewerBuilder : UIBehaviour
    {
#if UNITY_EDITOR

        public static void BuildPDFViewerWithin(GameObject root, PDFViewer prefabViewer)
        {
            if (root.transform.childCount > 0)
            {
                int c = root.transform.childCount;
                for (int i = 0; i < c; ++i)
                {
                    Destroy(root.transform.GetChild(0).gameObject);
                }
            }

            PDFViewer viewer = root.GetComponent<PDFViewer>();

            if (viewer == null)
            {
                viewer = root.AddComponent<PDFViewer>();
            }

            if (prefabViewer != null)
            {
                CopyViewer(prefabViewer, viewer);
            }

            RectTransform rootTransform = root.GetComponent<RectTransform>();
            if (rootTransform == null)
            {
                rootTransform = root.AddComponent<RectTransform>();
                rootTransform.anchorMin = Vector2.zero;
                rootTransform.anchorMax = Vector2.one;
                rootTransform.offsetMin = Vector2.zero;
                rootTransform.offsetMax = Vector2.zero;
            }

            GameObject internalPrefab = (GameObject) AssetDatabase.LoadAssetAtPath(
                AssetDatabase.GUIDToAssetPath(AssetDatabase.FindAssets("PDFViewer_Internal")[0]), typeof (GameObject));

            GameObject viewerInternal = (GameObject) Instantiate(internalPrefab);
            PrefabUtility.DisconnectPrefabInstance(viewerInternal);

            viewerInternal.GetComponent<PDFViewerInternal>().m_PDFViewer = root.GetComponent<PDFViewer>();
            viewer.m_Internal = viewerInternal.GetComponent<PDFViewerInternal>();
            viewerInternal.name = "_Internal";
            RectTransform internalRectTransform = viewerInternal.transform as RectTransform;
            internalRectTransform.SetParent(viewer.transform);
            internalRectTransform.anchorMin = Vector2.zero;
            internalRectTransform.anchorMax = Vector2.one;
            internalRectTransform.offsetMin = Vector2.zero;
            internalRectTransform.offsetMax = Vector2.zero;

            Selection.activeGameObject = root;
        }

        public static string[] GetNewFeatures(GameObject root)
        {
            float version = float.Parse(root.GetComponent<PDFViewer>().m_Internal.m_Version);

            List<string> newFeatures = new List<string>();

            if (version < 2.2f)
            {
                newFeatures.Add("Version 2.2 includes:");
                newFeatures.Add("  -Bookmarks");
                newFeatures.Add("  -Thumbnails");
                newFeatures.Add("  -Links");
            }

            if (version < 3.0f)
            {
                newFeatures.Add("Version 3.0 includes:");
                newFeatures.Add("  -Search Panel");
            }

            return newFeatures.ToArray();
        }

        public static void UpdateToLatestVersion(GameObject root)
        {
            float version = float.Parse(root.GetComponent<PDFViewer>().m_Internal.m_Version);

            if (version < 2.2f)
                UpdateToVersion_22(root);
            if (version < 3.0f)
                UpdateToVersion_30(root);
        }

        public static void UpdateToVersion_22(GameObject root)
        {
            Transform internalTransform = null;
            int c = root.transform.childCount;
            for (int i = 0; i < c; ++i)
            {
                var obj = root.transform.GetChild(i);
                if (obj.name == "_Internal")
                {
                    internalTransform = obj;
                }
            }

            if (internalTransform != null)
            {

                GameObject leftPanelPrefab = (GameObject)AssetDatabase.LoadAssetAtPath(
                    AssetDatabase.GUIDToAssetPath(AssetDatabase.FindAssets("PDFViewer_Internal_LeftPanel")[0]), typeof(GameObject));

                GameObject leftPanelInternal = (GameObject) Instantiate(leftPanelPrefab);
                PrefabUtility.DisconnectPrefabInstance(leftPanelInternal);

                PDFViewer pdfViewer = root.GetComponent<PDFViewer>();

                internalTransform.GetComponent<PDFViewerInternal>().m_PDFViewer = pdfViewer;
                pdfViewer.m_Internal.m_LeftPanel = leftPanelInternal.GetComponent<PDFViewerLeftPanel>();
                leftPanelInternal.name = "_LeftPanel";

                RectTransform internalRectTransform = leftPanelInternal.transform as RectTransform;
                internalRectTransform.SetParent(internalTransform);
                internalRectTransform.SetSiblingIndex(pdfViewer.m_Internal.m_Overlay.transform.GetSiblingIndex());

                internalRectTransform.anchorMin = Vector2.zero;
                internalRectTransform.anchorMax = Vector2.up;
                internalRectTransform.offsetMin = new Vector2(0.0f, 0.0f);
                internalRectTransform.offsetMax = new Vector2(0.0f, 0.0f);
                internalRectTransform.pivot = Vector2.zero;
                internalRectTransform.sizeDelta = new Vector2(350.0f,
                    -pdfViewer.m_Internal.m_TopPanel.sizeDelta.y + 1.0f);

                if (pdfViewer.m_Internal.m_PageSample.gameObject.GetComponent<PDFViewerPage>() == null)
                {
                    PDFViewerPage pageSample =
                        pdfViewer.m_Internal.m_PageSample.gameObject.AddComponent<PDFViewerPage>();

                    string[] assetPaths = AssetDatabase.FindAssets("HandLinkCursor");
                    string path = "";

                    foreach (string asset in assetPaths)
                    {
                        if (AssetDatabase.GUIDToAssetPath(asset).Contains("PDFRenderer"))
                            path = AssetDatabase.GUIDToAssetPath(asset);
                    }

                    pageSample.m_HandCursor =
                        (Texture2D)
                            AssetDatabase.LoadAssetAtPath(path, typeof (Texture2D));
                }

                pdfViewer.m_Internal.m_Viewport.GetComponent<ScrollRect>().inertia = true;
                pdfViewer.m_Internal.m_Viewport.GetComponent<ScrollRect>().decelerationRate = 0.025f;

                pdfViewer.m_Internal.m_Version = "2.2";
            }
        }

        public static void UpdateToVersion_30(GameObject root)
        {
            PDFViewer pdfViewer = root.GetComponent<PDFViewer>();

            Transform documentIcon = pdfViewer.m_Internal.m_TopPanel.Find("DocumentIcon");
            if (documentIcon != null)
                DestroyImmediate(documentIcon.gameObject);

            GameObject searchPanelPrefab =
                (GameObject)
                    AssetDatabase.LoadAssetAtPath(
                        AssetDatabase.GUIDToAssetPath(AssetDatabase.FindAssets("PDFViewer_SearchPanel")[0]), typeof (GameObject));

            GameObject searchPanel = (GameObject) Instantiate(searchPanelPrefab);
            PrefabUtility.DisconnectPrefabInstance(searchPanel);

            pdfViewer.m_Internal.m_SearchPanel = searchPanel.transform as RectTransform;
            pdfViewer.m_Internal.m_SearchPanel.SetParent(pdfViewer.m_Internal.transform);
            pdfViewer.m_Internal.m_SearchPanel.SetSiblingIndex(pdfViewer.m_Internal.m_Viewport.GetSiblingIndex() + 1);
            pdfViewer.m_Internal.m_SearchPanel.anchoredPosition = new Vector2(-234.0f, -59.0f);
            pdfViewer.m_Internal.m_SearchPanel.sizeDelta = new Vector2(400.0f, 150.0f);
            pdfViewer.m_Internal.m_SearchPanel.name = "_SearchPanel";

            GameObject topPanelSearchPrefab =
                (GameObject)
                    AssetDatabase.LoadAssetAtPath(
                        AssetDatabase.GUIDToAssetPath(AssetDatabase.FindAssets("PDFViewer_TopPanel_Search")[0]),
                        typeof (GameObject));

            GameObject topPanelSearch = (GameObject) Instantiate(topPanelSearchPrefab);
            PrefabUtility.DisconnectPrefabInstance(topPanelSearch);

            topPanelSearch.transform.SetParent(pdfViewer.m_Internal.m_TopPanel);
            (topPanelSearch.transform as RectTransform).anchoredPosition = Vector2.zero;
            topPanelSearch.name = "_SearchButton";

            pdfViewer.m_Internal.m_PageCountLabel.resizeTextForBestFit = true;
            pdfViewer.m_Internal.m_PageCountLabel.resizeTextMinSize = 10;
            pdfViewer.m_Internal.m_PageCountLabel.resizeTextMaxSize = 22;

            pdfViewer.m_Internal.m_Version = "3.0";
        }

        private static void CopyViewer(PDFViewer prefabViewer, PDFViewer viewer)
        {
            BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance |
                                 BindingFlags.Default | BindingFlags.DeclaredOnly;
            PropertyInfo[] pinfos = typeof (PDFViewer).GetProperties(flags);
            foreach (var pinfo in pinfos)
            {
                if (pinfo.CanWrite)
                {
                    try
                    {
                        pinfo.SetValue(viewer, pinfo.GetValue(prefabViewer, null), null);
                    }
                    catch
                    {
                    }
                }
            }
            FieldInfo[] finfos = typeof (PDFViewer).GetFields(flags);
            foreach (var finfo in finfos)
            {
                finfo.SetValue(viewer, finfo.GetValue(prefabViewer));
            }
        }

        protected override void Start()
        {
            GameObject pdfViewer = new GameObject("PDFViewer");
            RectTransform rootTransform = pdfViewer.AddComponent<RectTransform>();
            rootTransform.SetParent(transform.parent);
            rootTransform.anchorMin = Vector2.zero;
            rootTransform.anchorMax = Vector2.one;
            rootTransform.offsetMin = Vector2.zero;
            rootTransform.offsetMax = Vector2.zero;

            BuildPDFViewerWithin(pdfViewer, GetComponent<PDFViewer>());
            UpdateToLatestVersion(pdfViewer);

            DestroyImmediate(gameObject);
        }

#endif
    }
}
