﻿using System;
using System.Threading.Tasks;

namespace IceCube.Framework.Hotfix
{
    public class HotfixApp
    {
#pragma warning disable 1998
        public virtual async Task Load(string rABPath, string rHotfixModuleName)
        {

        }
#pragma warning restore 1998

        public virtual void InitApp(string rDLLPath, string rPDBPath)
        {

        }

        public virtual void InitApp(byte[] rDLLBytes, byte[] rPDBBytes)
        {

        }

        public virtual HotfixObject Instantiate(string rTypeName, params object[] rArgs)
        {
            return null;
        }

        public virtual T Instantiate<T>(string rTypeName, params object[] rArgs)
        {
            return default;
        }

        public virtual object Invoke(object rObj, string rTypeName, string rMethodName, params object[] rArgs)
        {
            return null;
        }

        public virtual object Invoke(HotfixObject rHotfixObj, string rMethodName, params object[] rArgs)
        {
            return null;
        }

        public virtual object InvokeParent(HotfixObject rHotfixObj, string rParentType, string rMethodName, params object[] rArgs)
        {
            return null;
        }

        public virtual object InvokeStatic(string rTypeName, string rMethodName, params object[] rArgs)
        {
            return null;
        }

        public virtual Type[] GetTypes()
        {
            return null;
        }
    }
}
