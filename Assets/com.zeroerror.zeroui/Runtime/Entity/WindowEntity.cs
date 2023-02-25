using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ZeroWindowFrame {

    public class WindowEntity {

        public GameObject WindowCanvasGo { get; set; }

        Canvas _windowCanvas;

        RectTransform _windowCanvasRt;

        RectTransform _windowMainView;

        Dictionary<string, Transform> _windowLayerDic = new Dictionary<string, Transform>();

        Dictionary<string, Transform> _windowDic = new Dictionary<string, Transform>();

        Dictionary<string, int2> _layerSortingDic = new Dictionary<string, int2>();

        int _curSortingOrder = 0;

        public WindowEntity() {
        }

        public void Inject() { 
            
        }

        public void Init(Vector2 resolution) {
            var root = new GameObject("WindowCanvas", typeof(Canvas), typeof(CanvasScaler));
            GameObject.DontDestroyOnLoad(root);

            var rootLayer = LayerMask.NameToLayer("Window");
            root.layer = rootLayer;

            var rootCanvas = root.GetComponent<Canvas>();
            rootCanvas.renderMode = RenderMode.ScreenSpaceCamera;
            rootCanvas.sortingLayerID = SortingLayer.NameToID("Billboard");

            CanvasScaler tempScale = root.GetComponent<CanvasScaler>();
            tempScale.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            tempScale.screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;
            tempScale.referenceResolution = resolution;

            var canvasRect = root.GetComponent<RectTransform>();
            canvasRect.position = Vector3.zero;//CanvasScaler 会进行重置坐标 等其加载完成

            var mainView = new GameObject("WindowMainView", typeof(RectTransform)).GetComponent<RectTransform>();
            ResetRect(mainView);
            mainView.transform.SetParent(root.transform, false);

            foreach (var item in SortingLayer.layers) {
                if (item.value == 0) continue;
                var layerGO = new GameObject(item.name, typeof(Canvas));
                var layerRct = layerGO.GetComponent<RectTransform>();
                layerRct.transform.SetParent(mainView.transform, false);
                layerRct.gameObject.layer = rootLayer;
                Canvas canvas = layerGO.GetComponent<Canvas>();
                canvas.overrideSorting = true;
                canvas.sortingLayerID = item.id;
                canvas.sortingOrder = _curSortingOrder;
                _layerSortingDic.Add(item.name, new int2(_curSortingOrder, 0));
                _curSortingOrder += 10;
                ResetRect(layerRct);
                _windowLayerDic.Add(item.name, layerRct.transform);
            }

            var eventSystem = new GameObject("EventSystem", typeof(EventSystem), typeof(StandaloneInputModule), typeof(BaseInput));
            eventSystem.transform.SetParent(root.transform, false);
        }

        void ResetRect(RectTransform rct) {
            rct.pivot = new Vector2(0.5f, 0.5f);
            rct.anchorMin = Vector2.zero;
            rct.anchorMax = Vector2.one;
            rct.offsetMax = Vector2.zero;
            rct.offsetMin = Vector2.zero;
        }

    }


}