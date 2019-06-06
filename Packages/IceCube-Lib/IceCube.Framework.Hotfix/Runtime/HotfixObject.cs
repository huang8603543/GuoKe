namespace IceCube.Framework.Hotfix
{
    public class HotfixObject
    {
        public string TypeName;
        public HotfixApp App;
        public object Object;

        public HotfixObject(HotfixApp rApp, string rTypeName)
        {
            App = rApp;
            TypeName = rTypeName;
        }

        public object Invoke(string rMethodName, params object[] rArgs)
        {
            if (App == null || Object == null)
                return null;
            return App.Invoke(this, rMethodName, rArgs);
        }

        public object InvokeParent(string rParentType, string rMethodName, params object[] rArgs)
        {
            if (App == null || Object == null)
                return null;
            return App.InvokeParent(this, rParentType, rMethodName, rArgs);
        }

        public object InvokeStatic(string rMethodName, params object[] rArgs)
        {
            if (App == null || Object == null)
                return null;
            return App.InvokeStatic(TypeName, rMethodName, rArgs);
        }
    }
}
