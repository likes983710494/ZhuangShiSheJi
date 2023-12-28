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
    public class PDFViewerLeftPanelScrollbar : UIBehaviour
    {
        private CanvasGroup m_CanvasGroup;
        private Scrollbar m_Scrollbar;

        void LateUpdate()
        {
            if (m_Scrollbar.size >= 0.98f && m_CanvasGroup.alpha != 0.0f)
            {
                m_CanvasGroup.alpha = 0.0f;
            }
            else if (m_Scrollbar.size < 0.98f && m_CanvasGroup.alpha != 1.0f)
            {
                m_CanvasGroup.alpha = 1.0f;
            }
        }

        protected override void OnEnable()
        {
            m_Scrollbar = GetComponent<Scrollbar>();
            m_CanvasGroup = GetComponent<CanvasGroup>();
        }
    }
}
