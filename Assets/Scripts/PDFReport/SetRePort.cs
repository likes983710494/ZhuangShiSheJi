
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
    string fileText1path = @"Assets\StreamingAssets\iamge\你好测试截图0.7817451.png";



    void Start()
    {

    }

    public IEnumerator 创建Pdf()
    {

        string[] Columns = new string[] { "序号", "分项工程", "所属分部工程", "分项金额" };
        DataTable 限额分解dt = new DataTable();
        foreach (string item in Columns)
        {
            限额分解dt.Columns.Add(item);
        }
        for (int i = 0; i < 6; i++) //将数据储存到list
        {
            DataRow dr = 限额分解dt.NewRow();
            object[] objs = { i + 1, "贴镶面", "楼地面装饰工程", "10" };
            dr.ItemArray = objs;
            限额分解dt.Rows.Add(dr);
        }
        DataRow dr1 = 限额分解dt.NewRow();
        object[] objs1 = { "分项累计金额", "10" };
        dr1.ItemArray = objs1;
        限额分解dt.Rows.Add(dr1);



        string[] 装饰设计Columns = new string[] { "分项名称/属性信息", "所属分部", "单价", "工程量", "合价" };
        DataTable 装饰设计dt = new DataTable();
        foreach (string item in 装饰设计Columns)
        {
            装饰设计dt.Columns.Add(item);
        }
        for (int i = 0; i < 10; i++) //将数据储存到list
        {
            DataRow xedr = 装饰设计dt.NewRow();
            object[] xeobjs = { "贴镶面", "楼地面装饰工程", "10", "贴镶面", "楼地面装饰工程" };
            xedr.ItemArray = xeobjs;
            装饰设计dt.Rows.Add(xedr);
        }


        string path = Application.persistentDataPath + "/装饰设计实验报告.pdf";
        using (PDFReport pdf = new PDFReport())
        {
            yield return pdf.初始化(path);
            pdf.AddTitle("装饰设计实验报告");
            pdf.AddFirstTitle("一、投资估算");
            pdf.AddNullLine();
            pdf.AddImage(fileText1path);
            pdf.AddContent("投资估算模块满分100分，共扣9分，得分91分。");
            pdf.AddSecondTitle("二、限额分解");
            pdf.AddNullLine();
            pdf.添加PDF表格(限额分解dt);
            pdf.AddSecondTitle("三、装饰设计");
            pdf.AddNullLine();
            pdf.添加PDF表格(装饰设计dt);
            pdf.AddSecondTitle("四、装饰效果展示");
            pdf.AddNullLine();
            pdf.AddImage(fileText1path);
            pdf.AddImage(fileText1path);
            pdf.AddImage(fileText1path);
            pdf.AddImage(fileText1path);




        }
        Debug.Log("创建成功打开文件:" + path);
        Application.OpenURL(path);
    }


    public void OpenPDF(string pdfName)
    {
        string path = Application.persistentDataPath + "/" + pdfName + ".pdf";
        Debug.Log("创建成功打开文件:" + path);
        Application.OpenURL(path);
    }
}
