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


			//获取数据
			if (m_extrasA != null)
			{
				string data = JsonMapper.ToJson(m_extrasA);
				extrasA extrasAData = m_extrasA;
				//改进 用递归查找
				if (m_extrasA.ElementID == 0 || m_extrasA.UniqueId == null || m_extrasA.Parameters == null)
				{
					if (this.transform.parent != null)
					{
						data = JsonMapper.ToJson(this.transform.parent.GetComponent<GLTFTypeInfo>().m_extrasA);
						extrasAData = this.transform.parent.GetComponent<GLTFTypeInfo>().m_extrasA;
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

							}
						}
						break;
					}
					else
					{
						Debug.Log("无尺寸信息");
					}

				}
				Debug.Log(data);

			}


		}
	}
}