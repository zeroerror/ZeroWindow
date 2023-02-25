using UnityEngine;
using UnityEngine.EventSystems;

namespace ZeroWindowFrame.Sample {

    public class SampleWindow :  WindowBase {

        UIButton uIButton;

        string WindowBase.WindowName => nameof(SampleWindow);

        GameObject WindowBase.WindowGO => GameObject;

        void WindowBase.Create() {
            Debug.Log("SampleWindow OnCreate");
        }

        void WindowBase.Show() {
            Debug.Log("SampleWindow OnShow");
        }

        void WindowBase.Hide() {
            Debug.Log("SampleWindow OnHide");
        }

        void WindowBase.Tick() {
            Debug.Log("SampleWindow OnTick");
        }

        void WindowBase.Dispose() {
            Debug.Log("SampleWindow OnDispose");
        }
    }

}