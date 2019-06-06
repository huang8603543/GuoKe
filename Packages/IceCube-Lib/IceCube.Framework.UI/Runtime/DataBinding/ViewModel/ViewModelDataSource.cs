using NaughtyAttributes;

namespace UnityEngine.UI
{
    [DefaultExecutionOrder(90)]
    [System.Serializable]
    public class ViewModelDataSource : MonoBehaviour
    {
        [Dropdown("ViewModelClasses")]
        public string ViewModelPath;

        [HideInInspector]
        public string[] ViewModelClasses = new string[0];

        public void GetPaths()
        {
            ViewModelClasses = DataBindingTypeResolve.GetAllViewModels().ToArray();
        }
    }
}
