using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using ZeroWin.Extension;
using ZeroWin.Logger;

namespace ZeroWin.Generic {

    [Serializable]
    public class WinAnimElementModel {

        public RectTransformModel beforeTrans;
        public Vector4 beforeColor;

        [Header("起点")] public RectTransform selfTrans;
        [Header("目标")] public RectTransform tarTrans;

        [Header("使用自定义旋转角度")] public bool usedCustomOffsetAngle;
        [Header("旋转角度")] public float customOffsetAngle;

        [Header("位移曲线")] public AnimationCurve animCurve_pos;
        [Header("旋转曲线")] public AnimationCurve animCurve_angle;
        [Header("缩放曲线")] public AnimationCurve animCurve_scale;
        [Header("RGB曲线")] public AnimationCurve animCurve_color;

        [Header("时长")] public float duration;
        [Header("片段名称")] public string elementName;
        [Header("循环类型")] public WinAnimLoopType loopType;

        // For Editor Preview
        WinAnimFSMState state;
        public WinAnimFSMState AnimState => state;
        public void SetAnimState(WinAnimFSMState state) {
            this.state = state;
        }

        float resTime;

        public void ResetToBefore() {
            if (!IsSetRight()) {
                return;
            }

            selfTrans.position = beforeTrans.pos;
            selfTrans.eulerAngles = new Vector3(0, 0, beforeTrans.angle);
            selfTrans.localScale = beforeTrans.localScale;
            if (selfTrans.TryGetComponent<Image>(out var Image)) {
                Image.color = beforeColor;
            }

            resTime = 0;
        }

        public void RestoreBeforeTrans() {
            if (!IsSetRight()) {
                return;
            }
            var startPos = selfTrans.position;
            var startAngleZ = selfTrans.rotation.eulerAngles.z;
            var startScale = selfTrans.localScale;
            beforeTrans = new RectTransformModel(startPos, startAngleZ, startScale);

            if (selfTrans.TryGetComponent<Image>(out var Image)) {
                beforeColor = Image.color;
            }
        }

        public WinAnimModel GetWinAnimModel() {
            if (!IsSetRight()) {
                return default;
            }

            var startPos = selfTrans.position;
            var endPos = tarTrans.position;
            Vector3 offset_pos = endPos - startPos;

            var startScale = selfTrans.localScale;
            var endScale = tarTrans.localScale;
            Vector3 offset_scale = endScale - startScale;

            WinAnimModel model = new WinAnimModel();
            model.offsetModel = new RectTransformModel(offset_pos, customOffsetAngle, offset_scale); ;
            model.animCurve_pos = animCurve_pos;
            model.animCurve_angle = animCurve_angle;
            model.animCurve_scale = animCurve_scale;
            if (selfTrans.TryGetComponent<Image>(out var Image_self)
            && tarTrans.TryGetComponent<Image>(out var Image_tar)) {
                model.animCurve_color = animCurve_color;
                model.offsetColor = Image_tar.color - Image_self.color;
            }

            model.duration = duration;
            model.loopType = loopType;
            model.animName = elementName;
            model.usedCustomOffsetAngle = usedCustomOffsetAngle;
            model.customOffsetAngle = customOffsetAngle;

            return model;
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

            var offsetAngleZ = usedCustomOffsetAngle ? customOffsetAngle : endAngleZ - startAngleZ;
            Vector3 offset_pos = endPos - startPos;
            Vector3 offset_scale = endScale - startScale;

            // Color
            bool hasColor_self = false;
            Vector4 startColor = Vector4.zero;
            if (selfTrans.TryGetComponent<Image>(out var selfImage)) {
                hasColor_self = true;
                startColor = selfImage.color;
            }
            bool hasColor_tar = false;
            Vector4 endColor = Vector4.zero;
            if (tarTrans.TryGetComponent<Image>(out var tarImage)) {
                hasColor_tar = true;
                endColor = tarImage.color;
            }
            Vector4 offsetColor = endColor - startColor;

            while (true) {
                while (state != WinAnimFSMState.Playing) {
                    yield return null;
                }

                var timeProportion = resTime / duration;
                float curveValue_pos = animCurve_pos.Evaluate(timeProportion);
                float curveValue_angle = animCurve_angle.Evaluate(timeProportion);
                float curveValue_scale = animCurve_scale.Evaluate(timeProportion);

                selfTrans.position = curveValue_pos * offset_pos + startPos;
                selfTrans.eulerAngles = new Vector3(0, 0, curveValue_angle * offsetAngleZ + startAngleZ);
                selfTrans.localScale = curveValue_scale * offset_scale + startScale;

                // Color
                if (hasColor_self && hasColor_tar) {
                    float curveValue_color = animCurve_color.Evaluate(timeProportion);
                    Vector4 curColor = curveValue_color * offsetColor + startColor;
                    selfImage.color = curColor;
                }

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