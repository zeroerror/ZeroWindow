using UnityEngine;

namespace ZeroWin.Generic {

    public struct WinAnimModel {

        public RectTransformModel offsetModel;
        public AnimationCurve animCurve_pos;
        public AnimationCurve animCurve_angleZ;
        public AnimationCurve animCurve_scale;
        public float duration;
        public WinAnimLoopType loopType;
        public string animName;

        public void Reset() {
            offsetModel = default;
            animCurve_pos = default;
            animCurve_angleZ = default;
            animCurve_scale = default;
            duration = 0;
        }

    }

}