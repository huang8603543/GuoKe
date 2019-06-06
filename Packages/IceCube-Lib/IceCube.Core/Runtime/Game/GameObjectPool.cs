using UnityEngine;

namespace IceCube.Core
{
    public class GameObjectPool
    {
        TObjectPool<GameObject> mObjectPool;

        GameObject mPrefabGo;

        public GameObject RootGo
        {
            get;
        }

        public GameObjectPool(string rPoolName, GameObject rPrefabGo, int rInitCount = 0)
        {
            mPrefabGo = rPrefabGo;
            mObjectPool = new TObjectPool<GameObject>(OnAlloc, OnFree, OnDestroy);

            RootGo = UtilTool.CreateGameObject(rPoolName);
            RootGo.SetActive(false);
            RootGo.transform.position = new Vector3(0, 1000, 0);

            for (int i = 0; i < rInitCount; i++)
            {
                mObjectPool.Alloc();
            }
        }

        public GameObjectPool(GameObject rRootGo, GameObject rPrefabGo, int rInitCount = 0)
        {
            mPrefabGo = rPrefabGo;
            mObjectPool = new TObjectPool<GameObject>(OnAlloc, OnFree, OnDestroy);

            RootGo = rRootGo;
            RootGo.SetActive(false);
            for (int i = 0; i < rInitCount; i++)
            {
                mObjectPool.Alloc();
            }
        }

        public GameObject Alloc()
        {
            return mObjectPool.Alloc();
        }

        public void Free(GameObject rGo)
        {
            if (rGo == null)
                return;
            mObjectPool.Free(rGo);
        }

        public void Destroy()
        {
            mPrefabGo = null;
            mObjectPool.Destory();
            UtilTool.SafeDestroy(RootGo);
        }

        GameObject OnAlloc()
        {
            return Object.Instantiate(mPrefabGo);
        }

        void OnFree(GameObject rGo)
        {
            rGo.transform.SetParent(RootGo.transform);
            rGo.transform.localPosition = Vector3.zero;
            rGo.transform.localRotation = Quaternion.identity;
            rGo.transform.localScale = Vector3.one;
        }

        void OnDestroy(GameObject rGo)
        {
            UtilTool.SafeDestroy(rGo);
        }
    }
}
