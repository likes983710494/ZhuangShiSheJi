using System.Collections;
using System.Collections.Generic;
//using System.Windows.Forms;
using UnityEngine;
using UnityEngine.UI;

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


	}

	/// <summary>
	/// 点击确认确认 完成一条 
	/// </summary>
	void VerifyOneProcess()
	{

	}




}
