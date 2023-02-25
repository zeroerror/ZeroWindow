using UnityEngine;
using UnityEngine.EventSystems;

namespace ZeroWindow.Sample {

    public class SampleWindow : WindowBase {

        UIButton uIButton;

        protected override void OnCreate() {
            Debug.Log("SampleWindow OnCreate");
        }

        protected override void OnShow() {
            Debug.Log("SampleWindow OnDisplay");
        }

        protected override void OnHide() {
            Debug.Log("SampleWindow OnHide");
        }

        protected override void OnTick() {
            Debug.Log("SampleWindow OnTick");
        }

    }

}