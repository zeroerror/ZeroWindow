using UnityEngine;
using UnityEngine.UI;

namespace ZeroWin {

    public class WinFactory {

        WinContext context;

        public WinFactory() {
        }

        public void Inject(WinContext context) {
            this.context = context;
        }

        public WinBase CreateWin(string windowName) {
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

    }

}
