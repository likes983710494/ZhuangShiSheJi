﻿using LitJson;
using NPOI.SS.Formula.Functions;
using UnityEngine;
using Unit.DecorativeDesign;
using UnityEngine.EventSystems;
namespace Siccity.GLTFUtility
{
	public class GLTFTypeInfo : MonoBehaviour, IPointerClickHandler
	{
		public string mesg;
		public extrasA m_extrasA;
		public void OnPointerClick(PointerEventData eventData)//OnMouseDown()
		{

			if (m_extrasA != null)
			{
				string data = JsonMapper.ToJson(m_extrasA);
				extrasA extrasAData = m_extrasA;
				//查找父节点数据
				if (m_extrasA.ElementID == 0 || m_extrasA.UniqueId == null || m_extrasA.Parameters == null)
				{
					while (this.transform.parent)
					{
						if (this.transform.parent != null)
						{
							data = JsonMapper.ToJson(this.transform.parent.GetComponent<GLTFTypeInfo>().m_extrasA);
							extrasAData = this.transform.parent.GetComponent<GLTFTypeInfo>().m_extrasA;
							break;
						}
					}


				}


				//查看模型有闪烁，鼠标选模型时候 关闭闪烁
				if (DecorativeDesignSaveDate.GlintObject != null)
				{
					DecorativeDesignSaveDate.GlintObject.GetComponent<HighlightableObject>().FlashingOff();
				}

				//储存上一个 高亮的物体 并关闭
				if (DecorativeDesignSaveDate.HighligObject != null)
				{
					DecorativeDesignSaveDate.HighligObject.GetComponent<HighlightableObject>().ConstantOff();
				}
				if (DecorativeDesignSaveDate.HighligObjectMaterial != null)
				{

					if (DecorativeDesignSaveDate.HighligObjectMaterial != DecorativeDesignSaveDate.HighligObject.GetComponent<MeshRenderer>().material)
					{
						DecorativeDesignSaveDate.HighligObject.GetComponent<HighlightableObject>().enabled = false;//会锁住材质无法替换所以先关闭


						// 获取当前的材质数组
						Material[] currentMaterials = DecorativeDesignSaveDate.HighligObject.GetComponent<MeshRenderer>().materials;
						// 创建一个新的材质数组，长度为当前数组长度加1
						Material[] newMaterials = new Material[currentMaterials.Length + 1];
						// 将新材质放在数组的第一个位置
						newMaterials[0] = DecorativeDesignSaveDate.HighligObjectMaterial;
						// 将原来的材质复制到新数组中，从第二个位置开始
						for (int i = 0; i < currentMaterials.Length; i++)
						{
							newMaterials[i + 1] = currentMaterials[i];
						}
						// 将新的材质数组赋值给meshRenderer的materials属性
						DecorativeDesignSaveDate.HighligObject.GetComponent<MeshRenderer>().materials = newMaterials;
						DecorativeDesignSaveDate.HighligObject.GetComponent<HighlightableObject>().enabled = true;//换完材质再打开
					}
				}

				GetComponent<HighlightableObject>().ConstantOn(Color.cyan);//此方法打开边缘发光，参数可以控制发光的颜色
				DecorativeDesignSaveDate.HighligObject = this.gameObject;
				DecorativeDesignSaveDate.HighligObjectMaterial = this.gameObject.GetComponent<MeshRenderer>().material;

				//删除做法设计的图
				for (int i = 0; i < DecorativeDesignRight.Instance_.Content_做法说明_01选择做法.transform.childCount; i++)
				{
					int x = i;
					GameObject child = DecorativeDesignRight.Instance_.Content_做法说明_01选择做法.transform.GetChild(x).gameObject;
					Destroy(child);
				}
				//关闭所有步骤状态 开启步骤初始状态
				DecorativeDesignSaveDate.InitProcedure();
			}


			//获取数据
			if (m_extrasA != null)
			{
				string data = JsonMapper.ToJson(m_extrasA);
				extrasA extrasAData = m_extrasA;
				//查找父节点数据
				if (m_extrasA.ElementID == 0 || m_extrasA.UniqueId == null || m_extrasA.Parameters == null)
				{
					while (this.transform.parent)
					{
						if (this.transform.parent != null)
						{
							data = JsonMapper.ToJson(this.transform.parent.GetComponent<GLTFTypeInfo>().m_extrasA);
							extrasAData = this.transform.parent.GetComponent<GLTFTypeInfo>().m_extrasA;
							break;
						}
					}


				}



				//在标准数据下
				DecorativeDesignSaveDate.ElementID = extrasAData.ElementID.ToString();
				DecorativeDesignSaveDate.UniqueId = extrasAData.UniqueId;
				foreach (var t in extrasAData.Parameters)
				{
					if (t.GroupName == "尺寸标注")
					{
						foreach (var t2 in t.Parameters)
						{

							if (t2.name == "面积")
							{
								Debug.Log("面积:" + t2.value);
								DecorativeDesignSaveDate.Acreage = t2.value;
								DecorativeDesignRight.Instance_.InputFielArea.text = t2.value;

							}
						}
						break;
					}
					else
					{
						Debug.Log("无尺寸信息");
						DecorativeDesignSaveDate.Acreage = "无尺寸信息";
						DecorativeDesignRight.Instance_.InputFielArea.text = "无尺寸信息";
					}

				}
				Debug.Log(data + this.gameObject.name);

			}


		}
	}
}