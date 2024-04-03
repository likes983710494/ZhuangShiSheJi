using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Org.BouncyCastle.Math.EC.Rfc7748;

public class RegisteManager : MonoBehaviour
{

    public Button registeEnter;
    public Button registeQuit;

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
        SceneManager.LoadScene("01MainMenu");
    }
    public void RegisteQuit()
    {
        Application.Quit();
    }


}
