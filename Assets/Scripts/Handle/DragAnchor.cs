
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragAnchor : MonoBehaviour, IDropHandler//�̳��¼��ӿ�
{
	void IDropHandler.OnDrop(PointerEventData eventData)
	{
		Debug.Log("eventData������"+ eventData.pointerDrag.name);
		eventData.pointerDrag.GetComponent<RectTransform>
		().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
		//���϶�ui�����ê��λ��==���ê��λ��
	}

	
}
