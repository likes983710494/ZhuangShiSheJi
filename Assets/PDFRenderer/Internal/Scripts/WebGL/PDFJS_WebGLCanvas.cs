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

namespace Paroxe.PdfRenderer.WebGL
{
    public class PDFJS_WebGLCanvas : IDisposable
    {
        private bool m_Disposed;
        private IntPtr m_NativePointer;

        public PDFJS_WebGLCanvas(IntPtr nativePointer)
        {
            m_NativePointer = nativePointer;
        }

        ~PDFJS_WebGLCanvas()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!m_Disposed)
            {
                if (m_NativePointer != IntPtr.Zero)
                {
#if UNITY_WEBGL && !UNITY_EDITOR
                    PDFJS_DestroyCanvas(m_NativePointer.ToInt32());
#endif
                    m_NativePointer = IntPtr.Zero;
                }

                m_Disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

#if UNITY_WEBGL && !UNITY_EDITOR
        [DllImport(PDFLibrary.PLUGIN_ASSEMBLY)]
        private static extern void PDFJS_DestroyCanvas(int canvasHandle);
#endif

    }
}
