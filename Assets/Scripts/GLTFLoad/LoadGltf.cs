
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
			if (InvokInfoDataStorage.Instance_.isNet == false)
			{
				Debug.Log("不进行接口请求状态");
				GLTFUtilityScript.Instance.ImportGLTFAsync(Application.streamingAssetsPath + "/Model" + "/装饰设计01.gltf", Initial);
			}
			else
			{
				if (Unit.UnitDollarData.ObjName == null)
				{
					Debug.Log("再次登录未下载模型+" + InvokInfoDataStorage.Instance_.infoDataStorage_.ObjName);
					GLTFUtilityScript.Instance.ImportGLTFAsync(Application.streamingAssetsPath + "/Model/" + InvokInfoDataStorage.Instance_.infoDataStorage_.ObjName, Initial);
				}
				else
				{

					Debug.Log("第一次登录下载模型+" + Unit.UnitDollarData.ObjName);
					GLTFUtilityScript.Instance.ImportGLTFAsync(Application.streamingAssetsPath + "/Model/" + Unit.UnitDollarData.ObjName, Initial);
				}

			}


			DecorativeDesignModus.Instance_.ModelMenu_UI.SetActive(false);
		}
		public void Initial(GameObject result, AnimationClip[] clip)
		{
			result.transform.position = ZeroParent.position;
			result.transform.localScale = Vector3.one;
			result.transform.SetParent(ZeroParent);
			//设置浏览模式相机脚本 
			result.AddComponent<CameraRotateAround>();
			result.GetComponent<CameraRotateAround>().targetPos = result.transform;
			//result.GetComponent<CameraRotateAround>().AutoR = true;
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
				//child.GetComponent<MeshRenderer>().material = material_;


				Material[] currentMaterials = child.GetComponent<MeshRenderer>().materials; ;
				// 创建一个新的材质数组，长度为当前数组长度1
				Material[] newMaterials = new Material[currentMaterials.Length + 1];
				Debug.Log("当前材质数组长度" + currentMaterials.Length + "//" + newMaterials.Length);
				// // 将原数组除了第一个元素之外的其他元素复制到新数组中
				for (int i = 0; i < currentMaterials.Length; i++)
				{

					newMaterials[i + 1] = currentMaterials[i];
					Debug.Log("保留材质" + i + newMaterials[i]);
				}
				newMaterials[0] = material_;

				child.GetComponent<MeshRenderer>().materials = newMaterials;


			}

		}
	}


}
