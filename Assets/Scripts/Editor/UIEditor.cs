using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.EventSystems;

namespace ZeroUIFrame
{
    public class UIEditor
    {

        [MenuItem("GameObject/ZeroUI/Button", false, 10)]
        public static void UIButton()
        {
            GameObject selectedGo = GetSelectedGo();

            GameObject buttonGo = new GameObject();

            UIButton button = buttonGo.AddComponent<UIButton>();
            UIImage image = buttonGo.AddComponent<UIImage>();

            buttonGo.transform.SetParent(selectedGo.transform);
            image.transform.SetParent(selectedGo.transform);

            buttonGo.name = "Button";
        }

        [MenuItem("GameObject/ZeroUI/Image", false, 10)]
        public static void UIImage()
        {
            GameObject selectedGo = GetSelectedGo();

            GameObject buttonGo = new GameObject();
            UIButton button = new UIButton();
            buttonGo.AddComponent<UIButton>();
            buttonGo.transform.SetParent(selectedGo.transform);
            buttonGo.name = "UIImage";
        }

        [MenuItem("GameObject/ZeroUI/Text", false, 10)]
        public static void UIText()
        {

        }

        static GameObject GetSelectedGo()
        {
            var selectedGo = Selection.activeGameObject;
            if (selectedGo == null)
            {
                GameObject canvasGo = GameObject.FindObjectOfType<Canvas>()?.gameObject;
                if (canvasGo == null)
                {
                    Debug.Log("当前没有Canvas, 创建Canvas");
                    canvasGo = new GameObject();

                    var canvas = canvasGo.AddComponent<Canvas>();
                    canvas.renderMode = RenderMode.ScreenSpaceCamera;

                    var graphicRaycaster = canvasGo.AddComponent<GraphicRaycaster>();
                }
                canvasGo.name = "Canvas";

                GameObject eventSystemGo = GameObject.FindObjectOfType<EventSystem>()?.gameObject;
                if (eventSystemGo == null)
                {
                    Debug.Log("当前没有Canvas, 创建Canvas");
                    eventSystemGo = new GameObject();
                    eventSystemGo.AddComponent<EventSystem>();
                    eventSystemGo.AddComponent<StandaloneInputModule>();
                }
                eventSystemGo.name = "EventSystem";

                selectedGo = canvasGo;
            }

            return selectedGo;
        }

    }

}