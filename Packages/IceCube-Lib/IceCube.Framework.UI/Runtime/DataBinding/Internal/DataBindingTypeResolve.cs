using System;
using System.Collections.Generic;
using IceCube.Core;
using System.Linq;
using System.Reflection;
using UnityEngine.Events;
using System.Collections;

namespace UnityEngine.UI
{
    public class DataBindingTypeResolve
    {
        public static List<string> GetAllViewModels()
        {
            var rTypeName = TypeResolveManager.Instance.GetAllTypes(true)
                .Where(rType => rType != null &&
                       rType.GetCustomAttributes(typeof(DataBindingAttribute), true).Any() &&
                       rType.BaseType?.FullName == "IceCube.Hotfix.Core.ViewModel")
                .Select(rType =>
                {
                    return rType.FullName;
                });
            return new List<string>(rTypeName);
        }

        public static List<string> GetAllViews()
        {
            var rTypeNames = TypeResolveManager.Instance.GetAllTypes(true)
                .Where(rType => rType != null &&
                                rType.BaseType?.FullName == "IceCube.Hotfix.Core.ViewController")
                .Select(rType =>
                {
                    return rType.FullName;
                });
            return new List<string>(rTypeNames);
        }
    }
}
