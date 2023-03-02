using System;
using UnityEngine;
using ZeroWin.Generic;
using ZeroWin.Logger;

namespace ZeroWin {

    public class WinAnimPlayer {

        string animName;
        public string AnimName => animName;
        public void SetAnimName(string animName) => this.animName = animName;

        RectTransformModel beforeModel;
        RectTransformModel tarOffsetModel;

        GameObject self;
        public void SetSelt(GameObject self) => this.self = self;

        GameObject tar;

        Action endAction;
        public void SetEndAction(Action endAction) => this.endAction = endAction;

        WinAnimFSMState state;
        public WinAnimFSMState State => state;

        WinAnimModel animModel;

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

            var offsetModel = tar != null ? tarOffsetModel : beforeModel;

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

            var curPos = curveValue_pos * offset_pos + beforeModel.pos;
            var curAngle = curveValue_angle * offsetAngleZ + beforeModel.angle;
            var curScale = curveValue_scale * offset_scale + beforeModel.localScale;

            self.transform.position = curPos;
            self.transform.eulerAngles = new Vector3(0, 0, curAngle);
            self.transform.localScale = curScale;

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
            self.transform.position = beforeModel.pos;
            self.transform.eulerAngles = new Vector3(0, 0, beforeModel.angle);
            self.transform.localScale = beforeModel.localScale;
        }

        public void SetTarget(GameObject tar) {
            this.tar = tar;
            tarOffsetModel.pos = (Vector2)tar.transform.position - beforeModel.pos;
            tarOffsetModel.angle = tar.transform.eulerAngles.z - beforeModel.angle;
            tarOffsetModel.localScale = (Vector2)tar.transform.localScale - beforeModel.localScale;
            WinLogger.Log($"设置动画目标: {tar.name} {tarOffsetModel}");
        }

        public void SetUseCustomOffsetAngle(bool usedCustomOffsetAngle) {
            animModel.usedCustomOffsetAngle = usedCustomOffsetAngle;
        }

        #region [FSM]

        public void EnterPlayining() {
            state = WinAnimFSMState.Playing;
            var rectTrans = self.GetComponent<RectTransform>();
            beforeModel = new RectTransformModel(rectTrans.position, rectTrans.eulerAngles.z, rectTrans.localScale);
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
