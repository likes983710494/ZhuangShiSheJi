using LitJson;
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

				if (m_extrasA.ElementID == 0 || m_extrasA.UniqueId == null || m_extrasA.Parameters == null)
				{
					if (this.transform.parent != null)
					{
						data = JsonMapper.ToJson(this.transform.parent.GetComponent<GLTFTypeInfo>().m_extrasA);

					}

				}
				Debug.Log(data);

			}


		}
	}
}