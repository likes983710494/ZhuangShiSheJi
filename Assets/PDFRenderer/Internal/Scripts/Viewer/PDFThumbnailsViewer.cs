/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Paroxe.PdfRenderer.Internal.Viewer
{
    public class PDFThumbnailsViewer : UIBehaviour
    {
        public PDFThumbnailItem m_ThumbnailItemPrefab;
        public RectTransform m_ThumbnailsContainer;

        private PDFPageRange m_CurrentPageRange;
        private PDFThumbnailItem m_HighlightedItem;
        private RectTransform m_LeftPanel;
        private PDFPageTextureHolder[] m_PageTextureHolders;
        private PDFDocument m_Document;
        private PDFViewer m_PDFViewer;
        private RectTransform m_RectTransform;
        private List<PDFThumbnailItem> m_ThumbnailList;
        private PDFViewerLeftPanel m_ViewerLeftPanel;
        private bool m_IsLoaded = false;
        private int m_UpdateFramesDelay;

        private void Cleanup()
        {
            m_IsLoaded = false;
            m_Document = null;
            m_HighlightedItem = null;

            if (m_PageTextureHolders != null)
            {
                foreach (PDFPageTextureHolder holder in m_PageTextureHolders)
                {
                    if (holder.Texture != null)
                    {
                        Texture2D tex = holder.Texture;
                        holder.Texture = null;

                        Destroy(tex);
                        Resources.UnloadUnusedAssets();
                    }
                }
            }

            m_PageTextureHolders = null;

            int childCount = m_ThumbnailItemPrefab.transform.parent.childCount;
            for (int i = 1; i < childCount; ++i)
            {
                Destroy(m_ThumbnailItemPrefab.transform.parent.GetChild(i).gameObject);
            }

            m_ThumbnailItemPrefab.gameObject.SetActive(false);
        }

        private PDFPageRange GetVisiblePageRange()
        {
            PDFPageRange pageRange = new PDFPageRange();

            int c = m_ThumbnailsContainer.childCount - 1;
            for (int i = 0; i < c; ++i)
            {
                if (PDFInternalUtils.CalculateRectTransformIntersectArea(m_ThumbnailsContainer.GetChild(i + 1) as RectTransform, m_RectTransform) > 0.0f)
                {
                    if (pageRange.m_From == -1)
                        pageRange.m_From = i;
                    else
                        pageRange.m_To = i + 1;
                }
                else if (pageRange.m_From != -1)
                    break;
            }

            if (pageRange.m_From != -1 && pageRange.m_To == -1)
                pageRange.m_To = pageRange.m_From + 1;

            return pageRange;
        }

        public void OnDocumentUnloaded()
        {
            Cleanup();
        }

        public void OnDocumentLoaded(PDFDocument document)
        {
            if (!m_IsLoaded && gameObject.activeInHierarchy)
            {
                m_Document = document;

                int c = m_Document.GetPageCount();

                m_PageTextureHolders = new PDFPageTextureHolder[c];
                m_ThumbnailList = new List<PDFThumbnailItem>();

                int currentPage = m_PDFViewer.CurrentPageIndex;

                m_ThumbnailItemPrefab.gameObject.SetActive(false);

                for (int i = 0; i < c; ++i)
                {
                    PDFThumbnailItem item = null;

                    item = ((GameObject)Instantiate(m_ThumbnailItemPrefab.gameObject)).GetComponent<PDFThumbnailItem>();
                    item.transform.SetParent(m_ThumbnailItemPrefab.transform.parent, false);
                    item.gameObject.SetActive(true);

                    item.m_Highlighted.gameObject.SetActive(false);
                    item.m_PageIndexLabel.text = (i + 1).ToString();

                    m_ThumbnailList.Add(item);

                    m_PageTextureHolders[i] = new PDFPageTextureHolder();
                    m_PageTextureHolders[i].m_PageIndex = i;
                    m_PageTextureHolders[i].m_Page = item.m_PageThumbnailRawImage.gameObject;
                    m_PageTextureHolders[i].m_PDFViewer = m_PDFViewer;
                    m_PageTextureHolders[i].Texture = null;
                }

                if (currentPage >= 0 && currentPage < m_PDFViewer.Document.GetPageCount())
                {
                    m_HighlightedItem = m_ThumbnailList[currentPage];
                    m_HighlightedItem.m_Highlighted.gameObject.SetActive(true);
                }

                m_CurrentPageRange = new PDFPageRange();

                m_UpdateFramesDelay = 2;
                m_IsLoaded = true;
            }
        }

        public void OnCurrentPageChanged(int newPageIndex)
        {
            if (!m_IsLoaded)
                return;

            if (m_HighlightedItem != null)
            {
                m_HighlightedItem.m_Highlighted.gameObject.SetActive(false);
            }

            if (newPageIndex >= 0)
            {
                m_HighlightedItem = m_ThumbnailList[newPageIndex];
                m_HighlightedItem.m_Highlighted.gameObject.SetActive(true);
            }

            UpdateHighlightedItem();
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            if (m_IsLoaded)
            {
                Cleanup();
            }
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            DoOnEnable();
        }

        public void DoOnEnable()
        {
            if (m_PDFViewer == null)
                m_PDFViewer = GetComponentInParent<PDFViewer>();
            if (m_RectTransform == null)
                m_RectTransform = transform as RectTransform;
            if (m_LeftPanel == null)
                m_LeftPanel = transform.parent as RectTransform;
            if (m_ViewerLeftPanel == null)
                m_ViewerLeftPanel = m_LeftPanel.GetComponent<PDFViewerLeftPanel>();
            if (m_CurrentPageRange == null)
                m_CurrentPageRange = new PDFPageRange();

            if (!m_IsLoaded
                && m_PDFViewer.Document != null
                && m_PDFViewer.Document.IsValid)
            {
                OnDocumentLoaded(m_PDFViewer.Document);
            }

            m_ThumbnailItemPrefab.gameObject.SetActive(false);
        }

        public void DoUpdate()
        {
            if (m_RectTransform.sizeDelta.x != m_LeftPanel.sizeDelta.x - 24.0f)
                m_RectTransform.sizeDelta = new Vector2(m_LeftPanel.sizeDelta.x - 24.0f, m_RectTransform.sizeDelta.y);

            if (!m_IsLoaded || !m_ViewerLeftPanel.IsOpened)
            {
                if (!m_ViewerLeftPanel.IsOpened)
                    m_UpdateFramesDelay = 2;
                return;
            }

            if (m_UpdateFramesDelay > 0)
            {
                --m_UpdateFramesDelay;
                return;
            }

            PDFPageRange pageRange = GetVisiblePageRange();

            if (!Equals(pageRange, m_CurrentPageRange))
            {
                PDFPageRange.UpdatePageAgainstRanges(m_CurrentPageRange, pageRange, m_Document, m_PageTextureHolders, null, 0.25f, null, m_PDFViewer.GetCachedNormalPageSizes());

                m_CurrentPageRange = pageRange;
            }
        }

        private void UpdateHighlightedItem()
        {
            if (m_HighlightedItem != null)
            {
                m_HighlightedItem.m_Highlighted.color = new Color(152.0f/255.0f, 192.0f/255.0f, 217.0f/255.0f, 1.0f);
            }
            else if (m_HighlightedItem != null)
            {
                m_HighlightedItem.m_Highlighted.color = new Color(200.0f/255.0f, 200.0f/255.0f, 200.0f/255.0f, 1.0f);
            }
        }
    }
}
