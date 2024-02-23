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
/// 可序列化
/// </summary>
[Serializable]
public class InfoDataStorage : ScriptableObject
{

    // Bullet 类直接继承自 ScriptableObject

    // 子弹类型
    // public BulletType bulletType = BulletType.DirectAttack;

    // 子弹速度
    public DataDownloadManagerData dataDownloadManagerData;

}


//----设计任务书 数据缓存-----
[Serializable]
public class DataDownloadManagerData
{
    public bool isDataPDF;//完成阅读pdf
    public bool isDataObj;//完成下载模型
    public bool isFinishDataDownload;//设计书模块状态---阅读pdf、下载模型两个状态都完毕，设计书模块状态为true
}
