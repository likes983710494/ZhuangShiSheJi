using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using NPOI.SS.Formula.Functions;
/// <summary>
/// 截图 功能
/// </summary>
public class ScreenCaptureExample : MonoBehaviour
{
    public static ScreenCaptureExample instance_;
    private Texture2D screenShot;



    void Awake()
    {
        instance_ = this;
    }
    void Start()
    {
        //实例化一张带透明通道大小为例如256*256的贴图
        screenShot = new Texture2D(1920, 612, TextureFormat.RGB24, false);



        //  StartCoroutine(ScreenCapture2(Application.persistentDataPath + "/你好测试截图" + Time.time.ToString() + ".png"));
        // Debug.Log(Application.streamingAssetsPath + "/iamge" + "/你好测试截图" + Time.time.ToString() + ".png");
        // StartCoroutine(ScreenCapture2(Application.streamingAssetsPath+"/iamge" + "/你好测试截图" + Time.time.ToString() + ".png"));


    }

    public IEnumerator ScreenCapture2(string filename)
    {
        //在一帧渲染之后读取屏幕信息
        yield return new WaitForEndOfFrame();
        //读取屏幕像素信息并存储为纹理数据
        //screenShot.ReadPixels(new Rect(Screen.width / 4, Screen.height / 4, Screen.width / 4 * 3, Screen.height / 4 * 3), 0, 0);
        screenShot.ReadPixels(new Rect(0, Screen.height / 4 + 50, Screen.width, Screen.height), 0, 0);
        screenShot.Apply();
        //将纹理数据转换成png图片文件
        byte[] bytes = screenShot.EncodeToPNG();
        //写入文件,并且指定路径
        File.WriteAllBytes(filename, bytes);
    }
}
