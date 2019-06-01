using System;
using System.Collections.Generic;
using UnityEngine;

namespace IceCube.Core
{
    public class TObjectPool<T> where T : new()
    {
        Stack<T> mStack;

        Func<T> mActionAlloc;
        Action<T> mActionFree;
        Action<T> mActionDestroy;

        public TObjectPool(Func<T>rActionAlloc, Action<T> rActionFree, Action<T> rActionDestroy)
        {
            mStack = new Stack<T>();

            mActionAlloc = rActionAlloc;
            mActionFree = rActionFree;
            mActionDestroy = rActionDestroy;
        }

        public T Alloc()
        {
            if (mStack.Count == 0)
            {
                if (mActionAlloc == null)
                    return default;
                return mActionAlloc();
            }
            else
            {
                return mStack.Pop();
            }
        }

        public void Free(T rElement)
        {
            if (mStack.Count > 0 && ReferenceEquals(mStack.Peek(), rElement))
            {

            }
        }

    }
}