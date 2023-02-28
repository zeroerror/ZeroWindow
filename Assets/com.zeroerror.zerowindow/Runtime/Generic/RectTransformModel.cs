using UnityEngine;

namespace ZeroWin {

    public struct RectTransformModel {

        public Vector2 pos;
        public float angleZ;
        public Vector2 scale;

        public RectTransformModel(Vector2 pos, float angle, Vector2 scale) {
            this.pos = pos;
            this.angleZ = angle;
            this.scale = scale;
        }

    }

}