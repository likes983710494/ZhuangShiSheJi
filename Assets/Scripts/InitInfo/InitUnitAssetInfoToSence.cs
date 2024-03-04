using System.Collections;
using System.Collections.Generic;
using Unit;
using UnityEngine;
using UnityEngine.UI;

//将接口的数据    导入到场景中
public class InitUnitAssetInfoToSence : MonoBehaviour
{// Start is called before the first frame update
    void Start()
    {
        D();
    }

    public void A()
    {

    }

    public void B()
    {

    }
    public void C()
    {

    }
    //装饰设计
    public void D()
    {

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
}
