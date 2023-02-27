using System.Collections;
using UnityEngine;

namespace ZeroWin {

    public class WinAnim : MonoBehaviour {

        public RectTransform target;
        public AnimationCurve animCurve;
        public Vector3 startPos;
        public Vector3 endPos;
        public float duration;

        public WinAnim() { }

        float resTime;
        int index;
        int keyFrameCount;

        void OnEnable() {
            Reset();
        }

        public IEnumerator DisplayAnimEnumerator() {
            while (true) {
                var offset = endPos - startPos;
                var timeProportion = resTime / duration;
                var curveValue = animCurve.Evaluate(timeProportion);
                target.position = curveValue * offset + startPos;

                var dt = Time.deltaTime;
                resTime += dt;
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
