using NaughtyAttributes.Editor;
using UnityEngine.UI;

namespace UnityEngine.UI
{
    [UnityEditor.CustomEditor(typeof(ViewControllerContainer), true)]
    public class ViewModelContainerInspector : InspectorEditor
    {
        ViewControllerContainer mTatget;

        protected override void OnEnable()
        {
            base.OnEnable();
            mTatget = target as ViewControllerContainer;
        }

        public override void OnInspectorGUI()
        {
            mTatget.GetAllViewModelDataSources();

            base.OnInspectorGUI();
            serializedObject.ApplyModifiedProperties();
        }
    }
}