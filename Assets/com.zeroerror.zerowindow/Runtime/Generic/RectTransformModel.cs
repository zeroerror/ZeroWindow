using UnityEngine;

namespace ZeroWin {

    public struct RectTransformModel {

        public Vector2 pos;
        public float angleZ;
        public Vector2 localScale;

        public RectTransformModel(Vector2 pos, float angle, Vector2 localScale) {
            this.pos = pos;
            this.angleZ = angle;
            this.localScale = localScale;
        }

    }

}