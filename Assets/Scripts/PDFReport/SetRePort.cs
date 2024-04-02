
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using iTextSharp.text.pdf;
using Unit;
using System.Linq;
using System.IO;
using System.Reflection;
//生成pdf
public class SetRePort : MonoBehaviour
{

    private string tzgsfilepath = @"Assets\StreamingAssets\ScreenShot\tzgs\投资估算.png";//投资估算地址
    private List<DataTable> DataTable_List = new List<DataTable>();

    public Button Confirm_button;
    public Button Check_button;//查看实验报告pdf按钮
    void Start()
    {
        Confirm_button.onClick.AddListener(() =>
        {
            StartCoroutine(创建Pdf());
        });
        Check_button.onClick.AddListener(() =>
       {
           OpenPDF();
       });

    }
    public IEnumerator 创建Pdf()
    {
        string path = Application.persistentDataPath + "/装饰设计实验报告.pdf";
        //限额分解
        DataTable_List.Clear();
        for (int i = 0; i < Unit.UnitDollarData.CombineList.Count; i++)
        {
            DataTable 限额分解dt = new DataTable();
            string[] Columns = new string[] { "序号", "所属分部工程", "所属分项工程", "分项金额" };
            foreach (string item in Columns)
            {
                限额分解dt.Columns.Add(item);
            }

            for (int j = 0; j < Unit.UnitDollarData.CombineList[i].antidiastoleList.Count; j++) //将数据储存到list
            {
                DataRow dr = 限额分解dt.NewRow();
                object[] objs = Unit.UnitDollarData.CombineList[i].antidiastoleList[j].GetType().GetProperties().Select(p => p.GetValue(Unit.UnitDollarData.CombineList[i].antidiastoleList[j])).ToArray();

                Debug.Log("fenjie" + Unit.UnitDollarData.CombineList[i].antidiastoleList[j]);
                dr.ItemArray = objs;
                限额分解dt.Rows.Add(dr);
            }


            DataRow dr_hj = 限额分解dt.NewRow();
            object[] objs1 = { "分项累计金额", Unit.UnitDollarData.AmountArray[i] };
            dr_hj.ItemArray = objs1;
            限额分解dt.Rows.Add(dr_hj);


            DataTable_List.Add(限额分解dt);



        }



        //装饰设计
        string[] 装饰设计Columns = new string[] { "分部名称/属性信息", "所属分项", "当前部件编号", "做法说明-工程设计",
        "做法说明-工程材质", "文字说明", "工程量(平方米)","单价(元)",  "合价","id", "部件id" };
        //楼地面
        DataTable 装饰设计dt楼地面 = new DataTable();
        foreach (string item in 装饰设计Columns)
        {
            装饰设计dt楼地面.Columns.Add(item);
        }
        for (int i = 0; i < UnitDollarData.楼地面_DesignsList.Count; i++) //将数据储存到list
        {

            DataRow xedr = 装饰设计dt楼地面.NewRow();

            object[] xeobjs = UnitDollarData.楼地面_DesignsList[i].GetType().GetProperties().Select(p => p.GetValue(UnitDollarData.楼地面_DesignsList[i])).ToArray();

            //object[] xeobjs = { "贴镶面", "楼地面装饰工程", tzgsfilepath, tzgsfilepath, "楼地面装饰工程", "楼地面装饰工程", "楼地面装饰工程", "楼地面装饰工程" };
            Debug.Log("墙柱面_DesignsList长度" + UnitDollarData.楼地面_DesignsList.Count);
            Debug.Log("PDF" + UnitDollarData.楼地面_DesignsList[i].departmentName + "----路径：" + UnitDollarData.楼地面_DesignsList[i].designImagePath + "||");
            Debug.Log("长度" + xeobjs.Length);
            Debug.Log("设计" + UnitDollarData.楼地面_DesignsList[i]);
            foreach (var obj in xeobjs)
            {
                Debug.Log("示例：" + obj);
            }

            xedr.ItemArray = xeobjs;
            装饰设计dt楼地面.Rows.Add(xedr);
        }
        //墙柱面
        DataTable 装饰设计dt = new DataTable();
        foreach (string item in 装饰设计Columns)
        {
            装饰设计dt.Columns.Add(item);
        }
        for (int i = 0; i < UnitDollarData.墙柱面_DesignsList.Count; i++) //将数据储存到list
        {

            DataRow xedr = 装饰设计dt.NewRow();

            object[] xeobjs = UnitDollarData.墙柱面_DesignsList[i].GetType().GetProperties().Select(p => p.GetValue(UnitDollarData.墙柱面_DesignsList[i])).ToArray();

            //object[] xeobjs = { "贴镶面", "楼地面装饰工程", tzgsfilepath, tzgsfilepath, "楼地面装饰工程", "楼地面装饰工程", "楼地面装饰工程", "楼地面装饰工程" };
            Debug.Log("墙柱面_DesignsList长度" + UnitDollarData.墙柱面_DesignsList.Count);
            Debug.Log("PDF" + UnitDollarData.墙柱面_DesignsList[i].departmentName + "----路径：" + UnitDollarData.墙柱面_DesignsList[i].designImagePath + "||");
            Debug.Log("长度" + xeobjs.Length);
            Debug.Log("设计" + UnitDollarData.墙柱面_DesignsList[i]);
            foreach (var obj in xeobjs)
            {
                Debug.Log("示例：" + obj);
            }

            xedr.ItemArray = xeobjs;
            装饰设计dt.Rows.Add(xedr);
        }
        //天棚
        DataTable 装饰设计dt天棚 = new DataTable();
        foreach (string item in 装饰设计Columns)
        {
            装饰设计dt天棚.Columns.Add(item);
        }
        for (int i = 0; i < UnitDollarData.天棚工程_DesignsList.Count; i++) //将数据储存到list
        {

            DataRow xedr = 装饰设计dt天棚.NewRow();

            object[] xeobjs = UnitDollarData.天棚工程_DesignsList[i].GetType().GetProperties().Select(p => p.GetValue(UnitDollarData.天棚工程_DesignsList[i])).ToArray();

            //object[] xeobjs = { "贴镶面", "楼地面装饰工程", tzgsfilepath, tzgsfilepath, "楼地面装饰工程", "楼地面装饰工程", "楼地面装饰工程", "楼地面装饰工程" };
            Debug.Log("墙柱面_DesignsList长度" + UnitDollarData.天棚工程_DesignsList.Count);
            Debug.Log("PDF" + UnitDollarData.天棚工程_DesignsList[i].departmentName + "----路径：" + UnitDollarData.天棚工程_DesignsList[i].designImagePath + "||");
            Debug.Log("长度" + xeobjs.Length);
            Debug.Log("设计" + UnitDollarData.天棚工程_DesignsList[i]);
            foreach (var obj in xeobjs)
            {
                Debug.Log("示例：" + obj);
            }

            xedr.ItemArray = xeobjs;
            装饰设计dt天棚.Rows.Add(xedr);
        }
        //油漆涂料
        DataTable 装饰设计dt油漆涂料 = new DataTable();
        foreach (string item in 装饰设计Columns)
        {
            装饰设计dt油漆涂料.Columns.Add(item);
        }
        for (int i = 0; i < UnitDollarData.油漆涂料_DesignsList.Count; i++) //将数据储存到list
        {

            DataRow xedr = 装饰设计dt油漆涂料.NewRow();

            object[] xeobjs = UnitDollarData.油漆涂料_DesignsList[i].GetType().GetProperties().Select(p => p.GetValue(UnitDollarData.油漆涂料_DesignsList[i])).ToArray();

            //object[] xeobjs = { "贴镶面", "楼地面装饰工程", tzgsfilepath, tzgsfilepath, "楼地面装饰工程", "楼地面装饰工程", "楼地面装饰工程", "楼地面装饰工程" };
            Debug.Log("墙柱面_DesignsList长度" + UnitDollarData.油漆涂料_DesignsList.Count);
            Debug.Log("PDF" + UnitDollarData.油漆涂料_DesignsList[i].departmentName + "----路径：" + UnitDollarData.油漆涂料_DesignsList[i].designImagePath + "||");
            Debug.Log("长度" + xeobjs.Length);
            Debug.Log("设计" + UnitDollarData.油漆涂料_DesignsList[i]);
            foreach (var obj in xeobjs)
            {
                Debug.Log("示例：" + obj);
            }

            xedr.ItemArray = xeobjs;
            装饰设计dt油漆涂料.Rows.Add(xedr);
        }
        //其他装饰
        DataTable 装饰设计dt其他 = new DataTable();
        foreach (string item in 装饰设计Columns)
        {
            装饰设计dt其他.Columns.Add(item);
        }
        for (int i = 0; i < UnitDollarData.其他装饰_DesignsList.Count; i++) //将数据储存到list
        {

            DataRow xedr = 装饰设计dt其他.NewRow();

            object[] xeobjs = UnitDollarData.其他装饰_DesignsList[i].GetType().GetProperties().Select(p => p.GetValue(UnitDollarData.其他装饰_DesignsList[i])).ToArray();

            //object[] xeobjs = { "贴镶面", "楼地面装饰工程", tzgsfilepath, tzgsfilepath, "楼地面装饰工程", "楼地面装饰工程", "楼地面装饰工程", "楼地面装饰工程" };
            Debug.Log("墙柱面_DesignsList长度" + UnitDollarData.其他装饰_DesignsList.Count);
            Debug.Log("PDF" + UnitDollarData.其他装饰_DesignsList[i].departmentName + "----路径：" + UnitDollarData.其他装饰_DesignsList[i].designImagePath + "||");
            Debug.Log("长度" + xeobjs.Length);
            Debug.Log("设计" + UnitDollarData.其他装饰_DesignsList[i]);
            foreach (var obj in xeobjs)
            {
                Debug.Log("示例：" + obj);
            }

            xedr.ItemArray = xeobjs;
            装饰设计dt其他.Rows.Add(xedr);
        }





        Unit.UnitDollarData.ReportPath = path;
        using (PDFReport pdf = new PDFReport())
        {

            yield return pdf.初始化(path);

            if (File.Exists(path))
            {
                Debug.Log("PDF创建成功");
                pdf.AddTitle("装饰设计阶段造价实验报告");
                pdf.AddSecondTitle("姓名：xxx   班级：2024级1班", 1);
                pdf.AddSecondTitle("一、投资估算");
                pdf.AddNullLine();
                pdf.AddImage(tzgsfilepath);
                pdf.AddContent("投资估算模块共扣" + Unit.UnitDollarData.DeductionNumber + "分。");

                pdf.AddSecondTitle("二、限额分解");
                pdf.AddNullLine();
                for (int i = 0; i < DataTable_List.Count; i++)
                {
                    pdf.添加PDF表格(DataTable_List[i]);
                    pdf.AddNullLine();
                    pdf.AddNullLine();
                }





                pdf.AddSecondTitle("三、装饰设计");
                pdf.AddNullLine();
                // pdf.添加PDF表格(装饰设计dt);
                pdf.添加PDF表格图片(装饰设计dt楼地面);
                pdf.AddNullLine();
                pdf.添加PDF表格图片(装饰设计dt);
                pdf.AddNullLine();
                pdf.添加PDF表格图片(装饰设计dt天棚);
                pdf.AddNullLine();
                pdf.添加PDF表格图片(装饰设计dt油漆涂料);
                pdf.AddNullLine();
                pdf.添加PDF表格图片(装饰设计dt其他);


                pdf.AddSecondTitle("四、装饰效果展示");
                pdf.AddNullLine();
                for (int i = 0; i < Unit.UnitDollarData.ImagePathList.Count; i++)
                {

                    pdf.AddContent(Unit.UnitDollarData.ImagePathList[i].name);
                    pdf.AddNullLine();
                    Debug.Log(1);
                    for (int j = 0; j < Unit.UnitDollarData.ImagePathList[i].imagePathList.Count; j++)
                    {

                        string filepath = Unit.UnitDollarData.ImagePathList[i].imagePathList[j];
                        pdf.AddImage(filepath);
                    }
                    yield return new WaitForSeconds(0.1f);

                }
                pdf.Dispose();



            }

        }

        Debug.Log("创建===>" + path);
        //Application.OpenURL(path);

    }


    public void OpenPDF()
    {
        string path = Unit.UnitDollarData.ReportPath;
        Debug.Log("创建成功打开文件:" + path);
        Application.OpenURL(path);
    }
}
