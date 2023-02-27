using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ZeroWin {

    public class WinButton : Button, IPointerDownHandler, IPointerUpHandler, IDragHandler {

        bool isPressing;
        public bool IsPressing => isPressing;

        // ======= Pointer Down =======
        object[] args_down;
        Action<PointerEventData, object[]> pointerDownAction;
        public void OnPointerDown_Clear() {
            pointerDownAction = null;
            args_down = null;
        }
        public void OnPointerDown(Action<PointerEventData, object[]> action, params object[] args) {
            pointerDownAction += action;
            args_down = args;
        }
        public override void OnPointerDown(PointerEventData eventData) {
            pointerDownAction?.Invoke(eventData, args_down);
            isPressing = true;
        }

        // ======= Pointer Up =======
        object[] args_up;
        Action<PointerEventData, object[]> pointerUpAction;
        public void OnPointerUp_Clear() {
            pointerUpAction = null;
            args_up = null;
        }
        public void OnPointerUp(Action<PointerEventData, object[]> action, params object[] args) {
            pointerUpAction += action;
            args_up = args;
        }
        public override void OnPointerUp(PointerEventData eventData) {
            pointerUpAction?.Invoke(eventData, args_up);
            isPressing = false;
        }

        // ======= Pointer Drag =======
        object[] args_drag;
        Action<PointerEventData, object[]> pointerDragAction;
        public void OnPointerDrag_Clear() {
            pointerDragAction = null;
            args_drag = null;
        }
        public void OnPointerDrag(Action<PointerEventData, object[]> action, params object[] args) {
            pointerDragAction += action;
            args_drag = args;
        }
        public void OnDrag(PointerEventData eventData) {
            pointerDragAction?.Invoke(eventData, args_drag);
        }

    }

}