using System;
using System.Reflection;

namespace IceCube.Core
{
    public class TSingleton<T> where T : class
    {
        static object syncRoot = new object();
        static T instance;

        public static readonly Type[] EmptyTypes = new Type[0];

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    lock(syncRoot)
                    {
                        if (instance == null)
                        {
                            ConstructorInfo ci = typeof(T).GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, EmptyTypes, null);
                            if (ci == null)
                            {
                                throw new InvalidOperationException("Class must contain a private constructor.");
                            }
                            instance = (T)ci.Invoke(null);
                        }
                    }
                }
                return instance;
            }
        }
    }
}