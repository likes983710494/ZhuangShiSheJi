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
using Unit;

/// <summary>
/// ���ű�
/// </summary>
public class DecorativeDesignRight : MonoBehaviour
{
	public static DecorativeDesignRight Instance_ { get; private set; }
	public Dropdown DropdownBranch;//�ֲ� ����
	public Dropdown DropdownSubentry;//���� ����
	public Button ButtonModu;//����˵����ť
	public InputField InputFielArea;// ����� ������  ���ģ�ͺ��Զ���ȡ������������ʾ
	public InputField InputFielPrice;// ����� ����
	public InputField InputFielTotal;// ����� �ϼ�
	public GameObject Content_����˵��_01ѡ������;// ����˵���������ͼƬ  ���ڵ�
	public GameObject Content_����˵��_02ѡ�����;//����˵��������Ʋ���  ���ڵ�
	public GameObject Content_����˵��_03����˵��;//����˵������������ֲ���  ���ڵ�
	public UnityEngine.UI.Text Text_Ŀ¼;
	public UnityEngine.UI.Text Text_�������;
	public UnityEngine.UI.Button Button_��һ��, Button_���;//��һ������һ����ť ��ѡ������ʱ����  


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
		//���� ���ڵ��ģ�͵���״̬
		DecorativeDesignSaveDate.DropdownBranch = DropdownBranch;
		DecorativeDesignSaveDate.DropdownSubentry = DropdownSubentry;
		DecorativeDesignSaveDate.ButtonModu = ButtonModu;
		DecorativeDesignSaveDate.InputFielArea = InputFielArea;
		DecorativeDesignSaveDate.InputFielPrice = InputFielPrice;
		DecorativeDesignSaveDate.InputFielTotal = InputFielTotal;



		DropdownBranch.onValueChanged.AddListener(BranchDropdownChange);
		DropdownSubentry.onValueChanged.AddListener(delegate
		{
			SubentryDropdownChange(DropdownSubentry);
		});

		//DropdownSubentry.interactable = false;
		Button_��һ��.gameObject.SetActive(false);
		Button_���.gameObject.SetActive(false);
		ButtonModu.onClick.AddListener(() =>
		{
			DecorativeDesignModus.Instance_.LeftMakerUnfoldButton.interactable = true;
			DecorativeDesignModus.Instance_.LeftMakerPlan.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(320, 630);
			Text_Ŀ¼.text = DecorativeDesignSaveDate.departmentName + "->" + DecorativeDesignSaveDate.subentryName;
			Text_�������.text = "ѡ�񹤳����";
			Content_����˵��_01ѡ������.SetActive(true);
			Content_����˵��_02ѡ�����.SetActive(false);
			Content_����˵��_03����˵��.SetActive(false);
			//����˵����������ʱ �ᰴ�� wasdqe ���� ��������ر�ģ��ģʽ�ű�
			Camera.main.transform.GetComponent<SimpleCameraController>().enabled = false;

		});

		Button_��һ��.onClick.AddListener(() =>
		{

			SetLastButton();

		});
		Button_���.onClick.AddListener(() =>
		{
			SetFnishButton();
			//����˵����������ʱ �ᰴ������ ���������ٿ���ģ��ģʽ�ű�
			if (Camera.main.transform.GetComponent<SimpleCameraController>().enabled == true)
			{
				Camera.main.transform.GetComponent<SimpleCameraController>().enabled = true;
			}

		});

		//onEndEdit
		InputFielPrice.onValueChanged.AddListener((string Price) =>
				{
					PriceOnEndEditTotal(Price);
				});

		// ��ʼ�����Content_����˵��_02ѡ�����-- �µİ�ť
		StartCoroutine(MaterialButtonChange());

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
		if (index != 0)
		{
			DropdownSubentry.interactable = true;
		}



