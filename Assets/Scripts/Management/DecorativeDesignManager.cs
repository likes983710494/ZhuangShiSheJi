using System.Collections;
using System.Collections.Generic;
//using System.Windows.Forms;
using UnityEngine;
using UnityEngine.UI;

public class DecorativeDesignManager : MonoBehaviour
{
	public DecorativeDesignLeft DecorativeDesignLeft_;
	public DecorativeDesignRight DecorativeDesignRight_;

	public  Button Button_装饰设计;
	void Start()
    {
		Button_装饰设计.onClick.AddListener(() =>
		{
			DecorativeDesignLeft_.SetAllocationMoney();
		});

	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
