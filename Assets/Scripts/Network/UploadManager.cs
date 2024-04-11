
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using UnityEngine;
using LitJson;

/// <summary>
/// 上传管理器
/// </summary>
public class UploadManager : MonoBehaviour
{
    private static UploadManager _instance;

    public static UploadManager Instance { get => _instance; set => _instance = value; }
    private void Awake()
    {
        _instance = this;
    }

    public static string Access_token = "";
    private long getStartTime;
    private DateTime startTime;
    private DateTime endTime;
    public List<StepDataForm> StepList = new List<StepDataForm>();

    private void Start()
    {
        getStartTime = GetTimeStamp();
        startTime = DateTime.Now;
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    endTime= DateTime.Now;
        //    Debug.Log("startTime:"+ startTime);
        //    Debug.Log("endTime:" + endTime);
        //    Debug.Log(GetTimeDifference(startTime, endTime));
        //}
    }
    /// <summary>
    /// 数据固定格式
    /// </summary>
    public void RewindUploadData(string userName, string title, int score, string appid, string originId, string ext_data)
    {
        endTime = DateTime.Now;
        FixedDataForm fixedDataForm = new FixedDataForm();
        fixedDataForm.Username = userName;
        fixedDataForm.Title = title;
        fixedDataForm.ChildProjectTitle = "";
        fixedDataForm.Status = 1;
        fixedDataForm.Score = score;
        fixedDataForm.StartTime = getStartTime;
        fixedDataForm.EndTime = GetTimeStamp();
        fixedDataForm.TimeUsed = GetTimeDifference(startTime, endTime);
        fixedDataForm.Appid = appid;
        fixedDataForm.OriginId = originId;
        fixedDataForm.Steps = StepList;
        fixedDataForm.Group_Id = "";
        fixedDataForm.Group_Name = "";
        fixedDataForm.Role_In_Group = "";
        fixedDataForm.Group_Members = "";
        fixedDataForm.Ext_data = ext_data;
        UploadData(fixedDataForm);
    }
    /// <summary>
    /// 数据固定格式
    /// </summary>
    public void RewindUploadData(string userName, string title, string childProjectTitle, int score, string appid, string originId, List<StepDataForm> stepDataForms, string group_Id, string group_Name, string role_In_Group, string group_Members, string ext_data)
    {
        endTime = DateTime.Now;
        FixedDataForm fixedDataForm = new FixedDataForm();
        fixedDataForm.Username = userName;
        fixedDataForm.Title = title;
        fixedDataForm.ChildProjectTitle = childProjectTitle;
        fixedDataForm.Status = 1;
        fixedDataForm.Score = score;
        fixedDataForm.StartTime = getStartTime;
        fixedDataForm.EndTime = GetTimeStamp();
        fixedDataForm.TimeUsed = GetTimeDifference(startTime, endTime);
        fixedDataForm.Appid = appid;
        fixedDataForm.OriginId = originId;
        fixedDataForm.Steps = stepDataForms;
        fixedDataForm.Group_Id = group_Id;
        fixedDataForm.Group_Name = group_Name;
        fixedDataForm.Role_In_Group = role_In_Group;
        fixedDataForm.Group_Members = group_Members;
        fixedDataForm.Ext_data = ext_data;
        UploadData(fixedDataForm);
    }
    /// <summary>
    /// 上传数据表单
    /// </summary>
    /// <param name="fixedDataForm">固定数据表单</param>
    public void UploadData(FixedDataForm fixedDataForm)
    {
        string url = AppUrlConfig.BaseUrl + "";
        string json = JsonMapper.ToJson(fixedDataForm);
        JsonData jsonData = JsonMapper.ToObject<JsonData>(json);
        Post(url, GetSaveInfo, jsonData);
    }

    public void GetSaveInfo(long code, HttpWebRequests request, HttpWebResponses rsponse)
    {
        if (code == 200 && rsponse.code == 200)
        {
            JsonData jsonData = JsonMapper.ToObject(rsponse.text);
            Debug.Log(jsonData["code"] + "---上传成功！");
        }
        else
        {
            Debug.LogError("-----code" + code + "------rsponse.code" + rsponse.code);
            Debug.Log("上传数据失败，请检测网络或服务器未响应");
        }
    }

