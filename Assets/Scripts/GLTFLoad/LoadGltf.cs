
using LitJson;
using Siccity.GLTFUtility;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

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

			DecorativeDesignModus.Instance_.ModelMenu_UI.SetActive(false);
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


			foreach (Transform child in result.transform.GetComponentsInChildren<Transform>(true))
			{

				if (child.GetComponent<Camera>() != null)
				{
					Debug.Log(child.name);
					child.GetComponent<Camera>().enabled = false;
				}
				//如果elementiD和UniqueId相同 ， 符合缓存上的数据
				if (child.GetComponent<GLTFTypeInfo>() != null)
				{
					//child.GetComponent<GLTFTypeInfo>().mesg = child.name;
					//给墙柱面添加本地缓存或者接口材质
					GetDesignsListData(child, InvokInfoDataStorage.Instance_.infoDataStorage_.decorativeDesignManagerData.楼地面_DesignsList);
					GetDesignsListData(child, InvokInfoDataStorage.Instance_.infoDataStorage_.decorativeDesignManagerData.墙柱面_DesignsList);
					GetDesignsListData(child, InvokInfoDataStorage.Instance_.infoDataStorage_.decorativeDesignManagerData.天棚工程_DesignsList);
					GetDesignsListData(child, InvokInfoDataStorage.Instance_.infoDataStorage_.decorativeDesignManagerData.油漆涂料_DesignsList);
					GetDesignsListData(child, InvokInfoDataStorage.Instance_.infoDataStorage_.decorativeDesignManagerData.其他装饰_DesignsList);


				}

			}
			//模型加载完毕 关闭模型加载进度条
			DecorativeDesignModus.Instance_.模型等待进度条.SetActive(false);
			//装饰设计提示窗
			DecorativeDesignModus.Instance_.Image_通知提示框.SetActive(true);
			//模型出来 展示菜单
			DecorativeDesignModus.Instance_.ModelMenu_UI.SetActive(true);

		}
		private void GetDesignsListData(Transform child, List<Design> DesignsList_)
		{
			if (DesignsList_.Count > 0)
			{
				for (int i = 0; i < DesignsList_.Count; i++)
				{

					if (child.GetComponent<GLTFTypeInfo>().m_extrasA != null)
					{

						if (child.GetComponent<GLTFTypeInfo>().m_extrasA.ElementID.ToString()
						== DesignsList_[i].ElementID
						&&
						child.GetComponent<GLTFTypeInfo>().m_extrasA.UniqueId.ToString()
						== DesignsList_[i].UniqueId
						)
						{
							//1.模型更换材质

							child.GetComponent<HighlightableObject>().enabled = false;//会锁住材质无法替换所以先关闭
							StopCoroutine("MaterialA");
							StartCoroutine(MaterialA(DesignsList_[i].designMaterialPath, child));
							child.GetComponent<HighlightableObject>().enabled = true;//在开启

							//2.底部数据更换
							//3.弹窗数据赋值更换
						}
					}


				}
			}
		}

		private IEnumerator MaterialA(string imageName, Transform child)
		{
			string filePath = System.IO.Path.Combine(Application.streamingAssetsPath + "/做法分类02-选择材质/", imageName + ".jpg");
			string url = "file://" + filePath;
			UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(url);
			yield return uwr.SendWebRequest();

			if (uwr.result != UnityWebRequest.Result.Success)
			{
				Debug.LogError("Failed to load texture: " + uwr.error);
			}
			else
			{
				Texture2D texture = DownloadHandlerTexture.GetContent(uwr);
				// 在这里使用 texture，例如将其应用到某个材质上
				Material material_ = new Material(Shader.Find("Unlit/Texture"));
				material_.name = imageName;
				material_.SetTexture("_MainTex", texture);
				child.GetComponent<MeshRenderer>().material = material_;
			}

		}
	}


}
