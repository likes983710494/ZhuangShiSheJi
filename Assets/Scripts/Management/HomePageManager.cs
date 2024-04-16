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
	public Button Button_音频;
	public Button Button_学员信息;
	public GameObject studentInfo;//学员信息面板

	public List<Button> ButtonsAddAudioList;//添加按钮音频  

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
		//添加按钮音频
		for (int i = 0; i < ButtonsAddAudioList.Count; i++)
		{
			int x = i;
			ButtonsAddAudioList[x].gameObject.AddComponent<ButtonAudioClick>();
		}


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
		Button_退出.onClick.AddListener(delegate
		{
			Application.Quit();
		});
		Button_音频.onClick.AddListener(delegate
		{
			Button_音频.transform.GetChild(1).gameObject.SetActive(!Button_音频.transform.GetChild(1).gameObject.activeSelf);
			Button_音频.transform.GetChild(2).gameObject.SetActive(!Button_音频.transform.GetChild(2).gameObject.activeSelf);
			if (Button_音频.transform.GetChild(1).gameObject.activeSelf == true)
			{
				GetComponent<AudioSource>().Play();
			}
			else
			{
				GetComponent<AudioSource>().Pause();
			}
		});
		Button_学员信息.onClick.AddListener(delegate
		{
			Button_学员信息.transform.GetChild(1).gameObject.SetActive(!Button_学员信息.transform.GetChild(1).gameObject.activeSelf);
			Button_学员信息.transform.GetChild(2).gameObject.SetActive(!Button_学员信息.transform.GetChild(2).gameObject.activeSelf);
			if (Button_学员信息.transform.GetChild(1).gameObject.activeSelf == true)
			{

				studentInfo.SetActive(false);
			}
			else
			{

				studentInfo.SetActive(true);
				studentInfo.transform.GetChild(0).GetComponent<Text>().text = IdentityInfoNet.Instance_.studentName;
				studentInfo.transform.GetChild(1).GetComponent<Text>().text = IdentityInfoNet.Instance_.studenGrade;
				studentInfo.transform.GetChild(2).GetComponent<Text>().text = IdentityInfoNet.Instance_.studenClass;
				studentInfo.transform.GetChild(3).GetComponent<Text>().text = IdentityInfoNet.Instance_.studenUsername;

			}
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
