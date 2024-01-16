
using LitJson;
using Siccity.GLTFUtility;
using System.Collections.Generic;
using UnityEngine;


namespace PlanesWalker
{
	public class LoadGltf : MonoBehaviour
	{
		public Camera BackRoundCamera;
		public Transform ZeroParent;

		private void Start()
		{

			GLTFUtilityScript.Instance.ImportGLTFAsync(Application.streamingAssetsPath + "/NewProject.gltf", asd);
		}
		public void asd(GameObject result, AnimationClip[] clip)
		{
			result.transform.position = ZeroParent.position;
			result.transform.localScale = Vector3.one;
			result.transform.SetParent(ZeroParent);

			foreach (Transform child in result.transform.GetComponentsInChildren<Transform>(true))
			{
				if (child.GetComponent<Camera>() != null)
				{
					Debug.Log(child.name);
					child.GetComponent<Camera>().enabled = false;
				}
			}
			//模型加载完毕 关闭模型加载进度条
			DecorativeDesignModus.Instance_.模型等待进度条.SetActive(false);


		}
	}
}
