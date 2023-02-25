using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.EventSystems;

namespace ZeroWindowFrame.Editor
{
    public class UIEditor
    {

        [MenuItem("GameObject/ZeroUI/Button", false, 10)]
        public static void UIButton()
        {
            GameObject selectedGO = GetSelectedGO();

            GameObject buttonGO = new GameObject();

            UIButton button = buttonGO.AddComponent<UIButton>();
            UIImage image = buttonGO.AddComponent<UIImage>();

            buttonGO.transform.SetParent(selectedGO.transform);
            image.transform.SetParent(selectedGO.transform);

            buttonGO.name = "Button";
        }

        [MenuItem("GameObject/ZeroUI/Image", false, 10)]
        public static void UIImage()
        {
            GameObject selectedGO = GetSelectedGO();

            GameObject buttonGO = new GameObject();
            UIButton button = new UIButton();
            buttonGO.AddComponent<UIButton>();
            buttonGO.transform.SetParent(selectedGO.transform);
            buttonGO.name = "UIImage";
        }

        [MenuItem("GameObject/ZeroUI/Text", false, 10)]
        public static void UIText()
        {

        }

        static GameObject GetSelectedGO()
        {
            var selectedGO = Selection.activeGameObject;
            if (selectedGO == null)
            {
                GameObject canvasGO = GameObject.FindObjectOfType<Canvas>()?.gameObject;
                if (canvasGO == null)
                {
                    Debug.Log("当前没有Canvas, 创建Canvas");
                    canvasGO = new GameObject();

                    var canvas = canvasGO.AddComponent<Canvas>();
                    canvas.renderMode = RenderMode.ScreenSpaceCamera;

                    var graphicRaycaster = canvasGO.AddComponent<GraphicRaycaster>();
                }
                canvasGO.name = "Canvas";

                GameObject eventSystemGO = GameObject.FindObjectOfType<EventSystem>()?.gameObject;
                if (eventSystemGO == null)
                {
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