/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using UnityEngine;

namespace Paroxe.PdfRenderer.Internal.Viewer
{
    class PDFViewerDefaultActionHandler : IPDFDeviceActionHandler
    {
        public void HandleGotoAction(IPDFDevice device, int pageIndex)
        {
            device.GoToPage(pageIndex);
        }

        public void HandleLaunchAction(IPDFDevice device, string filePath)
        {
#if !UNITY_WEBGL
            if (filePath.Trim().Substring(filePath.Length - 4).ToLower().Contains("pdf"))
            {
                device.LoadDocumentWithFile(filePath, "", 0);
            }
#endif
        }

        public string HandleRemoteGotoActionPasswordResolving(IPDFDevice device, string resolvedFilePath)
        {
            return "";
        }

        public string HandleRemoteGotoActionPathResolving(IPDFDevice device, string filePath)
        {
            return filePath;
        }

        public void HandleRemoteGotoActionResolved(IPDFDevice device, PDFDocument document, int pageIndex)
        {
#if !UNITY_WEBGL
            device.LoadDocument(document, "", pageIndex);
#endif
        }

        public void HandleRemoteGotoActionUnresolved(IPDFDevice device, string resolvedFilePath)
        {
            // ...
        }

        public void HandleUnsuportedAction(IPDFDevice device)
        {
            // ...
        }

        public void HandleUriAction(IPDFDevice device, string uri)
        {
            if (uri.Trim().Substring(uri.Length - 4).ToLower().Contains("pdf"))
            {
#if !UNITY_WEBGL
                device.LoadDocumentFromWeb(uri, "", 0);
#endif
            }
            else if (device.AllowOpenURL)
            {
                if (uri.Trim().ToLowerInvariant().StartsWith("http:")
                    || uri.Trim().ToLowerInvariant().StartsWith("https:")
                    || uri.Trim().ToLowerInvariant().StartsWith("ftp:"))
                {
                    Application.OpenURL(uri);
                }
            }
        }
    }
}
