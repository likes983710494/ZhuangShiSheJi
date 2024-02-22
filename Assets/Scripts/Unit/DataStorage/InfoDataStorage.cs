// 创建一个可序列化的子弹类 Bullet.CS

using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

// 子弹类型枚举
public enum BulletType
{
    DirectAttack = 0,  // 直接攻击
    Phony,             // 假子弹
    Real,              // 真子弹弹
}

/// <summary>
/// 可序列化
/// </summary>
[Serializable]
public class InfoDataStorage : ScriptableObject
{

    // Bullet 类直接继承自 ScriptableObject

    // 子弹类型
    public BulletType bulletType = BulletType.DirectAttack;

    // 子弹速度
    public int speed = 10;

    // 伤害数值
    public int damage = 5;

    // 子弹关联的特效
    public List<GameObject> Obj = new List<GameObject>();
}
