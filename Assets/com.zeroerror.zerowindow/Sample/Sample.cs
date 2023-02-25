using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using ZeroWindow;

namespace ZeroWindow.Sample {

    public class Sample : MonoBehaviour {

        WindowCore windowCore;
        bool isShow = false;

        void Awake() {
            windowCore = new WindowCore(new Vector2(1920, 1080), "UI");
            Action action = async () => {
                // Load UI assets
                AssetLabelReference labelReference = new AssetLabelReference();
                labelReference.labelString = "UI";
                var list = await Addressables.LoadAssetsAsync<GameObject>(labelReference, null).Task;

                // Inject UI assets
                windowCore.Inject(list);

                // Show UI
                SampleWindow sampleUI = windowCore.API.Show("SampleWindow", "Normal") as SampleWindow;
                isShow = true;
            };
            action.Invoke();
        }

        void Update() {

            if (Input.GetKeyDown(KeyCode.Space)) {
                if (isShow) {
                    windowCore.API.HideAll();
                    isShow = false;
                }else{
                    windowCore.API.ShowAll();
                    isShow = true;
                }
            }

        }
    }

}