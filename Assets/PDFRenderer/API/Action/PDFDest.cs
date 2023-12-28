/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using System;
using System.Runtime.InteropServices;

namespace Paroxe.PdfRenderer
{
#if !UNITY_WEBGL
    /// <summary>
    /// Represents a destination into a PDF document.
    /// </summary>
    public class PDFDest : IDisposable
    {
        private bool m_Disposed;
        private IntPtr m_NativePointer;
        private IDisposable m_Source;
        private PDFDocument m_Document;
        private int m_PageIndex = -1;

        public PDFDest(PDFAction action, IntPtr nativePointer)
        {
            if (action == null)
                throw new NullReferenceException();
            if (nativePointer == IntPtr.Zero)
                throw new NullReferenceException();

            PDFLibrary.AddRef("PDFDest");

            m_Source = action;
            m_Document = action.Document;

            m_NativePointer = nativePointer;
        }

        public PDFDest(PDFLink link, IntPtr nativePointer)
        {
            if (link == null)
                throw new NullReferenceException();
            if (nativePointer == IntPtr.Zero)
                throw new NullReferenceException();

            PDFLibrary.AddRef("PDFDest");

            m_Source = link;
            m_Document = link.Page.Document;

            m_NativePointer = nativePointer;
        }

        public PDFDest(PDFBookmark bookmark, IntPtr nativePointer)
        {
            if (bookmark == null)
                throw new NullReferenceException();
            if (nativePointer == IntPtr.Zero)
                throw new NullReferenceException();

            PDFLibrary.AddRef("PDFDest");

            m_Source = bookmark;
            m_Document = bookmark.Document;

            m_NativePointer = nativePointer;
        }

        ~PDFDest()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!m_Disposed)
            {
                m_NativePointer = IntPtr.Zero;

                PDFLibrary.RemoveRef("PDFDest");

                m_Disposed = true;
            }
        }

        public PDFDocument Document
        {
            get { return m_Document; }
        }

        public IDisposable Source
        {
            get { return m_Source; }
        }

        public IntPtr NativePointer
        {
            get { return m_NativePointer; }
        }

        public int PageIndex
        {
            get
            {
                if (m_PageIndex < 0)
                    m_PageIndex = (int)FPDFDest_GetPageIndex(m_Document.NativePointer, m_NativePointer);
                return m_PageIndex;
            }
        }

#region NATIVE

        [DllImport(PDFLibrary.PLUGIN_ASSEMBLY)]
        private static extern uint FPDFDest_GetPageIndex(IntPtr document, IntPtr dest);

#endregion
    }
#endif
}
