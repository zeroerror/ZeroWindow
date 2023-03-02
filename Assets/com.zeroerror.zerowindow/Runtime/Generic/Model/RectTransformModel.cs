using UnityEngine;

namespace ZeroWin {

    public struct RectTransformModel {

        public Vector2 pos;
        public float angle;
        public Vector2 localScale;

        public RectTransformModel(Vector2 pos, float angle, Vector2 localScale) {
            this.pos = pos;
            this.angle = angle;
            this.localScale = localScale;
        }

        public override string ToString() {
            return $"位置 {pos} 角度 {angle} 缩放 {localScale}";
        }

    }

}