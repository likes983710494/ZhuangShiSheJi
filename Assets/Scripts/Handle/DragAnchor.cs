
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragAnchor : MonoBehaviour, IDropHandler//继承事件接口
{
	void IDropHandler.OnDrop(PointerEventData eventData)
	{
		Debug.Log("eventData：：："+ eventData.pointerDrag.name);
		eventData.pointerDrag.GetComponent<RectTransform>
		().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
		//被拖动ui物体的锚点位置==插槽锚点位置
	}

	
}
