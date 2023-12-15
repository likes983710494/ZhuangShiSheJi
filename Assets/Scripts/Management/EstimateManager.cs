using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EstimateManager : MonoBehaviour
{
    public List<Transform> ParentTrms = new List<Transform>();
	public List<bool> isSet = new List<bool>();//储存每个框结果

	public Button submitButton;

	void Start()
    {
		

	}

    // Update is called once per frame
    void Update()
    {
        
    }
	public void SetParentAndTransform()
	{
		isSet.Clear();//清空
		if (ParentTrms[0].GetChild(0) != null && ParentTrms[0].GetChild(0).name == ParentTrms[0].name)
		{
			// 建筑工程费用判断是对的
			isSet.Add(true);
		}
		else
		{
			isSet.Add(false);
		}
		if(ParentTrms[1].GetChild(0) != null)
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

		if (ParentTrms[2].GetChild(0) != null)
		{
			if (ParentTrms[1].GetChild(0).name == "设备工器具购置费及安装费" || ParentTrms[1].GetChild(0).name == "流动资金" || ParentTrms[1].GetChild(0).name == "其他费用")
			{
				//流动资金 判断是对的
				isSet.Add(true);
			}
			else
			{
				isSet.Add(false);
			}
		}
		if (ParentTrms[3].GetChild(0) != null)
		{
			if (ParentTrms[1].GetChild(0).name == "设备工器具购置费及安装费" || ParentTrms[1].GetChild(0).name == "流动资金" || ParentTrms[1].GetChild(0).name == "其他费用")
			{
				//其他费用 判断是对的
				isSet.Add(true);
			}
			else
			{
				isSet.Add(false);
			}
		}
		for (int i = 0; i < isSet.Count; i++)
		{
			if (isSet[i] == false) {
				//减分
				Debug.Log("减分");
				break;
			}
				
		}
		
	}
}
