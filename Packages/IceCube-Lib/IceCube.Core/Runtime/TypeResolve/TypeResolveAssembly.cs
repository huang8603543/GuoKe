using System;
using System.Linq;
using System.Reflection;

namespace IceCube.Core
{
    public interface ITypeResolveAssemblyProxy
    {
        void Load();
        Type[] GetAllTypes();
        object Instantiate(string rTypeName, params object[] rArgs);
        T Instantiate<T>(string rTypeName, params object[] rArgs);
        object Invoke(object rObj, string rTypeName, string rMethodName, params object[] rArgs);
    }

    public abstract class TypeResolveAssembly
    {
        public string AssemblyName;
        public bool IsHotfix;

        public TypeResolveAssembly(string rAssemblyName)
        {
            AssemblyName = rAssemblyName;
            IsHotfix = false;
        }

        public virtual void Load()
        {

        }

        public virtual Type[] GetAllTypes()
        {
            return null;
        }

        public virtual object Instantiate(string rTypeName, params object[] rArgs)
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
    }

    public class TypeResolveAssembly_Mono : TypeResolveAssembly
    {
        Assembly mAssembly;

        public TypeResolveAssembly_Mono(string rAssemblyName) : base(rAssemblyName)
        {
            IsHotfix = false;
        }

        public override void Load()
        {
            var rAllAssemblies = AppDomain.CurrentDomain.GetAssemblies();
            for (int i = 0, len = rAllAssemblies.Length; i < len; i++)
            {
                if (rAllAssemblies[i].GetName().Name.Equals(AssemblyName))
                {
                    mAssembly = rAllAssemblies[i];
                    break;
                }
            }
        }

        public override Type[] GetAllTypes()
        {
            if (mAssembly == null)
                return new Type[0];
            return mAssembly.GetTypes();
        }

        public override object Instantiate(string rTypeName, params object[] rArgs)
        {
            if (mAssembly == null)
                return null;
            return Activator.CreateInstance(mAssembly.GetType(rTypeName), rArgs);
        }

        public override T Instantiate<T>(string rTypeName, params object[] rArgs)
        {
            if (mAssembly == null)
                return default;
            return (T)Activator.CreateInstance(mAssembly.GetType(rTypeName), rArgs);
        }

        public override object Invoke(object rObj, string rTypeName, string rMethodName, params object[] rArgs)
        {
            if (mAssembly == null)
                return null;
            if (rObj == null)
                return null;

            var rType = mAssembly.GetType(rTypeName);
            if (rType == null)
                return null;

            return rType.InvokeMember(rMethodName, ReflectionAssist.flags_method_inst, null, rObj, rArgs);
        }
    }

    public class TypeResolveAssembly_Hotfix : TypeResolveAssembly
    {
        public ITypeResolveAssemblyProxy Proxy;

        public TypeResolveAssembly_Hotfix(string rAssemblyName) : base(rAssemblyName)
        {
            IsHotfix = true;
            var rProxyType = AppDomain.CurrentDomain.GetAssemblies()
                .Single(rAssembly => rAssembly.GetName().Name.Equals("Framework.Hotfix"))?.GetTypes()?
                .SingleOrDefault(rType => rType.FullName.Equals("IceCube.Framework.Hotfix.HotfixTypeResolveAssemblyProxy"));

            if (rProxyType != null)
            {
                Proxy = ReflectionAssist.Construct(rProxyType, new Type[] { typeof(string) }, rAssemblyName) as ITypeResolveAssemblyProxy;
            }
        }

        public override void Load()
        {
            Proxy?.Load();
        }

        public override Type[] GetAllTypes()
        {
            return Proxy?.GetAllTypes();
        }

        public override object Instantiate(string rTypeName, params object[] rArgs)
        {
            return Proxy?.Instantiate(rTypeName, rArgs);
        }

        public override T Instantiate<T>(string rTypeName, params object[] rArgs)
        {
            if (Proxy == null)
                return default;
            return Proxy.Instantiate<T>(rTypeName, rArgs);
        }

        public override object Invoke(object rObj, string rTypeName, string rMethodName, params object[] rArgs)
        {
            if (Proxy == null)
                return null;
            return Proxy.Invoke(rObj, rTypeName, rMethodName, rArgs);
        }
    }
}
