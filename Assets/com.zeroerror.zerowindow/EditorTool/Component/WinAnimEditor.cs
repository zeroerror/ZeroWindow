using UnityEngine;
using UnityEditor;
using Unity.EditorCoroutines.Editor;
using ZeroWin.Extension;
using ZeroWin.Generic;
using ZeroWin.Logger;

namespace ZeroWin.EditorTool {

    [CustomEditor(typeof(WinAnim))]
    public class WinAnimEditor : Editor {

        WinAnim winAnim;
        WinAnimElementModel[] elements;
        SerializedProperty property_elements;
        EditorCoroutine coroutine;
        const int MAX_COROUNTINE_COUNT = 10;

        void OnEnable() {
            if (target == null) {
                return;
            }

            winAnim = (WinAnim)target;
            elements = winAnim.elements;

            if (elements == null) {
                return;
            }

            var len = elements.Length;
            for (int i = 0; i < len; i++) {
                elements[i].RestoreBeforeTrans();
            }

            property_elements = serializedObject.FindProperty("elements");
        }

        void OnDisable() {
            ResetAllToBefore();
            ResetCoroutine();
            selectedElementName = null;
        }

        #region [Reset]

        void ResetAllToBefore() {
            if (elements == null) {
                return;
            }

            var len = elements.Length;
            for (int i = 0; i < len; i++) {
                var element = elements[i];
                if (element == null || element.AnimState == WinAnimFSMState.Stop) {
                    continue;
                }
                element.ResetToBefore();
            }
        }

        void ResetCoroutine() {
            if (coroutine == null) {
                return;
            }

            EditorCoroutineUtility.StopCoroutine(coroutine);
            coroutine = null;
        }

        #endregion

        #region [GUI]

        string selectedElementName;

        public override void OnInspectorGUI() {
            serializedObject.Update();

            EditorGUILayout.PropertyField(property_elements, new GUIContent("动画元素"));

            var elementCount = elements?.Length;
            for (int i = 0; i < elementCount; i++) {
                var element = elements[i];
                if (!element.IsSetRight()) {
                    continue;
                }

                if (selectedElementName != null && selectedElementName != element.elementName) {
                    continue;
                }

                ShowOneElement(elements[i]);
            }

            serializedObject.ApplyModifiedProperties();
        }

        void ShowOneElement(WinAnimElementModel element) {
            var animState = element.AnimState;
            var elementName = element.elementName;
            var buttonText = $"播放动画 {elementName}";

            GUIStyle style = new GUIStyle("Button");
            style.normal.textColor = Color.green;
            if (animState == WinAnimFSMState.Stop && GUILayout.Button(buttonText, style)) {
                element.RestoreBeforeTrans();
                element.SetAnimState(WinAnimFSMState.Playing);
                coroutine = EditorCoroutineUtility.StartCoroutine(element.DisplayCoroutine(), winAnim);

                selectedElementName = elementName;
                WinLogger.Log($"播放动画 {elementName}");
            }

            style.normal.textColor = Color.red;
            if (animState != WinAnimFSMState.Stop && GUILayout.Button("停止", style)) {
                element.ResetToBefore();
                ResetCoroutine();
                WinLogger.Log($"停止动画 {elementName}");
                selectedElementName = null;
            }

            style.normal.textColor = Color.yellow;
            if (animState == WinAnimFSMState.Playing && GUILayout.Button("暂停", style)) {
                element.SetAnimState(WinAnimFSMState.Pause);
                element.SetPause(true);
                WinLogger.Log($"暂停动画 {elementName}");
            }

            style.normal.textColor = Color.gray;
            if (animState == WinAnimFSMState.Pause && GUILayout.Button("继续", style)) {
                element.SetAnimState(WinAnimFSMState.Playing);
                element.SetPause(false);
                WinLogger.Log($"继续动画 {elementName}");
            }

        }

        #endregion

    }

}
