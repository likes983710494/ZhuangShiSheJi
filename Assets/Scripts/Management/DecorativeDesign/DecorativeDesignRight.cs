using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Menu;
/// <summary>
/// 左侧脚本
/// </summary>
public class DecorativeDesignRight : MonoBehaviour
{
	public static DecorativeDesignRight Instance_ { get; private set; }
	public Dropdown DropdownBranch;//分部
	public Dropdown DropdownSubentry;//分项
	public Button  ButtonModus;//做法说明按钮


	private void Awake()
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
		DropdownBranch.onValueChanged.AddListener(BranchDropdownChange);
		DropdownSubentry.onValueChanged.AddListener(SubentryDropdownChange);

		DropdownSubentry.interactable = false;
	}


	void Update()
	{

	}
	/// <summary>
	/// 分部选项
	/// </summary>
	/// <param name="index">下拉框下标</param>
	public void BranchDropdownChange(int index)
	{
		//清空数组
		DropdownSubentry.ClearOptions();
		DropdownSubentry.interactable = true;
		switch (index)
		{
			case 0:
				DropdownSubentry.AddOptions(new List<string> { "找平层", "整体面层", "块料面层", "其他面层", "其他面层" });
				break;
			case 1:
				DropdownSubentry.AddOptions(new List<string> { "墙、柱面抹灰", "镶贴块料面层", "墙、装饰面", "隔断、幕墙", "墙、柱面吸音" });
				break;
			case 2:
				DropdownSubentry.AddOptions(new List<string> { "天棚抹灰", "天棚龙骨", "天棚饰面", "雨篷" });
				break;
			case 3:
				DropdownSubentry.AddOptions(new List<string> { "木材面油漆", "金属面油漆", "抹灰面油漆、涂料", "基层处理", "裱糊" });
				break;
			case 4:
				DropdownSubentry.AddOptions(new List<string> { "柜类、货架", "装饰线条", "扶手、栏杆、栏板", "暖气罩", "浴厕配件", "招牌、灯箱", "美术字", "零星木装饰", "工艺门扇" });
				break;

		}

	}
	/// <summary>
	/// 分项下拉选项
	/// </summary>
	/// <param name="index"></param>
	public void SubentryDropdownChange(int index)
	{
		//设置关联模型

	}
}
