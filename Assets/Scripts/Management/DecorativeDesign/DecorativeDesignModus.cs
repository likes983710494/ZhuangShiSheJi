using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//装饰设计-做法说明
public class DecorativeDesignModus : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DecorativeDesignRight.Instance_.ButtonModus.onClick.AddListener(() =>
        {


        });
    }

    // Update is called once per frame
    void Update()
    {

    }
}
