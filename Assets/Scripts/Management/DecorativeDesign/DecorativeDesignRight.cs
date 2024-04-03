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
/// 左侧脚本
/// </summary>
public class DecorativeDesignRight : MonoBehaviour
{
	public static DecorativeDesignRight Instance_ { get; private set; }
	public Dropdown DropdownBranch;//分部 数据
	public Dropdown DropdownSubentry;//分项 数据
	public Button ButtonModu;//做法说明按钮
	public InputField InputFielArea;// 输入框 工程量  点击模型后自动获取到工程量并显示
	public InputField InputFielPrice;// 输入框 单价
	public InputField InputFielTotal;// 输入框 合价
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
		//储存 用于点击模型调用状态
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
		Button_上一步.gameObject.SetActive(false);
		Button_完成.gameObject.SetActive(false);
		ButtonModu.onClick.AddListener(() =>
		{
			DecorativeDesignModus.Instance_.LeftMakerUnfoldButton.interactable = true;
			DecorativeDesignModus.Instance_.LeftMakerPlan.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(320, 630);
			Text_目录.text = DecorativeDesignSaveDate.departmentName + "->" + DecorativeDesignSaveDate.subentryName;
			Text_左侧名称.text = "选择工程设计";
			Content_做法说明_01选择做法.SetActive(true);
			Content_做法说明_02选择材质.SetActive(false);
			Content_做法说明_03补充说明.SetActive(false);
			//做法说明输入文字时 会按到 wasdqe 按键 所以这里关闭模型模式脚本
			Camera.main.transform.GetComponent<SimpleCameraController>().enabled = false;

		});

