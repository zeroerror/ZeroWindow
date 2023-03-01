using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using ZeroWin.Logger;

namespace ZeroWin {

    public class WinService {

        public GameObject WinCanvasGo { get; set; }

        Canvas _windowCanvas;

        RectTransform _windowCanvasRt;

        RectTransform _windowMainView;

        Dictionary<string, Transform> layerDic;

        HashSet<string> windowHashSet;

        Dictionary<string, int2> sortingInfoDic;

        public WinService(Vector2 resolution, string windowLayerName) {
            layerDic = new Dictionary<string, Transform>();
            windowHashSet = new HashSet<string>();
            sortingInfoDic = new Dictionary<string, int2>();

            var root = new GameObject("WinCanvas", typeof(Canvas), typeof(CanvasScaler));
            GameObject.DontDestroyOnLoad(root);

            var rootLayer = LayerMask.NameToLayer(windowLayerName);
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

            var mainView = new GameObject("WinMainView", typeof(RectTransform)).GetComponent<RectTransform>();
            ResetRect(mainView);
            mainView.transform.SetParent(root.transform, false);

            var layers = SortingLayer.layers;
            var layerCount = layers.Length;
            int curSortingOrder = 0;
            for (int i = 0; i < layerCount; i++) {
                var layer = layers[i];
                var layerName = layer.name;
                var layerGO = new GameObject(layerName, typeof(Canvas));
                var layerRct = layerGO.GetComponent<RectTransform>();
                layerRct.transform.SetParent(mainView.transform, false);
                layerRct.gameObject.layer = rootLayer;
                Canvas canvas = layerGO.GetComponent<Canvas>();
                canvas.overrideSorting = true;
                canvas.sortingLayerID = layer.id;
                canvas.sortingOrder = curSortingOrder;
                sortingInfoDic.Add(layerName, new int2(curSortingOrder, 0));
                curSortingOrder += 10;
                ResetRect(layerRct);
                layerDic.Add(layerName, layerRct.transform);
                WinLogger.Log($"{nameof(WinService)}: Add Layer: {layerName}");
            }

            var eventSystem = new GameObject("EventSystem", typeof(EventSystem), typeof(StandaloneInputModule), typeof(BaseInput));
            eventSystem.transform.SetParent(root.transform, false);
        }

        public void PushToCanvas(WinBase window, string layerName) {
            Canvas canvas = window.GetComponent<Canvas>();

            var windowName = window.name;
            if (!windowHashSet.Contains(windowName)) {
                windowHashSet.Add(windowName);
                canvas.overrideSorting = true;
                canvas.sortingLayerName = layerName;
            } else {
                WinLogger.Log($"{nameof(WinService)}: window {windowName} is already exist");
            }

            int2 sortingInfo = sortingInfoDic[layerName];
            sortingInfo.y++;
            sortingInfoDic[layerName] = sortingInfo;

            canvas.sortingOrder = sortingInfo.x + sortingInfo.y;

            var layerRootTrans = layerDic[layerName];
            window.transform.SetParent(layerRootTrans, false);
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