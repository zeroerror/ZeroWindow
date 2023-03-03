using UnityEngine;

namespace ZeroWin.Generic {


    public struct WinAnimModel {

        public bool usedCustomOffsetAngle;
        public float customOffsetAngle;

        public RectTransformModel offsetModel;
        public Vector4 offsetColor;

        public AnimationCurve animCurve_pos;
        public AnimationCurve animCurve_angle;
        public AnimationCurve animCurve_scale;
        public AnimationCurve animCurve_color;

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
            
            duration = 0;
        }

    }

}