using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Org.BouncyCastle.Math.EC.Rfc7748;

//登录管理脚本
public class RegisteManager : MonoBehaviour
{
    public static RegisteManager Instance_;
    public Button registeEnter;
    public Button registeQuit;

    public InputField username_InputFile;
    public InputField passwor_InputFile;
    public Text text_loginfailure;

    public List<Button> ButtonsAddAudioList;//添加按钮音频
    void Awake()
    {
        if (Instance_ == null)
        {
            Instance_ = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        registeEnter.onClick.AddListener(delegate { RegisteEnter(); });
        registeQuit.onClick.AddListener(delegate { RegisteQuit(); });
        for (int i = 0; i < ButtonsAddAudioList.Count; i++)
        {
            int x = i;
            ButtonsAddAudioList[x].gameObject.AddComponent<ButtonAudioClick>();
        }
        text_loginfailure.gameObject.SetActive(false);

        // 添加监听器来响应文本变化
        username_InputFile.onValueChanged.AddListener(delegate { text_loginfailure.gameObject.SetActive(false); });
        passwor_InputFile.onValueChanged.AddListener(delegate { text_loginfailure.gameObject.SetActive(false); });
    }

    public void RegisteEnter()
    {
        LoginMessage.Instance_.LoginRequest_post(username_InputFile.text, passwor_InputFile.text);

    }
    public void RegisteQuit()
    {
        Application.Quit();
    }


}
