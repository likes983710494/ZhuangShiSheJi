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
        switch (index)
        {
            case 0:
                for (int i = 0; i < DecorativeDesignManager.Instance_.楼地面_DesignsList.Count; i++)
                {
                    GameObject prefab = Resources.Load<GameObject>("prefab/Decorative/button_分部列表");
                    GameObject instance_ = Instantiate(prefab);
                    instance_.transform.SetParent(分部列表_Content_分部列表.transform);
                    instance_.transform.localScale = new Vector3(1, 1, 1);
                    object[] value = DecorativeDesignManager.Instance_.楼地面_DesignsList[i].GetType().GetProperties().Select(p => p.GetValue(DecorativeDesignManager.Instance_.楼地面_DesignsList[i])).ToArray();
                    for (int j = 0; j < value.Length; j++)
                    {
                        if (instance_.transform.GetChild(j).GetComponent<Text>() != null)
                        {
                            instance_.transform.GetChild(j).GetComponent<Text>().text = value[j].ToString();
                        }
                        else
                        {
                            Debug.Log("列表里的按钮");
                        }
                    }

                }
                break;

            case 1:

                for (int i = 0; i < DecorativeDesignManager.Instance_.墙柱面_DesignsList.Count; i++)
                {
                    GameObject prefab = Resources.Load<GameObject>("prefab/Decorative/button_分部列表");
                    GameObject instance_ = Instantiate(prefab);
                    instance_.transform.SetParent(分部列表_Content_分部列表.transform);
                    instance_.transform.localScale = new Vector3(1, 1, 1);
                    object[] value = DecorativeDesignManager.Instance_.墙柱面_DesignsList[i].GetType().GetProperties().Select(
                        p => p.GetValue(DecorativeDesignManager.Instance_.墙柱面_DesignsList[i])).ToArray();
                    if (instance_.transform.GetChild(0).GetComponent<Text>() != null)
                    {
                        instance_.transform.GetChild(0).GetComponent<Text>().text = i.ToString();
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
                break;
        }



        // public List<Design> 楼地面_DesignsList = new List<Design>();
        // public List<Design> 墙柱面_DesignsList = new List<Design>();
        // public List<Design> 天棚工程_DesignsList = new List<Design>();
        // public List<Design> 油漆涂料_DesignsList = new List<Design>();
        // public List<Design> 其他装饰_DesignsList = new List<Design>();
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
                Debug.Log(path_);
                工程设计_Plane.transform.GetChild(0).gameObject.SetActive(false);
                工程设计_Plane.transform.GetChild(1).gameObject.SetActive(true);
                工程设计_Plane.transform.GetChild(1).GetComponentInChildren<UnityEngine.UI.InputField>().text = path_;


                break;
        }











    }

}
