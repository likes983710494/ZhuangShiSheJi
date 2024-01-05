
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using iTextSharp.text.pdf;
using Unit;
//生成pdf
public class SetRePort : MonoBehaviour
{

    private string tzgsfilepath = @"Assets\StreamingAssets\ScreenShot\tzgs\投资估算.png";//投资估算地址



    void Start()
    {

    }
    public IEnumerator 创建Pdf()
    {







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
            pdf.AddTitle("装饰设计阶段造价实验报告");
            pdf.AddFirstTitle("一、投资估算");
            pdf.AddNullLine();
            pdf.AddImage(tzgsfilepath);
            pdf.AddContent("投资估算模块满分100分，共扣" + Unit.UnitDollarData.DeductionNumber + "分" + "，得分" + Unit.UnitDollarData.EstimateNumber + "分。");

            pdf.AddSecondTitle("二、限额分解");
            pdf.AddNullLine();
            for (int i = 0; i < Unit.UnitDollarData.CombineList.Count; i++)
            {
                string[] Columns = new string[] { "序号", "分项工程", "所属分部工程", "分项金额" };
                DataTable 限额分解dt = new DataTable();
                foreach (string item in Columns)
                {
                    限额分解dt.Columns.Add(item);
                }
                for (int j = 0; j < Unit.UnitDollarData.CombineList[i].antidiastoleList.Count; j++) //将数据储存到list
                {
                    DataRow dr = 限额分解dt.NewRow();
                    object[] objs = Unit.UnitDollarData.CombineList[i].antidiastoleList.ToArray();
                    dr.ItemArray = objs;
                    限额分解dt.Rows.Add(dr);
                }
                DataRow dr_hj = 限额分解dt.NewRow();
                object[] objs1 = { "分项累计金额", "10" };
                dr_hj.ItemArray = objs1;
                限额分解dt.Rows.Add(dr_hj);

                pdf.添加PDF表格(限额分解dt);
                pdf.AddNullLine();

            }


            pdf.AddSecondTitle("三、装饰设计");
            pdf.AddNullLine();
            pdf.添加PDF表格(装饰设计dt);
            pdf.AddSecondTitle("四、装饰效果展示");
            pdf.AddNullLine();
            pdf.AddImage(tzgsfilepath);
            pdf.AddImage(tzgsfilepath);
            pdf.AddImage(tzgsfilepath);
            pdf.AddImage(tzgsfilepath);




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
