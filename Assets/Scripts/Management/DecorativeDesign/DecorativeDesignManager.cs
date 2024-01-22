using System.Collections;
using System.Collections.Generic;
//using System.Windows.Forms;
using UnityEngine;
using UnityEngine.UI;
using Unit;
using Unit.DecorativeDesign;
public class DecorativeDesignManager : MonoBehaviour
{
	public DecorativeDesignLeft DecorativeDesignLeft_;

	public Button Button_装饰设计;

	public Button Button_确认;
	public Button Button_提交;
	void Start()
	{
		Button_装饰设计.onClick.AddListener(() =>
		{
			DecorativeDesignLeft_.SetAllocationMoney();
			DecorativeDesignRight.Instance_.LoadGLTF.SetActive(true);
		});
		//确认按钮
		Button_确认.onClick.AddListener(() =>
		{
			VerifyOneProcess();
		});
		Button_提交.onClick.AddListener(() =>
		{
			SubmitDecorativeDesign();
		});

	}

	/// <summary>
	/// 点击确认确认 完成一条 
	/// </summary>
	public void VerifyOneProcess()
	{
		//pdf储存
		//恢复全部初始状态
		DecorativeDesignSaveDate.InitAllProcedure();
		Button_确认.interactable = false;
		//左侧抽屉按钮  关闭
		DecorativeDesignModus.Instance_.LeftMakerUnfoldButton.interactable = false;
	}

	/// <summary>
	/// 提交装饰设计
	/// </summary>
	public void SubmitDecorativeDesign()
	{

	}




}
