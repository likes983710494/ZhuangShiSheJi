using System.Collections;
using System.Collections.Generic;
//using System.Windows.Forms;
using UnityEngine;
using UnityEngine.UI;
using Unit;
using Unit.DecorativeDesign;
public class DecorativeDesignManager : MonoBehaviour
{
	public static DecorativeDesignManager Instance_ { get; private set; }
	public Button Button_装饰设计;

	public Button Button_确认;
	public Button Button_提交;
	public Button Button_返回;

	public Button Button_截图;

	//在确认后 中间 更多面板也会用到
	public List<Design> 楼地面_DesignsList = new List<Design>();
	public List<Design> 墙柱面_DesignsList = new List<Design>();
	public List<Design> 天棚工程_DesignsList = new List<Design>();
	public List<Design> 油漆涂料_DesignsList = new List<Design>();
	public List<Design> 其他装饰_DesignsList = new List<Design>();


	private float 楼地面_汇总金额 = 0;
	private float 墙柱面_汇总金额 = 0;
	private float 天棚工程_汇总金额 = 0;
	private float 油漆涂料_汇总金额 = 0;
	private float 其他装饰_汇总金额 = 0;

	void Awake()
	{
		if (Instance_ == null)
		{
			Instance_ = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}
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
		Button_返回.onClick.AddListener(() =>
		{

			DecorativeDesignToButton_返回();
		});
		Button_截图.onClick.AddListener(() =>
		{
			GameObject.Find("----------通用-------- ").GetComponent<SelectScreenshot>().SetIsOpenShot();
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
				//给底部显示增加的设计数
				DecorativeDesignLeft.Instance_.BottomButtonMoreLsit[0].gameObject.transform.GetChild(0).GetComponent<Text>().text =
				楼地面_DesignsList.Count + "条";

				break;
			case "墙、柱面装饰与隔断、幕墙":
				墙柱面_DesignsList.Add(DecorativeDesignRight.Instance_.SetDesignConvertPdf(design_));
				墙柱面_汇总金额 = 墙柱面_汇总金额 + float.Parse(DecorativeDesignRight.Instance_.SetDesignConvertPdf(design_).Total);
				//给汇总金额赋值
				DecorativeDesignLeft.Instance_.CollectList[1].text = 墙柱面_汇总金额.ToString();
				Debug.Log("所用金额:" + 墙柱面_汇总金额);
				DecorativeDesignLeft.Instance_.BottomButtonMoreLsit[1].gameObject.transform.GetChild(0).GetComponent<Text>().text =
			墙柱面_DesignsList.Count + "条";

				break;
			case "天棚":
				天棚工程_DesignsList.Add(DecorativeDesignRight.Instance_.SetDesignConvertPdf(design_));
				天棚工程_汇总金额 = 天棚工程_汇总金额 + float.Parse(DecorativeDesignRight.Instance_.SetDesignConvertPdf(design_).Total);
				//给汇总金额赋值
				DecorativeDesignLeft.Instance_.CollectList[2].text = 天棚工程_汇总金额.ToString();
				DecorativeDesignLeft.Instance_.BottomButtonMoreLsit[2].gameObject.transform.GetChild(0).GetComponent<Text>().text =
			天棚工程_DesignsList.Count + "条";

				break;
			case "油漆、涂料及裱糊":
				油漆涂料_DesignsList.Add(DecorativeDesignRight.Instance_.SetDesignConvertPdf(design_));
				油漆涂料_汇总金额 = 油漆涂料_汇总金额 + float.Parse(DecorativeDesignRight.Instance_.SetDesignConvertPdf(design_).Total);
				//给汇总金额赋值
				DecorativeDesignLeft.Instance_.CollectList[3].text = 油漆涂料_汇总金额.ToString();
				DecorativeDesignLeft.Instance_.BottomButtonMoreLsit[3].gameObject.transform.GetChild(0).GetComponent<Text>().text =
			   油漆涂料_DesignsList.Count + "条";
				break;
			case "其他装饰":
				//添加列表
				其他装饰_DesignsList.Add(DecorativeDesignRight.Instance_.SetDesignConvertPdf(design_));
				//汇总金额
				其他装饰_汇总金额 = 其他装饰_汇总金额 + float.Parse(DecorativeDesignRight.Instance_.SetDesignConvertPdf(design_).Total);
				//给汇总金额赋值
				DecorativeDesignLeft.Instance_.CollectList[4].text = 其他装饰_汇总金额.ToString();
				DecorativeDesignLeft.Instance_.BottomButtonMoreLsit[4].gameObject.transform.GetChild(0).GetComponent<Text>().text =
			其他装饰_DesignsList.Count + "条";
				break;

		}
		//删除做法设计的图
		for (int i = 0; i < DecorativeDesignRight.Instance_.Content_做法说明_01选择做法.transform.childCount; i++)
		{
			int x = i;
			GameObject child = DecorativeDesignRight.Instance_.Content_做法说明_01选择做法.transform.GetChild(x).gameObject;
			Destroy(child);
		}
		//恢复全部初始状态
		DecorativeDesignSaveDate.InitAllProcedure();

		//确定 模型材质，鼠标再点击模型时不会被清除掉
		DecorativeDesignSaveDate.HighligObjectMaterial = DecorativeDesignSaveDate.HighligObject.GetComponent<MeshRenderer>().material;

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

		//本地缓存 模型数据
		InvokInfoDataStorage.Instance_.infoDataStorage_.decorativeDesignManagerData.楼地面_DesignsList = 楼地面_DesignsList;
		InvokInfoDataStorage.Instance_.infoDataStorage_.decorativeDesignManagerData.墙柱面_DesignsList = 墙柱面_DesignsList;
		InvokInfoDataStorage.Instance_.infoDataStorage_.decorativeDesignManagerData.天棚工程_DesignsList = 天棚工程_DesignsList;
		InvokInfoDataStorage.Instance_.infoDataStorage_.decorativeDesignManagerData.油漆涂料_DesignsList = 油漆涂料_DesignsList;
		InvokInfoDataStorage.Instance_.infoDataStorage_.decorativeDesignManagerData.其他装饰_DesignsList = 其他装饰_DesignsList;
		//本地缓存 底部金额
		InvokInfoDataStorage.Instance_.infoDataStorage_.decorativeDesignManagerData.楼地面_汇总金额 = 楼地面_汇总金额.ToString();
		InvokInfoDataStorage.Instance_.infoDataStorage_.decorativeDesignManagerData.墙柱面_汇总金额 = 墙柱面_汇总金额.ToString();
		InvokInfoDataStorage.Instance_.infoDataStorage_.decorativeDesignManagerData.天棚工程_汇总金额 = 天棚工程_汇总金额.ToString();
		InvokInfoDataStorage.Instance_.infoDataStorage_.decorativeDesignManagerData.油漆涂料_汇总金额 = 油漆涂料_汇总金额.ToString();
		InvokInfoDataStorage.Instance_.infoDataStorage_.decorativeDesignManagerData.其他装饰_汇总金额 = 其他装饰_汇总金额.ToString();

		//清除DesignsList
		// UnitDollarData.楼地面_DesignsList.Clear();
		// UnitDollarData.墙柱面_DesignsList.Clear();
		// UnitDollarData.天棚工程_DesignsList.Clear();
		// UnitDollarData.油漆涂料_DesignsList.Clear();
		// UnitDollarData.其他装饰_DesignsList.Clear();



	}

	//装饰设计返回按钮  事件
	public void DecorativeDesignToButton_返回()
	{

		if (GameObject.Find("三维模型展示").transform.childCount > 0)
		{

			GameObject.Find("三维模型展示").transform.GetChild(0).gameObject.SetActive(false);
		}

		//关闭模型模式菜单
		DecorativeDesignModus.Instance_.ModelMenu_UI.SetActive(false);


	}


}
