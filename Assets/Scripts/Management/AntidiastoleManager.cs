using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Unit;
/// <summary>
/// 限额分解
/// </summary>
public class AntidiastoleManager : MonoBehaviour
{

	public static AntidiastoleManager Instance_ { get; private set; }
	public Button submitButton;//提交
	public Button affirmButton;//确认按钮
	public GameObject promptContentPlane;//提示面板父节点
	public Text promptContentText;//提示内容

	public List<InputField> 楼地面装饰InputField = new List<InputField>();//楼地面装饰
	public List<InputField> 墙柱面装饰InputField = new List<InputField>();//墙柱面装饰
	public List<InputField> 天棚工程InputField = new List<InputField>();//天棚工程
	public List<InputField> 油漆涂料InputField = new List<InputField>();//油漆涂料及裱糊工程
	public List<InputField> 其他装饰InputField = new List<InputField>();//其他装饰工程


	public List<InputField> Amount_InputField = new List<InputField>();//第二列 分部 


	private Antidiastole m_Antidiastole = new Antidiastole();

	private Combine combine = new Combine();

	public Text 总金额_Amount;//由老师设定，后台发送

	private float 总金额_Cache = 0;//和总金额比较
	private float proportion = 0;//cache占总金额比例
	private bool is楼地面装饰, is墙柱面装饰, is天棚工程, is油漆涂料, is其他装饰, isAmount;

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
		affirmButton.gameObject.SetActive(false);
		affirmButton.onClick.AddListener(PromptContentAffirm);
		submitButton.onClick.AddListener(SubmitEachAmount);

		for (int i = 0; i < Amount_InputField.Count; i++)
		{
			Amount_InputField[i].onValueChanged.AddListener((string value) =>
			{

				isAmount = SetIsSubmit(Amount_InputField, isAmount);
			});
		}

