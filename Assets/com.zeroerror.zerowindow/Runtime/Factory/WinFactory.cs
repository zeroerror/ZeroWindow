using UnityEngine;
using UnityEngine.UI;
using ZeroWin.Generic;
using ZeroWin.Extension;
using ZeroWin.Logger;

namespace ZeroWin {

    public class WinFactory {

        WinContext context;

        public WinFactory() {
        }

        public void Inject(WinContext context) {
            this.context = context;
        }

        public WinBase CreateWinBase(string windowName) {
            var windowAssets = context.WinAssets;
            if (!windowAssets.TryGetValue(windowName, out var windowPrefab)) {
                WinLogger.LogError($"Win {windowName} 不存在");
                return null;
            }

            var go = GameObject.Instantiate(windowPrefab);
            GraphicRaycaster gr = go.transform.GetComponent<GraphicRaycaster>();
            if (gr == null) {
                go.AddComponent<GraphicRaycaster>();
            }

            var window = go.GetComponent<WinBase>();
            window.Create();

            return window;
        }

        public WinAnimPlayer CreateAnimPlayer(string animElementName, GameObject self) {
            var winAnim = self.transform.GetComponent<WinAnim>();

            if (!winAnim.TryGetAnimElement(animElementName, out var animElement)) {
                WinLogger.LogError($"Anim {animElementName} 不存在");
                return null;
            }

            WinAnimModel animModel = new WinAnimModel();
            animModel.offsetModel = animElement.GetOffsetModel();
            animModel.animCurve_pos = animElement.animCurve_pos;
            animModel.animCurve_angleZ = animElement.animCurve_angleZ;
            animModel.animCurve_scale = animElement.animCurve_scale;
            animModel.duration = animElement.duration;
            animModel.loopType = animElement.loopType;
            animModel.animName = animElementName;

            WinAnimPlayer animPlayer = new WinAnimPlayer(animModel);
            animPlayer.SetSelt(self);
            animPlayer.SetAnimName(animElementName);
            return animPlayer;
        }

    }

}
