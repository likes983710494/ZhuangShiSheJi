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
    /// Represents the annotation link in a PDF page.
    /// </summary>
    public class PDFLink : IDisposable
    {
        private bool m_Disposed;
        private IntPtr m_NativePointer;
        private PDFPage m_Page;

        public PDFLink(PDFPage page, IntPtr nativePointer)
        {
            if (page == null)
                throw new NullReferenceException();
            if (nativePointer == IntPtr.Zero)
                throw new NullReferenceException();

            PDFLibrary.AddRef("PDFLink");

            m_Page = page;

            m_NativePointer = nativePointer;
        }

        ~PDFLink()
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

                PDFLibrary.RemoveRef("PDFLink");

                m_Disposed = true;
            }
        }

        public PDFPage Page
        {
            get { return m_Page; }
        }

        public IntPtr NativePointer
        {
            get { return m_NativePointer; }
        }

        /// <summary>
        /// Gets the named destination assigned to a link. Return null if there is no destination associated with the link,
        /// in this case the application should try GetAction() instead.
        /// </summary>
        /// <returns></returns>
        public PDFDest GetDest()
        {
            IntPtr destPtr = FPDFLink_GetDest(m_Page.Document.NativePointer, m_NativePointer);
            if (destPtr != IntPtr.Zero)
                return new PDFDest(this, destPtr);
            return null;
        }

        /// <summary>
        /// Gets the PDF action assigned to a link.
        /// </summary>
        /// <returns></returns>
        public PDFAction GetAction()
        {
            IntPtr actionPtr = FPDFLink_GetAction(m_NativePointer);
            if (actionPtr != IntPtr.Zero)
                return new PDFAction(this, actionPtr);
            return null;
        }

#region NATIVE

        [DllImport(PDFLibrary.PLUGIN_ASSEMBLY)]
        private static extern IntPtr FPDFLink_GetAction(IntPtr link);

        [DllImport(PDFLibrary.PLUGIN_ASSEMBLY)]
        private static extern IntPtr FPDFLink_GetDest(IntPtr document, IntPtr link);

#endregion
    }
#endif
}
