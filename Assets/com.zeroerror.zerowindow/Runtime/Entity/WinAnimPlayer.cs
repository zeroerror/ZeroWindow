using System;
using UnityEngine;
using UnityEngine.UI;
using ZeroWin.Component;
using ZeroWin.Generic;
using ZeroWin.Logger;

namespace ZeroWin {

    public class WinAnimPlayer {

        string animName;
        public string AnimName => animName;
        public void SetAnimName(string animName) => this.animName = animName;

        GameObject self;
        GameObject tar;

        Action endAction;
        public void SetEndAction(Action endAction) => this.endAction = endAction;

        // Anim Model
        WinAnimModel animModel;

        // Cache
        RectTransform selfRectTrans;
        Image selfImage;

        // Self
        RectTransformModel selfModel;
        Vector4 selfColor;
        bool hasImage_self;

        // Target
        RectTransformModel toTarOffsetModel;
        Vector4 toTarOffsetColor;
        bool hasImage_tar;

        WinAnimFSMState state;
        public WinAnimFSMState State => state;

        float resTime;

        public WinAnimPlayer(WinAnimModel animModel) {
            this.animModel = animModel;
        }

        public void Reset() {
            state = WinAnimFSMState.Stop;
            animModel.Reset();
            resTime = 0;
        }

        public void Tick(float dt) {
            if (state != WinAnimFSMState.Playing) {
                return;
            }

            PlayAnim(dt);
        }

        void PlayAnim(float dt) {
            var duration = animModel.duration;

            bool hasTar = tar != null;
            var offsetModel = hasTar ? toTarOffsetModel : selfModel;

            var offset_pos = offsetModel.pos;
            var offsetAngleZ = animModel.usedCustomOffsetAngle ? animModel.customOffsetAngle : offsetModel.angle;
            var offset_scale = offsetModel.localScale;

            var animCurve_pos = animModel.animCurve_pos;
            var animCurve_angle = animModel.animCurve_angle;
            var animCurve_scale = animModel.animCurve_scale;

            var timeProportion = resTime / duration;
            float curveValue_pos = animCurve_pos.Evaluate(timeProportion);
            float curveValue_angle = animCurve_angle.Evaluate(timeProportion);
            float curveValue_scale = animCurve_scale.Evaluate(timeProportion);

            var curPos = curveValue_pos * offset_pos + selfModel.pos;
            var curAngle = curveValue_angle * offsetAngleZ + selfModel.angle;
            var curScale = curveValue_scale * offset_scale + selfModel.localScale;

            self.transform.position = curPos;
            self.transform.eulerAngles = new Vector3(0, 0, curAngle);
            self.transform.localScale = curScale;

            // Color
            if (hasImage_self && hasImage_tar) {
                var animCurve_color = animModel.animCurve_color;
                float curveValue_color = animCurve_color.Evaluate(timeProportion);
                Vector4 curColor = Vector4.zero;
                if (hasTar) {
                    curColor = curveValue_color * toTarOffsetColor + selfColor;
                } else {
                    curColor = curveValue_color * animModel.offsetColor + selfColor;
                }
                selfImage.color = curColor;
            }

            resTime += dt;

            if (resTime > duration) {
                resTime = 0;
                RefreshStateEveryPeriod();
                endAction?.Invoke();
            }
        }

        public void SetLoopType(WinAnimLoopType loopType) {
            animModel.loopType = loopType;
        }

        public void ResumeAnim() {
            state = WinAnimFSMState.Playing;
        }

        void RefreshStateEveryPeriod() {
            if (animModel.loopType == WinAnimLoopType.OnceAndStay) {
                state = WinAnimFSMState.Stop;
            } else if (animModel.loopType == WinAnimLoopType.OnceAndReset) {
                state = WinAnimFSMState.Stop;
                ResetToBefore();
            } else if (animModel.loopType == WinAnimLoopType.OnceAndHide) {
                state = WinAnimFSMState.Stop;
                self.SetActive(false);
            } else if (animModel.loopType == WinAnimLoopType.Loop) {
                ResetToBefore();
            }
        }

        void ResetToBefore() {
            self.transform.position = selfModel.pos;
            self.transform.eulerAngles = new Vector3(0, 0, selfModel.angle);
            self.transform.localScale = selfModel.localScale;
            if(hasImage_self){
                selfImage.color = selfColor;
            }
        }

        public void SetSelt(GameObject self) {
            this.self = self;
            this.selfRectTrans = self.GetComponent<RectTransform>();
            this.selfModel = new RectTransformModel(selfRectTrans.position, selfRectTrans.eulerAngles.z, selfRectTrans.localScale);

            if (self.TryGetComponent<Image>(out selfImage)) {
                this.selfColor = selfImage.color;
                this.hasImage_self = true;
            }
        }

        public void SetTarget(GameObject tar) {
            this.tar = tar;
            toTarOffsetModel.pos = (Vector2)tar.transform.position - selfModel.pos;
            toTarOffsetModel.angle = tar.transform.eulerAngles.z - selfModel.angle;
            toTarOffsetModel.localScale = (Vector2)tar.transform.localScale - selfModel.localScale;
            if (tar.TryGetComponent<Image>(out var tarImage) && hasImage_self) {
                hasImage_tar = true;
                toTarOffsetColor = tarImage.color - selfImage.color;
            }
            WinLogger.Log($"设置动画目标: {tar.name} {toTarOffsetModel} ColorOffset: {toTarOffsetColor}");
        }

        public void SetUseCustomOffsetAngle(bool usedCustomOffsetAngle) {
            animModel.usedCustomOffsetAngle = usedCustomOffsetAngle;
        }

        #region [FSM]

        public void EnterPlayining() {
            state = WinAnimFSMState.Playing;
        }

        public void EnterPause() {
            state = WinAnimFSMState.Pause;
        }

        public void EnterStop() {
            state = WinAnimFSMState.Stop;
        }

        #endregion

    }
}
