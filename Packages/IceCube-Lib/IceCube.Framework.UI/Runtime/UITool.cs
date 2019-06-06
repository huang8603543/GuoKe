namespace UnityEngine.UI
{
    public static class UITool
    {
        public static GameObject AddChild(this Transform rParent, GameObject rPrefabGo, string rLayerName = "UI")
        {
            if (rParent == null || rPrefabGo == null)
                return null;

            GameObject rTargetGo = GameObject.Instantiate(rPrefabGo);
            rTargetGo.name = rPrefabGo.name;
            rTargetGo.transform.SetParent(rParent, false);
            rTargetGo.transform.localScale = Vector3.one;
            rTargetGo.SetLayer(rLayerName);

            return rTargetGo;
        }

        public static GameObject AddChildNoScale(this Transform rParent, GameObject rPrefabGo, string rLayerName = "UI")
        {
            if (rParent == null || rPrefabGo == null)
                return null;

            GameObject rTargetGo = GameObject.Instantiate(rPrefabGo);
            rTargetGo.name = rPrefabGo.name;
            rTargetGo.transform.SetParent(rParent, false);
            rTargetGo.SetLayer(rLayerName);

            return rTargetGo;
        }

        public static void SetLayer(this GameObject rGo, string rLayerName)
        {
            if (rGo == null)
                return;

            SetLayer(rGo.transform, rLayerName);
        }

        public static void SetLayer(Transform rParent, string rLayerName)
        {
            if (rParent == null)
                return;

            rParent.gameObject.layer = LayerMask.NameToLayer(rLayerName);
            for (int i = 0, len = rParent.transform.childCount; i < len; i++)
            {
                var rTrans = rParent.transform.GetChild(i);
                SetLayer(rTrans, rLayerName);
            }
        }

        public static void DeleteChildren(this Transform rTrans, bool bNeedFilterDeactive = false)
        {
            if (rTrans == null)
                return;

            for (int i = rTrans.childCount - 1; i >= 0; i--)
            {
                Transform rChildTrans = rTrans.GetChild(i);

                if (bNeedFilterDeactive && !rChildTrans.gameObject.activeSelf)
                    continue;
                GameObject.DestroyImmediate(rChildTrans.gameObject);
            }
        }
    }
}
