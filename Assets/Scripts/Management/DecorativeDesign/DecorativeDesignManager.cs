using System.Collections;
using System.Collections.Generic;
//using System.Windows.Forms;
using UnityEngine;
using UnityEngine.UI;
using Unit;
using Unit.DecorativeDesign;
public class DecorativeDesignManager : MonoBehaviour
{
	public static DecorativeDesignManager Instance_ { get; private set; }
	public Button Button_װ�����;

	public Button Button_ȷ��;
	public Button Button_�ύ;

	//��ȷ�Ϻ� �м� �������Ҳ���õ�
	public List<Design> ¥����_DesignsList = new List<Design>();
	public List<Design> ǽ����_DesignsList = new List<Design>();
	public List<Design> ���﹤��_DesignsList = new List<Design>();
	public List<Design> ����Ϳ��_DesignsList = new List<Design>();
	public List<Design> ����װ��_DesignsList = new List<Design>();


	private float ¥����_���ܽ�� = 0;
	private float ǽ����_���ܽ�� = 0;
	private float ���﹤��_���ܽ�� = 0;
	private float ����Ϳ��_���ܽ�� = 0;
	private float ����װ��_���ܽ�� = 0;

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
				//���ײ���ʾ���ӵ������
				DecorativeDesignLeft.Instance_.BottomButtonMoreLsit[0].gameObject.transform.GetChild(0).GetComponent<Text>().text =
				DecorativeDesignManager.Instance_.¥����_DesignsList.Count + "��";

				break;
			case "ǽ������װ������ϡ�Ļǽ":
				ǽ����_DesignsList.Add(DecorativeDesignRight.Instance_.SetDesignConvertPdf(design_));
				ǽ����_���ܽ�� = ǽ����_���ܽ�� + float.Parse(DecorativeDesignRight.Instance_.SetDesignConvertPdf(design_).Total);
				//�����ܽ�ֵ
				DecorativeDesignLeft.Instance_.CollectList[1].text = ǽ����_���ܽ��.ToString();
				Debug.Log("���ý��:" + ǽ����_���ܽ��);
				DecorativeDesignLeft.Instance_.BottomButtonMoreLsit[1].gameObject.transform.GetChild(0).GetComponent<Text>().text =
			DecorativeDesignManager.Instance_.ǽ����_DesignsList.Count + "��";

				break;
			case "����":
				���﹤��_DesignsList.Add(DecorativeDesignRight.Instance_.SetDesignConvertPdf(design_));
				���﹤��_���ܽ�� = ���﹤��_���ܽ�� + float.Parse(DecorativeDesignRight.Instance_.SetDesignConvertPdf(design_).Total);
				//�����ܽ�ֵ
				DecorativeDesignLeft.Instance_.CollectList[2].text = ���﹤��_���ܽ��.ToString();
				DecorativeDesignLeft.Instance_.BottomButtonMoreLsit[2].gameObject.transform.GetChild(0).GetComponent<Text>().text =
			DecorativeDesignManager.Instance_.���﹤��_DesignsList.Count + "��";

				break;
			case "���ᡢͿ�ϼ��Ѻ�":
				����Ϳ��_DesignsList.Add(DecorativeDesignRight.Instance_.SetDesignConvertPdf(design_));
				����Ϳ��_���ܽ�� = ����Ϳ��_���ܽ�� + float.Parse(DecorativeDesignRight.Instance_.SetDesignConvertPdf(design_).Total);
				//�����ܽ�ֵ
				DecorativeDesignLeft.Instance_.CollectList[3].text = ����Ϳ��_���ܽ��.ToString();
				DecorativeDesignLeft.Instance_.BottomButtonMoreLsit[3].gameObject.transform.GetChild(0).GetComponent<Text>().text =
			   DecorativeDesignManager.Instance_.����Ϳ��_DesignsList.Count + "��";
				break;
			case "����װ��":
				//����б�
				����װ��_DesignsList.Add(DecorativeDesignRight.Instance_.SetDesignConvertPdf(design_));
				//���ܽ��
				����װ��_���ܽ�� = ����װ��_���ܽ�� + float.Parse(DecorativeDesignRight.Instance_.SetDesignConvertPdf(design_).Total);
				//�����ܽ�ֵ
				DecorativeDesignLeft.Instance_.CollectList[4].text = ����װ��_���ܽ��.ToString();
				DecorativeDesignLeft.Instance_.BottomButtonMoreLsit[4].gameObject.transform.GetChild(0).GetComponent<Text>().text =
			DecorativeDesignManager.Instance_.����װ��_DesignsList.Count + "��";
				break;

		}
		//ɾ��������Ƶ�ͼ
		for (int i = 0; i < DecorativeDesignRight.Instance_.Content_����˵��_01ѡ������.transform.childCount; i++)
		{
			int x = i;
			GameObject child = DecorativeDesignRight.Instance_.Content_����˵��_01ѡ������.transform.GetChild(x).gameObject;
			Destroy(child);
		}
		//�ָ�ȫ����ʼ״̬
		DecorativeDesignSaveDate.InitAllProcedure();

		//ȷ�� ģ�Ͳ��ʣ�����ٵ��ģ��ʱ���ᱻ�����
		DecorativeDesignSaveDate.HighligObjectMaterial = DecorativeDesignSaveDate.HighligObject.GetComponent<MeshRenderer>().material;

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
