using System;

using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoginMessage : MonoBehaviour
{
    public static LoginMessage Instance_ { get; private set; }
    public string Authorization;

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

    // void Start()

    public void LoginRequest_post(string username, string passwor)
    {
        StopAllCoroutines();
        StartCoroutine(SendLoginPostRequest(username, passwor));
    }
    public void InfoRequest(string taskplanUrl)
    {
        StartCoroutine(SendTaskplangetRequest(taskplanUrl));
    }

    /// <summary>
    /// 登录请求接口
    /// </summary>
    /// <param name="username"></param>
    /// <param name="passwor"></param>
    /// <returns></returns>
    IEnumerator SendLoginPostRequest(string username, string passwor)
    {
        // 请求的URL，包含查询参数
        string url = "http://192.168.1.33/api/admin/oauth2/token?username=" + username + "&grant_type=password&scope=server&password=" + passwor;
        // string bodyJsonString = "{\"password\":\"123456\"}";
        // byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
        // 使用UnityWebRequest发送POST请求
        using (UnityWebRequest webRequest = new UnityWebRequest(url, "POST"))
        {
            // 设置请求体
            // webRequest.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            // 设置下载处理器
            webRequest.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

            // 设置Content-Type header为application/json
            webRequest.SetRequestHeader("Content-Type", "application/json");
            // 添加自定义的Authorization header参数
            webRequest.SetRequestHeader("Authorization", "Basic dnI6dnI=");

            // 请求并等待服务器响应
            yield return webRequest.SendWebRequest();

            // 检查是否有错误发生
            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error: " + webRequest.error);
            }
            else
            {
                // 请求成功，处理服务器返回的数据
                Debug.Log("Received: " + webRequest.downloadHandler.text);
                // 以解析返回的JSON以获取token或其他数据
                loginDataForm myData = JsonUtility.FromJson<loginDataForm>(webRequest.downloadHandler.text);
                // 保存token
                Authorization = myData.access_token;
                SceneManager.LoadScene("01MainMenu");
            }

            //结束 处理占用资源
            //尽快停止UnityWebRequest
            webRequest.Abort();
            //默认值是true，调用该方法不需要设置Dispose()，Unity就会自动在完成后调用Dispose()释放资源。
            webRequest.disposeDownloadHandlerOnDispose = true;
            //不再使用此UnityWebRequest，并应清理它正在使用的任何资源
            webRequest.Dispose();
        }



    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="taskplan">地址</param>
    /// <param name="Authorization">token</param>
    /// <returns></returns>
    IEnumerator SendTaskplangetRequest(string taskplanUrl)
    {
        // 请求的URL，包含查询参数
        string url = taskplanUrl;
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {

            // 设置下载处理器
            webRequest.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

            // 设置Content-Type header为application/json
            webRequest.SetRequestHeader("Content-Type", "application/json");
            // 添加自定义的Authorization header参数
            webRequest.SetRequestHeader("Authorization", "Bearer " + Authorization);
            Debug.Log("Authorization" + Authorization);
            // 请求并等待服务器响应
            yield return webRequest.SendWebRequest();

            // 检查是否有错误发生
            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error: " + webRequest.error);
            }
            else
            {
                // 请求成功，处理服务器返回的数据
                Debug.Log("初始信息Received: " + webRequest.downloadHandler.text);

            }

            //结束 处理占用资源
            //尽快停止UnityWebRequest
            webRequest.Abort();
            //默认值是true，调用该方法不需要设置Dispose()，Unity就会自动在完成后调用Dispose()释放资源。
            webRequest.disposeDownloadHandlerOnDispose = true;
            //不再使用此UnityWebRequest，并应清理它正在使用的任何资源
            webRequest.Dispose();
        }
    }


}


/// <summary>
/// 登录请求数据对象
/// </summary>
[Serializable]
public class loginDataForm
{
    public string access_token;
    public string refresh_token;

}

