
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;
using LitJson;



public delegate void HttpWebCallBacks(long code, HttpWebRequests request, HttpWebResponses rsponse);
/// <summary>
/// Http请求信息
/// </summary>
[Serializable]
public struct HttpWebRequests
{
    //请求地址
    public string url;
    //表单字段
    public JsonData filelds;
    //表头
    public JsonData headers;
    public string token;
    public string session;
}
//Http返回信息
[Serializable]
public struct HttpWebResponses
{
    //返回Http状态码
    public long code;
    //返回http提示信息
    public string message;
    //请求接口时返回的头字典
    public Dictionary<string, string> headers;
    //请求API接口时返回的文本内容
    public string text;
    //请求返回的字节数值
    public byte[] bytes;
    //下载完成时返回的Texture
    public Texture texture;
    //下载资源时用到的线程
    public Thread thread;
    //下载的AB
    public AssetBundle bundle;

    /// <summary>
    /// 将text反序列化成T类型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T Deserialize<T>()
    {
        return JsonMapper.ToObject<T>(this.text);
        //return JsonConvert.DeserializeObject<T>(this.text);
    }
}
public class Unity_Web : MonoBehaviour
{
    private static Unity_Web web;

    public static Unity_Web Web { get => web; set => web = value; }
    private void Awake()
    {
        web = this;
        DontDestroyOnLoad(gameObject);
    }
    #region UnityWebRequest
    private void SendRequest(string url, HttpWebCallBacks callback, JsonData fields = null, JsonData header = null, int timeout = 5)
    {
        StartCoroutine(_SendRequest(url, callback, fields, header, timeout));
    }

    IEnumerator _SendRequest(string url, HttpWebCallBacks callback, JsonData fields = null, JsonData header = null, int timeout = 5)
    {
        using (UnityWebRequest webRequest = new UnityWebRequest(url))
        {
            webRequest.timeout = timeout;
            yield return webRequest.SendWebRequest();

            //组装 请求信息
            HttpWebRequests request = new HttpWebRequests();
            request.url = url;
            request.filelds = fields;
            request.headers = header;

            //组装 返回信息
            HttpWebResponses response = new HttpWebResponses();
            response.code = webRequest.responseCode;
            response.message = webRequest.error;
            response.headers = webRequest.GetResponseHeaders();
            response.text = webRequest.downloadHandler.text;
            response.bytes = webRequest.downloadHandler.data;

            //调用 委托
            callback?.Invoke(webRequest.responseCode, request, response);

            //结束 处理占用资源
            //尽快停止UnityWebRequest
            webRequest.Abort();
            //默认值是true，调用该方法不需要设置Dispose()，Unity就会自动在完成后调用Dispose()释放资源。
            webRequest.disposeDownloadHandlerOnDispose = true;
            //不再使用此UnityWebRequest，并应清理它正在使用的任何资源
            webRequest.Dispose();
        }

    }


    #endregion


    #region Post方法 
    /*
     Post方法将一个表上传到远程的服务器，一般来说我们登陆某个网站的时候会用到这个方法，我们的账号密码会以一个表单的形式传过去。
    */

    #region POST普通表单请求,支持Header,Authorization
    /// <summary>
    /// POST普通表单请求
    /// POST普通表单参数请求 - 数据以Parameters形式发送
    /// </summary>
    /// <param name="url">目标URL</param>
    /// <param name="callback">请求完成回调</param>
    /// <param name="fields">表单字段</param>
    /// <param name="header">请求头字典, 默认Content-Type=application/json</param>
    /// <param name="timeout"></param>
    public void Post(string url, HttpWebCallBacks callback, JsonData fields = null, JsonData header = null, int timeout = 5)
    {
        StartCoroutine(_Post(url, callback, fields, header, timeout));
    }

