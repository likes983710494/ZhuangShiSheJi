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
	private string pdfurl= "http://127.0.0.1/Texture.pdf";//pdf�������ص�ַ
	private string modelurl;//ģ���������ص�ַ
	//private string savePath;//�����ַ

	public string PDFName;//pdf����
	public string ObjName;//ģ������

	public Button loadPdfButton;//�����ĵ���ť
	public Button loadModelButton;//����ģ�Ͱ�ť

	public GameObject LoadPlane;//�������
	public Slider LoadSlider;
    public  List<GameObject> openPlanes;//��ȷ�ϵ���

	public PDFViewer PDFViewer_;//PDFԤ����

	public Scrollbar VerticalScrollbar;//PDFԤ�������Ҳ໬����
	public Toggle �����Toggle;
	void Start()
    {
		//�����ĵ���ť����¼�
		loadPdfButton.onClick.AddListener(() =>
		{
			LoadPlane.SetActive(true);
			OnDownloadAssets(pdfurl,0);
		});
		//����ģ�Ͱ�ť����¼�
		loadModelButton.onClick.AddListener(() =>
		{
			LoadPlane.SetActive(true);
			OnDownloadAssets(modelurl,1);
		});
		//�ĵ����ذ�ť
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
		//ģ������ȷ�ϰ�ť
		openPlanes[1].transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => {

			
			

		});

	}

    // Update is called once per frame
    void Update()
    {
        
    }
	/// <summary>
	///����������Դ ��StreamingAssets�ļ���
	/// </summary>
	/// <param name="url">������Դ���ص�ַ</param>
	/// /// <param int="type">����  0��pdf 1��ģ��</param>
	private void OnDownloadAssets(string url,int type)//������Դ
	{
		StartCoroutine(DownloadFormServer_IE(url, type));
		
	}


	private IEnumerator DownloadFormServer_IE(string url, int type)//�ӷ�����������Դ
	{
		if (url != null && url != "")
		{

			Debug.Log("��������" + url);
			LoadSlider.gameObject.SetActive(true);
			UnityWebRequest request = UnityWebRequest.Get(url);

			//��ʾ���ؽ��Ⱥ��ٶ�
			request.SendWebRequest();
			if (request.result == UnityWebRequest.Result.ProtocolError || request.result == UnityWebRequest.Result.ConnectionError)
			{
				Debug.Log(request.responseCode);
				Debug.Log(request.error);
				yield break;
			}
			while (!request.isDone)
			{
				//�����ٶ�
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

				//���ؽ��� ���ٶ�
				LoadSlider.value = request.downloadProgress;
				LoadSlider.transform.GetChild(0).GetComponent<Text>().text = (request.downloadProgress * 100).ToString("f") + "%  ";
				Debug.Log("����" + (request.downloadProgress * 100).ToString("f") + "%  " + netSpeedStr);
				yield return null;
			}

			DownloadHandler downloadHandler = request.downloadHandler;
			Debug.Log("�������");

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
			Debug.Log("��ַΪ��");
		}
	}

}
