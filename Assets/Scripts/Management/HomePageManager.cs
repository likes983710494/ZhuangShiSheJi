using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
// ��ҳ 
/// </summary>

public class HomePageManager : MonoBehaviour
{

	public static HomePageManager Instance_;

	public Button Button_���������;
	public Button Button_Ͷ�ʹ���;
	public Button Button_�޶�ֽ�;
	public Button Button_װ�����;
	public Button Button_װ��Ч��չʾ;

	public Button Button_����ʵ�鱨��;
	public Button Button_�˳�;

	//��Դ������
	//public Animator Animator_��Դ����;


	// public DataDownloadManager dataDownloadManager;//���������
	// public EstimateManager estimateManager;//Ͷ�ʹ���
	// public AntidiastoleManager antidiastoleManager;//�޶�ֽ�
	// public ResultManager resultManager;//װ��Ч��

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
		Button_���������.interactable = true;
		//Button_Ͷ�ʹ���.interactable = false;
		//Button_�޶�ֽ�.interactable = false;
		//Button_װ�����.interactable = false;
		//Button_װ��Ч��չʾ.interactable = false;
		Button_װ�����.onClick.AddListener(delegate
		{
			//��ʾģ��
			if (GameObject.Find("��άģ��չʾ").transform.childCount > 0)
			{

				GameObject.Find("��άģ��չʾ").transform.GetChild(0).gameObject.SetActive(true);
				//��ʾģ��ģʽ�˵�
				DecorativeDesignModus.Instance_.ModelMenu_UI.SetActive(true);
				//�ر�ģ�ͽ�����
				DecorativeDesignModus.Instance_.ģ�͵ȴ�������.gameObject.SetActive(false);
			}



		});
		Button_�˳�.onClick.AddListener(delegate {
			Application.Quit();
		});

	}

	void Update()
	{
		//���״̬���Ǹ�ģ��״̬��Ϊ ture
		if (InvokInfoDataStorage.Instance_.infoDataStorage_.dataDownloadManagerData.isFinishDataDownload == true &&
			InvokInfoDataStorage.Instance_.infoDataStorage_.estimateManagerData.isFinishEstimate == true &&
			InvokInfoDataStorage.Instance_.infoDataStorage_.antidiastoleManagerData.isFinishAntidiastole == true &&
			InvokInfoDataStorage.Instance_.infoDataStorage_.decorativeDesignManagerData.isFinishDesign == true &&
			InvokInfoDataStorage.Instance_.infoDataStorage_.resultManagerData.isFinishResult == true
		)
		{
			//չʾ��ť
			Button_����ʵ�鱨��.gameObject.SetActive(true);
		}
	}

}
