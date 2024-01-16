using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetHighligtObj : MonoBehaviour
{
    HighlightableObject highlightable;
    void Start()
    {
        highlightable = GetComponent<HighlightableObject>();
        highlightable.ConstantOn(Color.cyan);//此方法打开边缘发光，参数可以控制发光的颜色
        //highlightable.ConstantOff();//此方法关闭边缘发光
        // highlightable.FlashingOn();//此方法打开边缘闪烁发光，共有三个重载，第一个默认闪烁，第二个重载可以控制闪烁颜色，第三个重载可以控制颜色和闪烁间隔时间
        //highlightable.FlashingOff();//此方法关闭边缘闪烁发光
        // highlightable.FlashingOn(Color.red, Color.blue);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
