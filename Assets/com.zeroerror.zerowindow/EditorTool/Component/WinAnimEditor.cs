using UnityEngine;
using UnityEditor;
using Unity.EditorCoroutines.Editor;

namespace ZeroWin.EditorTool {

    [CustomEditor(typeof(WinAnim))]
    public class WinAnimEditor : Editor {

        WinAnim winAnim;

        SerializedProperty property_startRect;
        SerializedProperty property_endRect;
        SerializedProperty property_offsetAngleZ;

        SerializedProperty property_animCurve_pos;
        SerializedProperty property_animCurve_angle;
        SerializedProperty property_animCurve_scale;
        SerializedProperty property_duration;
        SerializedProperty property_isPausing;

        EditorCoroutine coroutine;

        WinAnimFSMState animState = WinAnimFSMState.Stop;

        void OnEnable() {
            winAnim = (WinAnim)target;
            property_startRect = serializedObject.FindProperty("startRect");
            property_endRect = serializedObject.FindProperty("endRect");
            property_offsetAngleZ = serializedObject.FindProperty("offsetAngleZ");

            property_animCurve_pos = serializedObject.FindProperty("animCurve_pos");
            property_animCurve_angle = serializedObject.FindProperty("animCurve_angle");
            property_animCurve_scale = serializedObject.FindProperty("animCurve_scale");
            property_duration = serializedObject.FindProperty("duration");
            property_isPausing = serializedObject.FindProperty("isPausing");
            ResetAnimDisplay();
        }

        void OnDisable() {
            ResetAnimDisplay();
            animState = WinAnimFSMState.Stop;
        }

        void ResetAnimDisplay() {
            if (coroutine == null) {
                return;
            }
            EditorCoroutineUtility.StopCoroutine(coroutine);
            winAnim.Reset();
        }

        public override void OnInspectorGUI() {
            serializedObject.Update();

            GUIStyle style = new GUIStyle("Button");
            style.normal.textColor = Color.green;
            if (animState == WinAnimFSMState.Stop && GUILayout.Button("播放", style)) {
                animState = WinAnimFSMState.Playing;
                ResetAnimDisplay();
                coroutine = EditorCoroutineUtility.StartCoroutine(winAnim.DisplayAnimEnumerator(), winAnim);
            }

            style.normal.textColor = Color.red;
            if (animState != WinAnimFSMState.Stop && GUILayout.Button("停止", style)) {
                animState = WinAnimFSMState.Stop;
                ResetAnimDisplay();
            }

            style.normal.textColor = Color.yellow;
            if (animState == WinAnimFSMState.Playing && GUILayout.Button("暂停", style)) {
                animState = WinAnimFSMState.Pause;
                property_isPausing.boolValue = true;
            }

            style.normal.textColor = Color.gray;
            if (animState == WinAnimFSMState.Pause && GUILayout.Button("继续", style)) {
                animState = WinAnimFSMState.Playing;
                property_isPausing.boolValue = false;
            }

            EditorGUILayout.PropertyField(property_startRect, new GUIContent("起始位置"));
            EditorGUILayout.PropertyField(property_endRect, new GUIContent("结束位置"));
            EditorGUILayout.PropertyField(property_offsetAngleZ, new GUIContent("旋转角度"));

            EditorGUILayout.PropertyField(property_duration, new GUIContent("动画时长"));
            EditorGUILayout.PropertyField(property_animCurve_pos, new GUIContent("动画曲线 - 位置"));
            EditorGUILayout.PropertyField(property_animCurve_angle, new GUIContent("动画曲线 - 角度"));
            EditorGUILayout.PropertyField(property_animCurve_scale, new GUIContent("动画曲线 - 缩放"));
            serializedObject.ApplyModifiedProperties();

            style.normal.textColor = Color.red;
            if (GUILayout.Button("保存(尚未实现)", style)) {
                Save();
            }
        }


        void Save() {

        }


    }

}
