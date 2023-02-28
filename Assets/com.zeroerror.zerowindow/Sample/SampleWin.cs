using UnityEngine;
using UnityEngine.EventSystems;
using ZeroWin.Extension;

namespace ZeroWin.Sample {

    public class SampleWin : WinBase {

        protected override void OnCreate() {
            Debug.Log("SampleWin: OnCreate");

            WinExtension.OnPointerDown(gameObject, "btn", OnPointerDown, "Hello World", 123);
            WinExtension.OnPointerDrag(gameObject, "btn", OnPointerDrag, "Hello World Draging------", 123);
        }

        protected override void OnShow() {
            Debug.Log("SampleWin: OnDisplay");

            var anim1 = transform.Find("anim1").gameObject;
            var anim2 = transform.Find("anim2").gameObject;
            var anim3 = transform.Find("anim3").gameObject;
            var anim4 = transform.Find("anim4").gameObject;
            var anim5 = transform.Find("anim5").gameObject;
            WinExtension.PlayAnim("anim_win_test",anim1);
            WinExtension.PlayAnim("anim_win_test",anim2);
            WinExtension.PlayAnim("anim_win_test",anim3);
            WinExtension.PlayAnim("anim_win_test",anim4);
            WinExtension.PlayAnim("anim_win_test",anim5);
        }

        protected override void OnHide() {
            Debug.Log("SampleWin: OnHide");
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