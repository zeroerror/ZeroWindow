using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using ZeroWin.Generic;

namespace ZeroWin.Extension {

    public static class WinExtension {


        #region [Pointer]

        public static void OnPointerDown(GameObject winGO, string path, Action<PointerEventData, object[]> action, params object[] args) {
            if (!TryGetChild(winGO, path, out var win)) {
                return;
            }

            if (!TryGetComponentByPath<WinButton>(winGO, path, out var button)) {
                win.gameObject.AddComponent<WinButton>();
            }

            button = win.gameObject.GetComponent<WinButton>();
            button.OnPointerDown_Clear();
            button.OnPointerDown(action, args);
        }

        public static void OnPointerUp(GameObject winGO, string path, Action<PointerEventData, object[]> action, params object[] args) {
            if (!TryGetChild(winGO, path, out var win)) {
                return;
            }

            if (!TryGetComponentByPath<WinButton>(winGO, path, out var button)) {
                win.gameObject.AddComponent<WinButton>();
            }

            button = win.gameObject.GetComponent<WinButton>();
            button.OnPointerUp_Clear();
            button.OnPointerUp(action, args);
        }

        public static void OnPointerDrag(GameObject winGO, string path, Action<PointerEventData, object[]> action, params object[] args) {
            if (!TryGetChild(winGO, path, out var win)) {
                return;
            }

            if (!TryGetComponentByPath<WinButton>(winGO, path, out var button)) {
                win.gameObject.AddComponent<WinButton>();
            }

            button = win.gameObject.GetComponent<WinButton>();
            button.OnPointerDrag_Clear();
            button.OnPointerDrag(action, args);
        }

        #endregion

        #region [Slider]

        public static void Slider_SetVal(GameObject trans, string path, float val) {
            if (!TryGetComponentByPath<Slider>(trans, path, out var slider)) {
                Debug.LogWarning($"Slider {path} 不存在");
                return;
            };

            slider.value = val;
        }

        #endregion

        #region [Common]

        public static bool TryGetComponentByPath<T>(GameObject trans, string path, out T component) {
            component = default;

            if (path == trans.name) {
                component = trans.GetComponent<T>();
                return true;
            }

            if (!TryGetChild(trans, path, out var child)) {
                return false;
            }

            component = child.GetComponent<T>();
            return component != null;
        }

        static bool TryGetChild(GameObject winGO, string path, out Transform childTrans) {
            childTrans = winGO.transform.Find(path);
            return childTrans != null;
        }

        #endregion

    }

}