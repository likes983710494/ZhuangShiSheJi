using System.Collections;
using System.Collections.Generic;
using Unit;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// װ�����-�ײ�-װ����Ƹ���
/// </summary>
public class DecorativeDesignLeft : MonoBehaviour
{
	//UnitDollarData
	 public List<Text> AllocationList = new List<Text>();//����
	public List<Text> CollectList = new List<Text>();//����
	void Start()
    {
		
	}


	void Update()
    {
        
    }
	/// <summary>
	/// ���÷�����
	/// </summary>
	public void SetAllocationMoney()
	{
		if(Unit.UnitDollarData.¥����װ��_Amount!=null)
			AllocationList[0].text = Unit.UnitDollarData.¥����װ��_Amount;
		if (Unit.UnitDollarData.ǽ����װ��_Amount != null)
			AllocationList[1].text = Unit.UnitDollarData.ǽ����װ��_Amount;
		if (Unit.UnitDollarData.���﹤��_Amount != null)
			AllocationList[2].text = Unit.UnitDollarData.���﹤��_Amount;
		if (Unit.UnitDollarData.����Ϳ��_Amount != null)
			AllocationList[3].text = Unit.UnitDollarData.����Ϳ��_Amount;
		if (Unit.UnitDollarData.����װ��_Amount != null)
			AllocationList[4].text = Unit.UnitDollarData.����װ��_Amount;
	}
}
