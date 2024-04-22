using System.Collections;
using System.Collections.Generic;
using Unit;
using UnityEngine;
using UnityEngine.UI;

//将接口的数据    导入到场景中
public class InitUnitAssetInfoToSence : MonoBehaviour
{//在每个模块前，接口直接把数据赋值给InvokInfoDataStorage.Instance_.infoDataStorage_即可


    void Start()
    {
        Getinfo();
        State();
        B();
        C();
        D();
        E();
    }
    void Getinfo()
    {
        if (InvokInfoDataStorage.Instance_.isNet == false)
        {
            Debug.Log("不进行接口请求状态");
        }
        else
        {
            //获取任务信息
            LoginMessage.Instance_.InfoRequest(AppUrlConfig.taskplan);
        }

    }
    void State()
    {

        if (InvokInfoDataStorage.Instance_.infoDataStorage_.dataDownloadManagerData.isFinishDataDownload == true)
        {
            HomePageManager.Instance_.Button_设计任务书.interactable = true;
        }
        if (InvokInfoDataStorage.Instance_.infoDataStorage_.estimateManagerData.isFinishEstimate == true)
        {
            HomePageManager.Instance_.Button_投资估算.interactable = true;
        }
        if (InvokInfoDataStorage.Instance_.infoDataStorage_.antidiastoleManagerData.isFinishAntidiastole == true)
        {
            HomePageManager.Instance_.Button_限额分解.interactable = true;
        }
        if (InvokInfoDataStorage.Instance_.infoDataStorage_.decorativeDesignManagerData.isFinishDesign == true)
        {
            HomePageManager.Instance_.Button_装饰设计.interactable = true;
        }
        if (InvokInfoDataStorage.Instance_.infoDataStorage_.resultManagerData.isFinishResult == true)
        {
            HomePageManager.Instance_.Button_装饰效果展示.interactable = true;
        }
    }
    //任务设计书
    public void A()
    {

    }
    //投资估算
    public void B()
    {
        Unit.UnitDollarData.DeductionNumber = int.Parse(InvokInfoDataStorage.Instance_.infoDataStorage_.estimateManagerData.DeductionNumber);

    }
    //限额分解
    public void C()
    {
        // AntidiastoleManager.Instance_.总金额_Amount="";

        //分部金额
        AntidiastoleManager.Instance_.Amount_InputField[0].text =
        InvokInfoDataStorage.Instance_.infoDataStorage_.antidiastoleManagerData.AmountInputField[0].ToString();
        AntidiastoleManager.Instance_.Amount_InputField[1].text =
       InvokInfoDataStorage.Instance_.infoDataStorage_.antidiastoleManagerData.AmountInputField[1].ToString();
        AntidiastoleManager.Instance_.Amount_InputField[2].text =
        InvokInfoDataStorage.Instance_.infoDataStorage_.antidiastoleManagerData.AmountInputField[2].ToString();
        AntidiastoleManager.Instance_.Amount_InputField[3].text =
       InvokInfoDataStorage.Instance_.infoDataStorage_.antidiastoleManagerData.AmountInputField[3].ToString();
        AntidiastoleManager.Instance_.Amount_InputField[4].text =
        InvokInfoDataStorage.Instance_.infoDataStorage_.antidiastoleManagerData.AmountInputField[4].ToString();
        //分项金额
        for (int i = 0; i < AntidiastoleManager.Instance_.楼地面装饰InputField.Count; i++)
        {
            AntidiastoleManager.Instance_.楼地面装饰InputField[i].text =
             InvokInfoDataStorage.Instance_.infoDataStorage_.antidiastoleManagerData.楼地面装饰InputField[i].ToString();
        }

        for (int i = 0; i < AntidiastoleManager.Instance_.墙柱面装饰InputField.Count; i++)
        {
            AntidiastoleManager.Instance_.墙柱面装饰InputField[i].text =
             InvokInfoDataStorage.Instance_.infoDataStorage_.antidiastoleManagerData.墙柱面装饰InputField[i].ToString();
        }

        for (int i = 0; i < AntidiastoleManager.Instance_.天棚工程InputField.Count; i++)
        {
            AntidiastoleManager.Instance_.天棚工程InputField[i].text =
             InvokInfoDataStorage.Instance_.infoDataStorage_.antidiastoleManagerData.天棚工程InputField[i].ToString();
        }

        for (int i = 0; i < AntidiastoleManager.Instance_.油漆涂料InputField.Count; i++)
        {
            AntidiastoleManager.Instance_.油漆涂料InputField[i].text =
             InvokInfoDataStorage.Instance_.infoDataStorage_.antidiastoleManagerData.油漆涂料InputField[i].ToString();
        }

        for (int i = 0; i < AntidiastoleManager.Instance_.其他装饰InputField.Count; i++)
        {
            AntidiastoleManager.Instance_.其他装饰InputField[i].text =
             InvokInfoDataStorage.Instance_.infoDataStorage_.antidiastoleManagerData.其他装饰InputField[i].ToString();
        }

        //提交按钮状态 
        AntidiastoleManager.Instance_.InfoDataStorage_GetAntidiastoleState();

    }
    //装饰设计
    public void D()
    {


        //装饰设计
        DecorativeDesignLeft.Instance_.CollectList[0].text = InvokInfoDataStorage.Instance_.infoDataStorage_.decorativeDesignManagerData.楼地面_汇总金额;
        DecorativeDesignLeft.Instance_.CollectList[1].text = InvokInfoDataStorage.Instance_.infoDataStorage_.decorativeDesignManagerData.墙柱面_汇总金额;
        DecorativeDesignLeft.Instance_.CollectList[2].text = InvokInfoDataStorage.Instance_.infoDataStorage_.decorativeDesignManagerData.天棚工程_汇总金额;
        DecorativeDesignLeft.Instance_.CollectList[3].text = InvokInfoDataStorage.Instance_.infoDataStorage_.decorativeDesignManagerData.油漆涂料_汇总金额;
        DecorativeDesignLeft.Instance_.CollectList[4].text = InvokInfoDataStorage.Instance_.infoDataStorage_.decorativeDesignManagerData.其他装饰_汇总金额;

        DecorativeDesignLeft.Instance_.AllocationList[0].text = InvokInfoDataStorage.Instance_.infoDataStorage_.decorativeDesignManagerData.楼地面_配额;
        DecorativeDesignLeft.Instance_.AllocationList[1].text = InvokInfoDataStorage.Instance_.infoDataStorage_.decorativeDesignManagerData.墙柱面_配额;
        DecorativeDesignLeft.Instance_.AllocationList[2].text = InvokInfoDataStorage.Instance_.infoDataStorage_.decorativeDesignManagerData.天棚工程_配额;
        DecorativeDesignLeft.Instance_.AllocationList[3].text = InvokInfoDataStorage.Instance_.infoDataStorage_.decorativeDesignManagerData.油漆涂料_配额;
        DecorativeDesignLeft.Instance_.AllocationList[4].text = InvokInfoDataStorage.Instance_.infoDataStorage_.decorativeDesignManagerData.其他装饰_配额;

        DecorativeDesignLeft.Instance_.BottomButtonMoreLsit[0].transform.GetChild(0).GetComponent<Text>().text =
         InvokInfoDataStorage.Instance_.infoDataStorage_.decorativeDesignManagerData.楼地面_DesignsList.Count.ToString() + "条";
        DecorativeDesignLeft.Instance_.BottomButtonMoreLsit[1].transform.GetChild(0).GetComponent<Text>().text =
        InvokInfoDataStorage.Instance_.infoDataStorage_.decorativeDesignManagerData.墙柱面_DesignsList.Count.ToString() + "条";
        DecorativeDesignLeft.Instance_.BottomButtonMoreLsit[2].transform.GetChild(0).GetComponent<Text>().text =
         InvokInfoDataStorage.Instance_.infoDataStorage_.decorativeDesignManagerData.天棚工程_DesignsList.Count.ToString() + "条";
        DecorativeDesignLeft.Instance_.BottomButtonMoreLsit[3].transform.GetChild(0).GetComponent<Text>().text =
        InvokInfoDataStorage.Instance_.infoDataStorage_.decorativeDesignManagerData.油漆涂料_DesignsList.Count.ToString() + "条";
        DecorativeDesignLeft.Instance_.BottomButtonMoreLsit[4].transform.GetChild(0).GetComponent<Text>().text =
        InvokInfoDataStorage.Instance_.infoDataStorage_.decorativeDesignManagerData.其他装饰_DesignsList.Count.ToString() + "条";
        // 将本地数据也 赋值给 场景中的操作数据
        UnitDollarData.楼地面_DesignsList = InvokInfoDataStorage.Instance_.infoDataStorage_.decorativeDesignManagerData.楼地面_DesignsList;
        UnitDollarData.墙柱面_DesignsList = InvokInfoDataStorage.Instance_.infoDataStorage_.decorativeDesignManagerData.墙柱面_DesignsList;
        UnitDollarData.天棚工程_DesignsList = InvokInfoDataStorage.Instance_.infoDataStorage_.decorativeDesignManagerData.天棚工程_DesignsList;
        UnitDollarData.油漆涂料_DesignsList = InvokInfoDataStorage.Instance_.infoDataStorage_.decorativeDesignManagerData.油漆涂料_DesignsList;
        UnitDollarData.其他装饰_DesignsList = InvokInfoDataStorage.Instance_.infoDataStorage_.decorativeDesignManagerData.其他装饰_DesignsList;
        DecorativeDesignManager.Instance_.楼地面_DesignsList = InvokInfoDataStorage.Instance_.infoDataStorage_.decorativeDesignManagerData.楼地面_DesignsList;
        DecorativeDesignManager.Instance_.墙柱面_DesignsList = InvokInfoDataStorage.Instance_.infoDataStorage_.decorativeDesignManagerData.墙柱面_DesignsList;
        DecorativeDesignManager.Instance_.天棚工程_DesignsList = InvokInfoDataStorage.Instance_.infoDataStorage_.decorativeDesignManagerData.天棚工程_DesignsList;
        DecorativeDesignManager.Instance_.油漆涂料_DesignsList = InvokInfoDataStorage.Instance_.infoDataStorage_.decorativeDesignManagerData.油漆涂料_DesignsList;
        DecorativeDesignManager.Instance_.其他装饰_DesignsList = InvokInfoDataStorage.Instance_.infoDataStorage_.decorativeDesignManagerData.其他装饰_DesignsList;
    }
    //装饰效果展示
    void E()
    {
        Unit.UnitDollarData.ImagePathList = InvokInfoDataStorage.Instance_.infoDataStorage_.resultManagerData.ImagePathList;
    }
}
