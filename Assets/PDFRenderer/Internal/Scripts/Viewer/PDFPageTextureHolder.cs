/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using Paroxe.PdfRenderer.WebGL;
using UnityEngine;
using UnityEngine.UI;

namespace Paroxe.PdfRenderer.Internal.Viewer
{
    public class PDFPageTextureHolder
    {
        public AspectRatioFitter m_AspectRatioFitter;
        public int m_PageIndex;
        public GameObject m_Page;
#if UNITY_WEBGL
        public bool m_RenderingStarted;
        public bool m_Visible;
        public IPDFJS_Promise m_RenderingPromise;
#endif

        private Texture2D m_Texture;
        public PDFViewer m_PDFViewer;

        public void RefreshTexture()
        {
            Texture = m_Texture;
        }

        public Texture2D Texture
        {
            get
            {
                return m_Texture;
            }
            set
            {
                m_Texture = value;

                RawImage rawImage = m_Page.GetComponent<RawImage>();
                if (rawImage == null)
                    rawImage = m_Page.gameObject.AddComponent<RawImage>();

                if (value != null)
                {
                    rawImage.texture = value;
                    rawImage.uvRect = new Rect(0.0f, 0.0f, 1.0f, 1.0f);
                    rawImage.color = Color.white;
                }
                else
                {
                    if (m_PDFViewer.PageTileTexture != null)
                    {
                        rawImage.texture = m_PDFViewer.PageTileTexture;

                        RectTransform rt = rawImage.transform as RectTransform;

                        rawImage.uvRect = new Rect(0.0f, 0.0f,
                            rt.sizeDelta.x / rawImage.texture.width,
                            rt.sizeDelta.y / rawImage.texture.height);
                    }
                    else
                    {
                        rawImage.texture = null;
                        rawImage.color = m_PDFViewer.PageColor;
                    }
                }

                if (m_AspectRatioFitter != null)
                    m_AspectRatioFitter.aspectRatio = value.width / (float)value.height;
            }
        }
    }
}
