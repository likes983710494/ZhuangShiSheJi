
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
    string fileText1path = @"Assets\StreamingAssets\iamge\��ò��Խ�ͼ0.7817451.png";



    void Start()
    {

    }

    public IEnumerator ����Pdf()
    {

        string[] Columns = new string[] { "���", "�����", "�����ֲ�����", "������" };
        DataTable �޶�ֽ�dt = new DataTable();
        foreach (string item in Columns)
        {
            �޶�ֽ�dt.Columns.Add(item);
        }
        for (int i = 0; i < 6; i++) //�����ݴ��浽list
        {
            DataRow dr = �޶�ֽ�dt.NewRow();
            object[] objs = { i + 1, "������", "¥����װ�ι���", "10" };
            dr.ItemArray = objs;
            �޶�ֽ�dt.Rows.Add(dr);
        }
        DataRow dr1 = �޶�ֽ�dt.NewRow();
        object[] objs1 = { "�����ۼƽ��", "10" };
        dr1.ItemArray = objs1;
        �޶�ֽ�dt.Rows.Add(dr1);



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
            pdf.AddTitle("װ�����ʵ�鱨��");
            pdf.AddFirstTitle("һ��Ͷ�ʹ���");
            pdf.AddNullLine();
            pdf.AddImage(fileText1path);
            pdf.AddContent("Ͷ�ʹ���ģ������100�֣�����9�֣��÷�91�֡�");
            pdf.AddSecondTitle("�����޶�ֽ�");
            pdf.AddNullLine();
            pdf.���PDF���(�޶�ֽ�dt);
            pdf.AddSecondTitle("����װ�����");
            pdf.AddNullLine();
            pdf.���PDF���(װ�����dt);
            pdf.AddSecondTitle("�ġ�װ��Ч��չʾ");
            pdf.AddNullLine();
            pdf.AddImage(fileText1path);
            pdf.AddImage(fileText1path);
            pdf.AddImage(fileText1path);
            pdf.AddImage(fileText1path);




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
