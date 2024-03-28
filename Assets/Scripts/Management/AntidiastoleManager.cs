using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Unit;
/// <summary>
/// �޶�ֽ�
/// </summary>
public class AntidiastoleManager : MonoBehaviour
{

	public static AntidiastoleManager Instance_ { get; private set; }
	public Button submitButton;//�ύ
	public Button affirmButton;//ȷ�ϰ�ť
	public GameObject promptContentPlane;//��ʾ��常�ڵ�
	public Text promptContentText;//��ʾ����

	public List<InputField> ¥����װ��InputField = new List<InputField>();//¥����װ��
	public List<InputField> ǽ����װ��InputField = new List<InputField>();//ǽ����װ��
	public List<InputField> ���﹤��InputField = new List<InputField>();//���﹤��
	public List<InputField> ����Ϳ��InputField = new List<InputField>();//����Ϳ�ϼ��Ѻ�����
	public List<InputField> ����װ��InputField = new List<InputField>();//����װ�ι���


	public List<InputField> Amount_InputField = new List<InputField>();//�ڶ��� �ֲ� 


	private Antidiastole m_Antidiastole = new Antidiastole();

	private Combine combine = new Combine();

	public Text �ܽ��_Amount;//����ʦ�趨����̨����

	private float �ܽ��_Cache = 0;//���ܽ��Ƚ�
	private float proportion = 0;//cacheռ�ܽ�����
	private bool is¥����װ��, isǽ����װ��, is���﹤��, is����Ϳ��, is����װ��, isAmount;

