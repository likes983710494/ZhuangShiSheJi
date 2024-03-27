using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using Font = iTextSharp.text.Font;
using Image = iTextSharp.text.Image;
public class PDFReport : IDisposable
{
    BaseFont heiBaseFont;//基础字体
    public Font titleFont;//报告字体样式
    public Font firstTitleFont;//大标题字体样式
    public Font secondTitleFont;//小标题字体样式
    public Font contentFont;//内容字体样式
    public Document document;//文档
    string newFontPath;

    public static IEnumerator 拷贝资源到读写路径(string Oldpath, string newPath)
    {
        if (File.Exists(newPath))
        {
            yield break;
        }
        Uri uri = new Uri(Oldpath);
        using (UnityWebRequest request = UnityWebRequest.Get(uri))
        {
            yield return request.SendWebRequest();
            if (string.IsNullOrEmpty(request.error))
            {
                yield return File.WriteAllBytesAsync(newPath, request.downloadHandler.data);
            }
            else
            {
                Debug.LogError(request.error);
            }
        }
    }

    public IEnumerator 初始化(string filePath)
    {
        document = new Document(PageSize.A4);
        string dirPath = Path.GetDirectoryName(filePath);
        Directory.CreateDirectory(dirPath);
        //为该Document创建一个Writer实例：
        FileStream os = new FileStream(filePath, FileMode.Create);
        PdfWriter.GetInstance(document, os);
        //打开文档
        document.Open();
        string oldPath = Application.streamingAssetsPath + "/SourceHanSansSC-Medium.otf";
        newFontPath = Application.persistentDataPath + "/SourceHanSansSC-Medium.otf";
        yield return 拷贝资源到读写路径(oldPath, newFontPath);
        //创建字体
        heiBaseFont = BaseFont.CreateFont(newFontPath, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
        titleFont = new Font(heiBaseFont, 26, 1);
        firstTitleFont = new Font(heiBaseFont, 20, 1);
        secondTitleFont = new Font(heiBaseFont, 13, 1);
        contentFont = new Font(heiBaseFont, 8, Font.NORMAL);
    }


    /*添加表格+表格图片*/
    public void 添加PDF表格图片(DataTable dt)
    {
        List<float> columns = new List<float>();
        for (int i = 0; i < dt.Columns.Count; i++)
        {
            columns.Add(1);
        }
        添加PDF表格图片(dt, columns.ToArray());
    }
    public void 添加PDF表格图片(DataTable dt, float[] columnW)
    {
        List<string> list = new List<string>();
        for (int i = 0; i < dt.Columns.Count; i++)
        {
            string s = dt.Columns[i].ColumnName;
            list.Add(s);
        }
        //数据
        foreach (DataRow row in dt.Rows)
        {
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                string s = row[i].ToString();
                list.Add(s);
            }
        }
        AddTableImage(columnW, list.ToArray());
    }

    public void AddTableImage(float[] columns, string[] content)
    {
        PdfPTable table = new PdfPTable(columns);
        table.WidthPercentage = 100; //宽度
        //要和列数一致
        table.SetTotalWidth(new float[] { 8, 8, 8, 16, 8, 8, 8, 8, 8, 0, 0 });
        for (int i = 0; i < content.Length; i++)
        {

            if (content[i].Contains("StreamingAssets"))
            {
                PdfPCell cell = new PdfPCell();
                //图片
                iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(content[i]);
                // 设置图片的大小
                img.ScaleToFit(90f, 90f);
                cell = new PdfPCell(img);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.FixedHeight = 100f;
                table.AddCell(cell);
            }
            else
            {
                //文字

                PdfPCell cell = new PdfPCell(new Phrase(content[i], contentFont));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                table.AddCell(cell);
            }



        }
        document.Add(table);
    }

