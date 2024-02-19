using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Unit;
using System.Linq;
using System.IO;
//装饰设计-中间展示
public class DecorativeDesignModus : MonoBehaviour
{

    public static DecorativeDesignModus Instance_ { get; private set; }
    public GameObject LeftMakerPlan;//左侧做法面板- 调整width控制显示
    public Button LeftMakerUnfoldButton; //展开面板 -抽屉按钮 
    public GameObject 模型等待进度条;
    public GameObject 分部列表_Plane;//分部列表面板

    public GameObject 分部列表_Content_分部列表;//父节点

    public GameObject 工程设计_Plane;//分部列表-工程设计面板

    public GameObject Image_通知提示框;

    public Toggle ToggleModel_browse;//模型 浏览模式
    public Toggle ToggleModel_free;//模型 自由模式

    public Transform ToggleModel_browseImage;//浏览模式 指示图片
    public Transform ToggleModel_freeImage;//自由模式 指示图片
    void Awake()
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
        LeftMakerUnfoldButton.interactable = false;
        分部列表_Plane.SetActive(false);
        LeftMakerUnfoldButton.onClick.AddListener(() =>
        {
            //收缩时候展开-展开时收缩
            //展开面板全部宽度
            Vector2 longWidth = new Vector2(1110, 630);
            Vector2 shortWidth = new Vector2(320, 630);

            if (LeftMakerPlan.transform.GetComponent<RectTransform>().sizeDelta == shortWidth)
            {
                //展开 变换按钮样式 
                LeftMakerPlan.transform.GetComponent<RectTransform>().sizeDelta = longWidth;
            }
            else if (LeftMakerPlan.transform.GetComponent<RectTransform>().sizeDelta == longWidth)
            {
                //缩回  变换按钮样式 
                LeftMakerPlan.transform.GetComponent<RectTransform>().sizeDelta = shortWidth;
            }

        });
        //默认隐藏 等待模型 在出现提示
        Image_通知提示框.SetActive(false);

        // 为ToggleModel_browse添加浏览模式事件
        ToggleModel_browse.onValueChanged.AddListener(OnToggleModelBrowseValueChanged);
        // 为ToggleModel_free添加自由模式事件
        ToggleModel_free.onValueChanged.AddListener(OnToggleModelFreeValueChanged);
        Camera.main.transform.GetComponent<SimpleCameraController>().enabled = false;

