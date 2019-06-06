using UnityEngine.UI;
using NaughtyAttributes.Editor;
using UnityEditor;

namespace UnityEngine.UI
{
    [CustomEditor(typeof(ViewModelDataSource), true)]
    public class ViewModelDataSourceInspector : InspectorEditor
    {
        ViewModelDataSource mTarget;

        protected override void OnEnable()
        {
            base.OnEnable();
            mTarget = target as ViewModelDataSource;
        }

        public override void OnInspectorGUI()
        {
            mTarget.GetPaths();
            base.OnInspectorGUI();
        }
    }
}