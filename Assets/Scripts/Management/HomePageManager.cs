using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
// ��ҳ 
/// </summary>

public class HomePageManager : MonoBehaviour
{

	public static HomePageManager Instance_;

	public Button Button_���������;
	public Button Button_Ͷ�ʹ���;
	public Button Button_�޶�ֽ�;
	public Button Button_װ�����;
	public Button Button_װ��Ч��չʾ;

	private void Awake()
	{
		if(Instance_==null)
		{
			Instance_ = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}
	void Start()
	{
		Button_���������.interactable = true;
		//Button_Ͷ�ʹ���.interactable = false;
		//Button_�޶�ֽ�.interactable = false;
		//Button_װ�����.interactable = false;
		//Button_װ��Ч��չʾ.interactable = false;

	}

	// Update is called once per frame
	void Update()
	{
		
	}
}
