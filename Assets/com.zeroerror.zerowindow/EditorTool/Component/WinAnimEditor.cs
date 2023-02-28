using UnityEngine;
using UnityEditor;
using Unity.EditorCoroutines.Editor;
using ZeroWin.Extension;
using ZeroWin.Generic;
using System.Collections.Generic;

namespace ZeroWin.EditorTool {

    [CustomEditor(typeof(WinAnim))]
    public class WinAnimEditor : Editor {

        WinAnim winAnim;
        WinAnimElementModel[] elements;
        SerializedProperty property_animElements;
        EditorCoroutine[] coroutineArray;

        void OnEnable() {
            if (target == null) {
                return;
            }

            winAnim = (WinAnim)target;
            elements = winAnim.animElements;
            property_animElements = serializedObject.FindProperty("animElements");

            ResetAllElement();
            coroutineArray = new EditorCoroutine[10];   // 启用协程数量限制
        }

        void OnDisable() {
            ResetAllElement();
            if (coroutineArray == null) {
                return;
            }

            var corLength = coroutineArray.Length;
            for (int i = 0; i < corLength; i++) {
                var cor = coroutineArray[i];
                if (cor == null) {
                    continue;
                }

                EditorCoroutineUtility.StopCoroutine(cor);
            }
            coroutineArray = null;
        }

        void ResetAllElement() {
            if (coroutineArray == null) {
                return;
            }
            winAnim.ResetAllElement();
        }

        public override void OnInspectorGUI() {
            serializedObject.Update();

            EditorGUILayout.PropertyField(property_animElements, new GUIContent("动画元素"));

            var elementCount = elements?.Length;
            for (int i = 0; i < elementCount; i++) {
                ShowOneElement(elements[i], i);
            }

            serializedObject.ApplyModifiedProperties();
        }

        void ShowOneElement(WinAnimElementModel element, int index) {
            var animState = element.AnimState;
            var property_isPausing = element.IsPausing;
            var property_startRect = element.startRect;
            var property_endRect = element.endRect;
            var property_offsetAngleZ = element.offsetAngleZ;

            GUIStyle style = new GUIStyle("Button");
            style.normal.textColor = Color.green;
            if (animState == WinAnimFSMState.Stop && GUILayout.Button("播放", style)) {
                element.SetAnimState(WinAnimFSMState.Playing);
                element.Reset();
                coroutineArray[index] = EditorCoroutineUtility.StartCoroutine(element.DisplayCoroutine(), winAnim);
            }

            style.normal.textColor = Color.red;
            if (animState != WinAnimFSMState.Stop && GUILayout.Button("停止", style)) {
                element.SetAnimState(WinAnimFSMState.Stop);
                element.Reset();
            }

            style.normal.textColor = Color.yellow;
            if (animState == WinAnimFSMState.Playing && GUILayout.Button("暂停", style)) {
                element.SetAnimState(WinAnimFSMState.Pause);
                property_isPausing = true;
            }

            style.normal.textColor = Color.gray;
            if (animState == WinAnimFSMState.Pause && GUILayout.Button("继续", style)) {
                element.SetAnimState(WinAnimFSMState.Playing);
                property_isPausing = false;
            }

        }

    }

}
