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
	public Dropdown DropdownBranch;//�ֲ�
	public Dropdown DropdownSubentry;//����
	void Start()
	{
		DropdownBranch.onValueChanged.AddListener(BranchDropdownChange);
		DropdownSubentry.onValueChanged.AddListener(SubentryDropdownChange);

		DropdownSubentry.interactable = false;
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
				DropdownSubentry.AddOptions(new List<string> { "��ƽ��", "�������", "�������", "�������", "������Ŀ" });
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
		//���ù���ģ��
		
	}
}
