using System.Collections;
using System.Collections.Generic;
using UnityEditor.AnimatedValues;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// //限额分解
/// </summary>
public class AntidiastoleManager : MonoBehaviour
{
	public Button submitButton;//提交
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

	void Start()
    {

		
	}

    // Update is called once per frame
    void Update()
    {
		楼地面装饰_Amount.text= EachAmount(楼地面装饰InputField).ToString();

	}
	/// <summary>
	/// 每项工程输入后的合计金额
	/// </summary>
	/// <param name="amountList">工程的输入金额</param>
	public float EachAmount(List<InputField> amountList)
	{
		float num = 0;
		
		for (int i = 0; i < amountList.Count; i++)
		{
			if (amountList[i].text!=null&& amountList[i].text != "")
			{
				num = num + float.Parse(amountList[i].text);
			}
			 
		}
		return num;
	}
}
