using System.Collections;
using System.Collections.Generic;
//using System.Windows.Forms;
using UnityEngine;
using UnityEngine.UI;

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


	}

	/// <summary>
	/// ���ȷ��ȷ�� ���һ�� 
	/// </summary>
	void VerifyOneProcess()
	{

	}




}
