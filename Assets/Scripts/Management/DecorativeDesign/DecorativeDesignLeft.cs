using System.Collections;
using System.Collections.Generic;
using Unit;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 装饰设计-底部-装饰设计概算
/// </summary>
public class DecorativeDesignLeft : MonoBehaviour
{
	//UnitDollarData
	 public List<Text> AllocationList = new List<Text>();//分配
	public List<Text> CollectList = new List<Text>();//汇总
	void Start()
    {
		
	}


	void Update()
    {
        
    }
	/// <summary>
	/// 设置分配金额
	/// </summary>
	public void SetAllocationMoney()
	{
		if(Unit.UnitDollarData.楼地面装饰_Amount!=null)
			AllocationList[0].text = Unit.UnitDollarData.楼地面装饰_Amount;
		if (Unit.UnitDollarData.墙柱面装饰_Amount != null)
			AllocationList[1].text = Unit.UnitDollarData.墙柱面装饰_Amount;
		if (Unit.UnitDollarData.天棚工程_Amount != null)
			AllocationList[2].text = Unit.UnitDollarData.天棚工程_Amount;
		if (Unit.UnitDollarData.油漆涂料_Amount != null)
			AllocationList[3].text = Unit.UnitDollarData.油漆涂料_Amount;
		if (Unit.UnitDollarData.其他装饰_Amount != null)
			AllocationList[4].text = Unit.UnitDollarData.其他装饰_Amount;
	}
}
