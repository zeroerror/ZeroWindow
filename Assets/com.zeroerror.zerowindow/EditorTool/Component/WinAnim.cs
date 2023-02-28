using System.Collections;
using UnityEngine;

namespace ZeroWin {

    public class WinAnim : MonoBehaviour {

        public RectTransform startRect;
        public RectTransform endRect;
        public float offsetAngleZ;
        public AnimationCurve animCurve_pos;
        public AnimationCurve animCurve_angle;
        public AnimationCurve animCurve_scale;

        RectTransformModel startTrans;
        public float duration;

        public bool isPausing;

        void Awake() {
            startTrans = new RectTransformModel();
        }

        float resTime;

        public IEnumerator DisplayAnimEnumerator() {
            if (startRect == null || endRect == null) {
                yield break;
            }

            var startPos = startRect.position;
            var startAngleZ = startRect.rotation.eulerAngles.z;
            var startScale = startRect.localScale;
            startTrans = new RectTransformModel(startPos, startAngleZ, startScale);

            var endPos = endRect.position;
            var endAngleZ = endRect.rotation.eulerAngles.z;
            var endScale = endRect.localScale;

            Vector3 offset_pos = endPos - startPos;
            Vector3 offset_scale = endScale - startScale;

            while (true) {
                while (isPausing) {
                    yield return null;
                }

                var timeProportion = resTime / duration;
                float curveValue_pos = animCurve_pos.Evaluate(timeProportion);
                float curveValue_angle = animCurve_angle.Evaluate(timeProportion);
                float curveValue_scale = animCurve_scale.Evaluate(timeProportion);

                startRect.position = curveValue_pos * offset_pos + startPos;
                startRect.eulerAngles = new Vector3(0, 0, curveValue_angle * offsetAngleZ + startAngleZ);
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
            startRect.eulerAngles = new Vector3(0, 0, startTrans.angleZ);
            startRect.localScale = startTrans.scale;
            resTime = 0;
            isPausing = false;
        }

    }

}
