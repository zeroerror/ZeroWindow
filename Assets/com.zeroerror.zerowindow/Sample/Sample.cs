using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using ZeroWin;

namespace ZeroWin.Sample {

    public class Sample : MonoBehaviour {

        WinCore windowCore;
        bool isShow;
        bool isInit;

        void Awake() {
            windowCore = new WinCore(new Vector2(1920, 1080), "UI");
            Action action = async () => {
                // Load UI assets
                AssetLabelReference labelReference = new AssetLabelReference();
                labelReference.labelString = "UI";
                var list = await Addressables.LoadAssetsAsync<GameObject>(labelReference, null).Task;

                // Inject UI assets
                windowCore.Inject(list);

                // Show UI
                SampleWin sampleUI = windowCore.API.Show("SampleWin", "Default") as SampleWin;
                isShow = true;

                isInit = true;
            };
            action.Invoke();

            ZeroWin.Logger.WinLogger.isEnabled = true;
        }

        void Update() {
            if (!isInit) return;

            if (Input.GetKeyDown(KeyCode.Space)) {
                if (isShow) {
                    windowCore.API.HideAll();
                    isShow = false;
                } else {
                    windowCore.API.ShowAll();
                    isShow = true;
                }
            }

            windowCore.Tick(Time.deltaTime);

        }

        void OnDestroy() {
            windowCore.Dispose();
            ZeroWin.Logger.WinLogger.isEnabled = false;
        }
    }

}