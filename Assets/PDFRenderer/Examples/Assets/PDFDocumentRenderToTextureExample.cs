/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using UnityEngine;
using Paroxe.PdfRenderer;

public class PDFDocumentRenderToTextureExample : MonoBehaviour
{
    public int m_Page = 0;

#if !UNITY_WEBGL

    void Start()
    {
        PDFDocument pdfDocument = new PDFDocument(PDFBytesSupplierExample.PDF_SAMPLE, "");

        if (pdfDocument.IsValid)
        {
            int pageCount = pdfDocument.GetPageCount();

            PDFRenderer renderer = new PDFRenderer();
            Texture2D tex = renderer.RenderPageToTexture(pdfDocument.GetPage(m_Page % pageCount), 1024, 1024);

            tex.filterMode = FilterMode.Bilinear;
            tex.anisoLevel = 8;

            GetComponent<MeshRenderer>().material.mainTexture = tex;
        }
    }
#endif
}
