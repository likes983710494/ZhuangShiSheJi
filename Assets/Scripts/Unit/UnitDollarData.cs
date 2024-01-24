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

		public static List<Antidiastole> GetntiAdiastole(List<InputField> InputFieldList)
		{

			List<Antidiastole> antidiastoleList = new List<Antidiastole>();
			for (int i = 0; i < InputFieldList.Count; i++)
			{
				Antidiastole antidiastole_ = new Antidiastole();
				antidiastole_.index = i;
				antidiastole_.name = InputFieldList[i].transform.parent.name;
				antidiastole_.departmentName = InputFieldList[i].transform.parent.parent.parent.name;
				antidiastole_.amount = float.Parse(InputFieldList[i].text);

				antidiastoleList.Add(antidiastole_);
			}
			return antidiastoleList;
		}

		// 装饰设计-底部-装饰设计概算学生的分配额度
		public static string 楼地面装饰_Amount;
		public static string 墙柱面装饰_Amount;
		public static string 天棚工程_Amount;
		public static string 油漆涂料_Amount;
		public static string 其他装饰_Amount;
		public static string[] AmountArray = new string[] { };
		public static bool isFinishAntidiastole;//完成限额分解



		//装饰设计所有状态-******---------------
		public static List<Design> 楼地面_DesignsList = new List<Design>();//pdf-楼地面装饰设计列表
		public static List<Design> 墙柱面_DesignsList = new List<Design>();
		public static List<Design> 天棚工程_DesignsList = new List<Design>();
		public static List<Design> 油漆涂料_DesignsList = new List<Design>();
		public static List<Design> 其他装饰_DesignsList = new List<Design>();

		public static Design design = new Design();

		//汇总金额
		public static float 楼地面_汇总金额 = 0;
		public static float 墙柱面_汇总金额 = 0;
		public static float 天棚工程_汇总金额 = 0;
		public static float 油漆涂料_汇总金额 = 0;
		public static float 其他装饰_汇总金额 = 0;
		public static bool isFinishDesign;//完成装饰设计

		//装饰效果所有状态-******---------------
		public static List<ImagePath> ImagePathList = new List<ImagePath>();//图片路径
		public static bool isFinishResult;//完成装饰效果



		//实验报告路径----------
		public static string ReportPath;//实验报告路径

	}
}

