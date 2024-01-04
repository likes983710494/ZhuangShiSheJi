using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
		
		public static bool isFinishAntidiastole;


	}
}

