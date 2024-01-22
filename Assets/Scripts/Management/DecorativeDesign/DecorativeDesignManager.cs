using System.Collections;
using System.Collections.Generic;
//using System.Windows.Forms;
using UnityEngine;
using UnityEngine.UI;
using Unit;
using Unit.DecorativeDesign;
public class DecorativeDesignManager : MonoBehaviour
{
	public DecorativeDesignLeft DecorativeDesignLeft_;

	public Button Button_װ�����;

	public Button Button_ȷ��;
	public Button Button_�ύ;
	void Start()
	{
		Button_װ�����.onClick.AddListener(() =>
		{
			DecorativeDesignLeft_.SetAllocationMoney();
			DecorativeDesignRight.Instance_.LoadGLTF.SetActive(true);
		});
		//ȷ�ϰ�ť
		Button_ȷ��.onClick.AddListener(() =>
		{
			VerifyOneProcess();
		});
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
		//�ָ�ȫ����ʼ״̬
		DecorativeDesignSaveDate.InitAllProcedure();
		Button_ȷ��.interactable = false;
		//�����밴ť  �ر�
		DecorativeDesignModus.Instance_.LeftMakerUnfoldButton.interactable = false;
	}

	/// <summary>
	/// �ύװ�����
	/// </summary>
	public void SubmitDecorativeDesign()
	{

	}




}
