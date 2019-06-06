using UnityEditor;
using UnityEngine;
using System.IO;

namespace IceCube.Framework.Hotfix.Editor
{
    public class HotfixMangerEditor : UnityEditor.Editor
    {
        private const string mSelectHotfixDebugModeMenuPath = "Tools/Hotfix Debug Mode";

        [MenuItem(mSelectHotfixDebugModeMenuPath, priority = 1000)]
        public static void SelectDevelopMode_Menu()
        {
            bool bSelected = Menu.GetChecked(mSelectHotfixDebugModeMenuPath);
            EditorPrefs.SetBool(HotfixManager.IsHotfixDebugModeKey, !bSelected);
            Menu.SetChecked(mSelectHotfixDebugModeMenuPath, !bSelected);
        }

        [MenuItem(mSelectHotfixDebugModeMenuPath, true)]
        public static bool SelectDevelopeMode_Check_Menu()
        {
            Menu.SetChecked(mSelectHotfixDebugModeMenuPath, EditorPrefs.GetBool(HotfixManager.IsHotfixDebugModeKey));
            return true;
        }

        [UnityEditor.Callbacks.DidReloadScripts]
        public static void AllScriptsReloaded()
        {
            //ViewModelInjectEditor.Inject();

            string rInjectDLLRootPath = "Library/ScriptAssemblies/Game.Hotfix.dll";
            string rInjectPDBRootPath = "Library/ScriptAssemblies/Game.Hotfix.pdb";

            string rNewDLLRootPath = HotfixManager.HotfixDllDir + "IceCubeHotfix.bytes";
            string rNewPDBRootPath = HotfixManager.HotfixDllDir + "IceCubeHotfix_PDB.bytes";

            File.Copy(rInjectDLLRootPath, rNewDLLRootPath, true);
            File.Copy(rInjectPDBRootPath, rNewPDBRootPath, true);

            Debug.Log("Generate hotfix bytes file success!!!");
        }
    }
}
