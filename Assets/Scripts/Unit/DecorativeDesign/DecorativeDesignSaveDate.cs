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

        /************************InitProcedure*******************************/
        public static Dropdown DropdownBranch;//分部下拉框 
        public static Dropdown DropdownSubentry;//分项 数据
        public static Button ButtonModu;//做法说明按钮
        public static InputField InputFielArea;// 输入框 工程量  点击模型后自动获取到工程量并显示
        public static InputField InputFielPrice;// 输入框 单价
        public static InputField InputFielTotal;// 输入框 合价


        public static void InitProcedure()
        {
            //显示状态
            DropdownBranch.interactable = true;
            DropdownSubentry.interactable = false;
            ButtonModu.interactable = false;
            InputFielArea.interactable = false;
            InputFielPrice.interactable = true;
            InputFielTotal.interactable = false;
            //初始化步骤显示
            DropdownBranch.value = 0;
            DropdownSubentry.value = 0;
            InputFielArea.placeholder.GetComponent<Text>().text = "请选择模型";
            InputFielPrice.placeholder.GetComponent<Text>().text = "请输入金额";
            InputFielTotal.placeholder.GetComponent<Text>().text = "";
            DecorativeDesignModus.Instance_.LeftMakerPlan.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 630f);
            DecorativeDesignModus.Instance_.LeftMakerUnfoldButton.interactable = false;
            //  单个步骤确认按钮
            GameObject.Find("----------装饰设计--------").GetComponent<DecorativeDesignManager>().Button_确认.interactable = false;

        }
    }
}

