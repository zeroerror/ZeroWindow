using UnityEngine;

namespace ZeroWin.Generic {


    public struct WinAnimModel {

        public RectTransformModel offsetModel;
        public Vector4 offsetColor;

        public AnimationCurve animCurve_pos;
        public AnimationCurve animCurve_angle;
        public AnimationCurve animCurve_scale;
        public AnimationCurve animCurve_color;
        public AnimationCurve animCurve_normalOffset;

        public bool usedCustomOffsetAngle;
        public float customOffsetAngle;
        public float normalBaseValue;

        public float duration;
        public WinAnimLoopType loopType;
        public string animName;

        public void Reset() {
            offsetModel = default;
            offsetColor = default;

            animCurve_pos = default;
            animCurve_angle = default;
            animCurve_scale = default;
            animCurve_color = default;
            animCurve_normalOffset = default;

            normalBaseValue = 0;
            duration = 0;
        }

    }

}