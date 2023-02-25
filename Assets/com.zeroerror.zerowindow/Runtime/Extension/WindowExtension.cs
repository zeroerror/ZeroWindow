using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using ZeroWindow.Generic;

namespace ZeroWindow.Extension {

    public static class WindowExtension {


        #region [Pointer]

        public static void OnPointerDown(GameObject winGO, string path, Action<PointerEventData, object[]> action, params object[] args) {
            if (!TryGetChild(winGO, path, out var win)) {
                return;
            }

            if (!TryGetComponent<UIButton>(winGO, path, out var button)) {
                win.gameObject.AddComponent<UIButton>();
            }

            button = win.gameObject.GetComponent<UIButton>();
            button.OnPointerDown_Clear();
            button.OnPointerDown(action, args);
        }

        public static void OnPointerUp(GameObject winGO, string path, Action<PointerEventData, object[]> action, params object[] args) {
            if (!TryGetChild(winGO, path, out var win)) {
                return;
            }

            if (!TryGetComponent<UIButton>(winGO, path, out var button)) {
                win.gameObject.AddComponent<UIButton>();
            }

            button = win.gameObject.GetComponent<UIButton>();
            button.OnPointerUp_Clear();
            button.OnPointerUp(action, args);
        }

        public static void OnPointerDrag(GameObject winGO, string path, Action<PointerEventData, object[]> action, params object[] args) {
            if (!TryGetChild(winGO, path, out var win)) {
                return;
            }

            if (!TryGetComponent<UIButton>(winGO, path, out var button)) {
                win.gameObject.AddComponent<UIButton>();
            }

            button = win.gameObject.GetComponent<UIButton>();
            button.OnPointerDrag_Clear();
            button.OnPointerDrag(action, args);
        }

        #endregion

        #region [Slider]
        public static void Slider_SetVal(GameObject winTrans, string path, float val) {
            if (!TryGetComponent<Slider>(winTrans, path, out var slider)) {
                Debug.LogWarning($"Slider {path} 不存在");
                return;
            };

            slider.value = val;
        }

        #endregion

        static bool TryGetComponent<T>(GameObject winTrans, string path, out T component) {
            component = default;

            if (path == winTrans.name) {
                component = winTrans.GetComponent<T>();
                return true;
            }

            if (!TryGetChild(winTrans, path, out var win)) {
                return false;
            }

            component = win.GetComponent<T>();
            return component != null;
        }

        static bool TryGetChild(GameObject winGO, string path, out Transform childTrans) {
            childTrans = winGO.transform.Find(path);
            return childTrans != null;
        }

    }

}