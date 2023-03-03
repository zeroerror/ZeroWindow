using UnityEngine;
using UnityEditor;
using ZeroWin.Component;

namespace ZeroWin.EditorTool {

    [CustomEditor(typeof(WinText), true)]
    [CanEditMultipleObjects]
    public class WinTextEditor : UnityEditor.UI.TextEditor {

        SerializedProperty m_TextID;

        GUIContent m_TextIDContent = new GUIContent("文字ID");

        protected override void OnEnable() {
            base.OnEnable();
            m_TextID = serializedObject.FindProperty("textID");
        }

        public override void OnInspectorGUI() {
            serializedObject.Update();

            EditorGUILayout.Space();
            EditorGUI.BeginChangeCheck();
            int newID = EditorGUILayout.IntField(m_TextIDContent, m_TextID.intValue);
            if (EditorGUI.EndChangeCheck()) {
                m_TextID.intValue = newID;
                serializedObject.ApplyModifiedProperties();
            }
            EditorGUILayout.Space();

            serializedObject.ApplyModifiedProperties();

            base.OnInspectorGUI();
        }
    }

}
