/* ========================================
*  Author：PlanesWalker Liu
*  Copyright Owner © PlanesWalker Liu.
* =========================================*/
using LitJson;
using Siccity.GLTFUtility;
using System.Collections.Generic;
using UnityEngine;

namespace PlanesWalker
{
    public class LoadGltf : MonoBehaviour
    {
        private void Start()
        {

            GLTFUtilityScript.Instance.ImportGLTFAsync(Application.streamingAssetsPath + "/NewProject.gltf", asd);
        }
        public void asd(GameObject result, AnimationClip[] clip)
        {
          
        }
    }
}