    IEnumerator _Post(string url, HttpWebCallBacks callback, JsonData fields = null, JsonData header = null, int timeout = 5)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.PostWwwForm(url, fields != null ? fields.ToJson() : null))
        {
            webRequest.SetRequestHeader("Authorization", "");
            //开始与远程服务器通信
            //等待通行完成
            yield return webRequest.SendWebRequest();

            //组装 请求信息
            HttpWebRequests request = new HttpWebRequests();
            request.url = url;
            request.filelds = fields;
            request.headers = header;

            //组装 返回信息
            HttpWebResponses response = new HttpWebResponses();
            response.code = webRequest.responseCode;
            response.message = webRequest.error;
            response.headers = webRequest.GetResponseHeaders();
            response.text = webRequest.downloadHandler.text;
            response.bytes = webRequest.downloadHandler.data;

            //调用 委托
            callback?.Invoke(webRequest.responseCode, request, response);

            //结束 处理占用资源
            //尽快停止UnityWebRequest
            webRequest.Abort();
            //默认值是true，调用该方法不需要设置Dispose()，Unity就会自动在完成后调用Dispose()释放资源。
            webRequest.disposeDownloadHandlerOnDispose = true;
            //不再使用此UnityWebRequest，并应清理它正在使用的任何资源
            webRequest.Dispose();
        }
    }
    #endregion


    #region POST RawBody请求,支持Header,Authorization
    /// <summary>
    /// 进行POST请求，数据以Raw格式携带在Body中进行请求
    /// </summary>
    /// <param name="url">目标URL地址</param>
    /// <param name="callback">回调完成函数</param>
    /// <param name="fields">携带数据</param>
    /// <param name="header"></param>
    /// <param name="timeout"></param>
    public void PostRaw(string url, HttpWebCallBacks callback, JsonData fields = null, JsonData header = null, int timeout = 5)
    {
        StartCoroutine(_PostRaw(url, callback, fields, header, timeout));
    }

    IEnumerator _PostRaw(string url, HttpWebCallBacks callback, JsonData fields, JsonData header, int timeout = 5)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.PostWwwForm(url, "POST"))
        {
            //负责拒绝或接受在https请求中收到的证书,用于Https请求中的证书处理
            //webRequest.certificateHandler = new CustomCertHandler();
            //设定超时
            webRequest.timeout = timeout;
            //设置信息头  根据实际需求来
            webRequest.SetRequestHeader("Content-Type", "application/json;");
            webRequest.SetRequestHeader("Authorization", "Bearer YOUR_ACCESS_TOKEN");
            webRequest.SetRequestHeader("Custom-Header", "Custom Value");
            //处理请求头设置
            if (header != null)
            {
                foreach (var item in header.Keys)
                {
                    webRequest.SetRequestHeader(item, header[item].ToString());
                }
            }
            //设置携带数据
            if (fields != null)
            {
                webRequest.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(fields.ToJson()));
            }
            yield return webRequest.SendWebRequest();


            //组装 请求信息
            HttpWebRequests request = new HttpWebRequests();
            request.url = url;
            request.filelds = fields;
            request.headers = header;

            //组装 返回信息
            HttpWebResponses response = new HttpWebResponses();
            response.code = webRequest.responseCode;
            response.message = webRequest.error;
            response.headers = webRequest.GetResponseHeaders();
            response.text = webRequest.downloadHandler.text;
            response.bytes = webRequest.downloadHandler.data;


            //调用 委托
            callback?.Invoke(webRequest.responseCode, request, response);

            //结束 处理占用资源
            //尽快停止UnityWebRequest
            webRequest.Abort();
            //默认值是true，调用该方法不需要设置Dispose()，Unity就会自动在完成后调用Dispose()释放资源。
            webRequest.disposeDownloadHandlerOnDispose = true;
            //不再使用此UnityWebRequest，并应清理它正在使用的任何资源
            webRequest.Dispose();
        }
    }

    #endregion

    #endregion


    #region GET方法 

    #region GET普通表单请求，支持Header,Authorization
    /// <summary>
    /// 普通GET请求
    /// </summary>
    /// <param name="url">Get请求目标URL</param>
    /// <param name="callback">请求完成回调函数</param>
    /// <param name="fields">标单字段数据，Dictionary类型 </param>
    /// <param name="header">请求Header数据,Dictionary类型 </param>
    /// <param name="timeout">超时时间  默认5秒</param>
    public void Get(string url, HttpWebCallBacks callback, JsonData fields = null, JsonData header = null, int timeout = 5)
    {
        StartCoroutine(_Get(url, callback, fields, header, timeout));
    }

    private IEnumerator _Get(string url, HttpWebCallBacks callback, JsonData fields = null, JsonData header = null, int timeout = 5)
    {


        //print("调用Get方法");
        string targetUrl = url;
        StringBuilder sb = new StringBuilder();

        if (fields != null)
        {
            foreach (var Key in fields.Keys)
            {
                sb.Append(Key + "=" + fields[Key] + "&");
            }
            if (url.Contains("?"))
            {
                targetUrl = url + "&" + sb.ToString();
            }
            else
            {
                targetUrl = url + "?" + sb.ToString();
            }

            targetUrl = targetUrl[targetUrl.Length - 1] == '&' ? targetUrl.Substring(0, targetUrl.Length - 1) : targetUrl;
        }

        using (UnityWebRequest webRequest = UnityWebRequest.Get(targetUrl))
        {
            //设定超时
            webRequest.timeout = timeout;
            //处理请求头设置
            webRequest.SetRequestHeader("Authorization", "");
            if (header != null)
            {
                foreach (var item in header.Keys)
                {
                    webRequest.SetRequestHeader(item.ToString(), header[item].ToString());
                }
            }
            yield return webRequest.SendWebRequest();


            //组装 请求信息
            HttpWebRequests request = new HttpWebRequests();
            request.url = url;
            request.filelds = fields;
            request.headers = header;

            //组装 返回信息
            HttpWebResponses response = new HttpWebResponses();
            response.code = webRequest.responseCode;
            response.message = webRequest.error;
            response.headers = webRequest.GetResponseHeaders();
            response.text = webRequest.downloadHandler.text;
            response.bytes = webRequest.downloadHandler.data;

            //调用 委托
            callback?.Invoke(webRequest.responseCode, request, response);

            //结束 处理占用资源
            //尽快停止UnityWebRequest
            webRequest.Abort();
            //默认值是true，调用该方法不需要设置Dispose()，Unity就会自动在完成后调用Dispose()释放资源。
            webRequest.disposeDownloadHandlerOnDispose = true;
            //不再使用此UnityWebRequest，并应清理它正在使用的任何资源
            webRequest.Dispose();
        }
    }
    #endregion

    #endregion
}

