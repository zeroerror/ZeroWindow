using UnityEngine;
using UnityEngine.UI;
using ZeroWin.Generic;
using ZeroWin.Extension;

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
                Debug.LogError($"Win {windowName} 不存在");
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

        public WinAnimPlayer CreateAnimPlayer(string winAnimName, GameObject self) {
            var winAnim = self.transform.GetComponent<WinAnim>();
            if (winAnim.animName != winAnimName) {
                Debug.LogError($"GameObject {self.name} WinAnim {winAnimName} 不存在");
                return null;
            }

            WinAnimModel animModel = new WinAnimModel();
            animModel.offsetModel = winAnim.GetOffsetModel();
            animModel.animCurve_pos = winAnim.animCurve_pos;
            animModel.animCurve_angleZ = winAnim.animCurve_angleZ;
            animModel.animCurve_scale = winAnim.animCurve_scale;
            animModel.duration = winAnim.duration;
            animModel.loopType = winAnim.loopType;
            animModel.animName = winAnimName;

            WinAnimPlayer animPlayer = new WinAnimPlayer(animModel);
            animPlayer.SetSelt(self);
            animPlayer.SetAnimName(winAnimName);
            return animPlayer;
        }

    }

}
