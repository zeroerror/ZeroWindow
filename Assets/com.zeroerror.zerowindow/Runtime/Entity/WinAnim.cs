using System.Collections;
using UnityEngine;

namespace ZeroWin {


    public class WinAnim : MonoBehaviour {

        struct Trans {

            public Vector3 pos;
            public Vector3 angle;
            public Vector3 scale;

            public Trans(Vector3 pos, Vector3 angle, Vector3 scale) {
                this.pos = pos;
                this.angle = angle;
                this.scale = scale;
            }

        }

        public RectTransform startRect;
        public RectTransform endRect;
        public AnimationCurve animCurve_pos;
        public AnimationCurve animCurve_angle;
        public AnimationCurve animCurve_scale;

        Trans startTrans;
        public float duration;

        public bool isPausing;

        public WinAnim() {
            startTrans = new Trans();
        }

        float resTime;

        public IEnumerator DisplayAnimEnumerator() {
            if (startRect == null || endRect == null) {
                yield break;
            }

            var startPos = startRect.position;
            var startAngle = startRect.eulerAngles;
            var startScale = startRect.localScale;
            startTrans = new Trans(startPos, startAngle, startScale);

            var endPos = endRect.position;
            var endAngle = endRect.eulerAngles;
            var endScale = endRect.localScale;

            var offset_pos = endPos - startPos;
            var offset_angle = endAngle - startAngle;
            var offset_scale = endScale - startScale;

            while (true) {
                while (isPausing) {
                    yield return null;
                }

                var timeProportion = resTime / duration;
                var curveValue_pos = animCurve_pos.Evaluate(timeProportion);
                var curveValue_angle = animCurve_angle.Evaluate(timeProportion);
                var curveValue_scale = animCurve_scale.Evaluate(timeProportion);

                startRect.position = curveValue_pos * offset_pos + startPos;
                startRect.eulerAngles = curveValue_angle * offset_angle + startAngle;
                startRect.localScale = curveValue_scale * offset_scale + startScale;

                resTime += Time.deltaTime;
                resTime = resTime > duration ? 0 : resTime;
                yield return null;
            }
        }

        public void Reset() {
            if (startRect == null || endRect == null) {
                return;
            }

            startRect.position = startTrans.pos;
            startRect.eulerAngles = startTrans.angle;
            startRect.localScale = startTrans.scale;
            resTime = 0;
            isPausing = false;
        }

    }

}
