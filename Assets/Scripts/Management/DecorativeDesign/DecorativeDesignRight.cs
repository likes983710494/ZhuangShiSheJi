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
/// 左侧脚本
/// </summary>
public class DecorativeDesignRight : MonoBehaviour
{
	public static DecorativeDesignRight Instance_ { get; private set; }
	public Dropdown DropdownBranch;//分部 数据
	public Dropdown DropdownSubentry;//分项 数据
	public Button ButtonModu;//做法说明按钮
	public GameObject Content_做法说明_01选择做法;// 做法说明工程设计图片  父节点
	public GameObject Content_做法说明_02选择材质;//做法说明工程设计材质  父节点
	public GameObject Content_做法说明_03补充说明;//做法说明工程设计文字补充  父节点
	public UnityEngine.UI.Text Text_目录;
	public UnityEngine.UI.Text Text_左侧名称;
	public UnityEngine.UI.Button Button_上一步, Button_完成;//上一步和下一步按钮 当选择做法时隐藏  


	/*************以上做法说明相关按钮*****************/
	public GameObject LoadGLTF;//加载gltf模型

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
			Text_目录.text = DecorativeDesignSaveDate.departmentName + "->" + DecorativeDesignSaveDate.subentryName;
			Text_左侧名称.text = "选择工程设计";
		});
		Button_上一步.gameObject.SetActive(false);
		Button_完成.gameObject.SetActive(false);
		Button_上一步.onClick.AddListener(() =>
		{

			SetLastButton();

		});
		LoadGLTF.SetActive(false);
	}


	void Update()
	{

	}
	/// <summary>
	/// 分部选项
	/// </summary>
	/// <param name="index">下拉框下标</param>
	public void BranchDropdownChange(int index)
	{
		//清空数组
		DropdownSubentry.ClearOptions();
		DropdownSubentry.interactable = true;
		switch (index)
		{
			case 0:
				Debug.Log("未选择");
				break;
			case 1:
				DropdownSubentry.AddOptions(new List<string> { "找平层", "整体面层", "块料面层", "其他面层", "其他面层" });
				DecorativeDesignSaveDate.departmentName = "楼地面装饰";
				break;
			case 2:
				DropdownSubentry.AddOptions(new List<string> { "墙、柱面抹灰", "镶贴块料面层", "墙、装饰面", "隔断、幕墙", "墙、柱面吸音" });
				DecorativeDesignSaveDate.departmentName = "墙、柱面装饰与隔断、幕墙";
				break;
			case 3:
				DropdownSubentry.AddOptions(new List<string> { "天棚抹灰", "天棚龙骨", "天棚饰面", "雨篷" });
				DecorativeDesignSaveDate.departmentName = "天棚";
				break;
			case 4:
				DropdownSubentry.AddOptions(new List<string> { "木材面油漆", "金属面油漆", "抹灰面油漆、涂料", "基层处理", "裱糊" });
				DecorativeDesignSaveDate.departmentName = "油漆、涂料及裱糊";
				break;
			case 5:
				DropdownSubentry.AddOptions(new List<string> { "柜类、货架", "装饰线条", "扶手、栏杆、栏板", "暖气罩", "浴厕配件", "招牌、灯箱", "美术字", "零星木装饰", "工艺门扇" });
				DecorativeDesignSaveDate.departmentName = "其他装饰";
				break;

		}

	}
	/// <summary>
	/// 分项下拉选项
	/// </summary>
	/// <param name="index"></param>
	public void SubentryDropdownChange(Dropdown change)
	{
		ButtonModu.interactable = true;
		//获取分项文件夹路径 生成工程设计按钮
		StartCoroutine(BeforloadImage(change.value, change.options[change.value].text));

		DecorativeDesignSaveDate.subentryName = change.options[change.value].text;
	}
	/// <summary>
	/// 生成工程设计按钮
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

		string path1 = Path.Combine(Application.streamingAssetsPath + "/做法分类/" + index + "/" + "/" + value + "/");
		string[] files = Directory.GetFiles(path1);
		foreach (string file in files)
		{
			if (Path.GetExtension(file) == ".png" || Path.GetExtension(file) == ".jpg")
			{
				string fileName = Path.GetFileName(file);
				WWW www = new WWW("file:///" + file);
				yield return www;
				//获取Texture
				Texture2D texture = www.texture;
				//根据获取的Texture创建一个sprite
				Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
				//生成预制体
				GameObject prefab = Resources.Load<GameObject>("prefab/Decorative/Image_做法说明BG");
				GameObject instance_ = Instantiate(prefab);
				instance_.transform.SetParent(Content_做法说明_01选择做法.transform);
				instance_.transform.localScale = new Vector3(1, 1, 1);
				instance_.name = fileName;
				instance_.transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = sprite;
				//进行02面板选择材质 功能添加
				instance_.transform.GetChild(0).GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() =>
				{
					Content_做法说明_02选择材质.SetActive(true);
					Content_做法说明_01选择做法.SetActive(false);
					Button_上一步.gameObject.SetActive(true);
					GameObject.Find("Scroll View视图_做法说明").GetComponent<UnityEngine.UI.ScrollRect>().content =
					Content_做法说明_02选择材质.GetComponent<RectTransform>();
					//改变左侧名称  选择工程设计   选择工程材质   工程补充说明
					Text_左侧名称.text = "选择工程材质";
				});
				int dotPosition = fileName.IndexOf('.');
				string beforeDot = fileName.Substring(0, dotPosition);
				instance_.transform.GetChild(1).GetComponent<UnityEngine.UI.Text>().text = beforeDot;
			}

		}

	}

	// 做法说明 --上一步按钮
	private void SetLastButton()
	{


		List<GameObject> Objs = new List<GameObject>() { Content_做法说明_01选择做法, Content_做法说明_02选择材质, Content_做法说明_03补充说明 };

		for (int i = 0; i < Objs.Count; i++)
		{
			if (Objs[i].activeSelf == true)
			{
				if (i != 0)
				{
					Debug.Log(Objs[i - 1].name);
					Objs[i - 1].SetActive(true);
					Objs[i].SetActive(false);
					Button_上一步.gameObject.SetActive(true);
					Button_完成.gameObject.SetActive(true);
				}
				if (i == 1)
				{
					Button_上一步.gameObject.SetActive(false);
					Button_完成.gameObject.SetActive(false);
					GameObject.Find("Scroll View视图_做法说明").GetComponent<UnityEngine.UI.ScrollRect>().content =
					Content_做法说明_01选择做法 .GetComponent<RectTransform>();
				}
				if (i == 2)
				{
					Button_上一步.gameObject.SetActive(true);
					Button_完成.gameObject.SetActive(false);
					GameObject.Find("Scroll View视图_做法说明").GetComponent<UnityEngine.UI.ScrollRect>().content =
					Content_做法说明_02选择材质.GetComponent<RectTransform>();
				}



			}
		}

	}

	//做法说明-完成按钮
	private void SetFnishButton()
	{
		//保存pdf数据
		//打开下一个的左侧步骤

	}


	public void MaterialButtonChange()
	{
		Text_左侧名称.text = "文字补充说明";
		//改变模型材质 
		//DecorativeDesignSaveDate.HighligObject.GetComponent<MeshRenderer>().material.color = newColor;
		//切换03补充说明面板
		Content_做法说明_02选择材质.SetActive(false);
		Content_做法说明_03补充说明.SetActive(true);
		Button_完成.gameObject.SetActive(true);
		GameObject.Find("Scroll View视图_做法说明").GetComponent<UnityEngine.UI.ScrollRect>().content =
					Content_做法说明_03补充说明.GetComponent<RectTransform>();

	}
}
