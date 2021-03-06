﻿//#if UNITY_EDITOR
//using UnityEditor;
//using UnityEngine;
//using System;
//using System.Text;
//using System.Collections.Generic;
//using Knight.Core;
//using Knight.Core.WindJson;
//using Knight.Framework.Hotfix;
//using System.Linq;

//[System.Reflection.Obfuscation(Exclude = true)]
//public class ILRuntimeCLRBinding
//{    
//    [MenuItem("Tools/ILRuntime/Generate CLR Binding Code by Analysis")]
//    static void GenerateCLRBindingByAnalysis()
//    {
//        //用新的分析热更dll调用引用来生成绑定代码
//        ILRuntime.Runtime.Enviorment.AppDomain domain = new ILRuntime.Runtime.Enviorment.AppDomain();

//        using (System.IO.FileStream fs = new System.IO.FileStream("Assets/Game/GameAsset/Hotfix/Libs/KnightHotfix.bytes", System.IO.FileMode.Open, System.IO.FileAccess.Read))
//        {
//            domain.LoadAssembly(fs);
        
//            //Crossbind Adapter is needed to generate the correct binding code
//            InitILRuntime(domain);
//            ILRuntime.Runtime.CLRBinding.BindingCodeGenerator.GenerateBindingCode(domain, "Assets/Game/Script/Generate/ILRuntime");
//        }
//        AssetDatabase.Refresh();
//    }

//    static void InitILRuntime(ILRuntime.Runtime.Enviorment.AppDomain domain)
//    {
//        //这里需要注册所有热更DLL中用到的跨域继承Adapter，否则无法正确抓取引用
//        domain.RegisterCrossBindingAdaptor(new CoroutineAdaptor());
//        domain.RegisterCrossBindingAdaptor(new IEqualityComparerAdaptor());
//        domain.RegisterCrossBindingAdaptor(new IEnumerableAdaptor());
//        domain.RegisterCrossBindingAdaptor(new IAsyncStateMachineAdaptor());
//    }
//}
//#endif
