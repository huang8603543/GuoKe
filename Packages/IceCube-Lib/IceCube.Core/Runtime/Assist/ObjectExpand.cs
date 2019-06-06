using UnityEngine;

namespace IceCube.Core
{
    public static class ObjectExpand
    {
        public static T SafeGetComponent<T>(this GameObject rGo) where T : Component
        {
            if (rGo == null)
                return default;
            return rGo.GetComponent<T>();
        }

        public static T ReceiveComponent<T>(this GameObject rGo) where T : Component
        {
            if (rGo == null)
                return default;

            T rComponent = rGo.GetComponent<T>();
            if (rComponent == null)
                rComponent = rGo.AddComponent<T>();

            return rComponent;
        }
    }
}