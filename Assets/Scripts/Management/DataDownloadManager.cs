using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using System;
using UnityEngine.UI;
using Paroxe.PdfRenderer;
using Unit;
/// <summary>
// 任务设计书 
/// </summary>

public class DataDownloadManager : MonoBehaviour
{



	private string PDFName;//pdf名字
	private string ObjName;//模型名字
	private Button PDF关闭button;


	public string pdfurl = "";//pdf网络下载地址
	public string modelurl = "";//模型网络下载地址
	public Button loadPdfButton;//下载文档按钮
	public Button loadModelButton;//下载模型按钮

	public GameObject LoadPlane;//下载面板
	public Slider LoadSlider;
	public List<GameObject> openPlanes;//打开确认弹窗

	public PDFViewer PDFViewer_;//PDF预览器

	public Scrollbar VerticalScrollbar;//PDF预览器的右侧滑动条
	public Toggle 已完成Toggle;

	void Start()
	{

		PDF关闭button = PDFViewer_.transform.GetChild(1).GetComponent<Button>();

		//下载文档按钮添加事件
		loadPdfButton.onClick.AddListener(() =>
		{
			LoadPlane.SetActive(true);
			OnDownloadAssets(pdfurl, 0);
		});
		//下载模型按钮添加事件
		loadModelButton.onClick.AddListener(() =>
		{
			LoadPlane.SetActive(true);
			OnDownloadAssets(modelurl, 1);
		});
		//文档下载按钮
		openPlanes[0].transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() =>
		{


			string extension = Path.GetExtension(PDFName);

			if (PDFName != "" && PDFName != null)
			{
				if (extension == ".PDF" || extension == ".pdf")
				{
					Debug.Log("文档名字=》》" + PDFName);
					PDFViewer_.FileName = PDFName;
				}


			}

			PDFViewer_.gameObject.SetActive(true);

		});
		//文档 取消按钮
		openPlanes[0].transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() =>
		{

			PDFViewer_.gameObject.SetActive(false);
			LoadPlane.SetActive(false);
			openPlanes[0].SetActive(false);



		});
		//模型下载确认按钮
		openPlanes[1].transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() =>
		{


			LoadPlane.SetActive(false);
			openPlanes[1].SetActive(false);
			//将下载模型完成的状态储存
			Unit.UnitDollarData.isDataObj = true;

			//本地 -将下载模型完成的状态储存
			InvokInfoDataStorage.Instance_.infoDataStorage_.dataDownloadManagerData.isDataObj = Unit.UnitDollarData.isDataObj;

			if (Unit.UnitDollarData.isDataObj && Unit.UnitDollarData.isDataPDF)
			{

				//设计任务书模块的状态
				Unit.UnitDollarData.isFinishDataDownload = true;

				//本地 -将设计任务书模块的状态
				InvokInfoDataStorage.Instance_.infoDataStorage_.dataDownloadManagerData.isFinishDataDownload = Unit.UnitDollarData.isFinishDataDownload;
			}
			if (Unit.UnitDollarData.isFinishDataDownload == true)
			{
				//开启首页的投资估算按钮状态
				HomePageManager.Instance_.Button_投资估算.interactable = true;
			}

		});
		//下载报错弹窗 提示
		openPlanes[2].transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() =>
		{

			LoadPlane.SetActive(false);
			openPlanes[2].SetActive(false);
		});

		//监控pdf VerticalScrollbar 滚动条
		VerticalScrollbar.onValueChanged.AddListener((value) =>
		{
			PDFScrollbarChange(value);
		});

		//关闭pdf面板
		PDF关闭button.onClick.AddListener(() =>
		{
			PDFViewer_.gameObject.SetActive(false);
			PDFViewer_.gameObject.SetActive(false);
			LoadPlane.SetActive(false);
			openPlanes[0].SetActive(false);

		});
		已完成Toggle.onValueChanged.AddListener((value) =>
		{

			if (value == true)
			{
				//开启模型下载
				loadModelButton.interactable = true;
				//将pdf状态储存
				Unit.UnitDollarData.isDataPDF = true;
				//本地 -将pdf状态储存
				InvokInfoDataStorage.Instance_.infoDataStorage_.dataDownloadManagerData.isDataPDF = Unit.UnitDollarData.isDataPDF;
				
			}

		});


	}

	// Update is called once per frame
	void Update()
	{

	}
	/// <summary>
	///调用下载资源 到StreamingAssets文件夹
	/// </summary>
	/// <param name="url">网络资源下载地址</param>
	/// /// <param int="type">类型  0是pdf 1是模型</param>
	private void OnDownloadAssets(string url, int type)//下载资源
	{
		StopAllCoroutines();
		StartCoroutine(DownloadFormServer_IE(url, type));

	}


	private IEnumerator DownloadFormServer_IE(string url, int type)//从服务器下载资源
	{
		if (url != null && url != "")
		{

			Debug.Log("正在下载" + url);
			LoadSlider.gameObject.SetActive(true);
			UnityWebRequest request = UnityWebRequest.Get(url);

			//显示下载进度和速度
			//yield return request.SendWebRequest();
			var asyncOperation = request.SendWebRequest();
			if (request.result == UnityWebRequest.Result.ProtocolError || request.result == UnityWebRequest.Result.ConnectionError)
			{
				Debug.Log(request.responseCode);
				Debug.Log(request.error);
				yield break;
			}
			while (!asyncOperation.isDone)
			{

				//下载速度
				string netSpeedStr = "0kb";
				if (request.downloadHandler != null && request.downloadHandler.data != null)
				{
					float speed = request.downloadHandler.data.Length / 1024;
					if (speed > 1024)
					{
						speed = (speed / 1024);
						netSpeedStr = speed.ToString("f") + "mb";
					}
					else
					{
						netSpeedStr = speed + "kb";
					}
				}

				//下载进度 和速度
				LoadSlider.value = request.downloadProgress;
				LoadSlider.transform.GetChild(0).GetComponent<Text>().text = (request.downloadProgress * 100).ToString("f") + "%  ";
				Debug.Log("进度" + (request.downloadProgress * 100).ToString("f") + "%  " + netSpeedStr);
				yield return null;
			}
			if (request.result != UnityWebRequest.Result.Success)
			{
				Debug.LogError($"Error: {request.error}");
			}
			else
			{
				LoadSlider.value = 1;
				LoadSlider.transform.GetChild(0).GetComponent<Text>().text = "100.00%";

				DownloadHandler downloadHandler = request.downloadHandler;
				//using (FileStream fs = new FileStream(path, FileMode.Create))
				//{
				//	fs.Write(data, 0, data.Length);
				//}
				// 检查是否有错误发生openPlanes[2].SetActive(true);



				byte[] data = request.downloadHandler.data;
				string downloadFileName = url.Substring(url.LastIndexOf('/') + 1);
				if (type == 0)
				{
					PDFName = downloadFileName;
					Unit.UnitDollarData.PDFName = PDFName;
					string extension = Path.GetExtension(PDFName);
					if (PDFName != "" && PDFName != null)
					{
						if (extension == ".bin" || extension == ".pdf" || extension == ".txt")
						{
							openPlanes[0].SetActive(true);
							using (FileStream fs = new FileStream(Application.streamingAssetsPath + "/PDF/" + downloadFileName, FileMode.Create))
							{
								if (fs != null && data != null)
								{
									fs.Write(data, 0, data.Length);
								}
								else
								{
									Debug.Log("文件流为空");
									openPlanes[2].SetActive(true);
									openPlanes[2].transform.GetChild(0).GetComponent<Text>().text = "无法连接到目标主机，\n请检查地址是否正确";
								}

							}

						}
						else
						{
							Debug.Log("下载文件格式不正确");
							openPlanes[2].SetActive(true);
							openPlanes[2].transform.GetChild(0).GetComponent<Text>().text = "下载文件格式不正确";
						}


					}

				}
				else if (type == 1)
				{
					ObjName = downloadFileName;
					Unit.UnitDollarData.ObjName = ObjName;
					openPlanes[1].SetActive(true);
					using (FileStream fs = new FileStream(Application.streamingAssetsPath + "/Model/" + downloadFileName, FileMode.Create))
					{
						if (fs != null && data != null)
						{
							fs.Write(data, 0, data.Length);
						}
						else
						{
							Debug.Log("文件流为空");
							openPlanes[2].SetActive(true);
							openPlanes[2].transform.GetChild(0).GetComponent<Text>().text = "无法连接到目标主机，\n请检查地址是否正确";
						}


					}
				}

			}





		}
		else
		{
			Debug.Log("地址为空");
		}
	}



	/// <summary>
	/// 监控pdf VerticalScrollbar 滚动条
	/// </summary>
	/// <param name="value"></param>
	private void PDFScrollbarChange(float value)
	{
		if (VerticalScrollbar.gameObject != null)
		{
			if (VerticalScrollbar.value == 0.0f)
			{
				//显示
				已完成Toggle.gameObject.SetActive(true);
			}

		}
	}
}
