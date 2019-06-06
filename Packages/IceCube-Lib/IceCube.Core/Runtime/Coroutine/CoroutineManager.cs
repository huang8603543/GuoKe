using UnityEngine;
using System.Collections;

namespace IceCube.Core
{
    public class CoroutineManager : TSingleton<CoroutineManager>
    {
        GameObject mCoroutineRootObj;

        private CoroutineManager() { }

        public void Initialize()
        {
            mCoroutineRootObj = new GameObject("___CoroutineRoot");
            UtilTool.ResetTransform(mCoroutineRootObj.transform);
            GameObject.DontDestroyOnLoad(mCoroutineRootObj);
        }

        public CoroutineHandler StartHandler(IEnumerator rIEnum)
        {
            var rCoroutineObj = UtilTool.CreateGameObject(mCoroutineRootObj, "cotoutine");
            CoroutineHandler rHandler = rCoroutineObj.ReceiveComponent<CoroutineHandler>();
            rHandler.StartHandler(rIEnum);
            return rHandler;
        }

        public Coroutine Start(IEnumerator rIEnum)
        {
            return StartHandler(rIEnum).Coroutine;
        }

        public void Stop(CoroutineHandler rCoroutineHandler)
        {
            if (rCoroutineHandler != null)
            {
                rCoroutineHandler.StopAllCoroutines();
                GameObject.DestroyImmediate(rCoroutineHandler.gameObject);
                rCoroutineHandler.Coroutine = null;
            }
            rCoroutineHandler = null;
        }
    }
}
