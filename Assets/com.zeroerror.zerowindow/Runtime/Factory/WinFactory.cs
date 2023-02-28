using UnityEngine;
using UnityEngine.UI;
using ZeroWin.Generic;

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

        public WinAnimPlayer CreateAnimPlayer(string animName) {
            WinAnimModel animModel = new WinAnimModel();
            // TODO: 从资源加载动画配置
            animModel.offsetModel = new RectTransformModel();
            animModel.animCurve_pos = new AnimationCurve();
            animModel.animCurve_angleZ = new AnimationCurve();
            animModel.animCurve_scale = new AnimationCurve();
            animModel.duration = 0f;

            WinAnimPlayer animPlayer = new WinAnimPlayer(animModel);
            return animPlayer;
        }

    }

}