    /***************************/
    public void 添加PDF表格(DataTable dt)
    {
        List<float> columns = new List<float>();
        for (int i = 0; i < dt.Columns.Count; i++)
        {
            columns.Add(1);
        }
        添加PDF表格(dt, columns.ToArray());
    }
    public void 添加PDF表格(DataTable dt, float[] columnW)
    {

        List<string> list = new List<string>();
        for (int i = 0; i < dt.Columns.Count; i++)
        {
            string s = dt.Columns[i].ColumnName;
            list.Add(s);
        }
        //数据
        foreach (DataRow row in dt.Rows)
        {
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                string s = row[i].ToString();
                list.Add(s);
            }
        }
        AddTable(columnW, list.ToArray());
    }
    /// <summary>
    /// 增加表格
    /// </summary>
    /// <param name="column">列数宽度比例</param>
    /// <param name="content">内容</param>
    public void AddTable(float[] columns, string[] content)
    {
        PdfPTable table = new PdfPTable(columns);
        table.WidthPercentage = 100; //宽度
        //table.SetTotalWidth(new float[] {10,10,10,10,10,10,10,20 });
        for (int i = 0; i < content.Length; i++)
        {
            PdfPCell cell = new PdfPCell(new Phrase(content[i], contentFont));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;

            table.AddCell(cell);
        }
        document.Add(table);
    }


    /// <summary>
    /// 空格
    /// 加入空行，用以区分上下行
    /// </summary>
    public void AddNullLine()
    {
        Paragraph nullLine = new Paragraph(" ", secondTitleFont);
        nullLine.Leading = 5;
        document.Add(nullLine);
    }

    /// <summary>
    /// 加入标题
    /// </summary>
    /// <param name="titleStr">标题内容</param>
    /// <param name="font">标题字体，分为一级标题和二级标题</param>
    /// <param name="alignmentType">对齐格式,0为左对齐,1为居中</param>
    public void AddTitle(string titleStr, int alignmentType = 1)
    {
        Paragraph contentP = new Paragraph(new Chunk(titleStr, titleFont));
        contentP.Alignment = alignmentType;
        document.Add(contentP);
    }

    /// <summary>
    /// 大标题
    /// </summary>
    /// <param name="titleStr"></param>
    /// <param name="alignmentType"></param>
    public void AddFirstTitle(string titleStr, int alignmentType = 0)
    {
        Paragraph contentP = new Paragraph(new Chunk(titleStr, firstTitleFont));
        contentP.Alignment = alignmentType;
        document.Add(contentP);
    }

    /// <summary>
    /// 小标题
    /// </summary>
    /// <param name="titleStr"></param>
    /// <param name="alignmentType"></param>
    public void AddSecondTitle(string titleStr, int alignmentType = 0)
    {
        Paragraph contentP = new Paragraph(new Chunk(titleStr, secondTitleFont));
        contentP.Alignment = alignmentType;
        document.Add(contentP);
    }
    /// <summary>
    /// 插入文字内容
    /// </summary>
    /// <param name="content">内容</param>
    /// <param name="alignmentType">对齐格式,0为左对齐,1为居中</param>
    public void AddContent(string content, int alignmentType = 0)
    {
        Paragraph contentP = new Paragraph(new Chunk(content, contentFont));
        contentP.Alignment = alignmentType;
        document.Add(contentP);
    }

    /// <summary>
    /// 插入图片
    /// </summary>
    /// <param name="imagePath"></param>
    /// <param name="scale"></param>
    public void AddImage(string imagePath, int width = 475, int height = 325)//int width = 475, int height = 325
    {
        if (!File.Exists(imagePath))
        {
            Debug.Log("该路径下不存在指定图片，请检测路径是否正确！");
            return;
        }
        Image image = Image.GetInstance(imagePath);
        image.ScaleToFit(width, height);
        image.Alignment = Element.ALIGN_JUSTIFIED;
        document.Add(image);
    }

    /// <summary>
    /// 关闭文档
    /// </summary>
    public void Dispose()
    {
        document.Close();
    }
}


