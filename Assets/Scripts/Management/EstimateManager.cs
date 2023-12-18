using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Ͷ�ʹ���
/// </summary>
public class EstimateManager : MonoBehaviour
{
    public List<Transform> ParentTrms = new List<Transform>();
	public List<bool> isSet = new List<bool>();//����ÿ������

	public Button submitButton;
	public Button affirmButton;//ȷ�ϰ�ť
	public GameObject promptContentPlane;//��ʾ��常�ڵ�
	public Text promptContentText;//��ʾ����

	void Start()
    {

		submitButton.onClick.AddListener(SetParentAndTransform);
		affirmButton.onClick.AddListener(PromptContentAffirm);

	}

    // Update is called once per frame
    void Update()
    {
		if (ParentTrms[6].childCount > 0 && ParentTrms[5].childCount > 0 && ParentTrms[4].childCount > 0 && ParentTrms[3].childCount > 0 && ParentTrms[2].childCount > 0 && ParentTrms[1].childCount > 0 && ParentTrms[0].childCount > 0)
		{
			submitButton.interactable = true;
		}
		else
		{
			submitButton.interactable = false;
		}

	}
	/// <summary>
	/// �ύ
	/// </summary>
	public void SetParentAndTransform()
	{
		isSet.Clear();//���
		int number = 0;
		if (ParentTrms[0].GetChild(0) != null && ParentTrms[0].GetChild(0).name == ParentTrms[0].name && ParentTrms[1].childCount > 0)
		{
			// �������̷����ж��ǶԵ�
			isSet.Add(true);
		}
		else
		{
			isSet.Add(false);
		}
		if( ParentTrms[1].childCount>0)
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

		if ( ParentTrms[2].childCount > 0)
		{
			if (ParentTrms[2].GetChild(0).name == "�豸�����߹��÷Ѽ���װ��" || ParentTrms[2].GetChild(0).name == "�����ʽ�" || ParentTrms[2].GetChild(0).name == "��������")
			{
				//�����ʽ� �ж��ǶԵ�
				isSet.Add(true);
			}
			else
			{
				isSet.Add(false);
			}
		}
		if ( ParentTrms[3].childCount > 0)
		{
			if (ParentTrms[3].GetChild(0).name == "�豸�����߹��÷Ѽ���װ��" || ParentTrms[3].GetChild(0).name == "�����ʽ�" || ParentTrms[3].GetChild(0).name == "��������")
			{
				//�������� �ж��ǶԵ�
				isSet.Add(true);
			}
			else
			{
				isSet.Add(false);
			}
		}

		if (ParentTrms[4].childCount > 0)
		{
			if (ParentTrms[4].GetChild(0).name == "��������" || ParentTrms[4].GetChild(0).name == "װ�ι���" || ParentTrms[4].GetChild(0).name == "��װ����")
			{
				//�������� 
				isSet.Add(true);
			}
			else
			{
				isSet.Add(false);
			}
		}
		if (ParentTrms[5].childCount > 0)
		{
			if (ParentTrms[5].GetChild(0).name == "��������" || ParentTrms[5].GetChild(0).name == "װ�ι���" || ParentTrms[5].GetChild(0).name == "��װ����")
			{
				//װ�ι���
				isSet.Add(true);
			}
			else
			{
				isSet.Add(false);
			}
		}

		if (ParentTrms[6].childCount > 0)
		{
			if (ParentTrms[6].GetChild(0).name == "��������" || ParentTrms[6].GetChild(0).name == "װ�ι���" || ParentTrms[6].GetChild(0).name == "��װ����")
			{
				//��װ����
				isSet.Add(true);
			}
			else
			{
				isSet.Add(false);
			}
		}

		if(ParentTrms[6].childCount > 0&& ParentTrms[5].childCount > 0 && ParentTrms[4].childCount > 0 && ParentTrms[3].childCount > 0 && ParentTrms[2].childCount > 0 && ParentTrms[1].childCount > 0 && ParentTrms[0].childCount > 0)
		{
			
			//�ύ�鿴�Ƿ�ϸ�
			for (int i = 0; i < isSet.Count; i++)
			{
				if (isSet[i] == false)
				{
					//����
					Debug.Log("����");
					break;
				}

				if (isSet[i] == true)
				{

					number++;
				}
				if (number == 7)
				{
					Debug.Log("����");
					
					submitButton.interactable = false;
					promptContentPlane.SetActive(true);
					promptContentText.text = "Ͷ�ʹ�����ȷ���뿪ʼ�޶�ֽ�";


				}

			}
		}
	
		
	}
	///
	public void PromptContentAffirm()
	{
	   //����ģ��
	   //�ύ�����Ϣ
	}
}