        ToggleModel_browseImage.gameObject.SetActive(false);
        ToggleModel_freeImage.gameObject.SetActive(false);

    }

    /// <summary>
    /// 浏览模式
    /// </summary>
    /// <param name="isOn"></param>
    public void OnToggleModelBrowseValueChanged(bool isOn = true)
    {
        if (isOn)
        {
            Camera.main.transform.GetComponent<SimpleCameraController>().enabled = false;
            GameObject.Find("三维模型展示").transform.GetChild(0).GetComponent<CameraRotateAround>().enabled = true;
            GameObject.Find("三维模型展示").transform.GetChild(0).GetComponent<CameraRotateAround>().InitCameraRotateAround();
            ToggleModel_browseImage.gameObject.SetActive(true);
            ToggleModel_freeImage.gameObject.SetActive(false);
        }
        else
        {
            GameObject.Find("三维模型展示").transform.GetChild(0).GetComponent<CameraRotateAround>().enabled = false;

        }
    }
    /// <summary>
    /// 自由模式
    /// </summary>
    /// <param name="isOn"></param>
    public void OnToggleModelFreeValueChanged(bool isOn)
    {
        if (isOn)
        {
            GameObject.Find("三维模型展示").transform.GetChild(0).GetComponent<CameraRotateAround>().enabled = false;
            Camera.main.transform.GetComponent<SimpleCameraController>().enabled = true;
            ToggleModel_freeImage.gameObject.SetActive(true);
            ToggleModel_browseImage.gameObject.SetActive(false);
        }
        else
        {
            Camera.main.transform.GetComponent<SimpleCameraController>().enabled = false;

        }
    }
    /// <summary>
    /// 设置弹窗显示以及文本内容
    /// </summary>
    /// <param name="enabled"> 状态</param>
    /// <param name="text">弹窗文本内容</param> <summary>
    /// <param name="type"> 是否修改当前文本内容0为否 1为是</param>
    /// </summary>
    public void SetImage_通知提示框(bool enabled, string text, int type = 0)
    {
        Image_通知提示框.SetActive(enabled);
        if (enabled == true && type == 1)
        {
            if (Image_通知提示框.transform.GetChild(1) != null)
            {
                Image_通知提示框.transform.GetChild(1).GetComponent<Text>().text = text;
            }

        }
    }
    /// <summary>
    /// 生成中间弹窗 数据
    /// </summary>
    /// <param name="index"></param> <summary>
    /// 
    /// </summary>
    /// <param name="index"></param>
    public void GenerateDataList(int index)
    {


        DecorativeDesignModus.Instance_.分部列表_Plane.SetActive(true);

        for (int i = 0; i < 分部列表_Content_分部列表.transform.childCount; i++)
        {
            int x = i;
            Destroy(分部列表_Content_分部列表.transform.GetChild(x).gameObject);
        }
        switch (index)
        {
            case 0:
                AddGenerateData(DecorativeDesignManager.Instance_.楼地面_DesignsList);

                break;

            case 1:
                AddGenerateData(DecorativeDesignManager.Instance_.墙柱面_DesignsList);

                break;
            case 2:
                AddGenerateData(DecorativeDesignManager.Instance_.天棚工程_DesignsList);

                break;
            case 3:
                AddGenerateData(DecorativeDesignManager.Instance_.油漆涂料_DesignsList);

                break;
            case 4:
                AddGenerateData(DecorativeDesignManager.Instance_.其他装饰_DesignsList);

                break;
        }
    }


    /// <summary>
    /// 生成分部列表数据
    /// </summary>
    public void AddGenerateData(List<Design> DesignsList)
    {
        for (int i = 0; i < DesignsList.Count; i++)
        {
            GameObject prefab = Resources.Load<GameObject>("prefab/Decorative/button_分部列表");
            GameObject instance_ = Instantiate(prefab);
            instance_.transform.SetParent(分部列表_Content_分部列表.transform);
            instance_.transform.localScale = new Vector3(1, 1, 1);
            object[] value = DesignsList[i].GetType().GetProperties().Select(
                p => p.GetValue(DesignsList[i])).ToArray();
            if (instance_.transform.GetChild(0).GetComponent<Text>() != null)
            {
                instance_.transform.GetChild(0).GetComponent<Text>().text = (i + 1).ToString();
            }

            for (int j = 0; j < value.Length; j++)
            {
                if (instance_.transform.GetChild(j + 1).GetComponent<Text>() != null)
                {
                    instance_.transform.GetChild(j + 1).GetComponent<Text>().text = value[j].ToString();
                }
                else
                {
                    int num = j;

                    if (instance_.transform.GetChild(j + 1).name == "Button (Legacy)_查看设计图 (4)")
                    {
                        instance_.transform.GetChild(j + 1).GetComponent<Button>().onClick.AddListener(() =>
                                                   {
                                                       //value[j].ToString(); 获取地址展示图片
                                                       工程设计_Plane.SetActive(true);
                                                       StopAllCoroutines();
                                                       StartCoroutine(ExamineDesignImage(value[num].ToString(), 0));
                                                   });
                    }
                    if (instance_.transform.GetChild(j + 1).name == "Button (Legacy)_编辑说明 (6)")
                    {
                        instance_.transform.GetChild(j + 1).GetComponent<Button>().onClick.AddListener(() =>
                        {
                            //value[j].ToString(); 获取地址展示文字
                            工程设计_Plane.SetActive(true);
                            StopAllCoroutines();
                            StartCoroutine(ExamineDesignImage(value[num].ToString(), 1));
                        });

                    }


                }
            }

        }
    }

    /// <summary>
    /// 列表-查看设计详情
    /// </summary>
    /// <param name="path_"></param>
    /// <returns></returns> <summary>
    /// 
    /// </summary>
    /// <param index="0">0 是工程设计 1是文字描述</param>
    /// <returns></returns>
    public IEnumerator ExamineDesignImage(string path_, int index)
    {

        switch (index)
        {
            case 0:
                if (Path.GetExtension(path_) == ".png" || Path.GetExtension(path_) == ".jpg")
                {
                    string fileName = Path.GetFileName(path_);
                    WWW www = new WWW("file:///" + path_);
                    yield return www;
                    //获取Texture
                    Texture2D texture = www.texture;
                    //根据获取的Texture创建一个sprite
                    Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                    工程设计_Plane.name = fileName;
                    工程设计_Plane.transform.GetChild(1).gameObject.SetActive(false);
                    工程设计_Plane.transform.GetChild(0).gameObject.SetActive(true);
                    工程设计_Plane.transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = sprite;

                    // int dotPosition = fileName.IndexOf('.');
                    // string beforeDot = fileName.Substring(0, dotPosition);


                }
                break;
            case 1:
                工程设计_Plane.transform.GetChild(0).gameObject.SetActive(false);
                工程设计_Plane.transform.GetChild(1).gameObject.SetActive(true);
                工程设计_Plane.transform.GetChild(1).GetComponentInChildren<UnityEngine.UI.InputField>().text = path_;
                break;
        }











    }

}
