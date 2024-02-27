using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
using System;
#if UNITY_EDITOR
public class CreateAsset : Editor
{

    [MenuItem("CreateAsset/Asset")]
    static void Create()
    {
        // 实例化类  InfoDataStorage  ScriptableObject
        InfoDataStorage asset = ScriptableObject.CreateInstance<InfoDataStorage>();

        // 如果实例化 InfoDataStorage 类为空，返回
        if (!asset)
        {
            Debug.LogWarning("InfoDataStorage not found");
            return;
        }

        // 自定义资源保存路径
        string path = Application.dataPath + "/Scripts/Unit/UnitAsset";

        // 如果项目总不包含该路径，创建一个
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        //将类名 InfoDataStorage 转换为字符串
        //拼接保存自定义资源（.asset） 路径
        path = string.Format("Assets/Scripts/Unit/UnitAsset/{0}.asset", (typeof(InfoDataStorage).ToString()));

        // 生成自定义资源到指定路径
        AssetDatabase.CreateAsset(asset, path);
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }


    [MenuItem("CreateAsset/GetAsset")]
    static void GetAsset()
    {
        //读取 .asset 文件, 直接转换为 类  InfoDataStorage
        InfoDataStorage infoDataStorage_ = AssetDatabase.LoadAssetAtPath<InfoDataStorage>("Assets/Scripts/Unit/UnitAsset/InfoDataStorage.asset");

        // 打印保存的数据
        // Debug.Log("asset Type  :" + Enum.GetName(typeof(asset Type), asset .asset Type));
        // Debug.Log("Speed       :" + asset .speed);
        // Debug.Log("damage      :" + asset .damage);

    }
}
#endif