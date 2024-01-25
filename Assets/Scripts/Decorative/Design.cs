using System.Collections;
using System.Collections.Generic;
using iTextSharp.text;
using UnityEngine;
//装饰设计-做法说明

[System.Serializable]

public class Design
{
    public string departmentName { get; set; }//所属分部名字
    public string subentryName { get; set; }//分项名字
    public string location { get; set; }//位置
    public string designImagePath { get; set; }//工程设计
    public string designMaterialPath { get; set; }//工程材料
    public string designDesc { get; set; }//工程设计说明 
    public string Acreage { get; set; }//面积 工程量
    public string Price { get; set; }//单价
    public string Total { get; set; }//合价

}
