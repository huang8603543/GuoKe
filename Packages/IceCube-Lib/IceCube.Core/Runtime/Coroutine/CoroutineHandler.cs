using UnityEngine;
using System.Collections;

namespace IceCube.Core
{
    public class CoroutineHandler : MonoBehaviour
    {
        public Coroutine Coroutine;
        public bool IsCompleted;
        public bool IsRunning;

        public Coroutine StartHandler(IEnumerator rIEum)
        {
            IsCompleted = false;
            IsRunning = true;
            Coroutine = StartCoroutine(StartHandlerAsync(rIEum));
            return Coroutine;
        }

        IEnumerator StartHandlerAsync(IEnumerator rIEnum)
        {
            yield return rIEnum;
            IsRunning = false;
            IsCompleted = true;

            yield return 0;

            CoroutineManager.Instance.Stop(this);
        }
    }
}
