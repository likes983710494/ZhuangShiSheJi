using System.Collections;
using System.Collections.Generic;
using NPOI.Util;
using UnityEngine;
using UnityEngine.UI;
//储存  主要用来正在操作时候界面 装饰设计步骤  并不用过来储存历史数据
namespace Unit.DecorativeDesign
{
    public static class DecorativeDesignSaveDate
    {
        public static GameObject HighligObject;//保存点击的模型部件 1.用于取消高亮  2.也用来改变模型材质

        public static string departmentName;//所属分部名字
        public static string subentryName;//分项名字

        public static Image designImage;//工程设计
        public static Material designMaterial;//工程材料
        public static string designDesc;//工程设计说明 
        public static string Acreage;//面积 工程量
        public static string Price;//单价
        public static string Total;//合价
    }
}

