// 创建一个可序列化的子弹类 Bullet.CS

using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;


// public enum BulletType
// {
//     DirectAttack = 0,  // 直接攻击
//     Phony,             // 假子弹
//     Real,              // 真子弹弹
// }

/// <summary>
/// 可序列化  本地缓存和接口样式
/// </summary>
[Serializable]
public class InfoDataStorage : ScriptableObject
{

    // Bullet 类直接继承自 ScriptableObject

    // 子弹类型
    // public BulletType bulletType = BulletType.DirectAttack;

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

    public List<Design> 楼地面_DesignsList = new List<Design>();
    public List<Design> 墙柱面_DesignsList = new List<Design>();
    public List<Design> 天棚工程_DesignsList = new List<Design>();
    public List<Design> 油漆涂料_DesignsList = new List<Design>();
    public List<Design> 其他装饰_DesignsList = new List<Design>();
    public bool isFinishDesign;//完成装饰设计
}
