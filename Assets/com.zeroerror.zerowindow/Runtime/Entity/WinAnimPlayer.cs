using UnityEngine;
using ZeroWin.Generic;

namespace ZeroWin {

    public class WinAnimPlayer {

        string animName;
        public string AnimName => animName;
        public void SetAnimName(string animName) => this.animName = animName;

        GameObject self;
        public void SetSelt(GameObject self) => this.self = self;

        RectTransformModel beforeModel;

        GameObject tar;
        public void SetTarget(GameObject tar) => this.tar = tar;

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
            var offset_pos = animModel.offsetModel.pos;
            var offsetAngleZ = animModel.offsetModel.angleZ;
            var offset_scale = animModel.offsetModel.localScale;
            var animCurve_pos = animModel.animCurve_pos;
            var animCurve_angleZ = animModel.animCurve_angleZ;
            var animCurve_scale = animModel.animCurve_scale;

            var timeProportion = resTime / duration;
            float curveValue_pos = animCurve_pos.Evaluate(timeProportion);
            float curveValue_angle = animCurve_angleZ.Evaluate(timeProportion);
            float curveValue_scale = animCurve_scale.Evaluate(timeProportion);

            var curPos = curveValue_pos * offset_pos + beforeModel.pos;
            var curAngle = curveValue_angle * offsetAngleZ + beforeModel.angleZ;
            var curScale = curveValue_scale * offset_scale + beforeModel.localScale;

            self.transform.position = curPos;
            self.transform.eulerAngles = new Vector3(0, 0, curAngle);
            self.transform.localScale = curScale;

            resTime += dt;

            if (resTime > duration) {
                resTime = 0;
                RefreshStateEveryPeriod();
            }
        }

        public void SetLoopType(WinAnimLoopType loopType) {
            animModel.loopType = loopType;
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
            self.transform.eulerAngles = new Vector3(0, 0, beforeModel.angleZ);
            self.transform.localScale = beforeModel.localScale;
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
