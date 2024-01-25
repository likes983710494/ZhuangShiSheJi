using System.Collections;
using System.Collections.Generic;
//using System.Windows.Forms;
using UnityEngine;
using UnityEngine.UI;
using Unit;
using Unit.DecorativeDesign;
public class DecorativeDesignManager : MonoBehaviour
{

	public Button Button_װ�����;

	public Button Button_ȷ��;
	public Button Button_�ύ;

	private List<Design> ¥����_DesignsList = new List<Design>();
	private List<Design> ǽ����_DesignsList = new List<Design>();
	private List<Design> ���﹤��_DesignsList = new List<Design>();
	private List<Design> ����Ϳ��_DesignsList = new List<Design>();
	private List<Design> ����װ��_DesignsList = new List<Design>();
	//���ܽ��
	private float ¥����_���ܽ�� = 0;
	private float ǽ����_���ܽ�� = 0;
	private float ���﹤��_���ܽ�� = 0;
	private float ����Ϳ��_���ܽ�� = 0;
	private float ����װ��_���ܽ�� = 0;
	void Start()
	{
		Button_װ�����.onClick.AddListener(() =>
		{
			DecorativeDesignLeft.Instance_.SetAllocationMoney();
			DecorativeDesignRight.Instance_.LoadGLTF.SetActive(true);
		});
		//ȷ�ϰ�ť
		Button_ȷ��.onClick.AddListener(() =>
		{
			VerifyOneProcess();
		});

		Button_�ύ.interactable = false;
		Button_�ύ.onClick.AddListener(() =>
		{
			SubmitDecorativeDesign();
		});

	}

	/// <summary>
	/// ���ȷ��ȷ�� ���һ�� 
	/// </summary>
	public void VerifyOneProcess()
	{
		//pdf����
		Design design_ = new Design();
		Debug.Log("ȷ�ϰ�ť��" + UnitDollarData.design.departmentName);
		switch (UnitDollarData.design.departmentName)
		{
			case "¥����װ��":
				¥����_DesignsList.Add(DecorativeDesignRight.Instance_.SetDesignConvertPdf(design_));
				// �ı���ܽ��
				¥����_���ܽ�� = ¥����_���ܽ�� + float.Parse(DecorativeDesignRight.Instance_.SetDesignConvertPdf(design_).Total);
				//�����ܽ�ֵ
				DecorativeDesignLeft.Instance_.CollectList[0].text = ¥����_���ܽ��.ToString();

				break;
			case "ǽ������װ������ϡ�Ļǽ":
				ǽ����_DesignsList.Add(DecorativeDesignRight.Instance_.SetDesignConvertPdf(design_));
				ǽ����_���ܽ�� = ǽ����_���ܽ�� + float.Parse(DecorativeDesignRight.Instance_.SetDesignConvertPdf(design_).Total);
				//�����ܽ�ֵ
				DecorativeDesignLeft.Instance_.CollectList[1].text = ǽ����_���ܽ��.ToString();
				Debug.Log("���ý��:" + ǽ����_���ܽ��);

				break;
			case "����":
				���﹤��_DesignsList.Add(DecorativeDesignRight.Instance_.SetDesignConvertPdf(design_));
				���﹤��_���ܽ�� = ���﹤��_���ܽ�� + float.Parse(DecorativeDesignRight.Instance_.SetDesignConvertPdf(design_).Total);
				//�����ܽ�ֵ
				DecorativeDesignLeft.Instance_.CollectList[2].text = ���﹤��_���ܽ��.ToString();

				break;
			case "���ᡢͿ�ϼ��Ѻ�":
				����Ϳ��_DesignsList.Add(DecorativeDesignRight.Instance_.SetDesignConvertPdf(design_));
				����Ϳ��_���ܽ�� = ����Ϳ��_���ܽ�� + float.Parse(DecorativeDesignRight.Instance_.SetDesignConvertPdf(design_).Total);
				//�����ܽ�ֵ
				DecorativeDesignLeft.Instance_.CollectList[3].text = ����Ϳ��_���ܽ��.ToString();
				break;
			case "����װ��":
				//����б�
				����װ��_DesignsList.Add(DecorativeDesignRight.Instance_.SetDesignConvertPdf(design_));
				//���ܽ��
				����װ��_���ܽ�� = ����װ��_���ܽ�� + float.Parse(DecorativeDesignRight.Instance_.SetDesignConvertPdf(design_).Total);
				//�����ܽ�ֵ
				DecorativeDesignLeft.Instance_.CollectList[4].text = ����װ��_���ܽ��.ToString();
				break;

		}
		Debug.Log("ǽ����_DesignsList.Count:" + ǽ����_DesignsList.Count);

		//�ָ�ȫ����ʼ״̬
		DecorativeDesignSaveDate.InitAllProcedure();

		Button_ȷ��.interactable = false;
		//�����밴ť  �ر�
		DecorativeDesignModus.Instance_.LeftMakerUnfoldButton.interactable = false;
		//�б���ӳɹ��������ύ,�ύ��ť��ʾ
		if (����װ��_DesignsList.Count > 0 || ����Ϳ��_DesignsList.Count > 0 || ���﹤��_DesignsList.Count > 0 || ǽ����_DesignsList.Count > 0 || ¥����_DesignsList.Count > 0)
		{
			Button_�ύ.interactable = true;
		}



	}

	/// <summary>
	/// �ύװ�����
	/// </summary>
	public void SubmitDecorativeDesign()
	{

		UnitDollarData.¥����_DesignsList = ¥����_DesignsList;
		UnitDollarData.ǽ����_DesignsList = ǽ����_DesignsList;
		UnitDollarData.���﹤��_DesignsList = ���﹤��_DesignsList;
		UnitDollarData.����Ϳ��_DesignsList = ����Ϳ��_DesignsList;
		UnitDollarData.����װ��_DesignsList = ����װ��_DesignsList;
		UnitDollarData.¥����_���ܽ�� = ¥����_���ܽ��;
		UnitDollarData.ǽ����_���ܽ�� = ǽ����_���ܽ��;
		UnitDollarData.���﹤��_���ܽ�� = ���﹤��_���ܽ��;
		UnitDollarData.����Ϳ��_���ܽ�� = ����Ϳ��_���ܽ��;
		UnitDollarData.����װ��_���ܽ�� = ����װ��_���ܽ��;
		//���DesignsList
		// UnitDollarData.¥����_DesignsList.Clear();
		// UnitDollarData.ǽ����_DesignsList.Clear();
		// UnitDollarData.���﹤��_DesignsList.Clear();
		// UnitDollarData.����Ϳ��_DesignsList.Clear();
		// UnitDollarData.����װ��_DesignsList.Clear();

	}




}
