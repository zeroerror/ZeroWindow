using System.Collections;
using UnityEngine;
using ZeroWin.Generic;

namespace ZeroWin.Extension {

    public class WinAnim : MonoBehaviour {

        public RectTransform startRect;
        public RectTransform endRect;

        public float offsetAngleZ;
        public AnimationCurve animCurve_pos;
        public AnimationCurve animCurve_angleZ;
        public AnimationCurve animCurve_scale;

        RectTransformModel startTrans;

        float resTime;
        public float duration;
        public bool isPausing;

        public WinAnimLoopType loopType;
        public string animName;

        void Awake() {
            startTrans = new RectTransformModel();
        }

        public void Reset() {
            if (startRect == null || endRect == null) {
                return;
            }

            startRect.position = startTrans.pos;
            startRect.eulerAngles = new Vector3(0, 0, startTrans.angleZ);
            startRect.localScale = startTrans.localScale;
            resTime = 0;
            isPausing = false;
        }

        public RectTransformModel GetOffsetModel() {
            if (startRect == null || endRect == null) {
                return default;
            }

            var startPos = startRect.position;
            var endPos = endRect.position;
            Vector3 offset_pos = endPos - startPos;

            var startScale = startRect.localScale;
            var endScale = endRect.localScale;
            Vector3 offset_scale = endScale - startScale;

            return new RectTransformModel(offset_pos, offsetAngleZ, offset_scale);
        }

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
                float curveValue_angle = animCurve_angleZ.Evaluate(timeProportion);
                float curveValue_scale = animCurve_scale.Evaluate(timeProportion);

                startRect.position = curveValue_pos * offset_pos + startPos;
                startRect.eulerAngles = new Vector3(0, 0, curveValue_angle * offsetAngleZ + startAngleZ);
                startRect.localScale = curveValue_scale * offset_scale + startScale;

                resTime += Time.deltaTime;
                resTime = resTime > duration ? 0 : resTime;
                yield return null;
            }
        }

    }

}
