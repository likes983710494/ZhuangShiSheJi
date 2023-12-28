/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using UnityEngine;
using System.Collections;
using Paroxe.PdfRenderer.WebGL;
using UnityEngine.UI;
using Paroxe.PdfRenderer;

public class WebGL_API_Usage : MonoBehaviour
{
    public RawImage m_RawImage;

    IEnumerator Start()
    {
        Debug.Log("WebGLTest: ");

        PDFJS_Promise<PDFDocument> documentPromise = PDFDocument.LoadDocumentFromUrlAsync("https://crossorigin.me/http://www.pdf995.com/samples/pdf.pdf");

        while (!documentPromise.HasFinished)
            yield return null;

        if (!documentPromise.HasSucceeded)
        {
            Debug.Log("Fail: documentPromise");
            yield break;
        }
        else
            Debug.Log("Success: documentPromise");

        PDFDocument document = documentPromise.Result;

        PDFJS_Promise<PDFPage> pagePromise = document.GetPageAsync(0);

        while (!pagePromise.HasFinished)
            yield return null;

        if (!pagePromise.HasSucceeded)
        {
            Debug.Log("Fail: pagePromise");
            yield break;
        }
        else
            Debug.Log("Success: pagePromise");

        PDFPage page = pagePromise.Result;

        PDFJS_Promise<Texture2D> renderPromise = PDFRenderer.RenderPageToTextureAsync(page, (int)page.GetPageSize().x, (int)page.GetPageSize().y);

        while (!renderPromise.HasFinished)
            yield return null;

        if (!renderPromise.HasSucceeded)
        {
            Debug.Log("Fail: pagePromise");
            yield break;
        }

        Texture2D renderedPageTexture = renderPromise.Result;

        (m_RawImage.transform as RectTransform).sizeDelta = new Vector2(renderedPageTexture.width, renderedPageTexture.height);
        m_RawImage.texture = renderedPageTexture;

        yield return new WaitForSeconds(2.5f);
    }
}
