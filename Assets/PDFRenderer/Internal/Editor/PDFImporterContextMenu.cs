/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using System.IO;
using UnityEditor;
using UnityEngine;

namespace Paroxe.PdfRenderer.Internal
{
    public class PDFImporterContextMenu
    {
        static string extension = ".pdf";
        static string newExtension = ".asset";
#if UNITY_EDITOR
        [MenuItem("Assets/PDF Renderer/Convert to .asset")]
        public static void ConvertPDFToAsset()
        {
            string path = AssetDatabase.GetAssetPath(Selection.activeObject);
            string newPath = ConvertToInternalPath(path);

            PDFAsset numSeq = AssetDatabase.LoadAssetAtPath(newPath, typeof (PDFAsset)) as PDFAsset;
            bool loaded = (numSeq != null);

            if (!loaded)
            {
                numSeq = ScriptableObject.CreateInstance<PDFAsset>();
            }

            numSeq.Load(File.ReadAllBytes(path));

            if (!loaded)
            {
                AssetDatabase.CreateAsset(numSeq, newPath);
            }

            AssetDatabase.SaveAssets();
        }

        public static string ConvertToInternalPath(string asset)
        {
            string left = asset.Substring(0, asset.Length - extension.Length);
            return left + newExtension;
        }

        public static bool HasExtension(string asset)
        {
            return asset.EndsWith(extension, System.StringComparison.OrdinalIgnoreCase);
        }

        [MenuItem("Assets/PDF Renderer/Convert to .asset", true)]
        static bool ValidateConvertPDFToAsset()
        {
            string path = AssetDatabase.GetAssetPath(Selection.activeObject);
            return HasExtension(path);
        }
#endif
    }

}
