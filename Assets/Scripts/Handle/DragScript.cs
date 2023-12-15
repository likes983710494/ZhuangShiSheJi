using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragScript : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler//继承事件接口
{
	private  RectTransform rec;
	private Image img;//被拖动ui物品
	private Color tempColor;//存放初始色值
	//[SerializeField]
	Canvas canvas;//


	void Awake()
	{
		rec = GetComponent<RectTransform>();
		img = GetComponent<Image>();
		tempColor = img.color;//初始颜色
		canvas=GameObject.Find("Canvas").GetComponent<Canvas>();
	}

	void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)//开始拖动事件
	{
		tempColor.a = 0.5f;
		img.color = tempColor;//拖拽时降低透明度
	}

	void IDragHandler.OnDrag(PointerEventData eventData)
	{
		rec.anchoredPosition += eventData.delta / canvas.scaleFactor;//计算鼠标拖动差值
		img.raycastTarget = false;
	}

	void IEndDragHandler.OnEndDrag(PointerEventData eventData)//拖拽结束事件
	{
		tempColor.a = 1;
		img.color = tempColor;
		img.raycastTarget = true;
	}

	



}

