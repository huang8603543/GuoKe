using System;
using UnityEngine;

namespace IceCube.Core
{
    public static class UtilTool
    {
        public static void ResetTransform(Transform transform)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }

        #region CreateGameObject

        public static GameObject CreateGameObject(string rName, params Type[] rComps)
        {
            GameObject rGo = new GameObject(rName, rComps);
            ResetTransform(rGo.transform);

            return rGo;
        }

        public static GameObject CreateGameObject(GameObject rParentGo, string rName, params Type[] rComps)
        {
            GameObject rGo = new GameObject(rName, rComps);
            rGo.transform.parent = rParentGo.transform;
            ResetTransform(rGo.transform);

            return rGo;
        }

        public static GameObject CreateGameObject(GameObject rTemplateGo)
        {
            GameObject rGo = GameObject.Instantiate(rTemplateGo);
            rGo.name = rTemplateGo.name;
            ResetTransform(rGo.transform);

            return rGo;
        }

        public static GameObject CreateGameObject(GameObject rTemplateGo, GameObject rParentGo)
        {
            GameObject rGo = GameObject.Instantiate(rTemplateGo);
            rGo.transform.parent = rParentGo.transform;

            rGo.name = rTemplateGo.name;
            ResetTransform(rGo.transform);

            return rGo;
        }

        #endregion

        public static void SafeDestroy(UnityEngine.Object rObj)
        {
            if (rObj != null)
                GameObject.DestroyImmediate(rObj, true);
            rObj = null;
        }
    }
}
