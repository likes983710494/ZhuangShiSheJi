using LitJson;
using NPOI.SS.Formula.Functions;
using UnityEngine;

namespace Siccity.GLTFUtility
{
	public class GLTFTypeInfo : MonoBehaviour
	{
		public string mesg;
		public extrasA m_extrasA;
		public void OnMouseDown()
		{
			if (m_extrasA != null)
			{
				string data = JsonMapper.ToJson(m_extrasA);
				extrasA extrasAData = m_extrasA;
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