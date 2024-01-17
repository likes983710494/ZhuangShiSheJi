using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Menu;
using Unit.DecorativeDesign;
using System.Runtime.InteropServices;
using System.Reflection;
using NPOI.SS.Formula.Functions;
using System.IO;
/// <summary>
/// ���ű�
/// </summary>
public class DecorativeDesignRight : MonoBehaviour
{
	public static DecorativeDesignRight Instance_ { get; private set; }
	public Dropdown DropdownBranch;//�ֲ� ����
	public Dropdown DropdownSubentry;//���� ����
	public Button ButtonModu;//����˵����ť
	public GameObject Content_����˵��_01ѡ������;// ����˵���������ͼƬ  ���ڵ�
	public GameObject Content_����˵��_02ѡ�����;//����˵��������Ʋ���  ���ڵ�
	public UnityEngine.UI.Text Text_Ŀ¼;


	/*************��������˵����ذ�ť*****************/
	public GameObject LoadGLTF;//����gltfģ��

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
		DropdownSubentry.onValueChanged.AddListener(delegate
		{
			SubentryDropdownChange(DropdownSubentry);
		});

		DropdownSubentry.interactable = false;
		ButtonModu.onClick.AddListener(() =>
		{
			DecorativeDesignModus.Instance_.LeftMakerUnfoldButton.interactable = true;
			DecorativeDesignModus.Instance_.LeftMakerPlan.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(320, 630);
			Text_Ŀ¼.text = DecorativeDesignSaveDate.departmentName + "->" + DecorativeDesignSaveDate.subentryName;
		});

		//LoadGLTF.SetActive(false);
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
				DecorativeDesignSaveDate.departmentName = "¥����װ��";
				break;
			case 1:
				DropdownSubentry.AddOptions(new List<string> { "ǽ������Ĩ��", "�����������", "ǽ��װ����", "���ϡ�Ļǽ", "ǽ����������" });
				DecorativeDesignSaveDate.departmentName = "ǽ������װ������ϡ�Ļǽ";
				break;
			case 2:
				DropdownSubentry.AddOptions(new List<string> { "����Ĩ��", "��������", "��������", "����" });
				DecorativeDesignSaveDate.departmentName = "����";
				break;
			case 3:
				DropdownSubentry.AddOptions(new List<string> { "ľ��������", "����������", "Ĩ�������ᡢͿ��", "���㴦��", "�Ѻ�" });
				DecorativeDesignSaveDate.departmentName = "���ᡢͿ�ϼ��Ѻ�";
				break;
			case 4:
				DropdownSubentry.AddOptions(new List<string> { "���ࡢ����", "װ������", "���֡����ˡ�����", "ů����", "ԡ�����", "���ơ�����", "������", "����ľװ��", "��������" });
				DecorativeDesignSaveDate.departmentName = "����װ��";
				break;

		}

	}
	/// <summary>
	/// ��������ѡ��
	/// </summary>
	/// <param name="index"></param>
	public void SubentryDropdownChange(Dropdown change)
	{
		ButtonModu.interactable = true;
		//��ȡ�����ļ���·�� ���ɹ�����ư�ť
		StartCoroutine(BeforloadImage(change.value, change.options[change.value].text));

		DecorativeDesignSaveDate.subentryName = change.options[change.value].text;
	}

	IEnumerator BeforloadImage(int index, string value)
	{

		string path1 = Path.Combine(Application.streamingAssetsPath + "/��������/" + index + "/" + "/" + value + "/");
		string[] files = Directory.GetFiles(path1);
		foreach (string file in files)
		{
			if (Path.GetExtension(file) == ".png" || Path.GetExtension(file) == ".jpg")
			{
				string fileName = Path.GetFileName(file);
				WWW www = new WWW("file:///" + file);
				yield return www;
				//��ȡTexture
				Texture2D texture = www.texture;
				//���ݻ�ȡ��Texture����һ��sprite
				Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
				//����Ԥ����
				GameObject prefab = Resources.Load<GameObject>("prefab/Decorative/Image_����˵��BG");
				GameObject instance_ = Instantiate(prefab);
				instance_.transform.SetParent(Content_����˵��_01ѡ������.transform);
				instance_.transform.localScale = new Vector3(1, 1, 1);
				instance_.name = fileName;
				instance_.transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = sprite;
				instance_.transform.GetChild(0).GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() =>
				{
					Content_����˵��_02ѡ�����.SetActive(true);
					Content_����˵��_01ѡ������.SetActive(false);
				});
				int dotPosition = fileName.IndexOf('.');
				string beforeDot = fileName.Substring(0, dotPosition);
				instance_.transform.GetChild(1).GetComponent<UnityEngine.UI.Text>().text = beforeDot;
			}

		}

	}
}
