using System;
using System.Collections;
using UnityEngine;

namespace ZeroWin.Generic {

    [Serializable]
    public class WinAnimElementModel {

        public RectTransformModel beforeTrans;

        public RectTransform selfTrans;
        public RectTransform tarTrans;

        [Header("使用自定义旋转角度")]
        public bool usedCustomOffsetAngle;
        public float customOffsetAngleZ;

        public AnimationCurve animCurve_pos;
        public AnimationCurve animCurve_angle;
        public AnimationCurve animCurve_scale;
        public WinAnimLoopType loopType;
        public string elementName;
        public float duration;

        float resTime;

        bool isPaused;
        public bool IsPaused => isPaused;
        public void SetPause(bool isPause) => this.isPaused = isPause;

        // For Editor Preview
        WinAnimFSMState animState;
        public WinAnimFSMState AnimState => animState;
        public void SetAnimState(WinAnimFSMState state) => animState = state;

        public void ResetToBefore() {
            if (!IsSetRight()) {
                return;
            }

            selfTrans.position = beforeTrans.pos;
            selfTrans.eulerAngles = new Vector3(0, 0, beforeTrans.angle);
            selfTrans.localScale = beforeTrans.localScale;
            resTime = 0;
            isPaused = false;
            animState = WinAnimFSMState.Stop;
        }

        public void RestoreBeforeTrans() {
            if (!IsSetRight()) {
                return;
            }
            var startPos = selfTrans.position;
            var startAngleZ = selfTrans.rotation.eulerAngles.z;
            var startScale = selfTrans.localScale;
            beforeTrans = new RectTransformModel(startPos, startAngleZ, startScale);
        }

        public RectTransformModel GetOffsetModel() {
            if (!IsSetRight()) {
                return default;
            }

            var startPos = selfTrans.position;
            var endPos = tarTrans.position;
            Vector3 offset_pos = endPos - startPos;

            var startScale = selfTrans.localScale;
            var endScale = tarTrans.localScale;
            Vector3 offset_scale = endScale - startScale;

            return new RectTransformModel(offset_pos, customOffsetAngleZ, offset_scale);
        }

        public IEnumerator DisplayCoroutine() {
            if (!IsSetRight()) {
                yield break;
            }

            var startPos = selfTrans.position;
            var startAngleZ = selfTrans.rotation.eulerAngles.z;
            var startScale = selfTrans.localScale;

            var endPos = tarTrans.position;
            var endAngleZ = tarTrans.rotation.eulerAngles.z;
            var endScale = tarTrans.localScale;

            var offsetAngleZ = usedCustomOffsetAngle ? customOffsetAngleZ : endAngleZ - startAngleZ;

            Vector3 offset_pos = endPos - startPos;
            Vector3 offset_scale = endScale - startScale;

            while (true) {
                while (isPaused) {
                    yield return null;
                }

                var timeProportion = resTime / duration;
                float curveValue_pos = animCurve_pos.Evaluate(timeProportion);
                float curveValue_angle = animCurve_angle.Evaluate(timeProportion);
                float curveValue_scale = animCurve_scale.Evaluate(timeProportion);

                selfTrans.position = curveValue_pos * offset_pos + startPos;
                selfTrans.eulerAngles = new Vector3(0, 0, curveValue_angle * offsetAngleZ + startAngleZ);
                selfTrans.localScale = curveValue_scale * offset_scale + startScale;

                resTime += Time.deltaTime;
                resTime = resTime > duration ? 0 : resTime;
                yield return null;
            }
        }

        public bool IsSetRight() {
            return selfTrans != null && tarTrans != null;
        }

    }

}