		switch (index)
		{
			case 0:

				DropdownSubentry.AddOptions(new List<string> { "��ѡ��" });
				break;
			case 1:
				DropdownSubentry.AddOptions(new List<string> { "��ƽ��", "�������", "�������", "�������", "������Ŀ" });
				DecorativeDesignSaveDate.departmentName = "¥����װ��";
				break;
			case 2:
				DropdownSubentry.AddOptions(new List<string> { "ǽ������Ĩ��", "�����������", "ǽ��װ����", "���ϡ�Ļǽ", "ǽ����������" });
				DecorativeDesignSaveDate.departmentName = "ǽ������װ������ϡ�Ļǽ";
				break;
			case 3:
				DropdownSubentry.AddOptions(new List<string> { "����Ĩ��", "��������", "��������", "����" });
				DecorativeDesignSaveDate.departmentName = "����";
				break;
			case 4:
				DropdownSubentry.AddOptions(new List<string> { "ľ��������", "����������", "Ĩ�������ᡢͿ��", "���㴦��", "�Ѻ�" });
				DecorativeDesignSaveDate.departmentName = "���ᡢͿ�ϼ��Ѻ�";
				break;
			case 5:
				DropdownSubentry.AddOptions(new List<string> { "���ࡢ����", "װ������", "���֡����ˡ�����", "ů����", "ԡ�����", "���ơ�����", "������", "����ľװ��", "��������" });
				DecorativeDesignSaveDate.departmentName = "����װ��";
				break;

		}
		//pdf ���ݷֲ�
		UnitDollarData.design.departmentName = DropdownBranch.options[index].text;//�ֲ�

	}
	/// <summary>
	/// ��������ѡ��
	/// </summary>
	/// <param name="index"></param>
	public void SubentryDropdownChange(Dropdown change)
	{

		ButtonModu.interactable = true;
		StopAllCoroutines();
		//��ȡ�����ļ���·�� ���ɹ�����ư�ť
		StartCoroutine(BeforloadImage(DropdownBranch.value, change.options[change.value].text));

		DecorativeDesignSaveDate.subentryName = change.options[change.value].text;
		//pdf���� ����
		UnitDollarData.design.subentryName = change.options[change.value].text;
	}
	/// <summary>
	/// ���ɹ�����ư�ť
	/// </summary>
	/// <param name="index"></param>
	/// <param name="value"></param>
	/// <returns></returns> <summary>
	/// 
	/// </summary>
	/// <param name="index"></param>
	/// <param name="value"></param>
	/// <returns></returns>
	IEnumerator BeforloadImage(int index, string value)
	{

		string path1 = Path.Combine(Application.streamingAssetsPath + "/��������/" + index + "/" + "/" + value + "/");

		if (Directory.Exists(path1))
		{

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
					//����02���ѡ����� �������
					instance_.transform.GetChild(0).GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() =>
					{
						Content_����˵��_02ѡ�����.SetActive(true);
						Content_����˵��_01ѡ������.SetActive(false);
						Button_��һ��.gameObject.SetActive(true);
						GameObject.Find("Scroll View��ͼ_����˵��").GetComponent<UnityEngine.UI.ScrollRect>().content =
						Content_����˵��_02ѡ�����.GetComponent<RectTransform>();
						//�ı��������  ѡ�񹤳����   ѡ�񹤳̲���   ���̲���˵��
						Text_�������.text = "ѡ�񹤳̲���";

						//pdf���ݣ� ��ȡͼƬ��ַ file
						UnitDollarData.design.designImagePath = file;
					});
					int dotPosition = fileName.IndexOf('.');
					string beforeDot = fileName.Substring(0, dotPosition);
					instance_.transform.GetChild(1).GetComponent<UnityEngine.UI.Text>().text = beforeDot;
				}

			}
		}
		else
		{
			Debug.Log("û���ҵ��ļ���");
		}
	}

	// ����˵�� --��һ����ť
	private void SetLastButton()
	{


		List<GameObject> Objs = new List<GameObject>() { Content_����˵��_01ѡ������, Content_����˵��_02ѡ�����, Content_����˵��_03����˵�� };

		for (int i = 0; i < Objs.Count; i++)
		{
			if (Objs[i].activeSelf == true)
			{
				if (i != 0)
				{

					Objs[i - 1].SetActive(true);
					Objs[i].SetActive(false);
					Button_��һ��.gameObject.SetActive(true);
					Button_���.gameObject.SetActive(true);
				}
				if (i == 1)
				{
					Button_��һ��.gameObject.SetActive(false);
					Button_���.gameObject.SetActive(false);
					GameObject.Find("Scroll View��ͼ_����˵��").GetComponent<UnityEngine.UI.ScrollRect>().content =
					Content_����˵��_01ѡ������.GetComponent<RectTransform>();
					Text_�������.text = "ѡ�񹤳����";
				}
				if (i == 2)
				{
					Button_��һ��.gameObject.SetActive(true);
					Button_���.gameObject.SetActive(false);
					GameObject.Find("Scroll View��ͼ_����˵��").GetComponent<UnityEngine.UI.ScrollRect>().content =
					Content_����˵��_02ѡ�����.GetComponent<RectTransform>();
					Text_�������.text = "ѡ�񹤳̲���";
				}



			}
		}

	}

	//����˵��-��ɰ�ť
	private void SetFnishButton()
	{
		Debug.Log("����˵��-��ɰ�ť");
		//����pdf ����˵�� ����˵��
		UnitDollarData.design.designDesc = Content_����˵��_03����˵��.transform.GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Text>().text;


		//����һ������ಽ�� �򿪵���
		InputFielPrice.interactable = true;
		//�ָ�����ԭ��״̬ 
		DecorativeDesignModus.Instance_.LeftMakerPlan.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 630);
		DecorativeDesignModus.Instance_.LeftMakerUnfoldButton.interactable = false;
	}

	/// <summary>
	/// ѡ��������Ĳ��ʰ�ť���÷���
	/// </summary> <summary>
	/// 
	/// </summary>
	public IEnumerator MaterialButtonChange()
	{


		/*------------*/
		string path1 = Path.Combine(Application.streamingAssetsPath + "/��������02-ѡ�����/");
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
				instance_.transform.SetParent(Content_����˵��_02ѡ�����.transform);
				instance_.transform.localScale = new Vector3(1, 1, 1);
				instance_.name = fileName;
				instance_.transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = sprite;

				int dotPosition = fileName.IndexOf('.');
				string beforeDot = fileName.Substring(0, dotPosition);
				instance_.transform.GetChild(1).GetComponent<UnityEngine.UI.Text>().text = beforeDot;
				//����02���ѡ����� �������
				instance_.transform.GetChild(0).GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() =>
				{
					Text_�������.text = "���ֲ���˵��";
					//�ı�ģ�Ͳ��� // ���ز���
					DecorativeDesignSaveDate.HighligObject.GetComponent<HighlightableObject>().enabled = false;//����ס�����޷��滻�����ȹر�
					Material material_ = new Material(Shader.Find("Unlit/Texture"));
					material_.name = beforeDot;
					material_.SetTexture("_MainTex", texture);

					// ��ȡ��ǰ�Ĳ�������
					Material[] currentMaterials = DecorativeDesignSaveDate.HighligObject.GetComponent<MeshRenderer>().materials;
					// ����һ���µĲ������飬����Ϊ��ǰ���鳤�ȼ�1
					Material[] newMaterials = new Material[currentMaterials.Length + 1];
					// ���²��ʷ�������ĵ�һ��λ��
					newMaterials[0] = material_;
					// ��ԭ���Ĳ��ʸ��Ƶ��������У��ӵڶ���λ�ÿ�ʼ
					for (int i = 0; i < currentMaterials.Length; i++)
					{
						newMaterials[i + 1] = currentMaterials[i];
					}
					// ���µĲ������鸳ֵ��meshRenderer��materials����
					DecorativeDesignSaveDate.HighligObject.GetComponent<MeshRenderer>().materials = newMaterials;
					// DecorativeDesignSaveDate.HighligObject.GetComponent<MeshRenderer>().material = material_;
					DecorativeDesignSaveDate.HighligObject.GetComponent<HighlightableObject>().enabled = true;//��������ٴ�
					DecorativeDesignSaveDate.HighligObject.GetComponent<HighlightableObject>().ConstantOn(Color.cyan);//�˷����򿪱�Ե���⣬�������Կ��Ʒ������ɫ

					//�л�03����˵�����
					Content_����˵��_02ѡ�����.SetActive(false);
					Content_����˵��_03����˵��.SetActive(true);
					Button_���.gameObject.SetActive(true);
					GameObject.Find("Scroll View��ͼ_����˵��").GetComponent<UnityEngine.UI.ScrollRect>().content =
					Content_����˵��_03����˵��.GetComponent<RectTransform>();

					//pdf���ݣ� ��ȡ���ʵ�ַ file
					//UnitDollarData.design.designMaterialPath = file;
					UnitDollarData.design.designMaterialPath = beforeDot;
				});

			}

		}







	}

	/// <summary>
	///  �����������-���ô��¼�
	///  ����*������=�ϼ�
	/// </summary> <summary>
	/// 
	/// </summary>
	public void PriceOnEndEditTotal(string Price)
	{

		//����pdf����  ����
		UnitDollarData.design.Price = Price;
		//  ����*������=�ϼ�
		if (Price != "" && InputFielArea.text != null && InputFielArea.text != "�޳ߴ���Ϣ")
		{
			InputFielTotal.interactable = true;
			InputFielTotal.text = (float.Parse(Price) * float.Parse(InputFielArea.text)).ToString();
			//����pdf����  �ϼ�
			UnitDollarData.design.Total = InputFielTotal.text;
			//����pdf����  ���
			UnitDollarData.design.Acreage = InputFielArea.text;
		}
		if (Price == "")
		{
			InputFielTotal.text = "";
		}
		//��ʵ״̬
		if (InputFielTotal.interactable == true && InputFielTotal.text != "")
		{
			GameObject.Find("----------װ�����--------").GetComponent<DecorativeDesignManager>().Button_ȷ��.interactable = true;
		}
		else
		{
			GameObject.Find("----------װ�����--------").GetComponent<DecorativeDesignManager>().Button_ȷ��.interactable = false;
		}



	}

	//��״̬ת��Ϊpdf�õ�����   ȷ�ϰ�ť����
	public Design SetDesignConvertPdf(Design design_)
	{
		design_.ElementID = DecorativeDesignSaveDate.ElementID; //����id
		design_.UniqueId = DecorativeDesignSaveDate.UniqueId; //Ψһid
		design_.departmentName = UnitDollarData.design.departmentName;//�ֲ�����
		design_.subentryName = UnitDollarData.design.subentryName;//����
		design_.designImagePath = UnitDollarData.design.designImagePath;//ͼ��ַ 
		design_.designMaterialPath = UnitDollarData.design.designMaterialPath;//���ʵ�ַ 
		design_.designDesc = UnitDollarData.design.designDesc;//����
		design_.Acreage = UnitDollarData.design.Acreage;//���
		design_.Price = UnitDollarData.design.Price;//����
		design_.Total = UnitDollarData.design.Total;//���                   // Debug.Log("ģ�͵�ַ��" + DecorativeDesignSaveDate.HighligObject);
		design_.location = DecorativeDesignSaveDate.ElementID;//ģ��if

		return design_;
	}
}
