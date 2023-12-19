using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using UnityEditor.AnimatedValues;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// //�޶�ֽ�
/// </summary>
public class AntidiastoleManager : MonoBehaviour
{
	public Button submitButton;//�ύ
	public Button affirmButton;//ȷ�ϰ�ť
	public GameObject promptContentPlane;//��ʾ��常�ڵ�
	public Text promptContentText;//��ʾ����

	public List<InputField> ¥����װ��InputField = new List<InputField>();//¥����װ��
	public List<InputField> ǽ����װ��InputField = new List<InputField>();//ǽ����װ��
	public List<InputField> ���﹤��InputField = new List<InputField>();//���﹤��
	public List<InputField> ����Ϳ��InputField = new List<InputField>();//����Ϳ�ϼ��Ѻ�����
	public List<InputField> ����װ��InputField = new List<InputField>();//����װ�ι���
	

	public Text ¥����װ��_Amount;
	public Text ǽ����װ��_Amount;
	public Text ���﹤��_Amount;
	public Text ����Ϳ��_Amount;
	public Text ����װ��_Amount;
	public Text �ܽ��_Amount;//����ʦ�趨����̨����

	private float �ܽ��_Cache=0;//���ܽ��Ƚ�
	private float proportion = 0;//cacheռ�ܽ�����
	private bool is¥����װ��, isǽ����װ��, is���﹤��, is����Ϳ��, is����װ��;
	void Start()
    {
		affirmButton.gameObject.SetActive(false);
		submitButton.onClick.AddListener(SubmitEachAmount);
		for (int i = 0; i < ¥����װ��InputField.Count; i++)
		{
			¥����װ��InputField[i].onValueChanged.AddListener((string value) => {
				¥����װ��_Amount.text = EachAmount(¥����װ��InputField).ToString();
				is¥����װ��= SetIsSubmit(¥����װ��InputField, is¥����װ��);
			});
		}
		for (int i = 0; i < ǽ����װ��InputField.Count; i++)
		{
			ǽ����װ��InputField[i].onValueChanged.AddListener((string value) => {
				ǽ����װ��_Amount.text = EachAmount(ǽ����װ��InputField).ToString();
				isǽ����װ�� = SetIsSubmit(ǽ����װ��InputField, isǽ����װ��);
			});
		}
		for (int i = 0; i < ���﹤��InputField.Count; i++)
		{
			���﹤��InputField[i].onValueChanged.AddListener((string value) => {
				���﹤��_Amount.text = EachAmount(���﹤��InputField).ToString();
				is���﹤�� = SetIsSubmit(���﹤��InputField, is���﹤��);
			});
		}
		for (int i = 0; i < ����Ϳ��InputField.Count; i++)
		{
			����Ϳ��InputField[i].onValueChanged.AddListener((string value) => {
				����Ϳ��_Amount.text = EachAmount(����Ϳ��InputField).ToString();
				is����Ϳ�� = SetIsSubmit(����Ϳ��InputField, is����Ϳ��);
			});
		}
		for (int i = 0; i < ����װ��InputField.Count; i++)
		{
			����װ��InputField[i].onValueChanged.AddListener((string value) => {
				����װ��_Amount.text = EachAmount(����װ��InputField).ToString();
				is����װ�� = SetIsSubmit(����װ��InputField, is����װ��);
			});
		}
	}

	// Update is called once per frame
    void Update()
    {
		//
		if (is¥����װ�� == true && isǽ����װ�� == true && is���﹤�� == true && is����Ϳ�� == true && is����װ�� == true)
		{
			submitButton.interactable = true;
		}
		else
		{
			submitButton.interactable = false;
		}

	}
	/// <summary>
	/// �Ƿ�����ύ
	/// </summary>
	/// <param name="amountList"></param>
	/// <param name="isAmount"></param>
	/// <returns></returns>
	public bool SetIsSubmit(List<InputField> amountList, bool isAmount) {
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

			if (number == amountList.Count)
			{
				Debug.Log("����");

				isAmount = true;

			
			}

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
			if (amountList[i].text!=null&& amountList[i].text != "")
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

		�ܽ��_Cache = float.Parse(¥����װ��_Amount.text) + float.Parse(ǽ����װ��_Amount.text) + float.Parse(���﹤��_Amount.text) + float.Parse(����Ϳ��_Amount.text) + float.Parse(����װ��_Amount.text);
		proportion = �ܽ��_Cache / float.Parse(�ܽ��_Amount.text);
		promptContentPlane.SetActive(true);
		if (proportion < 0.6)//С�ڰٷ�֮60
		{
			Debug.Log("�ֲ����̵Ļ��ܽ��̫�ͣ������µ���");
			promptContentText.text = "�ֲ����̵Ļ��ܽ��̫�ͣ������µ���";
		}
		if (proportion > 0.6 && proportion < 1)
		{
			Debug.Log("�ֲ����̵Ļ��ܽ�����");
			promptContentText.text = "�ֲ����̵Ļ��ܽ�����";
			affirmButton.gameObject.SetActive(true);
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
		//����ģ��
		//�ύȷ����Ϣ
	}

}
