using System.Collections;
using System.Collections.Generic;
using iTextSharp.text;
using UnityEngine;
//装饰设计-做法说明

[System.Serializable]

public class Design
{
    public string departmentName;//所属分部名字
    public string subentryName;//分项名字

    public string designImagePath;//工程设计
    public string designMaterialPath;//工程材料
    public string designDesc;//工程设计说明 
    public string Acreage;//面积 工程量
    public string Price;//单价
    public string Total;//合价

}
