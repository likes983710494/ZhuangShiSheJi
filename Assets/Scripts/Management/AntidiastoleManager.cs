using System.Collections;
using System.Collections.Generic;
using UnityEditor.AnimatedValues;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// //�޶�ֽ�
/// </summary>
public class AntidiastoleManager : MonoBehaviour
{
	public Button submitButton;//�ύ
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

	void Start()
    {

		
	}

    // Update is called once per frame
    void Update()
    {
		¥����װ��_Amount.text= EachAmount(¥����װ��InputField).ToString();

	}
	/// <summary>
	/// ÿ��������ĺϼƽ��
	/// </summary>
	/// <param name="amountList">���̵�������</param>
	public float EachAmount(List<InputField> amountList)
	{
		float num = 0;
		
		for (int i = 0; i < amountList.Count; i++)
		{
			if (amountList[i].text!=null&& amountList[i].text != "")
			{
				num = num + float.Parse(amountList[i].text);
			}
			 
		}
		return num;
	}
}
