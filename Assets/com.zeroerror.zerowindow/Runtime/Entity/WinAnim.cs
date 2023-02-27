using System.Collections;
using UnityEngine;

namespace ZeroWin {

    public class WinAnim : MonoBehaviour {

        public RectTransform startRect;

        public RectTransform endRect;

        public AnimationCurve animCurve;

        public float duration;

        public WinAnim() { }

        float resTime;
        int index;
        int keyFrameCount;

        void OnEnable() {
            Reset();
        }

        public IEnumerator DisplayAnimEnumerator() {
            var startPos = startRect.position;
            var endPos = endRect.position;
            var offset_pos = endPos - startPos;

            var startScale = startRect.localScale;
            var endScale = endRect.localScale;
            var offset_scale = endScale - startScale;

            while (true) {
                var timeProportion = resTime / duration;
                var curveValue = animCurve.Evaluate(timeProportion);

                startRect.position = curveValue * offset_pos + startPos;
                startRect.localScale = curveValue * offset_scale + startScale;

                resTime += Time.deltaTime;
                resTime = resTime > duration ? 0 : resTime;
                yield return null;
            }

        }

        void Reset() {
            resTime = 0;
            index = 0;
            keyFrameCount = animCurve.keys.Length;
        }

    }

}
