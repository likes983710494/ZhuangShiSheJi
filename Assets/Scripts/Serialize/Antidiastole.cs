using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//限额分解

[System.Serializable]
public class Antidiastole
{

    public int index { get; set; }//序号
    public string name { get; set; }//分项名字
    public string departmentName { get; set; }//所属分部名字

    public float amount { get; set; }//金额
}
