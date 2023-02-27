using UnityEngine;
using UnityEngine.EventSystems;
using ZeroWindow.Extension;

namespace ZeroWindow.Sample {

    public class SampleWindow : WindowEntity {

        protected override void OnCreate() {
            Debug.Log("SampleWindow: OnCreate");

            WindowExtension.OnPointerDown(gameObject, "btn", OnPointerDown, "Hello World", 123);
            WindowExtension.OnPointerDrag(gameObject, "btn", OnPointerDrag, "Hello World", 123);
        }

        protected override void OnShow() {
            Debug.Log("SampleWindow: OnDisplay");
        }

        protected override void OnHide() {
            Debug.Log("SampleWindow: OnHide");
        }

        protected override void OnTick() {
            Debug.Log("SampleWindow: OnTick");
        }

        void OnPointerDown(PointerEventData eventData, params object[] args) {
            Debug.Log("SampleWindow: OnPointerDown");
            Debug.Log($"args {args[0]} {args[1]}");
        }

        void OnPointerDrag(PointerEventData eventData, params object[] args) {
            Debug.Log("SampleWindow: OnPointerDrag");
            Debug.Log($"args {args[0]} {args[1]}");
        }

    }

}