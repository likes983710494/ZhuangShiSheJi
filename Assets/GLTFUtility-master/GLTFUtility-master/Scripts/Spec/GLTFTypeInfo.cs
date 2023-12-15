using LitJson;
using UnityEngine;

namespace Siccity.GLTFUtility
{
    public class GLTFTypeInfo: MonoBehaviour
    {
		public string mesg;
		public extrasA m_extrasA;
		public void OnMouseDown()
		{
			string data=JsonMapper.ToJson(m_extrasA);
			Debug.Log(data);
		}
	}
}