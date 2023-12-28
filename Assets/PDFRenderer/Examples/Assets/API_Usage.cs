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
using System.Collections.Generic;
using Paroxe.PdfRenderer;
using System.IO;
using System;
using System.Threading;

public class API_Usage : MonoBehaviour
{
    private static string _imageFolderPath = "";
#if !UNITY_WEBGL
    IEnumerator Start()
    {
        _imageFolderPath = Application.persistentDataPath + "/PDFFile/Images/";
        if (!Directory.Exists(_imageFolderPath))
        {
            Directory.CreateDirectory(_imageFolderPath);
        }
        //WWW www = new WWW("https://www.dropbox.com/s/tssavtnvaym2t6b/DocumentationEN.pdf?raw=1");
        WWW www = new WWW("http://paroxe.com/media/1076/documentationen.pdf");

        Debug.Log("Downloading document...");

        yield return www;

        if (www == null || www.error != null || !www.isDone)
            yield break;

        PDFDocument document = new PDFDocument(www.bytes, "");

        Debug.Log("Page count: " + document.GetPageCount());

        TextPageAPI(document);
        SearchAPI(document);
        BookmarkAPI(document);
    }

    void TextPageAPI(PDFDocument document)
    {
        Debug.Log("TEXTPAGE API-------------------------");

        PDFPage page = document.GetPage(1);
        Debug.Log("Page size: " + page.GetPageSize());

        PDFTextPage textPage = page.GetTextPage();
        int countChars = textPage.CountChars();
        Debug.Log("Page chars count: " + countChars);
        
        string str = textPage.GetText(0, countChars);
        Debug.Log("Page text: " + str);

        int rectCount = textPage.CountRects(0, countChars);
        Debug.Log("Rect count: " + rectCount);

        string boundedText = textPage.GetBoundedText(0, 0, page.GetPageSize().x, page.GetPageSize().y * 0.5f, 2000);
        Debug.Log("Bounded text: " + boundedText);
    }

    void SearchAPI(PDFDocument document)
    {
        Debug.Log("SEARCH API-------------------------");

        IList<PDFSearchResult> results = document.GetPage(1).GetTextPage().Search("pdf");

        Debug.Log("Search results count: " + results.Count);
        Debug.Log("First result char index: " + results[0].StartIndex + " and chars count: " + results[0].Count);

        // Get all rects corresponding to the first search result
        int rectsCount = document.GetPage(1).GetTextPage().CountRects(results[0].StartIndex, results[0].Count);
        Debug.Log("Search result rects count: " + rectsCount);
    }

    void BookmarkAPI(PDFDocument document)
    {
        Debug.Log("BOOKMARK API-------------------------");

        PDFBookmark rootBookmark = document.GetRootBookmark();
        OutputBookmarks(rootBookmark, 0);
    }

    void OutputBookmarks(PDFBookmark bookmark, int indent)
    {
        string line = "";
        for (int i = 0; i < indent; ++i)
            line += "    ";
        line += bookmark.GetTitle();
        Debug.Log(line);

        foreach (PDFBookmark child in bookmark.EnumerateChildrenBookmarks())
        {
            OutputBookmarks(child, indent + 1);
        }
    }
#endif
    
}
