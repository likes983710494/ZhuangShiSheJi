using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using UnityEditor.AnimatedValues;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// //限额分解
/// </summary>
public class AntidiastoleManager : MonoBehaviour
{
	public Button submitButton;//提交
	public Button affirmButton;//确认按钮
	public GameObject promptContentPlane;//提示面板父节点
	public Text promptContentText;//提示内容

	public List<InputField> 楼地面装饰InputField = new List<InputField>();//楼地面装饰
	public List<InputField> 墙柱面装饰InputField = new List<InputField>();//墙柱面装饰
	public List<InputField> 天棚工程InputField = new List<InputField>();//天棚工程
	public List<InputField> 油漆涂料InputField = new List<InputField>();//油漆涂料及裱糊工程
	public List<InputField> 其他装饰InputField = new List<InputField>();//其他装饰工程
	

	public Text 楼地面装饰_Amount;
	public Text 墙柱面装饰_Amount;
	public Text 天棚工程_Amount;
	public Text 油漆涂料_Amount;
	public Text 其他装饰_Amount;
	public Text 总金额_Amount;//由老师设定，后台发送

	private float 总金额_Cache=0;//和总金额比较
	private float proportion = 0;//cache占总金额比例
	private bool is楼地面装饰, is墙柱面装饰, is天棚工程, is油漆涂料, is其他装饰;
	void Start()
    {
		affirmButton.gameObject.SetActive(false);
		submitButton.onClick.AddListener(SubmitEachAmount);
		for (int i = 0; i < 楼地面装饰InputField.Count; i++)
		{
			楼地面装饰InputField[i].onValueChanged.AddListener((string value) => {
				楼地面装饰_Amount.text = EachAmount(楼地面装饰InputField).ToString();
				is楼地面装饰= SetIsSubmit(楼地面装饰InputField, is楼地面装饰);
			});
		}
		for (int i = 0; i < 墙柱面装饰InputField.Count; i++)
		{
			墙柱面装饰InputField[i].onValueChanged.AddListener((string value) => {
				墙柱面装饰_Amount.text = EachAmount(墙柱面装饰InputField).ToString();
				is墙柱面装饰 = SetIsSubmit(墙柱面装饰InputField, is墙柱面装饰);
			});
		}
		for (int i = 0; i < 天棚工程InputField.Count; i++)
		{
			天棚工程InputField[i].onValueChanged.AddListener((string value) => {
				天棚工程_Amount.text = EachAmount(天棚工程InputField).ToString();
				is天棚工程 = SetIsSubmit(天棚工程InputField, is天棚工程);
			});
		}
		for (int i = 0; i < 油漆涂料InputField.Count; i++)
		{
			油漆涂料InputField[i].onValueChanged.AddListener((string value) => {
				油漆涂料_Amount.text = EachAmount(油漆涂料InputField).ToString();
				is油漆涂料 = SetIsSubmit(油漆涂料InputField, is油漆涂料);
			});
		}
		for (int i = 0; i < 其他装饰InputField.Count; i++)
		{
			其他装饰InputField[i].onValueChanged.AddListener((string value) => {
				其他装饰_Amount.text = EachAmount(其他装饰InputField).ToString();
				is其他装饰 = SetIsSubmit(其他装饰InputField, is其他装饰);
			});
		}
	}

	// Update is called once per frame
    void Update()
    {
		//
		if (is楼地面装饰 == true && is墙柱面装饰 == true && is天棚工程 == true && is油漆涂料 == true && is其他装饰 == true)
		{
			submitButton.interactable = true;
		}
		else
		{
			submitButton.interactable = false;
		}

	}
	/// <summary>
	/// 是否符合提交
	/// </summary>
	/// <param name="amountList"></param>
	/// <param name="isAmount"></param>
	/// <returns></returns>
	public bool SetIsSubmit(List<InputField> amountList, bool isAmount) {
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

			if (number == amountList.Count)
			{
				Debug.Log("过关");

				isAmount = true;

			
			}

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
			if (amountList[i].text!=null&& amountList[i].text != "")
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

		总金额_Cache = float.Parse(楼地面装饰_Amount.text) + float.Parse(墙柱面装饰_Amount.text) + float.Parse(天棚工程_Amount.text) + float.Parse(油漆涂料_Amount.text) + float.Parse(其他装饰_Amount.text);
		proportion = 总金额_Cache / float.Parse(总金额_Amount.text);
		promptContentPlane.SetActive(true);
		if (proportion < 0.6)//小于百分之60
		{
			Debug.Log("分部工程的汇总金额太低，请重新调整");
			promptContentText.text = "分部工程的汇总金额太低，请重新调整";
		}
		if (proportion > 0.6 && proportion < 1)
		{
			Debug.Log("分部工程的汇总金额符合");
			promptContentText.text = "分部工程的汇总金额符合";
			affirmButton.gameObject.SetActive(true);
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
		//隐藏模块
		//提交确定信息
	}

}