    /// <summary>
    /// 步骤数据格式化
    /// </summary>
    public void RewindStepData(int seq, string title, long startTime, int expectTime, int maxScore, int score, int repeatCount, string evaluation, string scoringModel, string remarks, string ext_data)
    {
        StepDataForm stepDataForm = new StepDataForm();
        stepDataForm.Seq = seq;
        stepDataForm.Title = title;
        stepDataForm.StartTime = startTime;
        stepDataForm.EndTime = GetTimeStamp();
        stepDataForm.TimeUsed = GetTimeDifference(LongToDataTime(startTime), LongToDataTime(GetTimeStamp()));
        stepDataForm.ExpectTime = expectTime;
        stepDataForm.MaxScore = maxScore;
        stepDataForm.Score = score;
        stepDataForm.RepeatCount = repeatCount;
        stepDataForm.Evaluation = evaluation;
        stepDataForm.ScoringModel = scoringModel;
        stepDataForm.Remarks = remarks;
        stepDataForm.Ext_data = ext_data;
        int index = StepList.FindIndex(data => data.Seq == stepDataForm.Seq);
        if (index != -1)
        {
            StepList[index] = stepDataForm;  // 替换老数据
        }
        else
        {
            StepList.Add(stepDataForm); // 添加新数据
        }
    }
    /// <summary>
    /// 步骤数据格式化
    /// </summary>
    public void RewindStepData(StepDataForm stepDataForm)
    {
        int index = StepList.FindIndex(data => data.Seq == stepDataForm.Seq);
        if (index != -1)
        {
            StepList[index] = stepDataForm;  // 替换老数据
        }
        else
        {
            StepList.Add(stepDataForm); // 添加新数据
        }
    }











    /// <summary>
    /// 获取时间差值
    /// </summary>
    public int GetTimeDifference(DateTime startTime, DateTime endTime)
    {
        TimeSpan span = endTime.Subtract(startTime);
        double d = span.TotalSeconds;
        int time = (int)d;
        return time;
    }
    /// <summary>
    /// 时间戳转DateTime
    /// </summary>
    /// <param name="timeStamp"></param>
    /// <returns></returns>
    DateTime LongToDataTime(long timeStamp)
    {
        DateTime startTime = TimeZoneInfo.ConvertTime(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc), TimeZoneInfo.Local);
        DateTime dt = startTime.AddSeconds(timeStamp / 1000);
        return dt;
    }
    /// <summary>
    /// 获取当前时间戳,13位
    /// </summary>
    public static Int64 GetTimeStamp()
    {
        TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
        return Convert.ToInt64(ts.TotalMilliseconds);
    }


    #region Post和Get方式
    public void Post(string url, HttpWebCallBacks callback, JsonData fields = null, int timeout = 120000)
    {
        JsonData hender = new JsonData();
        hender["Authorization"] = "Bearer " + Access_token;
        hender["Content-Type"] = "application/json";
        hender["TENANT-ID"] = "1";
        hender["UC-USER"] = "1";
        Unity_Web.Web.PostRaw(url, callback, fields, hender, timeout);
    }

    public void LoginPost(string url, HttpWebCallBacks callback, JsonData fields = null, int timeout = 120000)
    {
        JsonData hender = new JsonData();
        hender["Authorization"] = "Basic dnI6dnI=";
        hender["Content-Type"] = "application/json";
        hender["TENANT-ID"] = "1";
        hender["UC-USER"] = "1";
        Unity_Web.Web.PostRaw(url, callback, fields, hender, timeout);
    }
    /// <summary>
    /// GET
    /// </summary>
    /// <param name="DataStr"></param>
    /// <param name="url"></param>
    /// <param name="Timeout"></param>
    /// <returns></returns>
    public void Get(string url, HttpWebCallBacks callback, JsonData fields = null, int timeout = 120000)
    {
        JsonData hender = new JsonData();
        hender["Authorization"] = "Bearer " + Access_token;
        hender["Content-Type"] = "application/json";
        hender["TENANT-ID"] = "1";
        hender["UC-USER"] = "1";
        Unity_Web.Web.Get(url, callback, fields, hender, timeout);
    }
    #endregion
}
