// using System.Collections.Generic;
// using System.Text;
// using UnityEngine;
// using UnityEngine.EventSystems;
// using ZeroWindow;
// using UnityEngine.UI;

// namespace ZeroWindow.Manager {


//     public static class WindowMgr {

//         public static GameObject WindowCanvasGo { get; set; }

//         static Canvas _windowCanvas;

//         static RectTransform _windowCanvasRt;

//         static RectTransform _windowMainView;

//         static Dictionary<string, Transform> _windowLayerDic = new Dictionary<string, Transform>();

//         static Dictionary<string, Transform> _windowDic = new Dictionary<string, Transform>();

//         static Dictionary<string, int2> _layerSortingDic = new Dictionary<string, int2>();

//         static int _curSortingOrder = 0;

//         public static void Ctor(Vector2 windowResolution) {
//             int layer = LayerMask.NameToLayer("Window");
//             WindowCanvasGo = new GameObject("WindowCanvas", typeof(Canvas), typeof(CanvasScaler));
//             _windowCanvas = WindowCanvasGo.GetComponent<Canvas>();
//             _windowCanvas.renderMode = RenderMode.ScreenSpaceCamera;
//             _windowCanvas.sortingLayerID = SortingLayer.NameToID("Billboard");
//             WindowCanvasGo.layer = layer;
//             _windowCanvasRt = WindowCanvasGo.GetComponent<RectTransform>();
//             GameObject.DontDestroyOnLoad(WindowCanvasGo);
//             CanvasScaler tempScale = WindowCanvasGo.GetComponent<CanvasScaler>();
//             tempScale.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
//             tempScale.screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;
//             tempScale.referenceResolution = windowResolution;
//             _windowCanvasRt.position = Vector3.zero;//CanvasScaler 会进行重置坐标 等其加载完成

//             _windowMainView = new GameObject("WindowMainView", typeof(RectTransform)).GetComponent<RectTransform>();
//             _SetRectTransform(ref _windowMainView);
//             _windowMainView.transform.SetParent(WindowCanvasGo.transform, false);

//             foreach (var item in SortingLayer.layers) {
//                 if (item.value == 0) continue;
//                 var layerGO = new GameObject(item.name, typeof(Canvas));
//                 var layerRct = layerGO.GetComponent<RectTransform>();
//                 layerRct.transform.SetParent(_windowMainView.transform, false);
//                 layerRct.gameObject.layer = layer;
//                 Canvas canvas = layerGO.GetComponent<Canvas>();
//                 canvas.overrideSorting = true;
//                 canvas.sortingLayerID = item.id;
//                 canvas.sortingOrder = _curSortingOrder;
//                 _layerSortingDic.Add(item.name, new int2(_curSortingOrder, 0));
//                 _curSortingOrder += 10;
//                 _SetRectTransform(ref layerRct);
//                 _windowLayerDic.Add(item.name, layerRct.transform);
//             }

//             var eventSystem = new GameObject("EventSystem", typeof(EventSystem), typeof(StandaloneInputModule), typeof(BaseInput));
//             eventSystem.transform.SetParent(WindowCanvasGo.transform, false);
//         }

//         public static bool IsActive(string windowName) {
//             Transform window = null;
//             return _CheckWindow(windowName, ref window) && window.gameObject.activeInHierarchy;
//         }

//         public static WindowBehavior OpenWindow(string windowName, params object[] args) {
//             Transform window = null;
//             if (!_CheckWindow(windowName, ref window)) {
//                 if (!_TryCreateWindow(windowName, ref window)) {
//                     Debug.LogError($"Window {windowName} 不存在");
//                     return null;
//                 } else {
//                     Debug.Log($"cWindow {windowName}");
//                 }
//             }

//             var uIBehavior = window.GetComponent<WindowBehavior>();

//             // 传参args
//             uIBehavior.args = args;

//             if (window.gameObject.activeInHierarchy) return uIBehavior;

//             Canvas canvas;
//             if (!window.GetComponent<Canvas>()) canvas = window.gameObject.AddComponent<Canvas>();
//             else canvas = window.GetComponent<Canvas>();
//             string layer = windowName.Split('_')[0];
//             int2 value = _layerSortingDic[layer];

//             if (!_windowDic.ContainsKey(windowName)) {
//                 _windowDic.Add(windowName, window);
//                 canvas.overrideSorting = true;
//                 canvas.sortingLayerID = SortingLayer.NameToID(layer);
//             } else {
//                 Debug.Log($"window {windowName} is already exist");
//             }

//             value.y++;
//             canvas.sortingOrder = value.x + value.y;
//             window.gameObject.SetActive(true);
//             _layerSortingDic[layer] = value;

//             return uIBehavior;
//         }

//         public static void CloseWindow(string windowName) {
//             Transform window = null;
//             if (!_CheckWindow(windowName, ref window)) {
//                 return;
//             }
//             if (!window.gameObject.activeInHierarchy) {
//                 return;
//             }

//             Canvas canvas = window.GetComponent<Canvas>();
//             string layer = windowName.Split('_')[0];
//             Transform layerTrans = _windowLayerDic[layer];
//             Canvas[] allCanvas = layerTrans.GetComponentsInChildren<Canvas>();

//             // Update Layer Sorting
//             for (int i = 1; i < allCanvas.Length; i++) {
//                 Canvas otherCanvas = allCanvas[i];
//                 if (otherCanvas.sortingOrder > canvas.sortingOrder) {
//                     otherCanvas.sortingOrder--;
//                 }
//             }

//             int2 value = _layerSortingDic[layer];
//             value.y--;
//             _layerSortingDic[layer] = value;
//             window.gameObject.SetActive(false);
//         }

//         public static void DestoryWindow(string windowName) {
//             Transform window = null;
//             if (!_CheckWindow(windowName, ref window)) {
//                 return;
//             }
//             GameObject.Destroy(window.gameObject);
//             string layer = windowName.Split('_')[0];
//             int2 value = _layerSortingDic[layer];
//             value.y--;
//             _layerSortingDic[layer] = value;
//         }

//         static bool _TryCreateWindow(string windowName, ref Transform window) {
//             GameObject go = WindowPanelAssets.Get(windowName);
//             if (!go) return false;

//             go.SetActive(false);
//             go = GameObject.Instantiate(go);
//             window = go.transform;
//             window.name = windowName;

//             if (window.GetComponent<GraphicRaycaster>() == null) {
//                 window.gameObject.AddComponent<GraphicRaycaster>();
//             }

//             string layer = windowName.Split('_')[0];
//             window.SetParent(_windowLayerDic[layer], false);
//             return true;
//         }

//         static bool _CheckWindow(string windowName, ref Transform window) {
//             if (_windowDic.ContainsKey(windowName)) {
//                 window = _windowDic[windowName];
//                 return true;
//             }

//             return false;
//         }

//         static void _SetRectTransform(ref RectTransform rct) {
//             rct.pivot = new Vector2(0.5f, 0.5f);
//             rct.anchorMin = Vector2.zero;
//             rct.anchorMax = Vector2.one;
//             rct.offsetMax = Vector2.zero;
//             rct.offsetMin = Vector2.zero;
//         }

//     }

// }

