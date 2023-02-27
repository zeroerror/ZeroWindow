using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.EventSystems;

namespace ZeroWindow.Editor {

    class WindowEditor {

        [MenuItem("GameObject/ZeroWindow/Button", false, 1)]
        static void UIButton() {
            GameObject selectedGO = GetSelectedGO();
            GameObject buttonGO = new GameObject();
            buttonGO.transform.SetParent(selectedGO.transform);
            buttonGO.name = "Button";
            WinButton button = buttonGO.AddComponent<WinButton>();
            WinImage image = buttonGO.AddComponent<WinImage>();
        }

        [MenuItem("GameObject/ZeroWindow/Image", false, 2)]
        static void WindowImage() {
            GameObject selectedGO = GetSelectedGO();
            GameObject imgGO = new GameObject();
            imgGO.transform.SetParent(selectedGO.transform);
            imgGO.name = "Image";
            imgGO.AddComponent<WinImage>();
        }

        [MenuItem("GameObject/ZeroWindow/Text", false, 3)]
        static void WindowText() {
            GameObject selectedGO = GetSelectedGO();
            GameObject textGO = new GameObject();
            textGO.transform.SetParent(selectedGO.transform);
            textGO.name = "Text";
            textGO.AddComponent<WinText>();
        }

        static GameObject GetSelectedGO() {
            var selectedGO = Selection.activeGameObject;
            if (selectedGO == null) {
                GameObject canvasGO = GameObject.FindObjectOfType<Canvas>()?.gameObject;
                if (canvasGO == null) {
                    Debug.Log("当前没有Canvas, 创建Canvas");
                    canvasGO = new GameObject();

                    var canvas = canvasGO.AddComponent<Canvas>();
                    canvas.renderMode = RenderMode.ScreenSpaceCamera;

                    var graphicRaycaster = canvasGO.AddComponent<GraphicRaycaster>();
                }
                canvasGO.name = "Canvas";

                GameObject eventSystemGO = GameObject.FindObjectOfType<EventSystem>()?.gameObject;
                if (eventSystemGO == null) {
                    Debug.Log("当前没有Canvas, 创建Canvas");
                    eventSystemGO = new GameObject();
                    eventSystemGO.AddComponent<EventSystem>();
                    eventSystemGO.AddComponent<StandaloneInputModule>();
                }
                eventSystemGO.name = "EventSystem";

                selectedGO = canvasGO;
            }

            return selectedGO;
        }

    }

}