/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Paroxe.PdfRenderer.Internal.Viewer
{
    public class PDFThumbnailItem : UIBehaviour
    {
        public AspectRatioFitter m_AspectRatioFitter;
        public Image m_Highlighted;
        public LayoutElement m_LayoutElement;
        public Text m_PageIndexLabel;
        public RawImage m_PageThumbnailRawImage;
        public RectTransform m_RectTransform;

        public void LateUpdate()
        {
            m_LayoutElement.preferredHeight = 180.0f*(m_RectTransform.sizeDelta.x/320.0f);
        }

        public void OnThumbnailClicked()
        {
            GetComponentInParent<PDFViewer>().GoToPage(int.Parse(m_PageIndexLabel.text) - 1);
        }
    }
}
