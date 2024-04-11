using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Org.BouncyCastle.Math.EC.Rfc7748;

//登录管理脚本
public class RegisteManager : MonoBehaviour
{

    public Button registeEnter;
    public Button registeQuit;

    public InputField username_InputFile;
    public InputField passwor_InputFile;

    public List<Button> ButtonsAddAudioList;//添加按钮音频
    void Awake()
    {

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
