/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using UnityEngine;

namespace Paroxe.PdfRenderer
{
    /// <summary>
    /// Interface for device implementation. PDFViewer implements it. It allows to
    /// decouple PDFViewer from the API
    /// </summary>
    public interface IPDFDevice
    {
        Vector2 GetDevicePageSize(int pageIndex);

        void GoToPage(int pageIndex);

        PDFDocument GetLoadedDocument();

        IPDFDeviceActionHandler BookmarksActionHandler { get; set; }

        IPDFDeviceActionHandler LinksActionHandler { get; set; }

        bool AllowOpenURL { get; set; }

#if !UNITY_WEBGL
        void LoadDocument(PDFDocument document, string password, int pageIndex);

        void LoadDocumentFromAsset(PDFAsset pdfAsset, int pageIndex);

        void LoadDocumentFromAsset(PDFAsset pdfAsset, string password, int pageIndex);

        void LoadDocumentFromResources(string folder, string fileName, string password, int pageIndex);

        void LoadDocumentFromStreamingAssets(string folder, string fileName, string password, int pageIndex);

        void LoadDocumentFromWeb(string url, string password, int pageIndex);

        void LoadDocumentWithBuffer(byte[] buffer, string password, int pageIndex);

        void LoadDocumentWithFile(string filePath, string password, int pageIndex);
#endif
    }
}
