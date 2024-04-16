using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System;
public class IdentityInfoNet : MonoBehaviour
{
    public static IdentityInfoNet Instance_ { get; private set; }

    [Header("任务id")]
    public string InfoId;//任务id
    [Header("任务书id")]
    public string taskBookId;
    [Header("任务书名称")]
    public string taskBookName;
    [Header("任务书地址")]
    public string taskBookUrl;

    [Header("资料数组")]
    public List<Resource> resList;

    [Header("模型id")]
    public string modelId;
    [Header("模型名字")]
    public string modelName;
    [Header("模型地址")]
    public string modelUrl;
    [Header("模型类型")]
    public string modelType;//模型类型
    [Header("学生姓名")]
    public string studentName;//学生姓名
    [Header("学生年级")]
    public string studenGrade;//年级
    [Header("学生班级")]
    public string studenClass;//班级
    [Header("学生学号")]
    public string studenUsername;//学号
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
}

[Serializable]
public class Resource
{
    [Header("资料id")]
    public string resourceId;
    [Header("资料名称")]
    public string resourceName;
    [Header("资料地址")]
    public string resourceUrl;

}