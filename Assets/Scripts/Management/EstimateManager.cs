using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EstimateManager : MonoBehaviour
{
    public List<Transform> ParentTrms = new List<Transform>();
	public List<bool> isSet = new List<bool>();//����ÿ������

	public Button submitButton;

	void Start()
    {
		

	}

    // Update is called once per frame
    void Update()
    {
        
    }
	public void SetParentAndTransform()
	{
		isSet.Clear();//���
		if (ParentTrms[0].GetChild(0) != null && ParentTrms[0].GetChild(0).name == ParentTrms[0].name)
		{
			// �������̷����ж��ǶԵ�
			isSet.Add(true);
		}
		else
		{
			isSet.Add(false);
		}
		if(ParentTrms[1].GetChild(0) != null)
		{
			if(ParentTrms[1].GetChild(0).name== "�豸�����߹��÷Ѽ���װ��" || ParentTrms[1].GetChild(0).name == "�����ʽ�" || ParentTrms[1].GetChild(0).name == "��������")
			{
				//�豸�����߹��÷Ѽ���װ�� �ж��ǶԵ�
				isSet.Add(true);
			}
			else
			{
				isSet.Add(false);
			}
		}

		if (ParentTrms[2].GetChild(0) != null)
		{
			if (ParentTrms[1].GetChild(0).name == "�豸�����߹��÷Ѽ���װ��" || ParentTrms[1].GetChild(0).name == "�����ʽ�" || ParentTrms[1].GetChild(0).name == "��������")
			{
				//�����ʽ� �ж��ǶԵ�
				isSet.Add(true);
			}
			else
			{
				isSet.Add(false);
			}
		}
		if (ParentTrms[3].GetChild(0) != null)
		{
			if (ParentTrms[1].GetChild(0).name == "�豸�����߹��÷Ѽ���װ��" || ParentTrms[1].GetChild(0).name == "�����ʽ�" || ParentTrms[1].GetChild(0).name == "��������")
			{
				//�������� �ж��ǶԵ�
				isSet.Add(true);
			}
			else
			{
				isSet.Add(false);
			}
		}
		for (int i = 0; i < isSet.Count; i++)
		{
			if (isSet[i] == false) {
				//����
				Debug.Log("����");
				break;
			}
				
		}
		
	}
}
