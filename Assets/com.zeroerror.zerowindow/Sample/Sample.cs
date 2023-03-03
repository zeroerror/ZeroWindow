using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using ZeroWin;

namespace ZeroWin.Sample {

    public class Sample : MonoBehaviour {

        bool isShow;
        bool isInit;
        WinCore winCore;

        void Awake() {
            winCore = new WinCore(new Vector2(1920, 1080), "UI");
            Action action = async () => {
                // Load UI assets
                AssetLabelReference labelReference = new AssetLabelReference();
                labelReference.labelString = "UI";
                var list = await Addressables.LoadAssetsAsync<GameObject>(labelReference, null).Task;

                // Inject UI assets
                winCore.Inject(list);

                // Show UI
                SampleWin sampleUI = winCore.API.Show("SampleWin", "Default") as SampleWin;
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
                    winCore.API.HideAll();
                    isShow = false;
                } else {
                    winCore.API.ShowAll();
                    isShow = true;
                }
            }

            winCore.Tick(Time.deltaTime);

        }

        void OnDestroy() {
            winCore.Dispose();
            ZeroWin.Logger.WinLogger.isEnabled = false;
        }
    }

}