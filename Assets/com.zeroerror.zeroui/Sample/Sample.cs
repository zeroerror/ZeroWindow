using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using ZeroWindowFrame;

namespace ZeroWindowFrame.Sample {

    public class Sample : MonoBehaviour {

        WindowCore windowCore;

        void Awake() {
            windowCore = new WindowCore();
        }

        void Start() {
            Action action = async () => {
                // Load UI assets
                AssetLabelReference labelReference = new AssetLabelReference();
                labelReference.labelString = "UI";
                var list = await Addressables.LoadAssetsAsync<GameObject>(labelReference, null).Task;

                // Inject UI assets
                windowCore.Inject(list);

                // Show UI
                SampleWindow sampleUI = windowCore.API.Show("SampleWindow") as SampleWindow;
            };
            action.Invoke();
        }

        void Update() {
            windowCore.Tick();
        }

        void OnDisable() {
            windowCore.API.HideAll();
        }

        void OnDestroy() {
            windowCore.API.DisposeAll();
        }

    }

}