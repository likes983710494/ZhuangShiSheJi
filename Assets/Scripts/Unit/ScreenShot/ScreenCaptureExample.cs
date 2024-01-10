using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using NPOI.SS.Formula.Functions;
/// <summary>
/// ��ͼ ����
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
        //ʵ����һ�Ŵ�͸��ͨ����СΪ����256*256����ͼ
        screenShot = new Texture2D(1920, 612, TextureFormat.RGB24, false);



        //  StartCoroutine(ScreenCapture2(Application.persistentDataPath + "/��ò��Խ�ͼ" + Time.time.ToString() + ".png"));
        // Debug.Log(Application.streamingAssetsPath + "/iamge" + "/��ò��Խ�ͼ" + Time.time.ToString() + ".png");
        // StartCoroutine(ScreenCapture2(Application.streamingAssetsPath+"/iamge" + "/��ò��Խ�ͼ" + Time.time.ToString() + ".png"));


    }

    public IEnumerator ScreenCapture2(string filename)
    {
        //��һ֡��Ⱦ֮���ȡ��Ļ��Ϣ
        yield return new WaitForEndOfFrame();
        //��ȡ��Ļ������Ϣ���洢Ϊ��������
        //screenShot.ReadPixels(new Rect(Screen.width / 4, Screen.height / 4, Screen.width / 4 * 3, Screen.height / 4 * 3), 0, 0);
        screenShot.ReadPixels(new Rect(0, Screen.height / 4 + 50, Screen.width, Screen.height), 0, 0);
        screenShot.Apply();
        //����������ת����pngͼƬ�ļ�
        byte[] bytes = screenShot.EncodeToPNG();
        //д���ļ�,����ָ��·��
        File.WriteAllBytes(filename, bytes);
    }
}
