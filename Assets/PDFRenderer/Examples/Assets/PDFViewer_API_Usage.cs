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
using Paroxe.PdfRenderer;
using System.Collections.Generic;

public class PDFViewer_API_Usage : MonoBehaviour
{
    public PDFViewer m_Viewer;
    public PDFAsset m_PDFAsset;

	IEnumerator Start()
    {
#if UNITY_WEBGL
        yield break;
#else
        Debug.Log(Application.persistentDataPath);

        m_Viewer.gameObject.SetActive(true);

        m_Viewer.LoadDocumentFromWeb("http://www.pdf995.com/samples/pdf.pdf", "");

        // Wait until the pdf document is loaded.
        while (!m_Viewer.IsLoaded)
            yield return null;

        PDFDocument document = m_Viewer.Document;
        Debug.Log("Page count: " + document.GetPageCount());

        PDFPage firstPage = document.GetPage(0);
        Debug.Log("First Page Size: " + firstPage.GetPageSize());

        PDFTextPage firstTextPage = firstPage.GetTextPage();
        Debug.Log("First Page Chars Count: " + firstTextPage.CountChars());

        IList<PDFSearchResult> searchResults = firstTextPage.Search("the", PDFSearchHandle.MatchOption.NONE, 0);
        Debug.Log("Search Results Count: " + searchResults.Count);

        // Wait 2 seconds and then load another document
        yield return new WaitForSeconds(2.0f);

        m_Viewer.LoadDocumentFromAsset(m_PDFAsset);
#endif

    }
}
