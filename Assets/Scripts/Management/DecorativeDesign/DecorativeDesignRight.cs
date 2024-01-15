using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Menu;
/// <summary>
/// ���ű�
/// </summary>
public class DecorativeDesignRight : MonoBehaviour
{
	public static DecorativeDesignRight Instance_ { get; private set; }
	public Dropdown DropdownBranch;//�ֲ�
	public Dropdown DropdownSubentry;//����
	public Button ButtonModu;//����˵����ť


	private void Awake()
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
		DropdownBranch.onValueChanged.AddListener(BranchDropdownChange);
		DropdownSubentry.onValueChanged.AddListener(SubentryDropdownChange);

		DropdownSubentry.interactable = false;
		ButtonModu.onClick.AddListener(() =>
		{
			DecorativeDesignModus.Instance_.LeftMakerUnfoldButton.interactable = true;
			DecorativeDesignModus.Instance_.LeftMakerPlan.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(320, 630);
		});
	}


	void Update()
	{

	}
	/// <summary>
	/// �ֲ�ѡ��
	/// </summary>
	/// <param name="index">�������±�</param>
	public void BranchDropdownChange(int index)
	{
		//�������
		DropdownSubentry.ClearOptions();
		DropdownSubentry.interactable = true;
		switch (index)
		{
			case 0:
				DropdownSubentry.AddOptions(new List<string> { "��ƽ��", "�������", "�������", "�������", "�������" });
				break;
			case 1:
				DropdownSubentry.AddOptions(new List<string> { "ǽ������Ĩ��", "�����������", "ǽ��װ����", "���ϡ�Ļǽ", "ǽ����������" });
				break;
			case 2:
				DropdownSubentry.AddOptions(new List<string> { "����Ĩ��", "��������", "��������", "����" });
				break;
			case 3:
				DropdownSubentry.AddOptions(new List<string> { "ľ��������", "����������", "Ĩ�������ᡢͿ��", "���㴦��", "�Ѻ�" });
				break;
			case 4:
				DropdownSubentry.AddOptions(new List<string> { "���ࡢ����", "װ������", "���֡����ˡ�����", "ů����", "ԡ�����", "���ơ�����", "������", "����ľװ��", "��������" });
				break;

		}

	}
	/// <summary>
	/// ��������ѡ��
	/// </summary>
	/// <param name="index"></param>
	public void SubentryDropdownChange(int index)
	{
		ButtonModu.interactable = true;
		//�ı���չ�����
		///

	}
}
