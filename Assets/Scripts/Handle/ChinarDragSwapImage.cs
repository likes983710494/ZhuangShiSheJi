// ========================================================
// 描述：Demo 02 —— 通过继承 + 接口实现 图片拖动替换位置。
// 作者：Chinar 
// 创建时间：2019-04-29 16:39:11
// 版 本：1.0
// ========================================================

using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace QmDreamer.UI
{
	/// <summary>
	/// 管理UI元素排序：使UI可通过拖动进行位置互换
	/// </summary>
    public class ChinarDragSwapImage : Button, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        private Transform beginParentTransform; //记录开始拖动时的父级对象        
		/// <summary>
		/// UI界面的顶层，这里我用的是 填入区
		/// (这个变量在开发中设置到单例中较好，不然每一个物品都会初始化查找
		/// GameObject.Find("填入区").transform;)
		/// </summary>
		private Transform topOfUiT;

		private Transform parentTr;



		protected override void Start()
        {
            base.Start();
            topOfUiT = GameObject.Find("填入区").transform;
        }


        public void OnBeginDrag(PointerEventData _)
        {
            if (transform.parent == topOfUiT) return;
            beginParentTransform = transform.parent;
            transform.SetParent(topOfUiT);
        }


        public void OnDrag(PointerEventData _)
        {
            transform.position = Input.mousePosition;
            if (transform.GetComponent<Image>().raycastTarget) transform.GetComponent<Image>().raycastTarget = false;
        }


        public void OnEndDrag(PointerEventData _)
        {
            GameObject go = _.pointerCurrentRaycast.gameObject;
			if (go.tag == "Grid") //如果当前拖动物体下是：格子 -（答案）时
            {
				
				SetPosAndParent(transform, go.transform);
                transform.GetComponent<Image>().raycastTarget = true;
            }
            else if (go.tag == "Good") //如果是交换的“答案”
            {
				
				//	SetPosAndParent(transform, go.transform.parent);   
				// go.transform.SetParent(topOfUiT);   //目标物品设置到 UI 顶层
				SetPosExchange(transform, go.transform);
				transform.GetComponent<Image>().raycastTarget = true;
				
			}
            else //其他任何情况，物体回归原始位置
            {
                SetPosAndParent(transform, beginParentTransform);
                transform.GetComponent<Image>().raycastTarget = true;
            }
        }


        /// <summary>
        /// 设置父物体，UI位置归正
        /// </summary>
        /// <param name="t">对象Transform</param>
        /// <param name="parent">要设置到的父级</param>
        private void SetPosAndParent(Transform t, Transform parent)
        {
            t.SetParent(parent);
            t.position = parent.position;
        }
		/// <summary>
		/// 如果是物体就互换
		/// </summary>
		/// <param name="t">自身</param>
		/// <param name="t2">交换的物体</param>
		private void SetPosExchange(Transform t, Transform t2)
		{
			parentTr = beginParentTransform;
			t.SetParent(t2.parent);
			t.position = t2.parent.position;
			t2.SetParent(parentTr);
			t2.position = parentTr.position;
		}
	}
}