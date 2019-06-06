using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using AppDomain = ILRuntime.Runtime.Enviorment.AppDomain;

namespace IceCube.Framework.Hotfix
{
    public class HotfixApp_ILRT : HotfixApp
    {
        AppDomain mApp;
        MemoryStream mDLLMS;
        MemoryStream mPDBMS;

        public HotfixApp_ILRT()
        { }

        ~HotfixApp_ILRT()
        {
            if (mApp.DebugService != null)
                mApp.DebugService.StopDebugService();
            mApp = null;
        }

        public override async Task Load(string rABPath, string rHotfixModuleName)
        {
            string rDLLPath = HotfixManager.HotfixDllDir + "IceCubeHotfix" + ".bytes";
            string rPDBPath = HotfixManager.HotfixDllDir + "IceCubeHotfix" + "_PDB.bytes";

            var rDLLBytes = File.ReadAllBytes(rDLLPath);
            var rPDBBytes = File.ReadAllBytes(rPDBPath);

            InitApp(rDLLBytes, rPDBBytes);
        }

        public override void InitApp(byte[] rDLLBytes, byte[] rPDBBytes)
        {
            mApp = new AppDomain();

            mDLLMS = (rDLLBytes != null) ? new MemoryStream(rDLLBytes) : null;
            mPDBMS = (rPDBBytes != null) ? new MemoryStream(rPDBBytes) : null;

            mApp.LoadAssembly(mDLLMS, mPDBMS, new ILRuntime.Mono.Cecil.Pdb.PdbReaderProvider());

            if (mApp.DebugService != null)
                mApp.DebugService.StartDebugService(56000);

            RegisterValueTypeBinder();
            RegisterCrossBindingAdaptor();
            RegisterCLRMethodRedirection();
            RegisterDelegates();
        }

        public override void InitApp(string rDLLPath, string rPDBPath)
        {
            byte[] rDLLBytes = File.Exists(rDLLPath) ? File.ReadAllBytes(rDLLPath) : null;
            byte[] rPDBBytes = File.Exists(rPDBPath) ? File.ReadAllBytes(rPDBPath) : null;
            InitApp(rDLLPath, rPDBPath);
        }

        public override HotfixObject Instantiate(string rTypeName, params object[] rArgs)
        {
            if (mApp == null)
                return null;
            var rObject = new HotfixObject(this, rTypeName);
            rObject.Object = mApp.Instantiate(rTypeName, rArgs);
            return rObject;
        }

        public override T Instantiate<T>(string rTypeName, params object[] rArgs)
        {
            if (mApp == null)
                return default;
            return mApp.Instantiate<T>(rTypeName, rArgs);
        }

        public override object Invoke(HotfixObject rHotfixObj, string rMethodName, params object[] rArgs)
        {
            if (mApp == null || rHotfixObj == null)
                return null;
            return mApp.Invoke(rHotfixObj.TypeName, rMethodName, rHotfixObj.Object, rArgs);
        }

        public override object InvokeParent(HotfixObject rHotfixObj, string rParentType, string rMethodName, params object[] rArgs)
        {
            if (mApp == null || rHotfixObj == null)
                return null;
            return this.mApp.Invoke(rParentType, rMethodName, rHotfixObj.Object, rArgs);
        }

        public override object InvokeStatic(string rTypeName, string rMethodName, params object[] rArgs)
        {
            if (mApp == null)
                return null;
            return mApp.Invoke(rTypeName, rMethodName, null, rArgs);
        }

        public override Type[] GetTypes()
        {
            if (mApp == null)
                return null;
            return mApp.LoadedTypes.Values.Select(x => x.ReflectionType).ToArray();
        }

        private unsafe void RegisterValueTypeBinder()
        {
            this.mApp.RegisterValueTypeBinder(typeof(Vector3), new Vector3Binder());
            this.mApp.RegisterValueTypeBinder(typeof(Quaternion), new QuaternionBinder());
            this.mApp.RegisterValueTypeBinder(typeof(Vector2), new Vector2Binder());
        }

        private void RegisterCrossBindingAdaptor()
        {
            this.mApp.RegisterCrossBindingAdaptor(new CoroutineAdaptor());
            this.mApp.RegisterCrossBindingAdaptor(new IEqualityComparerAdaptor());
            this.mApp.RegisterCrossBindingAdaptor(new IEnumerableAdaptor());
            this.mApp.RegisterCrossBindingAdaptor(new IAsyncStateMachineAdaptor());
        }

        private unsafe void RegisterCLRMethodRedirection()
        {
        }

        private void RegisterDelegates()
        {
        }
    }
}