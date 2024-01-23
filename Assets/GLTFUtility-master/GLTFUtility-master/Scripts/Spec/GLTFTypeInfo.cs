using LitJson;
using NPOI.SS.Formula.Functions;
using UnityEngine;
using Unit.DecorativeDesign;

namespace Siccity.GLTFUtility
{
	public class GLTFTypeInfo : MonoBehaviour
	{
		public string mesg;
		public extrasA m_extrasA;
		public void OnMouseDown()
		{
			//储存上一个 高亮的物体 并关闭
			if (DecorativeDesignSaveDate.HighligObject != null)
			{
				DecorativeDesignSaveDate.HighligObject.GetComponent<HighlightableObject>().ConstantOff();
			}
			GetComponent<HighlightableObject>().ConstantOn(Color.cyan);//此方法打开边缘发光，参数可以控制发光的颜色
			DecorativeDesignSaveDate.HighligObject = this.gameObject;

			//关闭所有步骤状态 开启步骤初始状态
			DecorativeDesignSaveDate.InitProcedure();


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