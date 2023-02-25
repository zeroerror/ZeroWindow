using UnityEngine;
using UnityEngine.UI;

namespace ZeroWindow {

    public class WindowDomain {

        WindowContext context;

        public WindowDomain() { }

        public void Inject(WindowContext context) {
            this.context = context;
        }

        public WindowBase Show(string windowName, string layerName, params object[] args) {
            var repo = context.Repo;
            if (!repo.TryGet(windowName, out var window)) {
                window = context.Factory.CreateWindow(windowName);
                repo.Add(window);
            }

            var service = context.Service;
            service.PushToCanvas(window, layerName);

            window.Show();
            return window;

        }

        public void DisplayAll() {
            var repo = context.Repo;
            repo.ForeachAll(ui => {
                ui.Show();
            });
        }

        public void Hide(string windowName) {
            var repo = context.Repo;
            if (!repo.TryGet(windowName, out var ui)) {
                Debug.LogWarning($"Window {windowName} 不存在");
                return;
            }
            ui.Hide();
        }

        public void HideAll() {
            var repo = context.Repo;
            repo.ForeachAll(ui => {
                ui.Hide();
            });
        }

        public void DisposeAllWindow() {
            var repo = context.Repo;
            repo.ForeachAll(window => {
                window.Dispose();
            });
            repo.ClearAll();
        }

        public void Dispose(string windowName) {
            var repo = context.Repo;
            if (!repo.TryGet(windowName, out var window)) {
                Debug.LogWarning($"Window {windowName} 不存在");
                return;
            }
            window.Dispose();
            repo.Remove(windowName);
        }

        #region [Slider]

        public void Slider_SetVal(GameObject windowGO, string path, float val) {
            Slider slider = null;
            if (!_CheckComponent<Slider>(windowGO, path, ref slider)) {
                Debug.LogWarning(windowGO.name + ": " + path + ": Slider Component Not Found!");
                return;
            };

            slider.value = val;
        }

        #endregion

        #region [Click]

        public void OnPointerDown(GameObject windowGO, string path, Callback func, params object[] args) {
            UIButton button = null;
            Transform ui = null;
            if (!_TryGetChildWindow(windowGO, path, ref ui)) return;

            if (!_CheckComponent<UIButton>(windowGO, path, ref button)) {
                ui.gameObject.AddComponent<UIButton>();
            }

            button = ui.gameObject.GetComponent<UIButton>();
            button.OnPointerDown_Clear();
            button.OnPointerDown((eventData) => {
                func?.Invoke(args, eventData);
            });
        }

        public void OnPointerUp(GameObject windowGO, string path, Callback func, params object[] args) {
            UIButton button = null;
            Transform ui = null;
            if (!_TryGetChildWindow(windowGO, path, ref ui)) return;

            if (!_CheckComponent<UIButton>(windowGO, path, ref button)) {
                ui.gameObject.AddComponent<UIButton>();
            }

            button = ui.gameObject.GetComponent<UIButton>();
            button.OnPointerUp_Clear();
            button.OnPointerUp((eventData) => {
                func?.Invoke(args, eventData);
            });
        }

        public void OnPointerDrag(GameObject windowGO, string path, Callback func, params object[] args) {
            UIButton button = null;
            Transform ui = null;
            if (!_TryGetChildWindow(windowGO, path, ref ui)) return;

            if (!_CheckComponent<UIButton>(windowGO, path, ref button)) {
                ui.gameObject.AddComponent<UIButton>();
            }

            button = ui.gameObject.GetComponent<UIButton>();
            button.OnPointerDrag_Clear();
            button.OnPointerDrag((eventData) => {
                func?.Invoke(args, eventData);
            });
        }

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

        #region [Window]

        public bool TryAddChildWindow(GameObject windowGO, string path, GameObject addChildWindow) {
            if (addChildWindow == null) {
                Debug.LogWarning(windowGO.name + ": AddChildWindow Failed! 不存在Window " + addChildWindow.name);
                return false;
            }

            string childName = addChildWindow.name;
            addChildWindow = GameObject.Instantiate(addChildWindow, windowGO.transform.Find(path));
            addChildWindow.name = childName;
            return true;
        }

        public void SetActive(GameObject windowGO, string childName, bool isActive) {
            Transform childWindow = null;
            if (!_TryGetChildWindow(windowGO, childName, ref childWindow)) {
                Debug.LogWarning(string.Format("{0}: childWindow: {1} 不存在！", windowGO.name, childName));
                return;
            }

            childWindow.gameObject.SetActive(isActive);
        }

        #endregion

        #region [Common]

        public T GetComponentFromChild<T>(GameObject windowGO, string name) where T : Component {
            return windowGO.transform.Find(name).GetComponent<T>();
        }

        #endregion

        bool _CheckComponent<T>(GameObject windowGO, string path, ref T component) {
            Transform ui = null;
            if (path == windowGO.transform.name) ui = windowGO.transform;
            else _TryGetChildWindow(windowGO, path, ref ui);
            if (ui == null) return false;

            component = ui.GetComponent<T>();
            if (component == null) {
                return false;
            }
            return component != null;
        }

        bool _TryGetChildWindow(GameObject windowGO, string path, ref Transform childWindow) {
            childWindow = windowGO.transform.Find(path);
            if (childWindow == null) {
                return false;
            }
            return true;
        }

    }

}