using System.Collections;
using System.Collections.Generic;
using NPOI.Util;
using UnityEngine;
using UnityEngine.UI;
//�������Ԫ ������
namespace Unit
{
	public static class UnitDollarData
	{
		//ѧ���ķ�����
		public static string ¥����װ��_Amount;
		public static string ǽ����װ��_Amount;
		public static string ���﹤��_Amount;
		public static string ����Ϳ��_Amount;
		public static string ����װ��_Amount;

		//����������ļ����ֶ�
		public static string PDFName;//pdf��
		public static string ObjName;//ģ����

		//���������״̬-******---------------
		public static bool isDataPDF;//����Ķ�pdf
		public static bool isDataObj;//�������ģ��
		public static bool isFinishDataDownload;//�����ģ��״̬---�Ķ�pdf������ģ������״̬����ϣ������ģ��״̬Ϊtrue




		//Ͷ�ʹ�������״̬-******-------------
		public static int DeductionNumber;//����
		public static int EstimateNumber;//�÷�
		public static bool isFinishEstimate;//��ɹ���





		//�޶�ֽ�����״̬-******---------------
		public static List<Combine> CombineList = new List<Combine>();//pdf-�޶�ֽ��б�

		public static List<Antidiastole> GetntiAdiastole(List<InputField> InputFieldList, Antidiastole antidiastole_)
		{
			List<Antidiastole> antidiastoleList = new List<Antidiastole>();
			for (int i = 0; i < InputFieldList.Count; i++)
			{
				antidiastole_.index = i;
				antidiastole_.name = InputFieldList[i].transform.parent.name;
				antidiastole_.departmentName = InputFieldList[i].transform.parent.parent.parent.name;
				antidiastole_.amount = float.Parse(InputFieldList[i].text);

				antidiastoleList.Add(antidiastole_);
			}
			return antidiastoleList;
		}

		public static bool isFinishAntidiastole;//����޶�ֽ�



		//װ���������״̬-******---------------


		//װ��Ч������״̬-******---------------
		public static List<ImagePath> ImagePathList = new List<ImagePath>();//ͼƬ·��
		public static bool isFinishResult;//���װ��Ч��

	}
}

