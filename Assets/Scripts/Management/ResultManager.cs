using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System.Reflection;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.UIElements;
using NPOI.SS.Formula.Functions;
using System.IO;
/// <summary>
/// װ��Ч������
/// </summary>
public class ResultManager : MonoBehaviour
{
	public UnityEngine.UI.Button submitButton;
	public UnityEngine.UI.Button OKButton; //button_���
	public List<GameObject> ChangePlane = new List<GameObject>();//image�л�
	public List<UnityEngine.UI.Toggle> ChangePlaneButtons = new List<UnityEngine.UI.Toggle>();//��ť
	public List<UnityEngine.UI.Button> AddImageButtons = new List<UnityEngine.UI.Button>();//ѡȡ����ͼƬ ��ť
	public GameObject OKPlane;//--BG

	public Transform PlaneParent0, PlaneParent1, PlaneParent2, PlaneParent3, PlaneParent4;//���ڵ�

	private List<Object> objects = new List<Object>();
	private List<Object> objects_save = new List<Object>();

	void Start()
	{

		submitButton.onClick.AddListener(SubmitLaodPlane);
		OKButton.onClick.AddListener(()=>{
			OKPlane.SetActive(false);
			//״̬���   ��ҳ��ʾ���ɱ��水ť
			Unit.UnitDollarData.isFinishResult = true;
			if (Unit.UnitDollarData.isFinishResult && Unit.UnitDollarData.isFinishDesign && Unit.UnitDollarData.isFinishAntidiastole
			&& Unit.UnitDollarData.isFinishEstimate && Unit.UnitDollarData.isFinishDataDownload)
			{
				HomePageManager.Instance_.Button_װ��Ч��չʾ.gameObject.SetActive(true);
			}

			HomePageManager.Instance_.Button_װ��Ч��չʾ.gameObject.SetActive(true);
		});


		for (int i = 0; i < ChangePlaneButtons.Count; i++)
		{
			int x = i;

			ChangePlaneButtons[x].onValueChanged.AddListener((value) =>
			{

				if (value == true)
				{
					ChangeLaodPlane(x);

				}

			});
		}


		for (int i = 0; i < AddImageButtons.Count; i++)
		{
			int x = i;

			AddImageButtons[x].onClick.AddListener(() =>
			{
				OpenFileImage(x + 1);


			});
		}

	}

	// Update is called once per frame
	void Update()
	{


		if (int.Parse(ChangePlane[0].name.Split('/')[ChangePlane[0].name.Split('/').Length - 1]) > 2 &&
		 	int.Parse(ChangePlane[1].name.Split('/')[ChangePlane[1].name.Split('/').Length - 1]) > 2 &&
			int.Parse(ChangePlane[2].name.Split('/')[ChangePlane[2].name.Split('/').Length - 1]) > 2 &&
			int.Parse(ChangePlane[3].name.Split('/')[ChangePlane[3].name.Split('/').Length - 1]) > 2 &&
			int.Parse(ChangePlane[4].name.Split('/')[ChangePlane[4].name.Split('/').Length - 1]) > 2)
		{
			//
			submitButton.interactable = true;
		}

		//	Split('/')

	}

	public void SubmiteInteract()
	{

	}

	public void ChangeLaodPlane(int objNum)
	{
		for (int i = 0; i < ChangePlane.Count; i++)
		{
			int x = i;
			ChangePlane[x].SetActive(false);
		}
		ChangePlane[objNum].SetActive(true);

		if (ChangePlane[objNum].activeSelf == true)
		{
			StopCoroutine(BeforloadImage(objNum + 1));
			StartCoroutine(BeforloadImage(objNum + 1));
		}

	}

	public void OpenFileImage(int index)
	{

		OpenFileName ofn = new OpenFileName();
		ofn.structSize = Marshal.SizeOf(ofn);
		ofn.filter = "ͼƬ�ļ�(*.jpg*.png)\0*.jpg;*.png";
		ofn.file = new string(new char[256]);
		ofn.maxFile = ofn.file.Length;
		ofn.fileTitle = new string(new char[64]);
		ofn.maxFileTitle = ofn.fileTitle.Length;

		//Ĭ��·��
		string path = Application.streamingAssetsPath + "/ScreenShot" + "/decorat";
		path = path.Replace('/', '\\');
		//Ĭ��·��
		//ofn.initialDir = "G:\\wenshuxin\\test\\HuntingGame_Test\\Assets\\StreamingAssets";
		ofn.initialDir = path;
		Debug.Log(ofn.initialDir);
		ofn.title = "Open Project";
		ofn.defExt = "JPG";//��ʾ�ļ�������

		//ע�� һ����Ŀ��һ��Ҫȫѡ ����0x00000008�Ҫȱ��
		ofn.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000200 | 0x00000008;//OFN_EXPLORER|OFN_FILEMUSTEXIST|OFN_PATHMUSTEXIST| OFN_ALLOWMULTISELECT|OFN_NOCHANGEDIR



		//���Windows����ʱ��ʼ����ѡ�е�ͼƬ
		if (WindowDll.GetOpenFileName(ofn))
		{
			Debug.Log("Selected file with full path: " + ofn.file);
			StopAllCoroutines();
			StartCoroutine(Load(ofn.file, index));

			//StopCoroutine(BeforloadImage(index + 1));
			StartCoroutine(BeforloadImage(index));
		}

	}


