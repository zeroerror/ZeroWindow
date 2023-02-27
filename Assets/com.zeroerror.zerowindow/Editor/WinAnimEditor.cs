using UnityEngine;
using UnityEditor;
using Unity.EditorCoroutines.Editor;

namespace ZeroWin.EditorTool {

    [CustomEditor(typeof(WinAnim))]
    public class WinAnimEditor : Editor {

        WinAnim winAnim;
        SerializedProperty property_startRect;
        SerializedProperty property_endRect;
        SerializedProperty property_animCurve;
        SerializedProperty property_duration;

        EditorCoroutine coroutine;

        void OnEnable() {
            winAnim = (WinAnim)target;
            property_startRect = serializedObject.FindProperty("startRect");
            property_endRect = serializedObject.FindProperty("endRect");
            property_animCurve = serializedObject.FindProperty("animCurve");
            property_duration = serializedObject.FindProperty("duration");
            Reset();
        }

        void OnDisable() {
            Reset();
        }

        void Reset() {
            if (coroutine == null) {
                return;
            }
            EditorCoroutineUtility.StopCoroutine(coroutine);
            winAnim.Reset();
        }

        public override void OnInspectorGUI() {
            serializedObject.Update();

            if (GUILayout.Button("播放")) {
                Reset();
                coroutine = EditorCoroutineUtility.StartCoroutine(winAnim.DisplayAnimEnumerator(), winAnim);
            }

            if (GUILayout.Button("停止")) {
                Reset();
            }

            EditorGUILayout.PropertyField(property_startRect);
            EditorGUILayout.PropertyField(property_endRect);
            EditorGUILayout.PropertyField(property_duration);
            EditorGUILayout.PropertyField(property_animCurve);
            serializedObject.ApplyModifiedProperties();
        }
    }

}
