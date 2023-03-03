using UnityEngine.EventSystems;
using UnityEngine;
using ZeroWin.Generic;
using ZeroWin.Logger;
using ZeroWin.Extension;

namespace ZeroWin.Sample {

    public class SampleWin : WinBase {

        string aniName;

        GameObject anim1;
        GameObject anim2;
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
            anim1 = transform.Find("anim1").gameObject;
            anim2 = transform.Find("anim2").gameObject;
            btn = transform.Find("btn").gameObject;
            img1 = transform.Find("img1").gameObject;
            img2 = transform.Find("img2").gameObject;
            img3 = transform.Find("img3").gameObject;
            img4 = transform.Find("img4").gameObject;
        }

        protected override void OnShow() {
            WinExtension.Anim_PlayWithTarget(anim1, aniName, img1);
            WinExtension.Anim_SetLoopType(anim1, aniName, WinAnimLoopType.Loop);
            WinExtension.Aim_SetEndAction(anim1, aniName, AnimEndAction1);
            WinExtension.Anim_SetUseCustomOffsetAngle(anim1, aniName, false);

            WinExtension.Anim_PlayWithTarget(anim2, aniName, img1);
            WinExtension.Anim_SetLoopType(anim2, aniName, WinAnimLoopType.Loop);
            WinExtension.Aim_SetEndAction(anim2, aniName, AnimEndAction2);
            WinExtension.Anim_SetUseCustomOffsetAngle(anim2, aniName, false);
        }

        protected override void OnHide() {
        }

        void OnPointerDown(PointerEventData eventData, params object[] args) {
            WinLogger.Log($"OnPointer Down ------- args {args[0]} {args[1]}");
        }

        void OnPointerDrag(PointerEventData eventData, params object[] args) {
            WinLogger.Log($"OnPointer Drag ------- args {args[0]} {args[1]}");
        }

        void AnimEndAction1() {
            WinExtension.Anim_SetTarget(anim1, aniName, img2);

            WinExtension.Aim_SetEndAction(anim1, aniName, () => {
                WinExtension.Anim_SetTarget(anim1, aniName, img3);

                WinExtension.Aim_SetEndAction(anim1, aniName, () => {
                    WinExtension.Anim_SetTarget(anim1, aniName, img4);

                    WinExtension.Aim_SetEndAction(anim1, aniName, () => {
                        WinExtension.Anim_SetTarget(anim1, aniName, img1);

                        WinExtension.Aim_SetEndAction(anim1, aniName, () => {
                            AnimEndAction1();
                        });
                    });
                });
            });
        }

        void AnimEndAction2() {
            WinExtension.Anim_SetTarget(anim2, aniName, img2);

            WinExtension.Aim_SetEndAction(anim2, aniName, () => {
                WinExtension.Anim_SetTarget(anim2, aniName, img3);

                WinExtension.Aim_SetEndAction(anim2, aniName, () => {
                    WinExtension.Anim_SetTarget(anim2, aniName, img4);

                    WinExtension.Aim_SetEndAction(anim2, aniName, () => {
                        WinExtension.Anim_SetTarget(anim2, aniName, img1);

                        WinExtension.Aim_SetEndAction(anim2, aniName, () => {
                            AnimEndAction2();
                        });
                    });
                });
            });
        }

    }

}