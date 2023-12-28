/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using System.Collections.Generic;

namespace Paroxe.PdfRenderer
{
    /// <summary>
    /// This interface allow to implement a custome colorec rects provider. For example, PDFViewer inherits
    /// this class and implements it to provide colorect rects during a search session to the renderer.
    /// </summary>
    public interface IPDFColoredRectListProvider
    {
        IList<PDFColoredRect> GetBackgroundColoredRectList(PDFPage page);
    }
}
