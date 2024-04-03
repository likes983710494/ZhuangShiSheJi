using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
// 首页 
/// </summary>

public class HomePageManager : MonoBehaviour
{

	public static HomePageManager Instance_;

	public Button Button_设计任务书;
	public Button Button_投资估算;
	public Button Button_限额分解;
	public Button Button_装饰设计;
	public Button Button_装饰效果展示;

	public Button Button_生成实验报告;
	public Button Button_退出;

	//资源进度条
	//public Animator Animator_资源进度;


	// public DataDownloadManager dataDownloadManager;//任务设计书
	// public EstimateManager estimateManager;//投资估算
	// public AntidiastoleManager antidiastoleManager;//限额分解
	// public ResultManager resultManager;//装饰效果

	private void Awake()
	{
		if (Instance_ == null)
		{
			Instance_ = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}
	void Start()
	{
		Button_设计任务书.interactable = true;
		//Button_投资估算.interactable = false;
		//Button_限额分解.interactable = false;
		//Button_装饰设计.interactable = false;
		//Button_装饰效果展示.interactable = false;
		Button_装饰设计.onClick.AddListener(delegate
		{
			//显示模型
			if (GameObject.Find("三维模型展示").transform.childCount > 0)
			{

				GameObject.Find("三维模型展示").transform.GetChild(0).gameObject.SetActive(true);
				//显示模型模式菜单
				DecorativeDesignModus.Instance_.ModelMenu_UI.SetActive(true);
				//关闭模型进度条
				DecorativeDesignModus.Instance_.模型等待进度条.gameObject.SetActive(false);
			}



		});
		Button_退出.onClick.AddListener(delegate {
			Application.Quit();
		});

	}

	void Update()
	{
		//如果状态都是各模块状态都为 ture
		if (InvokInfoDataStorage.Instance_.infoDataStorage_.dataDownloadManagerData.isFinishDataDownload == true &&
			InvokInfoDataStorage.Instance_.infoDataStorage_.estimateManagerData.isFinishEstimate == true &&
			InvokInfoDataStorage.Instance_.infoDataStorage_.antidiastoleManagerData.isFinishAntidiastole == true &&
			InvokInfoDataStorage.Instance_.infoDataStorage_.decorativeDesignManagerData.isFinishDesign == true &&
			InvokInfoDataStorage.Instance_.infoDataStorage_.resultManagerData.isFinishResult == true
		)
		{
			//展示按钮
			Button_生成实验报告.gameObject.SetActive(true);
		}
	}

}
