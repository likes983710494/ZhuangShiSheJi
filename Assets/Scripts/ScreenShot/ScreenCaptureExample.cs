using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using NPOI.SS.Formula.Functions;

public class ScreenCaptureExample : MonoBehaviour
{

    private Texture2D screenShot;
    public Button ��ͼ;
    void Start()
    {
        //ʵ����һ�Ŵ�͸��ͨ����СΪ256*256����ͼ
        screenShot = new Texture2D(512, 512, TextureFormat.RGB24, false);
        ��ͼ.onClick.AddListener(() => {

            //  StartCoroutine(ScreenCapture2(Application.persistentDataPath + "/��ò��Խ�ͼ" + Time.time.ToString() + ".png"));
            Debug.Log(Application.streamingAssetsPath + "/iamge" + "/��ò��Խ�ͼ" + Time.time.ToString() + ".png");
            StartCoroutine(ScreenCapture2(Application.streamingAssetsPath+"/iamge" + "/��ò��Խ�ͼ" + Time.time.ToString() + ".png"));

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
        //    StartCoroutine(ScreenCapture2(Application.persistentDataPath + "/��ò��Խ�ͼ" + Time.time.ToString() + ".png"));
        //}
    }
    private void ScreenCapture1()
    {
        ScreenCapture.CaptureScreenshot(Application.persistentDataPath + "/CaptureScreenshot" + Time.time.ToString() + ".png");
    }
    IEnumerator ScreenCapture2(string filename)
    {
        //��һ֡��Ⱦ֮���ȡ��Ļ��Ϣ
        yield return new WaitForEndOfFrame();
        //��ȡ��Ļ������Ϣ���洢Ϊ��������
        screenShot.ReadPixels(new Rect(Screen.width / 4, Screen.height / 4, Screen.width / 4 * 3, Screen.height / 4 * 3), 0, 0);
        screenShot.Apply();
        //����������ת����pngͼƬ�ļ�
        byte[] bytes = screenShot.EncodeToPNG();
        //д���ļ�,����ָ��·��
        File.WriteAllBytes(filename, bytes);
    }
}
