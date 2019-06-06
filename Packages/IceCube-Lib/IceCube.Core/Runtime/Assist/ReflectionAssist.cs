using System.Reflection;
using System;

namespace IceCube.Core
{
    public class ReflectionAssist
    {
        public static readonly BindingFlags flags_common        = BindingFlags.Instance     |
                                                                 BindingFlags.SetField      | BindingFlags.GetField     |
                                                                 BindingFlags.GetProperty   | BindingFlags.SetProperty;
        public static readonly BindingFlags flags_public        = flags_common              | BindingFlags.Public;
        public static readonly BindingFlags flags_nonpublic     = flags_common              | BindingFlags.NonPublic;
        public static readonly BindingFlags flags_all           = flags_common              | BindingFlags.Public       | BindingFlags.NonPublic;

        public static readonly BindingFlags flags_method        = BindingFlags.InvokeMethod | BindingFlags.Public       | BindingFlags.NonPublic;
        public static readonly BindingFlags flags_method_inst   = flags_method              | BindingFlags.Instance;
        public static readonly BindingFlags flags_method_static = flags_method              | BindingFlags.Static;

        public static readonly Type[] empty_types = new Type[0];

        public static ConstructorInfo GetConstructorInfo(BindingFlags rBindFlags, Type rType, Type[] rTypes)
        {
            return rType.GetConstructor(rBindFlags, null, rTypes, null);
        }

        public static object CreateInstance(Type rType, BindingFlags rBindFlags)
        {
            ConstructorInfo rConstructorInfo = GetConstructorInfo(rBindFlags, rType, empty_types);
            return rConstructorInfo.Invoke(null);
        }

        public static object Construct(Type rType)
        {
            ConstructorInfo rConstructorInfo = GetConstructorInfo(flags_all, rType, empty_types);
            return rConstructorInfo.Invoke(null);
        }

        public static object Construct(Type rType, params object[] rParams)
        {
            var rParamType = new Type[rParams.Length];
            for (int index = 0; index < rParams.Length; index++)
                rParamType[index] = rParams[index].GetType();
            ConstructorInfo rConstructorInfo = rType.GetConstructor(rParamType);
            return rConstructorInfo.Invoke(rParams);
        }

        public static object Construct(Type rType, Type[] rTypes, params object[] rParams)
        {
            ConstructorInfo rConstructorInfo = GetConstructorInfo(flags_all, rType, rTypes);
            return rConstructorInfo.Invoke(rParams);
        }

        public static object MethodMember(object rObject, string rMemberName, BindingFlags rBindFlags, params object[] rParams)
        {
            if (rObject == null)
                return null;
            Type rType = rObject.GetType();
            return rType.InvokeMember(rMemberName, rBindFlags, null, rObject, rParams);
        }

        public static object MethodMember(Type rType, string rMemberName, BindingFlags rBindFlags, params object[] rParams)
        {
            return rType.InvokeMember(rMemberName, rBindFlags, null, null, rParams);
        }
    }
}
