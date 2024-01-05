using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System.Reflection;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.UIElements;
using NPOI.SS.Formula.Functions;
/// <summary>
/// װ��Ч������
/// </summary>
public class ResultManager : MonoBehaviour
{
	public UnityEngine.UI.Button submitButton;
	public List<GameObject> ChangePlane = new List<GameObject>();//image�л�
	public List<UnityEngine.UI.Toggle> ChangePlaneButtons = new List<UnityEngine.UI.Toggle>();//��ť
	public List<UnityEngine.UI.Button> AddImageButtons = new List<UnityEngine.UI.Button>();//ѡȡ����ͼƬ ��ť
	public Transform PlaneParent0, PlaneParent1, PlaneParent2, PlaneParent3, PlaneParent4;//���ڵ�

	void Start()
	{
		submitButton.onClick.AddListener(SubmitLaodPlane);

		for (int i = 0; i < ChangePlaneButtons.Count; i++)
		{
			int x = i;

			ChangePlaneButtons[x].onValueChanged.AddListener((value) =>
			{
				ChangeLaodPlane(x);
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

		if (PlaneParent0.childCount > 2 && PlaneParent1.childCount > 2 && PlaneParent2.childCount > 2 && PlaneParent3.childCount > 2 && PlaneParent4.childCount > 2)
		{
			//
			submitButton.interactable = true;
		}

	}
	public void ChangeLaodPlane(int objNum)
	{
		for (int i = 0; i < ChangePlane.Count; i++)
		{
			int x = i;
			ChangePlane[x].SetActive(false);
		}
		ChangePlane[objNum].SetActive(true);
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
		string path = Application.streamingAssetsPath + "/ScreenShot" + "/decorate";
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
		}

	}


	/// <summary>
	/// ���ر���ͼƬ
	/// </summary>
	/// <param name="path">�����ļ�·��</param>
	/// <returns></returns>
	IEnumerator Load(string path, int index)
	{   //���������ʱ    
		//double startTime = (double)Time.time;

		int lastIndex = path.LastIndexOf('\\') + 1;
		string lastPart = path.Substring(lastIndex);
		Debug.Log("ѡȡ" + path + "----------name��" + lastPart);
		//����·��
		string savePath = Application.streamingAssetsPath + "/ScreenShot" + "/decorate" + "/" + lastPart;
		switch(index)
		{
			case 1:
				break;
		}
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
			instance_.transform.SetParent(instance_Parent);
			instance_.transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = sprite;
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
	/// ɾ��
	/// </summary>
	public void DestroyLoadPane()
	{

	}
	/// <summary>
	/// �ύ
	/// </summary>
	public void SubmitLaodPlane()
	{

	}

}

