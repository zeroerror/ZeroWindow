using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using ZeroWin.Generic;
using ZeroWin.Logger;

namespace ZeroWin.Extension {

    public static class WinExtension {

        static WinContext winContext;

        public static void Inject(WinContext context) {
            winContext = context;
        }

        public static void Dispose() {
            winContext = null;
        }

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
                WinLogger.LogWarning($"Slider {path} 不存在");
                return;
            };

            slider.value = val;
        }

        #endregion

        #region [Common]

        public static bool TryGetComponentByPath<T>(GameObject go, string path, out T component) {
            component = default;

            if (path == go.name) {
                component = go.GetComponent<T>();
                return true;
            }

            if (!TryGetChild(go, path, out var child)) {
                return false;
            }

            component = child.GetComponent<T>();
            return component != null;
        }

        static bool TryGetChild(GameObject go, string path, out Transform childTrans) {
            childTrans = go.transform.Find(path);
            return childTrans != null;
        }

        #endregion

        #region [Anim]

        public static void PlayAnim(string winAnimName, GameObject self) {
            var aniDomain = winContext.WinAnimDomain;
            aniDomain.PlayAnim(winAnimName, self);
        }

        public static void PlayAnimWithTarget(string winAnimName, GameObject self, GameObject tar) {
            var aniDomain = winContext.WinAnimDomain;
            aniDomain.PlayAnimWithTarget(self, winAnimName, tar);
        }

        public static void SetAnimLoopType(GameObject self, string winAnimName, WinAnimLoopType loopType) {
            var aniDomain = winContext.WinAnimDomain;
            aniDomain.SetAnimLoopType(self, winAnimName, loopType);
        }

        #endregion

    }

}