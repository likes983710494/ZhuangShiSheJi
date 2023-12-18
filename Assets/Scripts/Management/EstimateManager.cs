using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 投资估算
/// </summary>
public class EstimateManager : MonoBehaviour
{
    public List<Transform> ParentTrms = new List<Transform>();
	public List<bool> isSet = new List<bool>();//储存每个框结果

	public Button submitButton;
	public Button affirmButton;//确认按钮
	public GameObject promptContentPlane;//提示面板父节点
	public Text promptContentText;//提示内容

	void Start()
    {

		submitButton.onClick.AddListener(SetParentAndTransform);
		affirmButton.onClick.AddListener(PromptContentAffirm);

	}

    // Update is called once per frame
    void Update()
    {
		if (ParentTrms[6].childCount > 0 && ParentTrms[5].childCount > 0 && ParentTrms[4].childCount > 0 && ParentTrms[3].childCount > 0 && ParentTrms[2].childCount > 0 && ParentTrms[1].childCount > 0 && ParentTrms[0].childCount > 0)
		{
			submitButton.interactable = true;
		}
		else
		{
			submitButton.interactable = false;
		}

	}
	/// <summary>
	/// 提交
	/// </summary>
	public void SetParentAndTransform()
	{
		isSet.Clear();//清空
		int number = 0;
		if (ParentTrms[0].GetChild(0) != null && ParentTrms[0].GetChild(0).name == ParentTrms[0].name && ParentTrms[1].childCount > 0)
		{
			// 建筑工程费用判断是对的
			isSet.Add(true);
		}
		else
		{
			isSet.Add(false);
		}
		if( ParentTrms[1].childCount>0)
		{
			if(ParentTrms[1].GetChild(0).name== "设备工器具购置费及安装费" || ParentTrms[1].GetChild(0).name == "流动资金" || ParentTrms[1].GetChild(0).name == "其他费用")
			{
				//设备工器具购置费及安装费 判断是对的
				isSet.Add(true);
			}
			else
			{
				isSet.Add(false);
			}
		}

		if ( ParentTrms[2].childCount > 0)
		{
			if (ParentTrms[2].GetChild(0).name == "设备工器具购置费及安装费" || ParentTrms[2].GetChild(0).name == "流动资金" || ParentTrms[2].GetChild(0).name == "其他费用")
			{
				//流动资金 判断是对的
				isSet.Add(true);
			}
			else
			{
				isSet.Add(false);
			}
		}
		if ( ParentTrms[3].childCount > 0)
		{
			if (ParentTrms[3].GetChild(0).name == "设备工器具购置费及安装费" || ParentTrms[3].GetChild(0).name == "流动资金" || ParentTrms[3].GetChild(0).name == "其他费用")
			{
				//其他费用 判断是对的
				isSet.Add(true);
			}
			else
			{
				isSet.Add(false);
			}
		}

		if (ParentTrms[4].childCount > 0)
		{
			if (ParentTrms[4].GetChild(0).name == "建筑估算" || ParentTrms[4].GetChild(0).name == "装饰估算" || ParentTrms[4].GetChild(0).name == "安装估算")
			{
				//建筑估算 
				isSet.Add(true);
			}
			else
			{
				isSet.Add(false);
			}
		}
		if (ParentTrms[5].childCount > 0)
		{
			if (ParentTrms[5].GetChild(0).name == "建筑估算" || ParentTrms[5].GetChild(0).name == "装饰估算" || ParentTrms[5].GetChild(0).name == "安装估算")
			{
				//装饰估算
				isSet.Add(true);
			}
			else
			{
				isSet.Add(false);
			}
		}

		if (ParentTrms[6].childCount > 0)
		{
			if (ParentTrms[6].GetChild(0).name == "建筑估算" || ParentTrms[6].GetChild(0).name == "装饰估算" || ParentTrms[6].GetChild(0).name == "安装估算")
			{
				//安装估算
				isSet.Add(true);
			}
			else
			{
				isSet.Add(false);
			}
		}

		if(ParentTrms[6].childCount > 0&& ParentTrms[5].childCount > 0 && ParentTrms[4].childCount > 0 && ParentTrms[3].childCount > 0 && ParentTrms[2].childCount > 0 && ParentTrms[1].childCount > 0 && ParentTrms[0].childCount > 0)
		{
			
			//提交查看是否合格
			for (int i = 0; i < isSet.Count; i++)
			{
				if (isSet[i] == false)
				{
					//减分
					Debug.Log("减分");
					break;
				}

				if (isSet[i] == true)
				{

					number++;
				}
				if (number == 7)
				{
					Debug.Log("过关");
					
					submitButton.interactable = false;
					promptContentPlane.SetActive(true);
					promptContentText.text = "投资估算正确，请开始限额分解";


				}

			}
		}
	
		
	}
	///
	public void PromptContentAffirm()
	{
	   //隐藏模块
	   //提交完成信息
	}
}
