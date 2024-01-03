




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
    string fileText1path = @"Assets\StreamingAssets\ͼƬ.png";
    public IEnumerator ����Pdf(string pdfName)//test.pdf
    {
      
  
        string path = Application.persistentDataPath + "/"+pdfName+".pdf";
        Debug.Log("��ַ��"+path);

        using (pdf = new PDFReport())
        {
            yield return pdf.��ʼ��(path);
            //�������
            AddTitle(pdfName);
            AddContent(pdfName);
            AddImage(@"Assets\StreamingAssets\ͼƬ.png");
            AddForm(new string[] { "���", "����", "��Ʒ", "ϵ��2222222222222222222222222222222", "�������", "������", "����", "����" });
        }
       
    }

    /// <summary>
    /// ����pdf
    /// </summary>
    /// <param name="pdfName"> �ļ���</param>
    /// <returns></returns>
    public void CreatPDF(string pdfName)
    {
        StartCoroutine(����Pdf(pdfName));
    }
    /// <summary>
    /// ���ĵ�
    /// </summary>
    /// <param name="pdfName"></param>
    public void OpenPDF(string pdfName)
    {
        string path = Application.persistentDataPath + "/" + pdfName + ".pdf";
        Debug.Log("�����ɹ����ļ�:" + path);
        Application.OpenURL(path);
    }
    /// <summary>
    /// ������ֱ���
    /// </summary>
    /// <param name="content"></param>
    public void AddTitle(string content)
    {
        if (content != null)
            pdf.AddTitle(content);
    }
    /// <summary>
    /// �����������
    /// </summary>
    /// <param name="content"></param>
    public void AddContent(string content)
    {
        if (content != null)
            pdf.AddContent(content);
    }

    /// <summary>
    /// ���ͼƬ
    /// </summary>
    /// <param name="imgPath"> 
    ///  string imgPath = @"Assets\StreamingAssets\ͼƬ.png";
    /// param>
    public  void AddImage(string imgPath, int width = 475, int height = 325)
    {
        if (imgPath != null)
            pdf.AddImage(imgPath, width, height);
    }
    /// <summary>
    /// ��ӱ�� 
    /// </summary>
    /// <param name="Columns"> ���ͷ
    ///  Columns = new string[] { "���", "����", "��Ʒ", "ϵ��2222222222222222222222222222222", "�������", "������", "����", "����" };
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
    
       //���һ������ ����ͷ��Ӧ
            DataRow dr = dt.NewRow();
       object[] objs = { 9991111, "��������", "���ǲ�Ʒ", "ϵ���Ŵ�",
                "�������6666", "������111", "����33.3333", "���鰡��������������������������������������������" };
        dr.ItemArray = objs;
            dt.Rows.Add(dr);
            pdf.���PDF���(dt);
    }



}