	/// <summary>
	/// ���ر���ͼƬ
	/// </summary>
	/// <param name="path">�����ļ�·��</param>
	/// <returns></returns>
	IEnumerator Load(string path, int index)
	{

		//���������ʱ    
		//double startTime = (double)Time.time;

		int lastIndex = path.LastIndexOf('\\') + 1;
		string lastPart = path.Substring(lastIndex);

		//����ѡ�� �ϴ����ͣ��ļ��з��� ����·��
		string savePath = Application.streamingAssetsPath + "/ScreenShot" + "/decorat/" + index + "/" + lastPart;
		Debug.Log("ѡȡ" + path + "----------name��" + lastPart + "��ַ:" + savePath);
		WWW www = new WWW("file:///" + path);
		yield return www;
		if (www != null && string.IsNullOrEmpty(www.error))
		{
			//��ȡTexture
			Texture2D texture = www.texture;
			texture.Apply();
			//ֱ�ӽ�ѡ��ͼ����
			byte[] bytes = texture.EncodeToPNG();
			//���Ե�ַ
			//string filename = @"G:\wenshuxin\BackGround\6.jpg";
			System.IO.File.WriteAllBytes(savePath, bytes);
			//���ݻ�ȡ��Texture����һ��sprite
			Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
			//����Ԥ����
			GameObject prefab = Resources.Load<GameObject>("prefab/Resul/Image_���ϴ�BG");
			Transform instance_Parent = GameObject.Find("Image_���ϴ�").transform;
			GameObject instance_ = Instantiate(prefab);
			objects_save.Add(instance_);
			instance_.transform.SetParent(instance_Parent);
			instance_.transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = sprite;
			instance_.transform.GetChild(1).GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() =>
			{

				DestroyLoadPane(instance_, savePath);


			});

			//��sprite��ʾ��ͼƬ��
			//image.sprite = sprite;
			//ͼƬ����Ϊԭʼ�ߴ�
			//image.SetNativeSize();


			//���������ʱ
			// startTime = (double)Time.time - startTime;
			// Debug.Log("������ʱ:" + startTime);
		}
	}

	/// <summary>
	/// ��ѡȡ֮Ԥ��ͼƬǰ������ǰ���س��ļ��ڵ�ͼƬ
	/// </summary>
	IEnumerator BeforloadImage(int x)
	{
		for (int i = 0; i < objects.Count; i++)
		{
			Destroy(objects[i]);
		}
		for (int i = 0; i < objects_save.Count; i++)
		{
			Destroy(objects_save[i]);
		}
		objects.Clear();
		objects_save.Clear();
		Transform instance_Parent = GameObject.Find("Image_���ϴ�").transform;
		//ѡȡʱ���ȼ��������ļ������ͼƬ���� չʾ
		string path1 = Path.Combine(Application.streamingAssetsPath + "/ScreenShot/decorat/" + x + "/");
		string[] files = Directory.GetFiles(path1);
		foreach (string file in files)
		{
			if (Path.GetExtension(file) == ".png" || Path.GetExtension(file) == ".jpg")
			{
				WWW www = new WWW("file:///" + file);
				yield return www;
				//��ȡTexture
				Texture2D texture = www.texture;
				//���ݻ�ȡ��Texture����һ��sprite
				Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
				//����Ԥ����
				GameObject prefab = Resources.Load<GameObject>("prefab/Resul/Image_���ϴ�BG");
				GameObject instance_ = Instantiate(prefab);
				objects.Add(instance_);
				instance_.transform.SetParent(instance_Parent);
				instance_.transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = sprite;

				instance_.transform.GetChild(1).GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() =>
				{
					DestroyLoadPane(instance_, file);

				});

			}

		}

		instance_Parent.parent.parent.parent.name = instance_Parent.parent.parent.parent.name + instance_Parent.childCount;
		string[] parts = instance_Parent.parent.parent.parent.name.Split('/');
		string lastPart = parts[parts.Length - 1];
		string afterPart = parts[parts.Length - 2];
		instance_Parent.parent.parent.parent.name = afterPart + "/" + instance_Parent.childCount;
	}
	/// <summary>
	/// 
	/// </summary>
	/// <param name="instance_"> ɾ����ǰ����</param>
	/// <param name="path"></param> ��ǰ��ɾ���ĵ�ַ<summary>
	/// 
	/// </summary>

	public void DestroyLoadPane(Object instance_, string file)
	{
		Destroy(instance_);
		// string path = Path.Combine(Application.streamingAssetsPath, "���ͼƬ����.jpg");
		string path = Path.Combine(file);
		if (File.Exists(path))
		{
			File.Delete(path);
			Debug.Log("�ļ���ɾ��");
		}
		else
		{
			Debug.Log("�ļ�������");
		}
	}
	/// <summary> 
	/// �ύ
	/// </summary>
	public void SubmitLaodPlane()
	{
		// ��ȡ1234�ļ����µ�ͼƬ
		for (int i = 1; i < 6; i++)
		{
			ImagePath imagePath_ = new ImagePath();
			string path = Path.Combine(Application.streamingAssetsPath + "/ScreenShot/decorat/" + i.ToString() + "/");
			string[] files = Directory.GetFiles(path);
			foreach (string file in files)
			{
				if (Path.GetExtension(file) == ".png" || Path.GetExtension(file) == ".jpg")
				{
					string PathName = "";
					switch (i)
					{
						case 1:
							PathName = "1.¥����װ�Σ�";
							break;
						case 2:
							PathName = "2.ǽ������װ������ϡ�Ļǽ��";
							break;
						case 3:
							PathName = "3.���﹤�̣�";
							break;
						case 4:
							PathName = "4.���ᡢͿ�ϼ��Ѻ���";
							break;
						case 5:
							PathName = "5.����װ�ι��̣�";
							break;
					}
					imagePath_.name = PathName;
					imagePath_.imagePathList.Add(file);

				}

			}
			Unit.UnitDollarData.ImagePathList.Add(imagePath_);
		}

		

	}



}

