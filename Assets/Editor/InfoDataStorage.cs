// 创建一个可序列化的类 

using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;


// public enum BulletType
// {
//     DirectAttack = 0,  
//     Phony,             
//     Real,              
// }

/// <summary>
/// 可序列化  本地缓存和接口样式
/// </summary>
[Serializable]
public class InfoDataStorage : ScriptableObject
{

    // InfoDataStorage 类直接继承自 ScriptableObject


    //登录时间
    public string LoginTime;
    //登录时长
    public string LoginDuration;

    // 设计任务书
    public DataDownloadManagerData dataDownloadManagerData;
    //投资估算
    public EstimateManagerData estimateManagerData;
    //限额分解
    public AntidiastoleManagerData antidiastoleManagerData;
    //装饰设计
    public DecorativeDesignManagerData decorativeDesignManagerData;
    //装饰效果展示
    public ResultManagerData resultManagerData;

}


//----设计任务书 数据缓存-----
[Serializable]
public class DataDownloadManagerData
{
    public bool isDataPDF;//完成阅读pdf
    public bool isDataObj;//完成下载模型
    public bool isFinishDataDownload;//设计书模块状态---阅读pdf、下载模型两个状态都完毕，设计书模块状态为true
}

//----投资估算 数据缓存-----
[Serializable]
public class EstimateManagerData
{
    public string DeductionNumber;//投资估算 当前扣分
    public string EstimateNumber;//投资估算 当前得分
    public bool isFinishEstimate;//完成估算
}
//----*限额分解  数据缓存
[Serializable]
public class AntidiastoleManagerData
{
    public string[] AmountInputField; //分数从左到右 第一层金额
    public List<string> 楼地面装饰InputField;//分数从左到右
    public List<string> 墙柱面装饰InputField;//分数从左到右
    public List<string> 天棚工程InputField;//分数从左到右
    public List<string> 油漆涂料InputField;//分数从左到右
    public List<string> 其他装饰InputField;//分数从左到右

    public bool isFinishAntidiastole;//完成限额分解

}

//----*装饰设计 数据缓存
[Serializable]
public class DecorativeDesignManagerData
{

    //模型材质
    public List<Design> 楼地面_DesignsList = new List<Design>();
    public List<Design> 墙柱面_DesignsList = new List<Design>();
    public List<Design> 天棚工程_DesignsList = new List<Design>();
    public List<Design> 油漆涂料_DesignsList = new List<Design>();
    public List<Design> 其他装饰_DesignsList = new List<Design>();
    //底部  配额数据
    public string 楼地面_配额;//楼地面配额
    public string 墙柱面_配额;//墙柱面配额
    public string 天棚工程_配额;//天棚工程配额
    public string 油漆涂料_配额;//油漆涂料配额
    public string 其他装饰_配额;//其他装饰配额

    //底部 金额数据
    public string 楼地面_汇总金额;//楼地面汇总金额
    public string 墙柱面_汇总金额;//墙柱面汇总金额
    public string 天棚工程_汇总金额;//天棚工程汇总金额
    public string 油漆涂料_汇总金额;//油漆涂料汇总金额
    public string 其他装饰_汇总金额;//其他装饰汇总金额
    public bool isFinishDesign;//完成装饰设计
}
//----装饰效果战术 数据缓存
[Serializable]
public class ResultManagerData
{
    public static List<ImagePath> ImagePathList = new List<ImagePath>();//图片路径
    public static bool isFinishResult;//完成装饰效果
}