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
	public UnityEngine.UI.Text Text_目录;


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
		});

		//LoadGLTF.SetActive(false);
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
				DropdownSubentry.AddOptions(new List<string> { "找平层", "整体面层", "块料面层", "其他面层", "其他面层" });
				DecorativeDesignSaveDate.departmentName = "楼地面装饰";
				break;
			case 1:
				DropdownSubentry.AddOptions(new List<string> { "墙、柱面抹灰", "镶贴块料面层", "墙、装饰面", "隔断、幕墙", "墙、柱面吸音" });
				DecorativeDesignSaveDate.departmentName = "墙、柱面装饰与隔断、幕墙";
				break;
			case 2:
				DropdownSubentry.AddOptions(new List<string> { "天棚抹灰", "天棚龙骨", "天棚饰面", "雨篷" });
				DecorativeDesignSaveDate.departmentName = "天棚";
				break;
			case 3:
				DropdownSubentry.AddOptions(new List<string> { "木材面油漆", "金属面油漆", "抹灰面油漆、涂料", "基层处理", "裱糊" });
				DecorativeDesignSaveDate.departmentName = "油漆、涂料及裱糊";
				break;
			case 4:
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
				instance_.transform.GetChild(0).GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() =>
				{
					Content_做法说明_02选择材质.SetActive(true);
					Content_做法说明_01选择做法.SetActive(false);
				});
				int dotPosition = fileName.IndexOf('.');
				string beforeDot = fileName.Substring(0, dotPosition);
				instance_.transform.GetChild(1).GetComponent<UnityEngine.UI.Text>().text = beforeDot;
			}

		}

	}
}
