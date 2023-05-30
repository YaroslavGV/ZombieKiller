using UnityEngine;
using UnityEditor;

namespace SaveLoad
{
    [CustomEditor(typeof(JsonHandlerInstaller))]
    public class JsonHandlerInstallerEditor : Editor
    {
        private JsonHandlerInstaller _installer;

        void OnEnable ()
        {
            _installer = target as JsonHandlerInstaller;
        }

        public override void OnInspectorGUI ()
        {
            IJsonHandler handler = _installer.Handler;
            if (handler == null)
            {
                base.OnInspectorGUI();
                return;
            }

            GUILayoutOption buttonOptions = GUILayout.Width(100);
            foreach (string key in handler.Keys)
            {
                EditorGUILayout.BeginHorizontal();
                bool haveHdata = handler.HasData(key);
                string title = haveHdata ? "Clear Data" : "No Data";
                GUI.enabled = haveHdata;
                GUILayout.Label(key);
                if (GUILayout.Button(title, buttonOptions))
                    handler.Clear(key);
                EditorGUILayout.EndHorizontal();
            }
            GUILayout.Space(EditorGUIUtility.standardVerticalSpacing);
            GUI.enabled = true;
            if (GUILayout.Button("Save"))
                handler.Save();
            if (GUILayout.Button("Clear All Data"))
                handler.ClearAll();
            if (GUILayout.Button("Debug All"))
                handler.DebugAll();
        }
    }
}