using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragScript : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler//�̳��¼��ӿ�
{
	private  RectTransform rec;
	private Image img;//���϶�ui��Ʒ
	private Color tempColor;//��ų�ʼɫֵ
	//[SerializeField]
	Canvas canvas;//


	void Awake()
	{
		rec = GetComponent<RectTransform>();
		img = GetComponent<Image>();
		tempColor = img.color;//��ʼ��ɫ
		canvas=GameObject.Find("Canvas").GetComponent<Canvas>();
	}

	void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)//��ʼ�϶��¼�
	{
		tempColor.a = 0.5f;
		img.color = tempColor;//��קʱ����͸����
	}

	void IDragHandler.OnDrag(PointerEventData eventData)
	{
		rec.anchoredPosition += eventData.delta / canvas.scaleFactor;//��������϶���ֵ
		img.raycastTarget = false;
	}

	void IEndDragHandler.OnEndDrag(PointerEventData eventData)//��ק�����¼�
	{
		tempColor.a = 1;
		img.color = tempColor;
		img.raycastTarget = true;
	}

	



}

