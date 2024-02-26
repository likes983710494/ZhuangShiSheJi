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
// ��������� 
/// </summary>

public class DataDownloadManager : MonoBehaviour
{



	private string PDFName;//pdf����
	private string ObjName;//ģ������
	private Button PDF�ر�button;


	public string pdfurl = "";//pdf�������ص�ַ
	public string modelurl = "";//ģ���������ص�ַ
	public Button loadPdfButton;//�����ĵ���ť
	public Button loadModelButton;//����ģ�Ͱ�ť

	public GameObject LoadPlane;//�������
	public Slider LoadSlider;
	public List<GameObject> openPlanes;//��ȷ�ϵ���

	public PDFViewer PDFViewer_;//PDFԤ����

	public Scrollbar VerticalScrollbar;//PDFԤ�������Ҳ໬����
	public Toggle �����Toggle;

	void Start()
	{

		PDF�ر�button = PDFViewer_.transform.GetChild(1).GetComponent<Button>();

		//�����ĵ���ť����¼�
		loadPdfButton.onClick.AddListener(() =>
		{
			LoadPlane.SetActive(true);
			OnDownloadAssets(pdfurl, 0);
		});
		//����ģ�Ͱ�ť����¼�
		loadModelButton.onClick.AddListener(() =>
		{
			LoadPlane.SetActive(true);
			OnDownloadAssets(modelurl, 1);
		});
		//�ĵ����ذ�ť
		openPlanes[0].transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() =>
		{


			string extension = Path.GetExtension(PDFName);

			if (PDFName != "" && PDFName != null)
			{
				if (extension == ".PDF" || extension == ".pdf")
				{
					Debug.Log("�ĵ�����=����" + PDFName);
					PDFViewer_.FileName = PDFName;
				}


			}

			PDFViewer_.gameObject.SetActive(true);

		});
		//�ĵ� ȡ����ť
		openPlanes[0].transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() =>
		{

			PDFViewer_.gameObject.SetActive(false);
			LoadPlane.SetActive(false);
			openPlanes[0].SetActive(false);



		});
		//ģ������ȷ�ϰ�ť
		openPlanes[1].transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() =>
		{


			LoadPlane.SetActive(false);
			openPlanes[1].SetActive(false);
			//������ģ����ɵ�״̬����
			Unit.UnitDollarData.isDataObj = true;

			//���� -������ģ����ɵ�״̬����
			InvokInfoDataStorage.Instance_.infoDataStorage_.dataDownloadManagerData.isDataObj = Unit.UnitDollarData.isDataObj;

			if (Unit.UnitDollarData.isDataObj && Unit.UnitDollarData.isDataPDF)
			{

				//���������ģ���״̬
				Unit.UnitDollarData.isFinishDataDownload = true;

				//���� -�����������ģ���״̬
				InvokInfoDataStorage.Instance_.infoDataStorage_.dataDownloadManagerData.isFinishDataDownload = Unit.UnitDollarData.isFinishDataDownload;
			}
			if (Unit.UnitDollarData.isFinishDataDownload == true)
			{
				//������ҳ��Ͷ�ʹ��㰴ť״̬
				HomePageManager.Instance_.Button_Ͷ�ʹ���.interactable = true;
			}

		});
		//���ر����� ��ʾ
		openPlanes[2].transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() =>
		{

			LoadPlane.SetActive(false);
			openPlanes[2].SetActive(false);
		});

		//���pdf VerticalScrollbar ������
		VerticalScrollbar.onValueChanged.AddListener((value) =>
		{
			PDFScrollbarChange(value);
		});

		//�ر�pdf���
		PDF�ر�button.onClick.AddListener(() =>
		{
			PDFViewer_.gameObject.SetActive(false);
			PDFViewer_.gameObject.SetActive(false);
			LoadPlane.SetActive(false);
			openPlanes[0].SetActive(false);

		});
		�����Toggle.onValueChanged.AddListener((value) =>
		{

			if (value == true)
			{
				//����ģ������
				loadModelButton.interactable = true;
				//��pdf״̬����
				Unit.UnitDollarData.isDataPDF = true;
				//���� -��pdf״̬����
				InvokInfoDataStorage.Instance_.infoDataStorage_.dataDownloadManagerData.isDataPDF = Unit.UnitDollarData.isDataPDF;
				
			}

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
	private void OnDownloadAssets(string url, int type)//������Դ
	{
		StopAllCoroutines();
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
				// ����Ƿ��д�����openPlanes[2].SetActive(true);



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
									Debug.Log("�ļ���Ϊ��");
									openPlanes[2].SetActive(true);
									openPlanes[2].transform.GetChild(0).GetComponent<Text>().text = "�޷����ӵ�Ŀ��������\n�����ַ�Ƿ���ȷ";
								}

							}

						}
						else
						{
							Debug.Log("�����ļ���ʽ����ȷ");
							openPlanes[2].SetActive(true);
							openPlanes[2].transform.GetChild(0).GetComponent<Text>().text = "�����ļ���ʽ����ȷ";
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
							Debug.Log("�ļ���Ϊ��");
							openPlanes[2].SetActive(true);
							openPlanes[2].transform.GetChild(0).GetComponent<Text>().text = "�޷����ӵ�Ŀ��������\n�����ַ�Ƿ���ȷ";
						}


					}
				}

			}





		}
		else
		{
			Debug.Log("��ַΪ��");
		}
	}



	/// <summary>
	/// ���pdf VerticalScrollbar ������
	/// </summary>
	/// <param name="value"></param>
	private void PDFScrollbarChange(float value)
	{
		if (VerticalScrollbar.gameObject != null)
		{
			if (VerticalScrollbar.value == 0.0f)
			{
				//��ʾ
				�����Toggle.gameObject.SetActive(true);
			}

		}
	}
}
