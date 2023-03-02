using UnityEngine.EventSystems;
using UnityEngine;
using ZeroWin.Extension;
using ZeroWin.Generic;
using ZeroWin.Logger;

namespace ZeroWin.Sample {

    public class SampleWin : WinBase {

        string aniName;

        GameObject anim;
        GameObject btn;
        GameObject img1;
        GameObject img2;
        GameObject img3;
        GameObject img4;

        protected override void OnCreate() {
            WinLogger.Log($"{nameof(SampleWin)}: OnCreate");

            WinExtension.OnPointerDown(gameObject, "btn", OnPointerDown, "This is a string", 123);
            WinExtension.OnPointerDrag(gameObject, "btn", OnPointerDrag, "This is another string", 456);

            aniName = "111";
            anim = transform.Find("anim").gameObject;
            btn = transform.Find("btn").gameObject;
            img1 = transform.Find("img1").gameObject;
            img2 = transform.Find("img2").gameObject;
            img3 = transform.Find("img3").gameObject;
            img4 = transform.Find("img4").gameObject;
        }

        protected override void OnShow() {
            WinExtension.Anim_PlayWithTarget(anim, aniName, img1);

            WinExtension.Anim_SetLoopType(anim, aniName, WinAnimLoopType.Loop);

            WinExtension.Aim_SetEndAction(anim, aniName, AnimEndAction);

            WinExtension.Anim_SetUseCustomOffsetAngle(anim, aniName, false);
        }

        protected override void OnHide() {
        }

        void OnPointerDown(PointerEventData eventData, params object[] args) {
            WinLogger.Log($"OnPointer Down ------- args {args[0]} {args[1]}");
        }

        void OnPointerDrag(PointerEventData eventData, params object[] args) {
            WinLogger.Log($"OnPointer Drag ------- args {args[0]} {args[1]}");
        }

        void AnimEndAction() {
            WinExtension.Anim_SetTarget(anim, aniName, img2);

            WinExtension.Aim_SetEndAction(anim, aniName, () => {
                WinExtension.Anim_SetTarget(anim, aniName, img3);

                WinExtension.Aim_SetEndAction(anim, aniName, () => {
                    WinExtension.Anim_SetTarget(anim, aniName, img4);

                    WinExtension.Aim_SetEndAction(anim, aniName, () => {
                        WinExtension.Anim_SetTarget(anim, aniName, img1);
                        
                        WinExtension.Aim_SetEndAction(anim, aniName, () => {
                            AnimEndAction();
                        });
                    });
                });
            });
        }

    }

}