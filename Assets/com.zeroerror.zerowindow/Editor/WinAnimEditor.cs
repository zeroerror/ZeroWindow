using UnityEngine;
using UnityEditor;
using Unity.EditorCoroutines.Editor;

namespace ZeroWin.EditorTool {

    [CustomEditor(typeof(WinAnim))]
    public class WinAnimEditor : Editor {

        WinAnim winAnim;
        SerializedProperty property_target;
        SerializedProperty property_animCurve;
        SerializedProperty property_startPos;
        SerializedProperty property_endPos;
        SerializedProperty property_duration;

        EditorCoroutine coroutine;

        void OnEnable() {
            property_target = serializedObject.FindProperty("target");
            property_animCurve = serializedObject.FindProperty("animCurve");
            property_duration = serializedObject.FindProperty("duration");
            property_startPos = serializedObject.FindProperty("startPos");
            property_endPos = serializedObject.FindProperty("endPos");

            winAnim = (WinAnim)target;
        }

        public override void OnInspectorGUI() {
            serializedObject.Update();

            if (GUILayout.Button("播放")) {
                coroutine = EditorCoroutineUtility.StartCoroutine(winAnim.DisplayAnimEnumerator(), winAnim);
            }

            if (GUILayout.Button("停止")) {
                EditorCoroutineUtility.StopCoroutine(coroutine);
            }

            EditorGUILayout.PropertyField(property_target);
            EditorGUILayout.PropertyField(property_duration);
            EditorGUILayout.PropertyField(property_animCurve);
            EditorGUILayout.PropertyField(property_startPos);
            EditorGUILayout.PropertyField(property_endPos);
            serializedObject.ApplyModifiedProperties();
        }
    }

}
