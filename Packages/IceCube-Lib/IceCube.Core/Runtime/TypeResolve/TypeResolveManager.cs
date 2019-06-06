using System;
using System.Collections.Generic;
using UnityEngine;

namespace IceCube.Core
{
    public class TypeResolveManager : TSingleton<TypeResolveManager>
    {
        Dictionary<string, TypeResolveAssembly> mAssemblies;

        private TypeResolveManager()
        {
            mAssemblies = new Dictionary<string, TypeResolveAssembly>();
        }

        public void Initialize()
        {
            mAssemblies.Clear();
        }

        public Type GetType(string rTypeName)
        {
            foreach (var rPair in mAssemblies)
            {
                var rTypes = rPair.Value.GetAllTypes();
                if (rTypes == null)
                    continue;

                for (int i = 0, len = rTypes.Length; i < len; i++)
                {
                    if (rTypes[i].FullName.Equals(rTypeName))
                    {
                        return rTypes[i];
                    }
                }
            }
            return null;
        }

        public Type GetType(string rTypeName, out TypeResolveAssembly rTypeResolveAssembly)
        {
            rTypeResolveAssembly = null;
            foreach (var rPair in mAssemblies)
            {
                var rTypes = rPair.Value.GetAllTypes();
                for (int i = 0, len = rTypes.Length; i < len; i++)
                {
                    if (rTypes[i].FullName.Equals(rTypeName))
                    {
                        rTypeResolveAssembly = rPair.Value;
                        return rTypes[i];
                    }
                }
            }
            return null;
        }

        public object Invoke(object rObj, string rTypeName, string rMethodName, params object[] rArgs)
        {
            TypeResolveAssembly rAssembly = null;
            var rType = GetType(rTypeName, out rAssembly);
            if (rType == null)
                return null;

            return rAssembly.Invoke(rObj, rTypeName, rMethodName, rArgs);
        }

        public void AddAssembly(string rAssemblyName, bool bIsHotfix = false)
        {
            TypeResolveAssembly rTypeResolveAssembly = null;
            if (bIsHotfix)
                rTypeResolveAssembly = new TypeResolveAssembly_Hotfix(rAssemblyName);
            else
                rTypeResolveAssembly = new TypeResolveAssembly_Mono(rAssemblyName);
            rTypeResolveAssembly.Load();
            if (!mAssemblies.ContainsKey(rAssemblyName))
            {
                mAssemblies.Add(rAssemblyName, rTypeResolveAssembly);
            }
        }

        public Type[] GetTypes(string rAssemblyName)
        {
            TypeResolveAssembly rAssembly = null;
            if (mAssemblies.TryGetValue(rAssemblyName, out rAssembly))
            {
                return rAssembly.GetAllTypes();
            }
            return null;
        }

        public TypeResolveAssembly GetAssembly(Type rType)
        {
            var rTypeAssemblyName = rType.Assembly.GetName().Name;

            TypeResolveAssembly rAssembly = null;
            mAssemblies.TryGetValue(rTypeAssemblyName, out rAssembly);
            return rAssembly;
        }

        public List<Type> GetAllTypes(bool bIsHotfix)
        {
            var rTypes = new List<Type>();
            foreach (var rPair in mAssemblies)
            {
                if (rPair.Value.IsHotfix == bIsHotfix)
                    rTypes.AddRange(rPair.Value.GetAllTypes());
            }
            return rTypes;
        }

        public object Instantiate(string rTypeName, params object[] rArgs)
        {
            TypeResolveAssembly rAssembly = null;
            var rType = GetType(rTypeName, out rAssembly);
            if (rType == null)
                return null;
            if (rAssembly == null)
                return null;

            return rAssembly.Instantiate(rTypeName, rArgs);
        }

        public T Instantiate<T>(string rTypeName, params object[] rArgs)
        {
            TypeResolveAssembly rAssembly = null;
            var rType = GetType(rTypeName, out rAssembly);
            if (rType == null)
                return default;

            return rAssembly.Instantiate<T>(rTypeName, rArgs);
        }

#if UNITY_EDITOR

        [UnityEditor.Callbacks.DidReloadScripts]
        public static void ScriptsReloaded()
        {
            if (!Application.isPlaying)
            {
                Instance.Initialize();
            }
        }
#endif
    }
}
