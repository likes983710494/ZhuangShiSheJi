using System.Collections;
using System.Collections.Generic;
//using System.Windows.Forms;
using UnityEngine;
using UnityEngine.UI;
using Unit;
using Unit.DecorativeDesign;
public class DecorativeDesignManager : MonoBehaviour
{

	public Button Button_装饰设计;

	public Button Button_确认;
	public Button Button_提交;

	private List<Design> 楼地面_DesignsList = new List<Design>();
	private List<Design> 墙柱面_DesignsList = new List<Design>();
	private List<Design> 天棚工程_DesignsList = new List<Design>();
	private List<Design> 油漆涂料_DesignsList = new List<Design>();
	private List<Design> 其他装饰_DesignsList = new List<Design>();
	//汇总金额
	private float 楼地面_汇总金额 = 0;
	private float 墙柱面_汇总金额 = 0;
	private float 天棚工程_汇总金额 = 0;
	private float 油漆涂料_汇总金额 = 0;
	private float 其他装饰_汇总金额 = 0;
	void Start()
	{
		Button_装饰设计.onClick.AddListener(() =>
		{
			DecorativeDesignLeft.Instance_.SetAllocationMoney();
			DecorativeDesignRight.Instance_.LoadGLTF.SetActive(true);
		});
		//确认按钮
		Button_确认.onClick.AddListener(() =>
		{
			VerifyOneProcess();
		});

		Button_提交.interactable = false;
		Button_提交.onClick.AddListener(() =>
		{
			SubmitDecorativeDesign();
		});

	}

	/// <summary>
	/// 点击确认确认 完成一条 
	/// </summary>
	public void VerifyOneProcess()
	{
		//pdf储存
		Design design_ = new Design();
		Debug.Log("确认按钮：" + UnitDollarData.design.departmentName);
		switch (UnitDollarData.design.departmentName)
		{
			case "楼地面装饰":
				楼地面_DesignsList.Add(DecorativeDesignRight.Instance_.SetDesignConvertPdf(design_));
				// 改变汇总金额
				楼地面_汇总金额 = 楼地面_汇总金额 + float.Parse(DecorativeDesignRight.Instance_.SetDesignConvertPdf(design_).Total);
				//给汇总金额赋值
				DecorativeDesignLeft.Instance_.CollectList[0].text = 楼地面_汇总金额.ToString();

				break;
			case "墙、柱面装饰与隔断、幕墙":
				墙柱面_DesignsList.Add(DecorativeDesignRight.Instance_.SetDesignConvertPdf(design_));
				墙柱面_汇总金额 = 墙柱面_汇总金额 + float.Parse(DecorativeDesignRight.Instance_.SetDesignConvertPdf(design_).Total);
				//给汇总金额赋值
				DecorativeDesignLeft.Instance_.CollectList[1].text = 墙柱面_汇总金额.ToString();
				Debug.Log("所用金额:" + 墙柱面_汇总金额);

				break;
			case "天棚":
				天棚工程_DesignsList.Add(DecorativeDesignRight.Instance_.SetDesignConvertPdf(design_));
				天棚工程_汇总金额 = 天棚工程_汇总金额 + float.Parse(DecorativeDesignRight.Instance_.SetDesignConvertPdf(design_).Total);
				//给汇总金额赋值
				DecorativeDesignLeft.Instance_.CollectList[2].text = 天棚工程_汇总金额.ToString();

				break;
			case "油漆、涂料及裱糊":
				油漆涂料_DesignsList.Add(DecorativeDesignRight.Instance_.SetDesignConvertPdf(design_));
				油漆涂料_汇总金额 = 油漆涂料_汇总金额 + float.Parse(DecorativeDesignRight.Instance_.SetDesignConvertPdf(design_).Total);
				//给汇总金额赋值
				DecorativeDesignLeft.Instance_.CollectList[3].text = 油漆涂料_汇总金额.ToString();
				break;
			case "其他装饰":
				//添加列表
				其他装饰_DesignsList.Add(DecorativeDesignRight.Instance_.SetDesignConvertPdf(design_));
				//汇总金额
				其他装饰_汇总金额 = 其他装饰_汇总金额 + float.Parse(DecorativeDesignRight.Instance_.SetDesignConvertPdf(design_).Total);
				//给汇总金额赋值
				DecorativeDesignLeft.Instance_.CollectList[4].text = 其他装饰_汇总金额.ToString();
				break;

		}
		Debug.Log("墙柱面_DesignsList.Count:" + 墙柱面_DesignsList.Count);

		//恢复全部初始状态
		DecorativeDesignSaveDate.InitAllProcedure();

		Button_确认.interactable = false;
		//左侧抽屉按钮  关闭
		DecorativeDesignModus.Instance_.LeftMakerUnfoldButton.interactable = false;
		//列表添加成功，可以提交,提交按钮显示
		if (其他装饰_DesignsList.Count > 0 || 油漆涂料_DesignsList.Count > 0 || 天棚工程_DesignsList.Count > 0 || 墙柱面_DesignsList.Count > 0 || 楼地面_DesignsList.Count > 0)
		{
			Button_提交.interactable = true;
		}



	}

	/// <summary>
	/// 提交装饰设计
	/// </summary>
	public void SubmitDecorativeDesign()
	{

		UnitDollarData.楼地面_DesignsList = 楼地面_DesignsList;
		UnitDollarData.墙柱面_DesignsList = 墙柱面_DesignsList;
		UnitDollarData.天棚工程_DesignsList = 天棚工程_DesignsList;
		UnitDollarData.油漆涂料_DesignsList = 油漆涂料_DesignsList;
		UnitDollarData.其他装饰_DesignsList = 其他装饰_DesignsList;
		UnitDollarData.楼地面_汇总金额 = 楼地面_汇总金额;
		UnitDollarData.墙柱面_汇总金额 = 墙柱面_汇总金额;
		UnitDollarData.天棚工程_汇总金额 = 天棚工程_汇总金额;
		UnitDollarData.油漆涂料_汇总金额 = 油漆涂料_汇总金额;
		UnitDollarData.其他装饰_汇总金额 = 其他装饰_汇总金额;
		//清除DesignsList
		// UnitDollarData.楼地面_DesignsList.Clear();
		// UnitDollarData.墙柱面_DesignsList.Clear();
		// UnitDollarData.天棚工程_DesignsList.Clear();
		// UnitDollarData.油漆涂料_DesignsList.Clear();
		// UnitDollarData.其他装饰_DesignsList.Clear();

	}




}
