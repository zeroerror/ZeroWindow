using UnityEngine;
using UnityEngine.UI;

namespace ZeroUIFrame
{
    public delegate void Callback(params object[] args);

    public abstract class UIBehavior : MonoBehaviour
    {
        public object[] args;

        private void Awake()
        {
            GraphicRaycaster gr = transform.GetComponent<GraphicRaycaster>();
            if (gr == null)
            {
                gameObject.AddComponent<GraphicRaycaster>();
            }
        }

        #region [Slider]
        protected void Slider_SetVal(string uiName, float val)
        {
            Slider slider = null;
            if (!_CheckComponent<Slider>(uiName, ref slider))
            {
                Debug.LogError(transform.name + ": " + uiName + ": Slider Component Not Found!");
                return;
            };

            slider.value = val;
        }
        #endregion

        #region [Click]
        protected void OnPointerDown(string uiName, Callback func, params object[] args)
        {
            Debug.Log("asdas");
            UIButton button = null;
            Transform ui = null;
            if (!_TryGetChildUI(uiName, ref ui)) return;

            if (!_CheckComponent<UIButton>(uiName, ref button))
            {
                ui.gameObject.AddComponent<UIButton>();
            }

            button = ui.gameObject.GetComponent<UIButton>();
            button.OnPointerDown_Clear();
            button.OnPointerDown((eventData) =>
            {
                func?.Invoke(args, eventData);
            });
        }

        protected void OnPointerUp(string uiName, Callback func, params object[] args)
        {
            UIButton button = null;
            Transform ui = null;
            if (!_TryGetChildUI(uiName, ref ui)) return;

            if (!_CheckComponent<UIButton>(uiName, ref button))
            {
                ui.gameObject.AddComponent<UIButton>();
            }

            button = ui.gameObject.GetComponent<UIButton>();
            button.OnPointerUp_Clear();
            button.OnPointerUp((eventData) =>
            {
                func?.Invoke(args, eventData);
            });
        }

        protected void OnPointerDrag(string uiName, Callback func, params object[] args)
        {
            UIButton button = null;
            Transform ui = null;
            if (!_TryGetChildUI(uiName, ref ui)) return;

            if (!_CheckComponent<UIButton>(uiName, ref button))
            {
                ui.gameObject.AddComponent<UIButton>();
            }

            button = ui.gameObject.GetComponent<UIButton>();
            button.OnPointerDrag_Clear();
            button.OnPointerDrag((eventData) =>
            {
                func?.Invoke(args, eventData);
            });
        }

        #endregion

        #region [Text]
        protected void Text_SetFontSize(string uiName, int size)
        {
            Text text = null;
            if (!_CheckComponent<Text>(uiName, ref text))
            {
                Debug.LogError(transform.name + ": " + uiName + ": Text Component Not Found!");
                return;
            };

            text.fontSize = size;
        }
        protected void Text_SetColor(string uiName, string color)
        {
            Text text = null;
            if (!_CheckComponent<Text>(uiName, ref text))
            {
                Debug.LogError(transform.name + ": " + uiName + ": Text Component Not Found!");
                return;
            };

            ColorUtility.TryParseHtmlString(color, out Color nowColor);
            text.color = nowColor;
        }
        protected void Text_SetAlignment(string uiName, TextAnchor textAnchor)
        {
            Text text = null;
            if (!_CheckComponent<Text>(uiName, ref text))
            {
                Debug.LogError(transform.name + ": " + uiName + ": Text Component Not Found!");
                return;
            };

            text.alignment = textAnchor;
        }
        protected void Text_SetText(string uiName, object content)
        {
            Text text = null;
            if (!_CheckComponent<Text>(uiName, ref text))
            {
                Debug.LogError(transform.name + ": " + uiName + ": Text Component Not Found!");
                return;
            };

            text.text = content.ToString();
        }
        protected string Input_GetText(string uiName)
        {
            InputField inputField = null;
            if (!_CheckComponent<InputField>(uiName, ref inputField))
            {
                Debug.LogError(transform.name + ": " + uiName + ": InputField Component Not Found!");
                return "";
            }

            return inputField.text;
        }
        #endregion

        #region [Image]
        protected void Image_SetFillAmount(string uiName, float fill)
        {
            Image image = null;
            if (!_CheckComponent<Image>(uiName, ref image))
            {
                Debug.LogError(transform.name + ": " + uiName + ": Image Component Not Found!");
                return;
            }

            image.fillAmount = fill;
        }

        protected void Image_SetImage(string uiName, int resourceID)
        {
            Image image = null;
            if (!_CheckComponent<Image>(uiName, ref image))
            {
                Debug.LogError(transform.name + ": " + uiName + ": Image Component Not Found!");
                return;
            }

            //image.sprite = ResourceMgr.LoadSprite(resourceID);
        }
        protected void Image_SetColor(string uiName, string color)
        {
            Image image = null;
            if (!_CheckComponent<Image>(uiName, ref image))
            {
                Debug.LogError(transform.name + ": " + uiName + ": Image Component Not Found!");
                return;
            }

            ColorUtility.TryParseHtmlString(color, out Color nowColor);
            image.color = nowColor;
        }

        protected void RawImage_SetImage(string uiName, int resourceID)
        {
            RawImage rawImage = null;
            if (!_CheckComponent<RawImage>(uiName, ref rawImage))
            {
                Debug.LogError(transform.name + ": " + uiName + ": RawImage Component Not Found!");
                return;
            }

            //rawImage.texture = ResourceMgr.LoadTexture(resourceID);
        }
        #endregion

        #region [UI]

        protected bool TryAddChildUI(string uiName, GameObject addChildUI)
        {
            if (addChildUI == null)
            {
                Debug.LogError(transform.name + ": AddChildUI Failed! 不存在UI " + addChildUI.name);
                return false;
            }

            string childName = addChildUI.name;
            addChildUI = Instantiate(addChildUI, transform.Find(uiName));
            addChildUI.name = childName;
            return true;
        }

        protected void SetActive(string uiName, bool isActive)
        {
            Transform childUI = null;
            if (!_TryGetChildUI(uiName, ref childUI))
            {
                Debug.LogError(string.Format("{0}: childUI: {1} 不存在！", transform.name, uiName));
                return;
            }

            childUI.gameObject.SetActive(isActive);
        }

        #endregion

        #region [Common]

        protected T GetComponentFromChild<T>(string name) where T : Component
        {
            return transform.Find(name).GetComponent<T>();
        }

        #endregion

        #region PrivateMethod
        private bool _CheckComponent<T>(string uiName, ref T component)
        {
            Transform ui = null;
            if (uiName == transform.name) ui = transform;
            else _TryGetChildUI(uiName, ref ui);
            if (ui == null) return false;

            component = ui.GetComponent<T>();
            if (component == null)
            {
                //Debug.LogError(string.Format("{0}: {1} 组件 {2} 不存在！", transform.name, uiName, typeof(T)));
                return false;
            }
            return component != null;
        }

        private bool _TryGetChildUI(string uiName, ref Transform childUI)
        {
            childUI = transform.Find(uiName);
            Debug.Log(transform.name);
            if (childUI == null)
            {
                Debug.LogError(string.Format("{0}: uiName: {1} 不存在!", transform.name, uiName));
                return false;
            }
            return true;
        }
        #endregion

    }
}
