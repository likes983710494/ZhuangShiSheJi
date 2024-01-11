using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;
using Unit;
/// <summary>
/// 投资估算
/// </summary>
public class EstimateManager : MonoBehaviour
{
	public List<Transform> ParentTrms = new List<Transform>();//填入块

	public List<Transform> SonTrms = new List<Transform>();//选择块

	public List<Vector2> SonTrmsTrmsPos = new List<Vector2>(); //选择块原位置

	public List<bool> isSet = new List<bool>();//储存每个框结果

	public Button submitButton;
	public Button affirmButton;//确认按钮
	public GameObject promptContentPlane;//提示面板父节点
	public Text promptContentText;//提示内容


	private int number = 0;
	private int DeductionNumber = 0;//减分
	void Start()
	{

		submitButton.onClick.AddListener(SetParentAndTransform);
		affirmButton.onClick.AddListener(PromptContentAffirm);
		affirmButton.gameObject.SetActive(false);

		ChangeParentPos();

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
		number = 0;
		if (ParentTrms[0].GetChild(0) != null && ParentTrms[0].GetChild(0).name == ParentTrms[0].name && ParentTrms[1].childCount > 0)
		{
			// 建筑工程费用判断是对的
			isSet.Add(true);
		}
		else
		{
			isSet.Add(false);
		}
		if (ParentTrms[1].childCount > 0)
		{
			if (ParentTrms[1].GetChild(0).name == "设备工器具购置费及安装费" || ParentTrms[1].GetChild(0).name == "流动资金" || ParentTrms[1].GetChild(0).name == "其他费用")
			{
				//设备工器具购置费及安装费 判断是对的
				isSet.Add(true);
			}
			else
			{
				isSet.Add(false);
			}
		}

		if (ParentTrms[2].childCount > 0)
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
		if (ParentTrms[3].childCount > 0)
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

		if (ParentTrms[6].childCount > 0 && ParentTrms[5].childCount > 0 && ParentTrms[4].childCount > 0 && ParentTrms[3].childCount > 0 && ParentTrms[2].childCount > 0 && ParentTrms[1].childCount > 0 && ParentTrms[0].childCount > 0)
		{

			//提交查看是否合格
			for (int i = 0; i < isSet.Count; i++)
			{
				if (isSet[i] == false)
				{
					//减分
					Debug.Log("减分");
					DeductionNumber++;
					promptContentPlane.SetActive(true);
					promptContentText.text = "答题错误！-1分，请拖至正确位置。";
					break;
				}

				if (isSet[i] == true)
				{

					number++;
				}
				if (number == 7)
				{
					Debug.Log("过关");
					//保存截图
					StopAllCoroutines();
					StartCoroutine(ScreenCaptureExample.instance_.ScreenCapture2(Application.streamingAssetsPath + "/ScreenShot/" + "/tzgs/" + "/投资估算" + ".png"));

					submitButton.interactable = false;
					promptContentPlane.SetActive(true);
					affirmButton.gameObject.SetActive(true);
					promptContentText.text = "投资估算正确，请开始限额分解";
					//保存状态
					Unit.UnitDollarData.DeductionNumber = DeductionNumber;

					Unit.UnitDollarData.EstimateNumber = 100 - DeductionNumber;
					Debug.Log(100 - DeductionNumber + "和" + Unit.UnitDollarData.EstimateNumber);
					Unit.UnitDollarData.isFinishEstimate = true;

				}

			}
		}


	}
	///
	public void PromptContentAffirm()
	{
		//隐藏模块
		//提交完成信息
		Debug.Log("q" + number);
		//解锁限额分解
		if (number == 7)
		{
			//投资估算完成 ， 开启额分解
			Unit.UnitDollarData.isFinishEstimate = true;
			HomePageManager.Instance_.Button_限额分解.interactable = true;
		}

	}
	/// <summary>
	/// 首页进入投资估算之前调用 ，选择区选择模块位置打乱
	/// </summary>
	public void ChangeParentPos()
	{
		SonTrms = Outoforder(SonTrms);
		for (int i = 0; i < SonTrms.Count; i++)
		{

			int x = i;
			//SonTrms[x].localPosition = SonTrmsTrmsPos[x];
			SonTrms[x].GetComponent<RectTransform>().anchoredPosition = SonTrmsTrmsPos[x];
		}


	}

	/// <summary>
	/// 打乱排序
	/// </summary>

	public List<T> Outoforder<T>(List<T> pos)
	{
		Random randomNum = new Random();
		int index = 0;
		T temp;
		for (int i = 0; i < pos.Count; i++)
		{
			index = randomNum.Next(0, pos.Count - 1);
			if (index != i)
			{
				temp = pos[i];
				pos[i] = pos[index];
				pos[index] = temp;
			}
		}
		return pos;
	}


}
