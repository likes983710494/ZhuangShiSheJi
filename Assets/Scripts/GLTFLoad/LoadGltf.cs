
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

			//GLTFUtilityScript.Instance.ImportGLTFAsync(Application.streamingAssetsPath + "/NewProject.gltf", asd);

			GLTFUtilityScript.Instance.ImportGLTFAsync(Application.streamingAssetsPath + "/Model" + "/装饰设计01.gltf", Initial);
		}
		public void Initial(GameObject result, AnimationClip[] clip)
		{
			result.transform.position = ZeroParent.position;
			result.transform.localScale = Vector3.one;
			result.transform.SetParent(ZeroParent);
			//设置相机脚本 
			result.AddComponent<CameraRotateAround>();
			result.GetComponent<CameraRotateAround>().targetPos = result.transform;
			result.GetComponent<CameraRotateAround>().AutoR = true;
			result.GetComponent<CameraRotateAround>().CursorEnable = true;

			// result.transform.parent.position = new Vector3(result.transform.parent.position.x, result.transform.parent.position.y - 50,
			// result.transform.parent.position.z);

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
			//装饰设计提示窗
			DecorativeDesignModus.Instance_.Image_通知提示框.SetActive(true);


		}
	}
}