	void Awake()
	{
		if (Instance_ == null)
		{
			Instance_ = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}
	void Start()
	{
		affirmButton.gameObject.SetActive(false);
		affirmButton.onClick.AddListener(PromptContentAffirm);
		submitButton.onClick.AddListener(SubmitEachAmount);

		for (int i = 0; i < Amount_InputField.Count; i++)
		{
			Amount_InputField[i].onValueChanged.AddListener((string value) =>
			{

				isAmount = SetIsSubmit(Amount_InputField, isAmount);
			});
		}

		for (int i = 0; i < ¥����װ��InputField.Count; i++)
		{
			¥����װ��InputField[i].onValueChanged.AddListener((string value) =>
			{

				is¥����װ�� = SetIsSubmit(¥����װ��InputField, is¥����װ��);


			});
		}
		for (int i = 0; i < ǽ����װ��InputField.Count; i++)
		{
			ǽ����װ��InputField[i].onValueChanged.AddListener((string value) =>
			{

				isǽ����װ�� = SetIsSubmit(ǽ����װ��InputField, isǽ����װ��);

			});
		}
		for (int i = 0; i < ���﹤��InputField.Count; i++)
		{
			���﹤��InputField[i].onValueChanged.AddListener((string value) =>
			{

				is���﹤�� = SetIsSubmit(���﹤��InputField, is���﹤��);

			});
		}
		for (int i = 0; i < ����Ϳ��InputField.Count; i++)
		{
			����Ϳ��InputField[i].onValueChanged.AddListener((string value) =>
			{

				is����Ϳ�� = SetIsSubmit(����Ϳ��InputField, is����Ϳ��);

			});
		}
		for (int i = 0; i < ����װ��InputField.Count; i++)
		{
			����װ��InputField[i].onValueChanged.AddListener((string value) =>
			{

				is����װ�� = SetIsSubmit(����װ��InputField, is����װ��);

			});
		}
	}


	void Update()
	{
		//
		if (is¥����װ�� == true && isǽ����װ�� == true && is���﹤�� == true && is����Ϳ�� == true && is����װ�� == true && isAmount == true)
		{
			submitButton.interactable = true;
		}
		else
		{
			submitButton.interactable = false;
		}

	}

	//��ʼ������ ����״̬
	public void InfoDataStorage_GetAntidiastoleState()
	{
		isAmount = SetIsSubmit(Amount_InputField, isAmount);
		is¥����װ�� = SetIsSubmit(¥����װ��InputField, is¥����װ��);
		isǽ����װ�� = SetIsSubmit(ǽ����װ��InputField, isǽ����װ��);
		is���﹤�� = SetIsSubmit(���﹤��InputField, is���﹤��);
		is����Ϳ�� = SetIsSubmit(����Ϳ��InputField, is����Ϳ��);
		is����װ�� = SetIsSubmit(����װ��InputField, is����װ��);
	}

	/// <summary>
	/// �Ƿ�����ύ
	/// </summary>
	/// <param name="amountList"></param>
	/// <param name="isAmount"></param>
	/// <returns></returns>
	public bool SetIsSubmit(List<InputField> amountList, bool isAmount)
	{
		int number = 0;
		//�Ƿ�ȫ�����˷����ύ
		for (int i = 0; i < amountList.Count; i++)
		{
			if (amountList[i].text.Length == 0)
			{
				//����
				Debug.Log("δ����");
				isAmount = false;
				break;
			}
			else
			{
				number++;
			}

		}

		if (number == amountList.Count)
		{
			Debug.Log("����");

			isAmount = true;


		}
		return isAmount;
	}
	/// <summary>
	/// ÿ��������ĺϼƽ��
	/// </summary>
	/// <param name="amountList">���̵�������</param>
	public float EachAmount(List<InputField> amountList)
	{
		float num = 0;//���ܵ�����

		for (int i = 0; i < amountList.Count; i++)
		{
			if (amountList[i].text != null && amountList[i].text != "")
			{
				num = num + float.Parse(amountList[i].text);
			}

		}
		return num;
	}
	/// <summary>
	/// �ύ�޶�ֽ�
	/// </summary>
	public void SubmitEachAmount()
	{

		�ܽ��_Cache = float.Parse(Amount_InputField[0].text) + float.Parse(Amount_InputField[1].text) + float.Parse(Amount_InputField[2].text) + float.Parse(Amount_InputField[3].text) + float.Parse(Amount_InputField[4].text);
		proportion = �ܽ��_Cache / float.Parse(�ܽ��_Amount.text);
		promptContentPlane.SetActive(true);
		if (proportion < 0.6)//С�ڰٷ�֮60
		{
			Debug.Log("�ֲ����̵Ļ��ܽ��̫�ͣ������µ���");
			promptContentText.text = "�ֲ����̵Ļ��ܽ��̫�ͣ������µ���";
		}
		if (proportion > 0.6 && proportion <= 1)
		{


			//�ֲ����ͷ�����Ҫ��� ������ʾ


			if (float.Parse(Amount_InputField[0].text) != EachAmount(¥����װ��InputField))
			{
				Debug.Log("�����������¥����װ�εķֲ��ͷ���Ľ�");
				promptContentText.text = "�����������¥����װ�εķֲ��ͷ���Ľ�";
				affirmButton.gameObject.SetActive(false);
			}
			else if (float.Parse(Amount_InputField[1].text) != EachAmount(ǽ����װ��InputField))
			{
				Debug.Log("�����������¥����װ�εķֲ��ͷ���Ľ�");
				promptContentText.text = "�����������¥ǽ������װ������ϡ�Ļǽ���̵ķֲ��ͷ���Ľ�";
				affirmButton.gameObject.SetActive(false);
			}
			else if (float.Parse(Amount_InputField[2].text) != EachAmount(���﹤��InputField))
			{
				Debug.Log("�����������¥����װ�εķֲ��ͷ���Ľ�");
				promptContentText.text = "�����������¥���﹤�̵ķֲ��ͷ���Ľ�";
				affirmButton.gameObject.SetActive(false);
			}
			else if (float.Parse(Amount_InputField[3].text) != EachAmount(����Ϳ��InputField))
			{
				Debug.Log("�����������¥����װ�εķֲ��ͷ���Ľ�");
				promptContentText.text = "��������������ᡢͿ�ϼ��Ѻ����̵ķֲ��ͷ���Ľ�";
				affirmButton.gameObject.SetActive(false);
			}
			else if (float.Parse(Amount_InputField[4].text) != EachAmount(����װ��InputField))
			{
				Debug.Log("�����������¥����װ�εķֲ��ͷ���Ľ�");
				promptContentText.text = "�������������װ�εķֲ��ͷ���Ľ�";
				affirmButton.gameObject.SetActive(false);
			}
			else
			{
				Debug.Log("���̽�����,����ȷ�Ͻ�����һ��");
				promptContentText.text = "���̽�����,����ȷ�Ͻ�����һ�";
				affirmButton.gameObject.SetActive(true);

			}

		}
		if (proportion > 1)
		{
			Debug.Log("�ֲ����̵Ļ��ܽ��̫�ߣ������µ���");
			promptContentText.text = "�ֲ����̵Ļ��ܽ��̫�ߣ������µ���";
		}

	}

	/// <summary>
	/// ֪ͨ��ȷ����ť
	/// </summary>
	public void PromptContentAffirm()
	{
		//����ģ��-�ѷǴ���ʵ��

		//�ύȷ����Ϣ
		//������ݴ���	�����ֲ��޶���֪ͨ��װ�����
		Unit.UnitDollarData.¥����װ��_Amount = Amount_InputField[0].text;
		Unit.UnitDollarData.ǽ����װ��_Amount = Amount_InputField[1].text;
		Unit.UnitDollarData.���﹤��_Amount = Amount_InputField[2].text;
		Unit.UnitDollarData.����Ϳ��_Amount = Amount_InputField[3].text;
		Unit.UnitDollarData.����װ��_Amount = Amount_InputField[4].text;
		Unit.UnitDollarData.AmountArray = new string[] { Unit.UnitDollarData.¥����װ��_Amount, Unit.UnitDollarData.ǽ����װ��_Amount, Unit.UnitDollarData.���﹤��_Amount,
		 Unit.UnitDollarData.����Ϳ��_Amount, Unit.UnitDollarData.����װ��_Amount };

		//�޶�ֽ� �������ݻ���
		InvokInfoDataStorage.Instance_.infoDataStorage_.antidiastoleManagerData.AmountInputField = new string[] { Unit.UnitDollarData.¥����װ��_Amount, Unit.UnitDollarData.ǽ����װ��_Amount, Unit.UnitDollarData.���﹤��_Amount,
		 Unit.UnitDollarData.����Ϳ��_Amount, Unit.UnitDollarData.����װ��_Amount };
		InvokInfoDataStorage.Instance_.infoDataStorage_.antidiastoleManagerData.¥����װ��InputField = ¥����װ��InputField.Select(inputField => inputField.text).ToList();
		InvokInfoDataStorage.Instance_.infoDataStorage_.antidiastoleManagerData.ǽ����װ��InputField = ǽ����װ��InputField.Select(inputField => inputField.text).ToList();
		InvokInfoDataStorage.Instance_.infoDataStorage_.antidiastoleManagerData.���﹤��InputField = ���﹤��InputField.Select(inputField => inputField.text).ToList();
		InvokInfoDataStorage.Instance_.infoDataStorage_.antidiastoleManagerData.����Ϳ��InputField = ����Ϳ��InputField.Select(inputField => inputField.text).ToList();
		InvokInfoDataStorage.Instance_.infoDataStorage_.antidiastoleManagerData.����װ��InputField = ����װ��InputField.Select(inputField => inputField.text).ToList();
		InvokInfoDataStorage.Instance_.infoDataStorage_.antidiastoleManagerData.isFinishAntidiastole = true;

		//�޶�ֽ���� �� ����װ�����
		Unit.UnitDollarData.isFinishAntidiastole = true;
		HomePageManager.Instance_.Button_װ�����.interactable = true;
		//��������
		Unit.UnitDollarData.CombineList.Clear();
		combine.antidiastoleList = Unit.UnitDollarData.GetntiAdiastole(¥����װ��InputField);
		Unit.UnitDollarData.CombineList.Add(combine);
		Combine combine2 = new Combine();

		combine2.antidiastoleList = Unit.UnitDollarData.GetntiAdiastole(ǽ����װ��InputField);
		Unit.UnitDollarData.CombineList.Add(combine2);
		Combine combine3 = new Combine();

		combine3.antidiastoleList = Unit.UnitDollarData.GetntiAdiastole(���﹤��InputField);
		Unit.UnitDollarData.CombineList.Add(combine3);
		Combine combine4 = new Combine();

		combine4.antidiastoleList = Unit.UnitDollarData.GetntiAdiastole(����Ϳ��InputField);
		Unit.UnitDollarData.CombineList.Add(combine4);
		Combine combine5 = new Combine();

		combine5.antidiastoleList = Unit.UnitDollarData.GetntiAdiastole(����װ��InputField);
		Unit.UnitDollarData.CombineList.Add(combine5);


	}

}
