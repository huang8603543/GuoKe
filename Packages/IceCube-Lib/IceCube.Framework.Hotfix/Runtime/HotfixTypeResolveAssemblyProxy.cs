using System;
using System.IO;
using IceCube.Core;
using UnityEngine;

namespace IceCube.Framework.Hotfix
{
    public class HotfixTypeResolveAssemblyProxy : ITypeResolveAssemblyProxy
    {
        string mAssemblyName;

        public HotfixTypeResolveAssemblyProxy(string rAssemblyName)
        {
            mAssemblyName = rAssemblyName;
        }

        public void Load()
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                string rDLLPath = HotfixManager.HotfixDllDir + mAssemblyName + ".bytes";
                string rPDBPath = HotfixManager.HotfixDllDir + mAssemblyName + "_PDB.bytes";

                var rDLLBytes = File.ReadAllBytes(rDLLPath);
                var rPDBBytes = File.ReadAllBytes(rPDBPath);

                HotfixManager.Instance.Initialize();
                HotfixManager.Instance.InitApp(rDLLBytes, rPDBBytes);
            }
#endif
        }

        public Type[] GetAllTypes()
        {
            return HotfixManager.Instance.GetTypes();
        }

        public object Instantiate(string rTypeName, params object[] rArgs)
        {
            return HotfixManager.Instance.Instantiate(rTypeName, rArgs);
        }

        public T Instantiate<T>(string rTypeName, params object[] rArgs)
        {
            return HotfixManager.Instance.Instantiate<T>(rTypeName, rArgs);
        }

        public object Invoke(object rObj, string rTypeName, string rMethodName, params object[] rArgs)
        {
            return HotfixManager.Instance.Invoke(rObj, rTypeName, rMethodName, rArgs);
        }
    }
}
