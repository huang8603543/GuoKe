using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
    public class UIRoot : MonoBehaviour
    {
        public static UIRoot Instance
        {
            get;
            private set;
        }

        public GameObject DynamicRoot;
        public GameObject GlobalRoot;
        public GameObject PopupRoot;

        public Camera UICamera;
        public EventSystem UIEventSystem;

        private void Awake()
        {
            Instance = this;
        }

        public void Initialize()
        {
            DynamicRoot.SetActive(true);
            GlobalRoot.SetActive(true);
            PopupRoot.SetActive(true);
        }

        public void SetEventSystemEnable(bool bEnabled)
        {
            UIEventSystem.enabled = bEnabled;
        }
    }
}
