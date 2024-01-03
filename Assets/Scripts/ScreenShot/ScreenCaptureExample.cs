using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using NPOI.SS.Formula.Functions;

public class ScreenCaptureExample : MonoBehaviour
{

    private Texture2D screenShot;
    public Button 截图;
    void Start()
    {
        //实例化一张带透明通道大小为256*256的贴图
        screenShot = new Texture2D(512, 512, TextureFormat.RGB24, false);
        截图.onClick.AddListener(() => {

            //  StartCoroutine(ScreenCapture2(Application.persistentDataPath + "/你好测试截图" + Time.time.ToString() + ".png"));
            Debug.Log(Application.streamingAssetsPath + "/iamge" + "/你好测试截图" + Time.time.ToString() + ".png");
            StartCoroutine(ScreenCapture2(Application.streamingAssetsPath+"/iamge" + "/你好测试截图" + Time.time.ToString() + ".png"));

        });
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.F1))
        //{
        //    ScreenCapture1();
        //}
        //if (Input.GetKeyDown(KeyCode.F2))
        //{
        //    Debug.Log("F2");
        //    StartCoroutine(ScreenCapture2(Application.persistentDataPath + "/你好测试截图" + Time.time.ToString() + ".png"));
        //}
    }
    private void ScreenCapture1()
    {
        ScreenCapture.CaptureScreenshot(Application.persistentDataPath + "/CaptureScreenshot" + Time.time.ToString() + ".png");
    }
    IEnumerator ScreenCapture2(string filename)
    {
        //在一帧渲染之后读取屏幕信息
        yield return new WaitForEndOfFrame();
        //读取屏幕像素信息并存储为纹理数据
        screenShot.ReadPixels(new Rect(Screen.width / 4, Screen.height / 4, Screen.width / 4 * 3, Screen.height / 4 * 3), 0, 0);
        screenShot.Apply();
        //将纹理数据转换成png图片文件
        byte[] bytes = screenShot.EncodeToPNG();
        //写入文件,并且指定路径
        File.WriteAllBytes(filename, bytes);
    }
}
