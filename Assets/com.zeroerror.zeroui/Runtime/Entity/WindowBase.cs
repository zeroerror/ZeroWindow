using UnityEngine;
using UnityEngine.UI;

namespace ZeroWindowFrame {

    // TODO: Remove Load And Unload
    public delegate void Callback(params object[] args);

    public class WindowBase : MonoBehaviour {

        public string WindowName => gameObject.name;

        public WindowBase() { }

        /// Summary
        /// Only Called Once When UI Created    
        /// </summary>
        protected virtual void OnCreate() { }

        public void Create() {
            Debug.Log("Create");
            OnCreate();
        }

        /// Summary
        /// Called When UI Enabled
        /// </summary>
        protected virtual void OnDisplay() { }

        public void Show() {
            Debug.Log("Show");
            gameObject.SetActive(true);
            OnDisplay();
        }

        /// Summary 
        /// Called When UI Disabled
        /// </summary>
        protected virtual void OnCancelDisplay() { }

        public void Hide() {
            Debug.Log("Hide");
            gameObject.SetActive(false);
            OnCancelDisplay();
        }

        /// Summary
        /// Called Every Frame
        /// </summary>
        protected virtual void OnTick() { }

        public void Tick() {
            Debug.Log("Tick");
            OnTick();
        }

        /// Summary
        /// Called When UI Destroyed
        /// </summary>
        protected virtual void OnDispose() { }

        public void Dispose() {
            OnDispose();
        }

    }
}
