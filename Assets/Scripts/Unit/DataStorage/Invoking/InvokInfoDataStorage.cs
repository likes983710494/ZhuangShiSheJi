using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;
using System.IO;
//调用本地数据缓存*--公共类
public class InvokInfoDataStorage : MonoBehaviour
{

    public static InvokInfoDataStorage Instance_;
    public InfoDataStorage infoDataStorage_;
    void Awake()
    {

        if (Instance_ == null)
        {
            Instance_ = this;
        }
        else
        {
            Destroy(gameObject);
        }
        infoDataStorage_ = AssetDatabase.LoadAssetAtPath<InfoDataStorage>("Assets/Scripts/Unit/UnitAsset/InfoDataStorage.asset");

        //本地初始化缓存 先调用一次
    }


}
