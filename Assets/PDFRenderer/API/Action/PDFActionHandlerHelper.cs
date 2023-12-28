/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using System.IO;

namespace Paroxe.PdfRenderer
{
#if !UNITY_WEBGL
    /// <summary>
    /// Provides default action handling implementation.
    /// </summary>
    public static class PDFActionHandlerHelper
    {
        public static void ExecuteAction(IPDFDeviceActionHandler actionHandler, IPDFDevice device, PDFAction action)
        {
            if (action != null)
            {
                PDFAction.ActionType type = action.GetActionType();

                switch (type)
                {
                    case PDFAction.ActionType.Unsupported:
                        break;
                    case PDFAction.ActionType.GoTo:
                        PDFDest dest = action.GetDest();
                        actionHandler.HandleGotoAction(device, dest.PageIndex);
                        break;
                    case PDFAction.ActionType.RemoteGoTo:
                        string resolvedFilePath = actionHandler.HandleRemoteGotoActionPathResolving(device,
                            action.GetFilePath());

#if !((UNITY_4_6 || UNITY_4_7) && UNITY_WINRT)
                        if (File.Exists(resolvedFilePath))
                        {
                            string password = actionHandler.HandleRemoteGotoActionPasswordResolving(device,
                                resolvedFilePath);
                            PDFDocument newDocument = new PDFDocument(resolvedFilePath, password);

                            if (newDocument.IsValid)
                                actionHandler.HandleRemoteGotoActionResolved(device, newDocument,
                                    action.GetDest().PageIndex);
                            else
                                actionHandler.HandleRemoteGotoActionUnresolved(device, resolvedFilePath);
                        }
                        else
#endif
                        actionHandler.HandleRemoteGotoActionUnresolved(device, resolvedFilePath);


                        break;
                    case PDFAction.ActionType.Uri:
                        actionHandler.HandleUriAction(device, action.GetURIPath());
                        break;
                    case PDFAction.ActionType.Launch:
                        actionHandler.HandleLaunchAction(device, action.GetFilePath());
                        break;
                }
            }
        }

        public static void ExecuteBookmarkAction(IPDFDevice device, PDFBookmark bookmark)
        {
            if (device.BookmarksActionHandler != null)
            {
                PDFDest dest = bookmark.GetDest();

                if (dest != null)
                {
                    device.BookmarksActionHandler.HandleGotoAction(device, dest.PageIndex);
                }
                else
                {
                    PDFAction action = bookmark.GetAction();

                    if (action != null)
                        ExecuteAction(device.BookmarksActionHandler, device, action);
                }
            }
        }

        public static void ExecuteLinkAction(IPDFDevice device, PDFLink link)
        {
            if (device.LinksActionHandler != null)
            {
                PDFDest dest = link.GetDest();

                if (dest != null)
                {
                    device.LinksActionHandler.HandleGotoAction(device, dest.PageIndex);
                }
                else
                {
                    PDFAction action = link.GetAction();

                    if (action != null)
                        ExecuteAction(device.LinksActionHandler, device, action);
                }
            }
        }
    }
#endif
}
