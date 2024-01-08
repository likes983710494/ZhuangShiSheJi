using System.Collections;
using System.Collections.Generic;
using NPOI.Util;
using UnityEngine;
using UnityEngine.UI;
//储存个单元 的数据
namespace Unit
{
	public static class UnitDollarData
	{
		//学生的分配额度
		public static string 楼地面装饰_Amount;
		public static string 墙柱面装饰_Amount;
		public static string 天棚工程_Amount;
		public static string 油漆涂料_Amount;
		public static string 其他装饰_Amount;

		//设计书下载文件名字段
		public static string PDFName;//pdf名
		public static string ObjName;//模型名

		//设计书所有状态-******---------------
		public static bool isDataPDF;//完成阅读pdf
		public static bool isDataObj;//完成下载模型
		public static bool isFinishDataDownload;//设计书模块状态---阅读pdf、下载模型两个状态都完毕，设计书模块状态为true




		//投资估算所有状态-******-------------
		public static int DeductionNumber;//减分
		public static int EstimateNumber;//得分
		public static bool isFinishEstimate;//完成估算





		//限额分解所有状态-******---------------
		public static List<Combine> CombineList = new List<Combine>();//pdf-限额分解列表

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

		public static bool isFinishAntidiastole;//完成限额分解



		//装饰设计所有状态-******---------------


		//装饰效果所有状态-******---------------
		public static List<ImagePath> ImagePathList = new List<ImagePath>();//图片路径
		public static bool isFinishResult;//完成装饰效果

	}
}

