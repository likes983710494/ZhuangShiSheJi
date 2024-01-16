using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
//装饰设计-做法说明
public class DecorativeDesignModus : MonoBehaviour
{

    public static DecorativeDesignModus Instance_ { get; private set; }
    public GameObject LeftMakerPlan;//左侧做法面板- 调整width控制显示
    public Button LeftMakerUnfoldButton; //展开面板 -抽屉按钮 
    public GameObject 模型等待进度条;

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

    // Update is called once per frame
    void Update()
    {

    }
}
