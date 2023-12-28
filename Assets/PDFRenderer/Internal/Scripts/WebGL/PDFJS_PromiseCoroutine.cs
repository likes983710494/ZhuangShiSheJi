/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using UnityEngine;
using System.Collections;
using System;

namespace Paroxe.PdfRenderer.WebGL
{
    public class PDFJS_PromiseCoroutine
    {
        private Action<bool, object> m_Action;
        private MonoBehaviour m_MonoBehaviour;
        private Func<PDFJS_PromiseCoroutine, IPDFJS_Promise, object, IEnumerator> m_Coroutine;
        private object m_Parameters;
        private IPDFJS_Promise m_Promise;

        public IPDFJS_Promise Promise
        {
            get { return m_Promise; }
        }

        public object Parameters
        {
            get { return m_Parameters; }
            set { m_Parameters = value; }
        }

        public PDFJS_PromiseCoroutine(MonoBehaviour monoBehaviour, IPDFJS_Promise promise, Func<PDFJS_PromiseCoroutine, IPDFJS_Promise, object, IEnumerator> coroutine, object parameters)
        {
            m_MonoBehaviour = monoBehaviour;
            m_Coroutine = coroutine;
            m_Parameters = parameters;

            m_Promise = promise;
        }

        public PDFJS_PromiseCoroutine SetThenAction(Action<bool, object> action)
        {
            m_Action = action;
            return this;
        }

        public void ExecuteThenAction(bool success, object result)
        {
            if (m_Action != null)
                m_Action.Invoke(success, result);
        }

        public void Start()
        {
            m_MonoBehaviour.StartCoroutine(m_Coroutine(this, m_Promise, m_Parameters));
        }
    }
}
