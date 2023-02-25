using UnityEngine;
using UnityEngine.UI;

namespace ZeroWindow {

    public class WindowFactory {

        WindowContext context;

        public WindowFactory() {
        }

        public void Inject(WindowContext context) {
            this.context = context;
        }

        public WindowBase CreateWindow(string windowName) {
            var windowAssets = context.WindowAssets;
            if (!windowAssets.TryGetValue(windowName, out var windowPrefab)) {
                Debug.LogError($"Window {windowName} 不存在");
                return null;
            }

            var go = GameObject.Instantiate(windowPrefab);
            GraphicRaycaster gr = go.transform.GetComponent<GraphicRaycaster>();
            if (gr == null) {
                go.AddComponent<GraphicRaycaster>();
            }

            var window = go.GetComponent<WindowBase>();
            window.Create();

            return window;
        }

    }

}
