




using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using iTextSharp.text.pdf;

public class SetRePort : MonoBehaviour
{
    public static SetRePort Instance_;
    public static PDFReport pdf;
    private void Awake()
    {
        Instance_ = this;
       
    }

    void Start()
    {

       
    }
    string fileText1path = @"Assets\StreamingAssets\图片.png";
    public IEnumerator 创建Pdf(string pdfName)//test.pdf
    {
      
  
        string path = Application.persistentDataPath + "/"+pdfName+".pdf";
        Debug.Log("地址："+path);

        using (pdf = new PDFReport())
        {
            yield return pdf.初始化(path);
            //添加内容
            AddTitle(pdfName);
            AddContent(pdfName);
            AddImage(@"Assets\StreamingAssets\图片.png");
            AddForm(new string[] { "编号", "名称", "产品", "系列2222222222222222222222222222222", "建筑面积", "用漆量", "数量", "详情" });
        }
       
    }

    /// <summary>
    /// 创建pdf
    /// </summary>
    /// <param name="pdfName"> 文件名</param>
    /// <returns></returns>
    public void CreatPDF(string pdfName)
    {
        StartCoroutine(创建Pdf(pdfName));
    }
    /// <summary>
    /// 打开文档
    /// </summary>
    /// <param name="pdfName"></param>
    public void OpenPDF(string pdfName)
    {
        string path = Application.persistentDataPath + "/" + pdfName + ".pdf";
        Debug.Log("创建成功打开文件:" + path);
        Application.OpenURL(path);
    }
    /// <summary>
    /// 添加文字标题
    /// </summary>
    /// <param name="content"></param>
    public void AddTitle(string content)
    {
        if (content != null)
            pdf.AddTitle(content);
    }
    /// <summary>
    /// 添加文字内容
    /// </summary>
    /// <param name="content"></param>
    public void AddContent(string content)
    {
        if (content != null)
            pdf.AddContent(content);
    }

    /// <summary>
    /// 添加图片
    /// </summary>
    /// <param name="imgPath"> 
    ///  string imgPath = @"Assets\StreamingAssets\图片.png";
    /// param>
    public  void AddImage(string imgPath, int width = 475, int height = 325)
    {
        if (imgPath != null)
            pdf.AddImage(imgPath, width, height);
    }
    /// <summary>
    /// 添加表格 
    /// </summary>
    /// <param name="Columns"> 表格头
    ///  Columns = new string[] { "编号", "名称", "产品", "系列2222222222222222222222222222222", "建筑面积", "用漆量", "数量", "详情" };
    /// </param> 
    public void AddForm(string[] Columns)
    {
         DataTable dt = new DataTable();
        if (Columns != null)
        {
            foreach (string item in Columns)
            {
                dt.Columns.Add(item);
            }
        }
    
       //表格一行内容 鱼表格头对应
            DataRow dr = dt.NewRow();
       object[] objs = { 9991111, "这是名称", "这是产品", "系列门窗",
                "建筑面积6666", "用漆量111", "数量33.3333", "详情啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊啊" };
        dr.ItemArray = objs;
            dt.Rows.Add(dr);
            pdf.添加PDF表格(dt);
    }



}

