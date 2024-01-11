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
/// 装饰效果管理
/// </summary>
public class ResultManager : MonoBehaviour
{
	public UnityEngine.UI.Button submitButton;
	public UnityEngine.UI.Button OKButton; //button_真棒
	public List<GameObject> ChangePlane = new List<GameObject>();//image切换
	public List<UnityEngine.UI.Toggle> ChangePlaneButtons = new List<UnityEngine.UI.Toggle>();//按钮
	public List<UnityEngine.UI.Button> AddImageButtons = new List<UnityEngine.UI.Button>();//选取本地图片 按钮
	public GameObject OKPlane;//--BG

	public Transform PlaneParent0, PlaneParent1, PlaneParent2, PlaneParent3, PlaneParent4;//父节点

	private List<Object> objects = new List<Object>();
	private List<Object> objects_save = new List<Object>();

	void Start()
	{

		submitButton.onClick.AddListener(SubmitLaodPlane);
		OKButton.onClick.AddListener(()=>{
			OKPlane.SetActive(false);
			//状态完成   首页显示生成报告按钮
			Unit.UnitDollarData.isFinishResult = true;
			if (Unit.UnitDollarData.isFinishResult && Unit.UnitDollarData.isFinishDesign && Unit.UnitDollarData.isFinishAntidiastole
			&& Unit.UnitDollarData.isFinishEstimate && Unit.UnitDollarData.isFinishDataDownload)
			{
				HomePageManager.Instance_.Button_装饰效果展示.gameObject.SetActive(true);
			}

			HomePageManager.Instance_.Button_装饰效果展示.gameObject.SetActive(true);
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
		ofn.filter = "图片文件(*.jpg*.png)\0*.jpg;*.png";
		ofn.file = new string(new char[256]);
		ofn.maxFile = ofn.file.Length;
		ofn.fileTitle = new string(new char[64]);
		ofn.maxFileTitle = ofn.fileTitle.Length;

		//默认路径
		string path = Application.streamingAssetsPath + "/ScreenShot" + "/decorat";
		path = path.Replace('/', '\\');
		//默认路径
		//ofn.initialDir = "G:\\wenshuxin\\test\\HuntingGame_Test\\Assets\\StreamingAssets";
		ofn.initialDir = path;
		Debug.Log(ofn.initialDir);
		ofn.title = "Open Project";
		ofn.defExt = "JPG";//显示文件的类型

		//注意 一下项目不一定要全选 但是0x00000008项不要缺少
		ofn.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000200 | 0x00000008;//OFN_EXPLORER|OFN_FILEMUSTEXIST|OFN_PATHMUSTEXIST| OFN_ALLOWMULTISELECT|OFN_NOCHANGEDIR



		//点击Windows窗口时开始加载选中的图片
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
	/// 加载本地图片
	/// </summary>
	/// <param name="path">本地文件路径</param>
	/// <returns></returns>
	IEnumerator Load(string path, int index)
	{

		//计算加载用时    
		//double startTime = (double)Time.time;

		int lastIndex = path.LastIndexOf('\\') + 1;
		string lastPart = path.Substring(lastIndex);

		//根据选择 上传类型，文件夹分类 保存路径
		string savePath = Application.streamingAssetsPath + "/ScreenShot" + "/decorat/" + index + "/" + lastPart;
		Debug.Log("选取" + path + "----------name：" + lastPart + "地址:" + savePath);
		WWW www = new WWW("file:///" + path);
		yield return www;
		if (www != null && string.IsNullOrEmpty(www.error))
		{
			//获取Texture
			Texture2D texture = www.texture;
			texture.Apply();
			//直接将选择图保存
			byte[] bytes = texture.EncodeToPNG();
			//测试地址
			//string filename = @"G:\wenshuxin\BackGround\6.jpg";
			System.IO.File.WriteAllBytes(savePath, bytes);
			//根据获取的Texture创建一个sprite
			Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
			//生成预制体
			GameObject prefab = Resources.Load<GameObject>("prefab/Resul/Image_已上传BG");
			Transform instance_Parent = GameObject.Find("Image_已上传").transform;
			GameObject instance_ = Instantiate(prefab);
			objects_save.Add(instance_);
			instance_.transform.SetParent(instance_Parent);
			instance_.transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = sprite;
			instance_.transform.GetChild(1).GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() =>
			{

				DestroyLoadPane(instance_, savePath);


			});

			//将sprite显示在图片上
			//image.sprite = sprite;
			//图片设置为原始尺寸
			//image.SetNativeSize();


			//计算加载用时
			// startTime = (double)Time.time - startTime;
			// Debug.Log("加载用时:" + startTime);
		}
	}

	/// <summary>
	/// 在选取之预览图片前，先提前加载出文件内的图片
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
		Transform instance_Parent = GameObject.Find("Image_已上传").transform;
		//选取时，先加载里面文件夹里的图片进行 展示
		string path1 = Path.Combine(Application.streamingAssetsPath + "/ScreenShot/decorat/" + x + "/");
		string[] files = Directory.GetFiles(path1);
		foreach (string file in files)
		{
			if (Path.GetExtension(file) == ".png" || Path.GetExtension(file) == ".jpg")
			{
				WWW www = new WWW("file:///" + file);
				yield return www;
				//获取Texture
				Texture2D texture = www.texture;
				//根据获取的Texture创建一个sprite
				Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
				//生成预制体
				GameObject prefab = Resources.Load<GameObject>("prefab/Resul/Image_已上传BG");
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
	/// <param name="instance_"> 删除当前物体</param>
	/// <param name="path"></param> 当前被删除的地址<summary>
	/// 
	/// </summary>

	public void DestroyLoadPane(Object instance_, string file)
	{
		Destroy(instance_);
		// string path = Path.Combine(Application.streamingAssetsPath, "你的图片名称.jpg");
		string path = Path.Combine(file);
		if (File.Exists(path))
		{
			File.Delete(path);
			Debug.Log("文件已删除");
		}
		else
		{
			Debug.Log("文件不存在");
		}
	}
	/// <summary> 
	/// 提交
	/// </summary>
	public void SubmitLaodPlane()
	{
		// 获取1234文件夹下的图片
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
							PathName = "1.楼地面装饰：";
							break;
						case 2:
							PathName = "2.墙、柱面装饰与隔断、幕墙：";
							break;
						case 3:
							PathName = "3.天棚工程：";
							break;
						case 4:
							PathName = "4.油漆、涂料及裱糊：";
							break;
						case 5:
							PathName = "5.其他装饰工程：";
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