		for (int i = 0; i < 楼地面装饰InputField.Count; i++)
		{
			楼地面装饰InputField[i].onValueChanged.AddListener((string value) =>
			{

				is楼地面装饰 = SetIsSubmit(楼地面装饰InputField, is楼地面装饰);


			});
		}
		for (int i = 0; i < 墙柱面装饰InputField.Count; i++)
		{
			墙柱面装饰InputField[i].onValueChanged.AddListener((string value) =>
			{

				is墙柱面装饰 = SetIsSubmit(墙柱面装饰InputField, is墙柱面装饰);

			});
		}
		for (int i = 0; i < 天棚工程InputField.Count; i++)
		{
			天棚工程InputField[i].onValueChanged.AddListener((string value) =>
			{

				is天棚工程 = SetIsSubmit(天棚工程InputField, is天棚工程);

			});
		}
		for (int i = 0; i < 油漆涂料InputField.Count; i++)
		{
			油漆涂料InputField[i].onValueChanged.AddListener((string value) =>
			{

				is油漆涂料 = SetIsSubmit(油漆涂料InputField, is油漆涂料);

			});
		}
		for (int i = 0; i < 其他装饰InputField.Count; i++)
		{
			其他装饰InputField[i].onValueChanged.AddListener((string value) =>
			{

				is其他装饰 = SetIsSubmit(其他装饰InputField, is其他装饰);

			});
		}
	}


	void Update()
	{
		//
		if (is楼地面装饰 == true && is墙柱面装饰 == true && is天棚工程 == true && is油漆涂料 == true && is其他装饰 == true && isAmount == true)
		{
			submitButton.interactable = true;
		}
		else
		{
			submitButton.interactable = false;
		}

	}

	//初始化数据 调用状态
	public void InfoDataStorage_GetAntidiastoleState()
	{
		isAmount = SetIsSubmit(Amount_InputField, isAmount);
		is楼地面装饰 = SetIsSubmit(楼地面装饰InputField, is楼地面装饰);
		is墙柱面装饰 = SetIsSubmit(墙柱面装饰InputField, is墙柱面装饰);
		is天棚工程 = SetIsSubmit(天棚工程InputField, is天棚工程);
		is油漆涂料 = SetIsSubmit(油漆涂料InputField, is油漆涂料);
		is其他装饰 = SetIsSubmit(其他装饰InputField, is其他装饰);
	}

	/// <summary>
	/// 是否符合提交
	/// </summary>
	/// <param name="amountList"></param>
	/// <param name="isAmount"></param>
	/// <returns></returns>
	public bool SetIsSubmit(List<InputField> amountList, bool isAmount)
	{
		int number = 0;
		//是否全输入了符合提交
		for (int i = 0; i < amountList.Count; i++)
		{
			if (amountList[i].text.Length == 0)
			{
				//减分
				Debug.Log("未输入");
				isAmount = false;
				break;
			}
			else
			{
				number++;
			}

		}

		if (number == amountList.Count)
		{
			Debug.Log("过关");

			isAmount = true;


		}
		return isAmount;
	}
	/// <summary>
	/// 每项工程输入后的合计金额
	/// </summary>
	/// <param name="amountList">工程的输入金额</param>
	public float EachAmount(List<InputField> amountList)
	{
		float num = 0;//汇总单项金额

		for (int i = 0; i < amountList.Count; i++)
		{
			if (amountList[i].text != null && amountList[i].text != "")
			{
				num = num + float.Parse(amountList[i].text);
			}

		}
		return num;
	}
	/// <summary>
	/// 提交限额分解
	/// </summary>
	public void SubmitEachAmount()
	{

		总金额_Cache = float.Parse(Amount_InputField[0].text) + float.Parse(Amount_InputField[1].text) + float.Parse(Amount_InputField[2].text) + float.Parse(Amount_InputField[3].text) + float.Parse(Amount_InputField[4].text);
		proportion = 总金额_Cache / float.Parse(总金额_Amount.text);
		promptContentPlane.SetActive(true);
		if (proportion < 0.6)//小于百分之60
		{
			Debug.Log("分部工程的汇总金额太低，请重新调整");
			promptContentText.text = "分部工程的汇总金额太低，请重新调整";
		}
		if (proportion > 0.6 && proportion <= 1)
		{


			//分部金额和分项金额要相等 否则提示


			if (float.Parse(Amount_InputField[0].text) != EachAmount(楼地面装饰InputField))
			{
				Debug.Log("金额错误，请调整楼地面装饰的分部和分项的金额。");
				promptContentText.text = "金额错误，请调整楼地面装饰的分部和分项的金额。";
				affirmButton.gameObject.SetActive(false);
			}
			else if (float.Parse(Amount_InputField[1].text) != EachAmount(墙柱面装饰InputField))
			{
				Debug.Log("金额错误，请调整楼地面装饰的分部和分项的金额。");
				promptContentText.text = "金额错误，请调整楼墙、柱面装饰与隔断、幕墙工程的分部和分项的金额。";
				affirmButton.gameObject.SetActive(false);
			}
			else if (float.Parse(Amount_InputField[2].text) != EachAmount(天棚工程InputField))
			{
				Debug.Log("金额错误，请调整楼地面装饰的分部和分项的金额。");
				promptContentText.text = "金额错误，请调整楼天棚工程的分部和分项的金额。";
				affirmButton.gameObject.SetActive(false);
			}
			else if (float.Parse(Amount_InputField[3].text) != EachAmount(油漆涂料InputField))
			{
				Debug.Log("金额错误，请调整楼地面装饰的分部和分项的金额。");
				promptContentText.text = "金额错误，请调整油漆、涂料及裱糊工程的分部和分项的金额。";
				affirmButton.gameObject.SetActive(false);
			}
			else if (float.Parse(Amount_InputField[4].text) != EachAmount(其他装饰InputField))
			{
				Debug.Log("金额错误，请调整楼地面装饰的分部和分项的金额。");
				promptContentText.text = "金额错误，请调其他装饰的分部和分项的金额。";
				affirmButton.gameObject.SetActive(false);
			}
			else
			{
				Debug.Log("工程金额符合,请点击确认进行下一项");
				promptContentText.text = "工程金额符合,请点击确认进行下一项。";
				affirmButton.gameObject.SetActive(true);

			}

		}
		if (proportion > 1)
		{
			Debug.Log("分部工程的汇总金额太高，请重新调整");
			promptContentText.text = "分部工程的汇总金额太高，请重新调整";
		}

	}

	/// <summary>
	/// 通知栏确定按钮
	/// </summary>
	public void PromptContentAffirm()
	{
		//隐藏模块-已非代码实现

		//提交确定信息
		//金额数据储存	，将分部限额金额通知到装饰设计
		Unit.UnitDollarData.楼地面装饰_Amount = Amount_InputField[0].text;
		Unit.UnitDollarData.墙柱面装饰_Amount = Amount_InputField[1].text;
		Unit.UnitDollarData.天棚工程_Amount = Amount_InputField[2].text;
		Unit.UnitDollarData.油漆涂料_Amount = Amount_InputField[3].text;
		Unit.UnitDollarData.其他装饰_Amount = Amount_InputField[4].text;
		Unit.UnitDollarData.AmountArray = new string[] { Unit.UnitDollarData.楼地面装饰_Amount, Unit.UnitDollarData.墙柱面装饰_Amount, Unit.UnitDollarData.天棚工程_Amount,
		 Unit.UnitDollarData.油漆涂料_Amount, Unit.UnitDollarData.其他装饰_Amount };

		//限额分解 本地数据缓存
		InvokInfoDataStorage.Instance_.infoDataStorage_.antidiastoleManagerData.AmountInputField = new string[] { Unit.UnitDollarData.楼地面装饰_Amount, Unit.UnitDollarData.墙柱面装饰_Amount, Unit.UnitDollarData.天棚工程_Amount,
		 Unit.UnitDollarData.油漆涂料_Amount, Unit.UnitDollarData.其他装饰_Amount };
		InvokInfoDataStorage.Instance_.infoDataStorage_.antidiastoleManagerData.楼地面装饰InputField = 楼地面装饰InputField.Select(inputField => inputField.text).ToList();
		InvokInfoDataStorage.Instance_.infoDataStorage_.antidiastoleManagerData.墙柱面装饰InputField = 墙柱面装饰InputField.Select(inputField => inputField.text).ToList();
		InvokInfoDataStorage.Instance_.infoDataStorage_.antidiastoleManagerData.天棚工程InputField = 天棚工程InputField.Select(inputField => inputField.text).ToList();
		InvokInfoDataStorage.Instance_.infoDataStorage_.antidiastoleManagerData.油漆涂料InputField = 油漆涂料InputField.Select(inputField => inputField.text).ToList();
		InvokInfoDataStorage.Instance_.infoDataStorage_.antidiastoleManagerData.其他装饰InputField = 其他装饰InputField.Select(inputField => inputField.text).ToList();
		InvokInfoDataStorage.Instance_.infoDataStorage_.antidiastoleManagerData.isFinishAntidiastole = true;

		//限额分解完成 ， 开启装饰设计
		Unit.UnitDollarData.isFinishAntidiastole = true;
		HomePageManager.Instance_.Button_装饰设计.interactable = true;
		//储存数据
		Unit.UnitDollarData.CombineList.Clear();
		combine.antidiastoleList = Unit.UnitDollarData.GetntiAdiastole(楼地面装饰InputField);
		Unit.UnitDollarData.CombineList.Add(combine);
		Combine combine2 = new Combine();

		combine2.antidiastoleList = Unit.UnitDollarData.GetntiAdiastole(墙柱面装饰InputField);
		Unit.UnitDollarData.CombineList.Add(combine2);
		Combine combine3 = new Combine();

		combine3.antidiastoleList = Unit.UnitDollarData.GetntiAdiastole(天棚工程InputField);
		Unit.UnitDollarData.CombineList.Add(combine3);
		Combine combine4 = new Combine();

		combine4.antidiastoleList = Unit.UnitDollarData.GetntiAdiastole(油漆涂料InputField);
		Unit.UnitDollarData.CombineList.Add(combine4);
		Combine combine5 = new Combine();

		combine5.antidiastoleList = Unit.UnitDollarData.GetntiAdiastole(其他装饰InputField);
		Unit.UnitDollarData.CombineList.Add(combine5);


	}

}
