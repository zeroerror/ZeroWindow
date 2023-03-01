using UnityEngine.EventSystems;
using ZeroWin.Extension;
using ZeroWin.Generic;
using ZeroWin.Logger;

namespace ZeroWin.Sample {

    public class SampleWin : WinBase {

        protected override void OnCreate() {
            WinLogger.Log($"{nameof(SampleWin)}: OnCreate");

            WinExtension.OnPointerDown(gameObject, "btn", OnPointerDown, "Hello World", 123);
            WinExtension.OnPointerDrag(gameObject, "btn", OnPointerDrag, "Hello World Draging------", 123);
        }

        protected override void OnShow() {
            WinLogger.Log($"{nameof(SampleWin)}: OnDisplay");

            var anim = transform.Find("anim").gameObject;
            WinExtension.PlayAnim("111", anim);
            WinExtension.SetAnimLoopType(anim, "111", WinAnimLoopType.OnceAndHide);
        }

        protected override void OnHide() {
            WinLogger.Log($"{nameof(SampleWin)}: OnHide");
        }

        void OnPointerDown(PointerEventData eventData, params object[] args) {
            WinLogger.Log($"{nameof(SampleWin)}: OnPointerDown");
            WinLogger.Log($"args {args[0]} {args[1]}");
        }

        void OnPointerDrag(PointerEventData eventData, params object[] args) {
            WinLogger.Log($"{nameof(SampleWin)}: OnPointerDrag");
            WinLogger.Log($"args {args[0]} {args[1]}");
        }

    }

}