		Button_上一步.onClick.AddListener(() =>
		{

			SetLastButton();

		});
		Button_完成.onClick.AddListener(() =>
		{
			SetFnishButton();
			//做法说明输入文字时 会按到按键 所以这里再开启模型模式脚本
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

		// 初始化面板Content_做法说明_02选择材质-- 下的按钮
		StartCoroutine(MaterialButtonChange());

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
		if (index != 0)
		{
			DropdownSubentry.interactable = true;
		}



		switch (index)
		{
			case 0:

				DropdownSubentry.AddOptions(new List<string> { "请选择" });
				break;
			case 1:
				DropdownSubentry.AddOptions(new List<string> { "找平层", "整体面层", "块料面层", "其他面层", "其他项目" });
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
		//pdf 数据分部
		UnitDollarData.design.departmentName = DropdownBranch.options[index].text;//分部

	}
	/// <summary>
	/// 分项下拉选项
	/// </summary>
	/// <param name="index"></param>
	public void SubentryDropdownChange(Dropdown change)
	{

		ButtonModu.interactable = true;
		StopAllCoroutines();
		//获取分项文件夹路径 生成工程设计按钮
		StartCoroutine(BeforloadImage(DropdownBranch.value, change.options[change.value].text));

		DecorativeDesignSaveDate.subentryName = change.options[change.value].text;
		//pdf数据 分项
		UnitDollarData.design.subentryName = change.options[change.value].text;
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

						//pdf数据： 获取图片地址 file
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
			Debug.Log("没有找到文件夹");
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
					Content_做法说明_01选择做法.GetComponent<RectTransform>();
					Text_左侧名称.text = "选择工程设计";
				}
				if (i == 2)
				{
					Button_上一步.gameObject.SetActive(true);
					Button_完成.gameObject.SetActive(false);
					GameObject.Find("Scroll View视图_做法说明").GetComponent<UnityEngine.UI.ScrollRect>().content =
					Content_做法说明_02选择材质.GetComponent<RectTransform>();
					Text_左侧名称.text = "选择工程材质";
				}



			}
		}

	}

	//做法说明-完成按钮
	private void SetFnishButton()
	{
		Debug.Log("做法说明-完成按钮");
		//保存pdf 做法说明 补充说明
		UnitDollarData.design.designDesc = Content_做法说明_03补充说明.transform.GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Text>().text;


		//打开下一个的左侧步骤 打开单价
		InputFielPrice.interactable = true;
		//恢复弹窗原来状态 
		DecorativeDesignModus.Instance_.LeftMakerPlan.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 630);
		DecorativeDesignModus.Instance_.LeftMakerUnfoldButton.interactable = false;
	}

	/// <summary>
	/// 选择材质面板的材质按钮调用方法
	/// </summary> <summary>
	/// 
	/// </summary>
	public IEnumerator MaterialButtonChange()
	{


		/*------------*/
		string path1 = Path.Combine(Application.streamingAssetsPath + "/做法分类02-选择材质/");
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
				instance_.transform.SetParent(Content_做法说明_02选择材质.transform);
				instance_.transform.localScale = new Vector3(1, 1, 1);
				instance_.name = fileName;
				instance_.transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = sprite;

				int dotPosition = fileName.IndexOf('.');
				string beforeDot = fileName.Substring(0, dotPosition);
				instance_.transform.GetChild(1).GetComponent<UnityEngine.UI.Text>().text = beforeDot;
				//进行02面板选择材质 功能添加
				instance_.transform.GetChild(0).GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() =>
				{
					Text_左侧名称.text = "文字补充说明";
					//改变模型材质 // 加载材质
					DecorativeDesignSaveDate.HighligObject.GetComponent<HighlightableObject>().enabled = false;//会锁住材质无法替换所以先关闭
					Material material_ = new Material(Shader.Find("Unlit/Texture"));
					material_.name = beforeDot;
					material_.SetTexture("_MainTex", texture);

					// 获取当前的材质数组
					Material[] currentMaterials = DecorativeDesignSaveDate.HighligObject.GetComponent<MeshRenderer>().materials;
					// 创建一个新的材质数组，长度为当前数组长度加1
					Material[] newMaterials = new Material[currentMaterials.Length + 1];
					// 将新材质放在数组的第一个位置
					newMaterials[0] = material_;
					// 将原来的材质复制到新数组中，从第二个位置开始
					for (int i = 0; i < currentMaterials.Length; i++)
					{
						newMaterials[i + 1] = currentMaterials[i];
					}
					// 将新的材质数组赋值给meshRenderer的materials属性
					DecorativeDesignSaveDate.HighligObject.GetComponent<MeshRenderer>().materials = newMaterials;
					// DecorativeDesignSaveDate.HighligObject.GetComponent<MeshRenderer>().material = material_;
					DecorativeDesignSaveDate.HighligObject.GetComponent<HighlightableObject>().enabled = true;//换完材质再打开
					DecorativeDesignSaveDate.HighligObject.GetComponent<HighlightableObject>().ConstantOn(Color.cyan);//此方法打开边缘发光，参数可以控制发光的颜色

					//切换03补充说明面板
					Content_做法说明_02选择材质.SetActive(false);
					Content_做法说明_03补充说明.SetActive(true);
					Button_完成.gameObject.SetActive(true);
					GameObject.Find("Scroll View视图_做法说明").GetComponent<UnityEngine.UI.ScrollRect>().content =
					Content_做法说明_03补充说明.GetComponent<RectTransform>();

					//pdf数据： 获取材质地址 file
					//UnitDollarData.design.designMaterialPath = file;
					UnitDollarData.design.designMaterialPath = beforeDot;
				});

			}

		}







	}

	/// <summary>
	///  单价输入完后-调用此事件
	///  单价*工程量=合价
	/// </summary> <summary>
	/// 
	/// </summary>
	public void PriceOnEndEditTotal(string Price)
	{

		//保存pdf数据  单价
		UnitDollarData.design.Price = Price;
		//  单价*工程量=合价
		if (Price != "" && InputFielArea.text != null && InputFielArea.text != "无尺寸信息")
		{
			InputFielTotal.interactable = true;
			InputFielTotal.text = (float.Parse(Price) * float.Parse(InputFielArea.text)).ToString();
			//保存pdf数据  合价
			UnitDollarData.design.Total = InputFielTotal.text;
			//保存pdf数据  面积
			UnitDollarData.design.Acreage = InputFielArea.text;
		}
		if (Price == "")
		{
			InputFielTotal.text = "";
		}
		//现实状态
		if (InputFielTotal.interactable == true && InputFielTotal.text != "")
		{
			GameObject.Find("----------装饰设计--------").GetComponent<DecorativeDesignManager>().Button_确认.interactable = true;
		}
		else
		{
			GameObject.Find("----------装饰设计--------").GetComponent<DecorativeDesignManager>().Button_确认.interactable = false;
		}



	}

	//将状态转换为pdf用的数据   确认按钮调用
	public Design SetDesignConvertPdf(Design design_)
	{
		design_.ElementID = DecorativeDesignSaveDate.ElementID; //部件id
		design_.UniqueId = DecorativeDesignSaveDate.UniqueId; //唯一id
		design_.departmentName = UnitDollarData.design.departmentName;//分部名称
		design_.subentryName = UnitDollarData.design.subentryName;//分项
		design_.designImagePath = UnitDollarData.design.designImagePath;//图地址 
		design_.designMaterialPath = UnitDollarData.design.designMaterialPath;//材质地址 
		design_.designDesc = UnitDollarData.design.designDesc;//描述
		design_.Acreage = UnitDollarData.design.Acreage;//面积
		design_.Price = UnitDollarData.design.Price;//单价
		design_.Total = UnitDollarData.design.Total;//金额                   // Debug.Log("模型地址：" + DecorativeDesignSaveDate.HighligObject);
		design_.location = DecorativeDesignSaveDate.ElementID;//模型if

		return design_;
	}
}
