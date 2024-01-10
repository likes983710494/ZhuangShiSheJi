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

	//资源进度条
	public Animator Animator_资源进度;


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

	}

	// Update is called once per frame

}
