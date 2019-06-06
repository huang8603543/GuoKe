using IceCube.Core;
using System.Threading.Tasks;
using System;

namespace IceCube.Framework.Hotfix
{
    public class HotfixManager : TSingleton<HotfixManager>
    {
        public static string IsHotfixDebugModeKey = "HotfixManager_IsHotfixDebugMode";
        public static string HotfixDebugDllDir = "Library/ScriptAssemblies";
        public static string HotfixDllDir = "Assets/Game/GameAsset/Hotfix/Libs/";

        HotfixApp mApp;

        private HotfixManager() { }

        public void Initialize()
        {
            if (IsHotfixDebugMode())
                mApp = new HotfixApp_Reflect();
            else
                mApp = new HotfixApp_ILRT();
        }

        public void InitApp(byte[] rDLLBytes, byte[] rPDBBytes)
        {
            if (mApp == null)
                return;
            mApp.InitApp(rDLLBytes, rPDBBytes);
        }

        public void InitApp(string rDLLPath, string rPDBPath)
        {
            if (mApp == null)
                return;
            mApp.InitApp(rDLLPath, rPDBPath);
        }

        public async Task Load(string rABPath, string rHotfixModuleName)
        {
            if (mApp == null)
                return;
            await mApp.Load(rABPath, rHotfixModuleName);
        }

        public HotfixObject Instantiate(string rTypeName, params object[] rArgs)
        {
            if (mApp == null)
                return null;
            return mApp.Instantiate(rTypeName, rArgs);
        }

        public T Instantiate<T>(string rTypeName, params object[] rArgs)
        {
            if (mApp == null)
                return default;
            return mApp.Instantiate<T>(rTypeName, rArgs);
        }

        public object Invoke(object rObj, string rTypeName, string rMethodName, params object[] rArgs)
        {
            if (mApp == null)
                return null;
            return mApp.Invoke(rObj, rTypeName, rMethodName, rArgs);
        }

        public object Invoke(HotfixObject rHotfixObj, string rMethodName, params object[] rArgs)
        {
            if (mApp == null)
                return null;
            return mApp.Invoke(rHotfixObj, rMethodName, rArgs);
        }

        public object InvokeParent(HotfixObject rHotfixObj, string rParentType, string rMethodName, params object[] rArgs)
        {
            if (mApp == null)
                return null;
            return mApp.InvokeParent(rHotfixObj, rParentType, rMethodName, rArgs);
        }

        public object InvokeStatic(string rTypeName, string rMethodName, params object[] rArgs)
        {
            if (mApp == null)
                return null;
            return mApp.InvokeStatic(rTypeName, rMethodName, rArgs);
        }

        public Type[] GetTypes()
        {
            if (mApp == null)
                return null;
            return mApp.GetTypes();
        }

        public bool IsHotfixDebugMode()
        {
#if UNITY_EDITOR
            bool bIsHotfixDebugMode = false;
            bIsHotfixDebugMode = UnityEditor.EditorPrefs.GetBool(IsHotfixDebugModeKey);
            return bIsHotfixDebugMode;
#else
#if HOTFIX_REFLECT_USE
            return true;
#else
            return false;
#endif
#endif
        }
    }
}
