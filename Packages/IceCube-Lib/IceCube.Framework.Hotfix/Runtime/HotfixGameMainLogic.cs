using System.Threading.Tasks;
using UnityEngine;

namespace IceCube.Framework.Hotfix
{
    public class HotfixGameMainLogic : MonoBehaviour
    {
        public static HotfixGameMainLogic Instance
        {
            get;
            private set;
        }

        public string MainLogicScript;

        public HotfixObject MainLogicHotfixObj;

        private void Awake()
        {
            Instance = this;
        }

        public async Task Initialize()
        {
            MainLogicHotfixObj = HotfixManager.Instance.Instantiate(MainLogicScript);

            await (HotfixManager.Instance.Invoke(MainLogicHotfixObj, "Initialize") as Task);
        }

        private void Update()
        {
            if (MainLogicHotfixObj == null)
                return;
            HotfixManager.Instance.Invoke(MainLogicHotfixObj, "Update");
        }
    }
}
