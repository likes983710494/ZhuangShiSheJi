/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/


namespace Paroxe.PdfRenderer
{
    /// <summary>
    /// Reprensent a search result. To result location is describe with index of the first char and the length of the result.
    /// </summary>
    public struct PDFSearchResult
    {
        private readonly int m_PageIndex;
        private readonly int m_StartIndex; // index of the first character
        private readonly int m_Count; // number of characters

        public PDFSearchResult(int pageIndex, int startIndex, int count)
        {
            m_PageIndex = pageIndex;
            m_StartIndex = startIndex;
            m_Count = count;
        }

        /// <summary>
        /// Indicate whether the result is valid or invalid.
        /// </summary>
        public bool IsValid
        {
            get { return m_PageIndex != -1; }
        }

        /// <summary>
        /// The pageIndex of the result.
        /// </summary>
        public int PageIndex
        {
            get { return m_PageIndex; }
        }

        /// <summary>
        /// The index of the first character of the result within the page.
        /// </summary>
        public int StartIndex
        {
            get { return m_StartIndex; }
        }

        /// <summary>
        /// The length of the result within the page.
        /// </summary>
        public int Count
        {
            get { return m_Count; }
        }
    }
}
