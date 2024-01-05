
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using iTextSharp.text.pdf;
using Unit;
//����pdf
public class SetRePort : MonoBehaviour
{

    private string tzgsfilepath = @"Assets\StreamingAssets\ScreenShot\tzgs\Ͷ�ʹ���.png";//Ͷ�ʹ����ַ



    void Start()
    {

    }
    public IEnumerator ����Pdf()
    {







        string[] װ�����Columns = new string[] { "��������/������Ϣ", "�����ֲ�", "����", "������", "�ϼ�" };
        DataTable װ�����dt = new DataTable();
        foreach (string item in װ�����Columns)
        {
            װ�����dt.Columns.Add(item);
        }
        for (int i = 0; i < 10; i++) //�����ݴ��浽list
        {
            DataRow xedr = װ�����dt.NewRow();
            object[] xeobjs = { "������", "¥����װ�ι���", "10", "������", "¥����װ�ι���" };
            xedr.ItemArray = xeobjs;
            װ�����dt.Rows.Add(xedr);
        }


        string path = Application.persistentDataPath + "/װ�����ʵ�鱨��.pdf";
        using (PDFReport pdf = new PDFReport())
        {
            yield return pdf.��ʼ��(path);
            pdf.AddTitle("װ����ƽ׶����ʵ�鱨��");
            pdf.AddFirstTitle("һ��Ͷ�ʹ���");
            pdf.AddNullLine();
            pdf.AddImage(tzgsfilepath);
            pdf.AddContent("Ͷ�ʹ���ģ������100�֣�����" + Unit.UnitDollarData.DeductionNumber + "��" + "���÷�" + Unit.UnitDollarData.EstimateNumber + "�֡�");

            pdf.AddSecondTitle("�����޶�ֽ�");
            pdf.AddNullLine();
            for (int i = 0; i < Unit.UnitDollarData.CombineList.Count; i++)
            {
                string[] Columns = new string[] { "���", "�����", "�����ֲ�����", "������" };
                DataTable �޶�ֽ�dt = new DataTable();
                foreach (string item in Columns)
                {
                    �޶�ֽ�dt.Columns.Add(item);
                }
                for (int j = 0; j < Unit.UnitDollarData.CombineList[i].antidiastoleList.Count; j++) //�����ݴ��浽list
                {
                    DataRow dr = �޶�ֽ�dt.NewRow();
                    object[] objs = Unit.UnitDollarData.CombineList[i].antidiastoleList.ToArray();
                    dr.ItemArray = objs;
                    �޶�ֽ�dt.Rows.Add(dr);
                }
                DataRow dr_hj = �޶�ֽ�dt.NewRow();
                object[] objs1 = { "�����ۼƽ��", "10" };
                dr_hj.ItemArray = objs1;
                �޶�ֽ�dt.Rows.Add(dr_hj);

                pdf.���PDF���(�޶�ֽ�dt);
                pdf.AddNullLine();

            }


            pdf.AddSecondTitle("����װ�����");
            pdf.AddNullLine();
            pdf.���PDF���(װ�����dt);
            pdf.AddSecondTitle("�ġ�װ��Ч��չʾ");
            pdf.AddNullLine();
            pdf.AddImage(tzgsfilepath);
            pdf.AddImage(tzgsfilepath);
            pdf.AddImage(tzgsfilepath);
            pdf.AddImage(tzgsfilepath);




        }
        Debug.Log("�����ɹ����ļ�:" + path);
        Application.OpenURL(path);
    }


    public void OpenPDF(string pdfName)
    {
        string path = Application.persistentDataPath + "/" + pdfName + ".pdf";
        Debug.Log("�����ɹ����ļ�:" + path);
        Application.OpenURL(path);
    }
}
