
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
//����pdf
public class SetRePort : MonoBehaviour
{

    private string tzgsfilepath = @"Assets\StreamingAssets\ScreenShot\tzgs\Ͷ�ʹ���.png";//Ͷ�ʹ����ַ
    private List<DataTable> DataTable_List = new List<DataTable>();

    public Button Confirm_button;
    public Button Check_button;//�鿴ʵ�鱨��pdf��ť
    void Start()
    {
        Confirm_button.onClick.AddListener(() =>
        {
            StartCoroutine(����Pdf());
        });
        Check_button.onClick.AddListener(() =>
       {
           OpenPDF();
       });

    }
    public IEnumerator ����Pdf()
    {

        //�޶�ֽ�
        DataTable_List.Clear();
        for (int i = 0; i < Unit.UnitDollarData.CombineList.Count; i++)
        {
            DataTable �޶�ֽ�dt = new DataTable();
            string[] Columns = new string[] { "���", "�����", "�����ֲ�����", "������" };
            foreach (string item in Columns)
            {
                �޶�ֽ�dt.Columns.Add(item);
            }

            for (int j = 0; j < Unit.UnitDollarData.CombineList[i].antidiastoleList.Count; j++) //�����ݴ��浽list
            {
                DataRow dr = �޶�ֽ�dt.NewRow();
                object[] objs = Unit.UnitDollarData.CombineList[i].antidiastoleList[j].GetType().GetProperties().Select(p => p.GetValue(Unit.UnitDollarData.CombineList[i].antidiastoleList[j])).ToArray();

                Debug.Log("fenjie" + Unit.UnitDollarData.CombineList[i].antidiastoleList[j]);
                dr.ItemArray = objs;
                �޶�ֽ�dt.Rows.Add(dr);
            }


            DataRow dr_hj = �޶�ֽ�dt.NewRow();
            object[] objs1 = { "�����ۼƽ��", Unit.UnitDollarData.AmountArray[i] };
            dr_hj.ItemArray = objs1;
            �޶�ֽ�dt.Rows.Add(dr_hj);


            DataTable_List.Add(�޶�ֽ�dt);



        }



        //  װ�����
        string[] װ�����Columns = new string[] { "��������/������Ϣ", "�����ֲ�", "λ��", "����˵��-�������", "����˵��-���̲���", "����˵��", "����", "������", "�ϼ�" };
        DataTable װ�����dt = new DataTable();
        foreach (string item in װ�����Columns)
        {
            װ�����dt.Columns.Add(item);
        }
        for (int i = 0; i < UnitDollarData.ǽ����_DesignsList.Count; i++) //�����ݴ��浽list
        {
            DataRow xedr = װ�����dt.NewRow();

            object[] xeobjs = UnitDollarData.ǽ����_DesignsList[i].GetType().GetProperties().Select(p => p.GetValue(UnitDollarData.ǽ����_DesignsList[i])).ToArray();

            //object[] xeobjs = { "������", "¥����װ�ι���", tzgsfilepath, tzgsfilepath, "¥����װ�ι���", "¥����װ�ι���", "¥����װ�ι���", "¥����װ�ι���" };

            Debug.Log("PDF" + UnitDollarData.ǽ����_DesignsList[i].departmentName + "----·����" + UnitDollarData.ǽ����_DesignsList[i].designImagePath + "||");
            Debug.Log("����" + xeobjs.Length);
            Debug.Log("���" + UnitDollarData.ǽ����_DesignsList[i]);
            foreach (var obj in xeobjs)
            {
                Debug.Log("ʾ����" + obj);
            }

            xedr.ItemArray = xeobjs;
            װ�����dt.Rows.Add(xedr);
        }



        string path = Application.persistentDataPath + "/װ�����ʵ�鱨��.pdf";
        Unit.UnitDollarData.ReportPath = path;
        using (PDFReport pdf = new PDFReport())
        {

            yield return pdf.��ʼ��(path);

            if (File.Exists(path))
            {
                Debug.Log("PDF�����ɹ�");
                pdf.AddTitle("װ����ƽ׶����ʵ�鱨��");
                pdf.AddSecondTitle("������xxx   �༶��2024��1��", 1);
                pdf.AddSecondTitle("һ��Ͷ�ʹ���");
                pdf.AddNullLine();
                pdf.AddImage(tzgsfilepath);
                pdf.AddContent("Ͷ�ʹ���ģ������100�֣�����" + Unit.UnitDollarData.DeductionNumber + "��" + "���÷�" + Unit.UnitDollarData.EstimateNumber + "�֡�");

                pdf.AddSecondTitle("�����޶�ֽ�");
                pdf.AddNullLine();
                for (int i = 0; i < DataTable_List.Count; i++)
                {
                    pdf.���PDF���(DataTable_List[i]);
                    pdf.AddNullLine();
                    pdf.AddNullLine();
                }





                pdf.AddSecondTitle("����װ�����");
                pdf.AddNullLine();
                // pdf.���PDF���(װ�����dt);
                pdf.���PDF���ͼƬ(װ�����dt);




                pdf.AddSecondTitle("�ġ�װ��Ч��չʾ");
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

        Debug.Log("����===>" + path);
        //Application.OpenURL(path);

    }


    public void OpenPDF()
    {
        string path = Unit.UnitDollarData.ReportPath;
        Debug.Log("�����ɹ����ļ�:" + path);
        Application.OpenURL(path);
    }
}
