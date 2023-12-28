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
    public class PDFViewerInternal : UIBehaviour
    {
        [SerializeField] public RectTransform m_DownloadDialog;
        [SerializeField] public Text m_DownloadSourceLabel;
        [SerializeField] public bool m_DrawDefaultInspector = false;
        [SerializeField] public RectTransform m_HorizontalScrollBar;
        [SerializeField] public Image m_InvalidPasswordImage;
        [SerializeField] public PDFViewerLeftPanel m_LeftPanel = null;
        [SerializeField] public CanvasGroup m_Overlay;
        [SerializeField] public RectTransform m_PageContainer;
        [SerializeField] public Text m_PageCountLabel;
        [SerializeField] public Button m_PageDownButton;
        [SerializeField] public InputField m_PageInputField;
        [SerializeField] public RawImage m_PageSample;
        [SerializeField] public Button m_PageUpButton;
        [SerializeField] public Text m_PageZoomLabel;
        [SerializeField] public RectTransform m_PasswordDialog;
        [SerializeField] public InputField m_PasswordInputField;
        [SerializeField] public Text m_ProgressLabel;
        [SerializeField] public RectTransform m_ProgressRect;
        [SerializeField] public RectTransform m_ScrollCorner;
        [SerializeField] public ScrollRect m_ScrollRect;
        [SerializeField] public RectTransform m_TopPanel;
        [SerializeField] public string m_Version = "2.0";
        [SerializeField] public RectTransform m_VerticalScrollBar;
        [SerializeField] public RectTransform m_Viewport;
        [SerializeField] public RectTransform m_SearchPanel;
#if UNITY_EDITOR
        [SerializeField] public bool m_BannerIsOpened = true;
        [SerializeField] public bool m_UiShowLoadOptions = true;
        [SerializeField] public bool m_UiShowPasswordOptions = true;
        [SerializeField] public bool m_UiShowViewerSettings = true;
        [SerializeField] public bool m_UiShowSearchSettings = true;
        [SerializeField] public bool m_UiShowOtherSettings = true;
        [SerializeField] public bool m_UiShowRenderSettings = true;
        [SerializeField] public bool m_UiShowDebugSettings = true;
#endif
        public PDFViewer m_PDFViewer = null;

        public void OnDownloadCancelButtonClicked()
        {
            if (m_PDFViewer != null)
            {
                m_PDFViewer.OnDownloadCancelButtonClicked();
            }
        }

        public void OnNextPageButtonClicked()
        {
            if (m_PDFViewer != null)
            {
                m_PDFViewer.GoToNextPage();
            }
        }

        public void OnPageIndexEditEnd()
        {
            if (m_PDFViewer != null)
            {
                m_PDFViewer.OnPageEditEnd();
            }
        }

        public void OnPasswordDialogCancelButtonClicked()
        {
            if (m_PDFViewer != null)
            {
                m_PDFViewer.OnPasswordDialogCancelButtonClicked();
            }
        }

        public void OnPasswordDialogOkButtonClicked()
        {
            if (m_PDFViewer != null)
            {
                m_PDFViewer.OnPasswordDialogOkButtonClicked();
            }
        }

        public void OnPreviousPageButtonClicked()
        {
            if (m_PDFViewer != null)
            {
                m_PDFViewer.GoToPreviousPage();
            }
        }

        public void OnZoomInButtonClicked()
        {
            if (m_PDFViewer != null)
            {
                m_PDFViewer.ZoomIn();
            }
        }

        public void OnZoomOutButtonClicked()
        {
            if (m_PDFViewer != null)
            {
                m_PDFViewer.ZoomOut();
            }
        }
    }
}
