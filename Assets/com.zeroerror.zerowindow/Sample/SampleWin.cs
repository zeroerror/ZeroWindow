using UnityEngine;
using UnityEngine.EventSystems;
using ZeroWin.Extension;

namespace ZeroWin.Sample {

    public class SampleWin : WinBase {

        protected override void OnCreate() {
            Debug.Log("SampleWin: OnCreate");

            WinExtension.OnPointerDown(gameObject, "btn", OnPointerDown, "Hello World", 123);
            WinExtension.OnPointerDrag(gameObject, "btn", OnPointerDrag, "Hello World", 123);
        }

        protected override void OnShow() {
            Debug.Log("SampleWin: OnDisplay");
        }

        protected override void OnHide() {
            Debug.Log("SampleWin: OnHide");
        }

        protected override void OnTick() {
            Debug.Log("SampleWin: OnTick");
        }

        void OnPointerDown(PointerEventData eventData, params object[] args) {
            Debug.Log("SampleWin: OnPointerDown");
            Debug.Log($"args {args[0]} {args[1]}");
        }

        void OnPointerDrag(PointerEventData eventData, params object[] args) {
            Debug.Log("SampleWin: OnPointerDrag");
            Debug.Log($"args {args[0]} {args[1]}");
        }

    }

}