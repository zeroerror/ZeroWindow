using UnityEngine;
using UnityEngine.UI;

namespace ZeroWin {

    public class WinBaseDomain {

        WinContext context;

        public WinBaseDomain() { }

        public void Inject(WinContext context) {
            this.context = context;
        }

        public void TickAllWinBase() {
            var winBaseRepo = context.WinBaseRepo;
            winBaseRepo.ForeachAll(winBase => {
                winBase.Tick();
            });

        }

        public WinBase Show(string windowName, string layerName, params object[] args) {
            var repo = context.WinBaseRepo;
            if (!repo.TryGet(windowName, out var window)) {
                window = context.Factory.CreateWinBase(windowName);
                repo.Add(window);
            }

            var service = context.WinService;
            service.PushToCanvas(window, layerName);

            window.Show();
            return window;

        }

        public void DisplayAll() {
            var repo = context.WinBaseRepo;
            repo.ForeachAll(ui => {
                ui.Show();
            });
        }

        public void Hide(string windowName) {
            var repo = context.WinBaseRepo;
            if (!repo.TryGet(windowName, out var ui)) {
                Debug.LogWarning($"Win {windowName} 不存在");
                return;
            }
            ui.Hide();
        }

        public void HideAll() {
            var repo = context.WinBaseRepo;
            repo.ForeachAll(ui => {
                ui.Hide();
            });
        }

        public void DisposeAllWin() {
            var repo = context.WinBaseRepo;
            repo.ForeachAll(window => {
                window.Dispose();
            });
            repo.ClearAll();
        }

        public void Dispose(string windowName) {
            var repo = context.WinBaseRepo;
            if (!repo.TryGet(windowName, out var window)) {
                Debug.LogWarning($"Win {windowName} 不存在");
                return;
            }
            window.Dispose();
            repo.Remove(windowName);
        }

        #region [Slider]


        #endregion

        #region [Text]

        public void Text_SetFontSize(GameObject windowGO, string path, int size) {
            Text text = null;
            if (!_CheckComponent<Text>(windowGO, path, ref text)) {
                Debug.LogWarning(windowGO.name + ": " + path + ": Text Component Not Found!");
                return;
            };

            text.fontSize = size;
        }

        public void Text_SetColor(GameObject windowGO, string path, string color) {
            Text text = null;
            if (!_CheckComponent<Text>(windowGO, path, ref text)) {
                Debug.LogWarning(windowGO.name + ": " + path + ": Text Component Not Found!");
                return;
            };

            ColorUtility.TryParseHtmlString(color, out Color nowColor);
            text.color = nowColor;
        }

        public void Text_SetAlignment(GameObject windowGO, string path, TextAnchor textAnchor) {
            Text text = null;
            if (!_CheckComponent<Text>(windowGO, path, ref text)) {
                Debug.LogWarning(windowGO.name + ": " + path + ": Text Component Not Found!");
                return;
            };

            text.alignment = textAnchor;
        }

        public void Text_SetText(GameObject windowGO, string path, object content) {
            Text text = null;
            if (!_CheckComponent<Text>(windowGO, path, ref text)) {
                Debug.LogWarning(windowGO.name + ": " + path + ": Text Component Not Found!");
                return;
            };

            text.text = content.ToString();
        }

        public string Input_GetText(GameObject windowGO, string path) {
            InputField inputField = null;
            if (!_CheckComponent<InputField>(windowGO, path, ref inputField)) {
                Debug.LogWarning(windowGO.name + ": " + path + ": InputField Component Not Found!");
                return "";
            }

            return inputField.text;
        }
        #endregion

        #region [Image]

        public void Image_SetFillAmount(GameObject windowGO, string path, float fill) {
            Image image = null;
            if (!_CheckComponent<Image>(windowGO, path, ref image)) {
                Debug.LogWarning(windowGO.name + ": " + path + ": Image Component Not Found!");
                return;
            }

            image.fillAmount = fill;
        }

        public void Image_SetImage(GameObject windowGO, string path, int resourceID) {
            Image image = null;
            if (!_CheckComponent<Image>(windowGO, path, ref image)) {
                Debug.LogWarning(windowGO.name + ": " + path + ": Image Component Not Found!");
                return;
            }

            //image.sprite = ResourceMgr.LoadSprite(resourceID);
        }

        public void Image_SetColor(GameObject windowGO, string path, string color) {
            Image image = null;
            if (!_CheckComponent<Image>(windowGO, path, ref image)) {
                Debug.LogWarning(windowGO.name + ": " + path + ": Image Component Not Found!");
                return;
            }

            ColorUtility.TryParseHtmlString(color, out Color nowColor);
            image.color = nowColor;
        }

        public void RawImage_SetImage(GameObject windowGO, string path, int resourceID) {
            RawImage rawImage = null;
            if (!_CheckComponent<RawImage>(windowGO, path, ref rawImage)) {
                Debug.LogWarning(windowGO.name + ": " + path + ": RawImage Component Not Found!");
                return;
            }

            //rawImage.texture = ResourceMgr.LoadTexture(resourceID);
        }
        #endregion

        bool _CheckComponent<T>(GameObject windowGO, string path, ref T component) {
            Transform ui = null;
            if (path == windowGO.transform.name) ui = windowGO.transform;
            else TryGetChild(windowGO, path, ref ui);
            if (ui == null) return false;

            component = ui.GetComponent<T>();
            if (component == null) {
                return false;
            }
            return component != null;
        }

        bool TryGetChild(GameObject windowGO, string path, ref Transform childWin) {
            childWin = windowGO.transform.Find(path);
            if (childWin == null) {
                return false;
            }
            return true;
        }

    }

}