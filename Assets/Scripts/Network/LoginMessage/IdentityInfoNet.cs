using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System;
public class IdentityInfoNet : MonoBehaviour
{
    public static IdentityInfoNet Instance_ { get; private set; }

    public string InfoId;//任务id
    public string resourceId;
    public string modelId;
    public string resourceName;
    public string modelName;
    public string modelUrl;
    public string resourceUrl;
    public string modelType;//模型类型
    public string studentName;//学生姓名
    public string studenGrade;//年级
    public string studenClass;//班级
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

// [Serializable]
// class Resource{


// }