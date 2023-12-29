using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using System;
using UnityEngine.UI;
using Paroxe.PdfRenderer;

public class DataDownloadManager : MonoBehaviour
{
	private string pdfurl= "http://127.0.0.1/Texture.pdf";//pdf网络下载地址
	private string modelurl;//模型网络下载地址
	//private string savePath;//保存地址

	public string PDFName;//pdf名字
	public string ObjName;//模型名字

	public Button loadPdfButton;//下载文档按钮
	public Button loadModelButton;//下载模型按钮

	public GameObject LoadPlane;//下载面板
	public Slider LoadSlider;
    public  List<GameObject> openPlanes;//打开确认弹窗

	public PDFViewer PDFViewer_;//PDF预览器

	public Scrollbar VerticalScrollbar;//PDF预览器的右侧滑动条
	public Toggle 已完成Toggle;
	void Start()
    {
		//下载文档按钮添加事件
		loadPdfButton.onClick.AddListener(() =>
		{
			LoadPlane.SetActive(true);
			OnDownloadAssets(pdfurl,0);
		});
		//下载模型按钮添加事件
		loadModelButton.onClick.AddListener(() =>
		{
			LoadPlane.SetActive(true);
			OnDownloadAssets(modelurl,1);
		});
		//文档下载按钮
		openPlanes[0].transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => {

			PDFViewer_.gameObject.SetActive(true);
			if(PDFName!=""&& PDFName != null)
			{
				PDFViewer_.FileName = PDFName;
			}
			
		});
		openPlanes[0].transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => {

			PDFViewer_.gameObject.SetActive(false);
			LoadPlane.SetActive(false);
			openPlanes[0].SetActive(false);



		});
		//模型下载确认按钮
		openPlanes[1].transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => {

			
			

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
	private void OnDownloadAssets(string url,int type)//下载资源
	{
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
			request.SendWebRequest();
			if (request.result == UnityWebRequest.Result.ProtocolError || request.result == UnityWebRequest.Result.ConnectionError)
			{
				Debug.Log(request.responseCode);
				Debug.Log(request.error);
				yield break;
			}
			while (!request.isDone)
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

			DownloadHandler downloadHandler = request.downloadHandler;
			Debug.Log("下载完成");

			//using (FileStream fs = new FileStream(path, FileMode.Create))
			//{
			//	fs.Write(data, 0, data.Length);
			//}
			byte[] data = request.downloadHandler.data;
			string downloadFileName = url.Substring(url.LastIndexOf('/') + 1);
			if (type == 0)
			{
				PDFName = downloadFileName;
				openPlanes[0].SetActive(true);
				using (FileStream fs = new FileStream(Application.streamingAssetsPath + "/PDF/" + downloadFileName, FileMode.Create))
				{

					fs.Write(data, 0, data.Length);
				}
			}
			else if (type == 1)
			{
				ObjName = downloadFileName;
				openPlanes[1].SetActive(true);
				using (FileStream fs = new FileStream(Application.streamingAssetsPath + "/Model/" + downloadFileName, FileMode.Create))
				{

					fs.Write(data, 0, data.Length);
				}
			}


		}
		else
		{
			Debug.Log("地址为空");
		}
	}

}
