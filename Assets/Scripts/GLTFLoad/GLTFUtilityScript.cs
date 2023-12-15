/* ========================================
*  Author：PlanesWalker Liu
*  Copyright Owner © PlanesWalker Liu.
* =========================================*/
using System;
using UnityEngine;
namespace PlanesWalker
{
    using Siccity.GLTFUtility;
    /// <summary>
    /// 加载GLB模型单例
    /// </summary>
    public class GLTFUtilityScript
    {
        private static GLTFUtilityScript instance;
        public static GLTFUtilityScript Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GLTFUtilityScript();
                }
                return instance;
            }

        }

        Action action;
        public GameObject obj;
        bool isCreate = true;

        /// <summary>
        /// 同步加载 gltf and glb （不建议使用）
        /// </summary>
        /// <param name="filepath">路径</param>
        /// <param name="action">回调函数</param>
        public void ImportGLTF(string filepath, Action action)
        {
            UnloadAndDestroy(obj);
            obj = Importer.LoadFromFile(filepath);
            action = this.action;
        }

        /// <summary>
        /// 异步加载 gltf and glb   
        /// </summary>
        /// <param name="filepath">路径</param>
        /// <param name="action">回调函数</param>
        public void ImportGLTFAsync(string filepath, Action<GameObject, AnimationClip[]> action)
        {
            if (!isCreate) return;
            isCreate = false;
            UnloadAndDestroy(obj);
            //action = this.action;
            Importer.LoadFromFileAsync(filepath, new ImportSettings(), action);
        }

        public void ImportGLTFAsync(string filepath)
        {
            if (!isCreate) return;
            isCreate = false;
            UnloadAndDestroy(obj);
            Importer.LoadFromFileAsync(filepath, new ImportSettings(), OnFinishAsync);
        }

        public void ImportGLTAsync()
        {
            obj = new GameObject();
        }

        /// <summary>
        /// 加载完后的回调
        /// </summary>
        /// <param name="result">加载出来的物体</param>
        /// <param name="clip"></param>
        void OnFinishAsync(GameObject result, AnimationClip[] clip)
        {
            obj = result;
            isCreate = true;
            Debug.Log("Finished importing " + result.name);
            if (action != null)
                action();
        }

        /// <summary>
        /// 删除卸载 所有加载过的模型 清除缓存
        /// </summary>
        public void UnloadAndDestroy(GameObject obj)
        {
            if (obj != null)
            {
                GameObject.Destroy(obj);
            }
            Resources.UnloadUnusedAssets();
        }

        // <summary>
        // 删除卸载 所有加载过的模型 清除缓存
        // </summary>
        //public void UnloadAndDestroyALL()
        //{
        //    Resources.UnloadUnusedAssets();

        //}

    }

}
