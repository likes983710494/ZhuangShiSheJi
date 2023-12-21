using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System.Reflection;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.UIElements;
/// <summary>
/// 装饰效果管理
/// </summary>
public class ResultManager : MonoBehaviour
{
	public UnityEngine.UI.Button submitButton;
	public List<GameObject> ChangePlane= new List<GameObject>();//image切换
	public List<UnityEngine.UI.Toggle> ChangePlaneButtons = new List<UnityEngine.UI.Toggle>();//按钮
	public  List<UnityEngine.UI.Button> AddImageButtons = new List<UnityEngine.UI.Button>();//选取本地图片 按钮
	public Transform PlaneParent0, PlaneParent1, PlaneParent2, PlaneParent3, PlaneParent4;//父节点

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

			AddImageButtons[x].onClick.AddListener(OpenFileImage);
		}
		
	}

	// Update is called once per frame
	void Update()
	{
		
			if(PlaneParent0.childCount > 2&& PlaneParent1.childCount > 2 && PlaneParent2.childCount > 2 && PlaneParent3.childCount > 2 && PlaneParent4.childCount > 2)
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

    public void OpenFileImage()
    {
        
            OpenFileName ofn = new OpenFileName();
            ofn.structSize = Marshal.SizeOf(ofn);
            ofn.filter = "图片文件(*.jpg*.png)\0*.jpg;*.png";
            ofn.file = new string(new char[256]);
            ofn.maxFile = ofn.file.Length;
            ofn.fileTitle = new string(new char[64]);
            ofn.maxFileTitle = ofn.fileTitle.Length;
            //默认路径
            string path = Application.streamingAssetsPath;
            path = path.Replace('/', '\\');
            //默认路径
            //ofn.initialDir = "G:\\wenshuxin\\test\\HuntingGame_Test\\Assets\\StreamingAssets";
            ofn.initialDir = path;
            ofn.title = "Open Project";
            ofn.defExt = "JPG";//显示文件的类型
            //注意 一下项目不一定要全选 但是0x00000008项不要缺少
            ofn.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000200 | 0x00000008;//OFN_EXPLORER|OFN_FILEMUSTEXIST|OFN_PATHMUSTEXIST| OFN_ALLOWMULTISELECT|OFN_NOCHANGEDIR
            //点击Windows窗口时开始加载选中的图片
            if (WindowDll.GetOpenFileName(ofn))
            {
                Debug.Log("Selected file with full path: " + ofn.file);
                StartCoroutine(Load(ofn.file));
            }
        
    }


    /// <summary>
    /// 加载本地图片
    /// </summary>
    /// <param name="path">本地文件路径</param>
    /// <returns></returns>
    IEnumerator Load(string path)
    {   //计算加载用时    
        double startTime = (double)Time.time;


        WWW www = new WWW("file:///" + path);
        yield return www;
        if (www != null && string.IsNullOrEmpty(www.error))
        {
            //获取Texture
            Texture2D texture = www.texture;
       
            //直接将选择图保存
            byte[] bytes = texture.EncodeToJPG();
            //测试地址
            //string filename = @"G:\wenshuxin\BackGround\6.jpg";
            System.IO.File.WriteAllBytes(path, bytes);
            //根据获取的Texture创建一个sprite
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
			//生成预制体
			GameObject prefab = Resources.Load<GameObject>("prefab/Resul/Image_已上传BG");
			Transform instance_Parent = GameObject.Find("Image_已上传").transform;
			GameObject instance_ = Instantiate(prefab);
			instance_.transform.SetParent(instance_Parent);
			instance_.transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = sprite;
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
	/// 删除
	/// </summary>
	public void DestroyLoadPane()
	{
		
	}
	/// <summary>
	/// 提交
	/// </summary>
	public void SubmitLaodPlane()
	{

	}

}

