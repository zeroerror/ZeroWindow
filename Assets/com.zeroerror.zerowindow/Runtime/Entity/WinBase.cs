using UnityEngine;

namespace ZeroWin {

    public class WinBase : MonoBehaviour {

        public string WinBaseName => gameObject.name;

        public WinBase() { }

        /// Summary
        /// Only Called Once When UI Created    
        /// </summary>
        protected virtual void OnCreate() { }

        public void Create() {
            OnCreate();
        }

        /// Summary
        /// Called When UI Enabled
        /// </summary>
        protected virtual void OnShow() { }

        public void Show() {
            OnShow();
            gameObject.SetActive(true);
        }

        /// Summary 
        /// Called When UI Disabled
        /// </summary>
        protected virtual void OnHide() { }

        public void Hide() {
            OnHide();
            gameObject.SetActive(false);
        }

        /// Summary
        /// Called Every Frame
        /// </summary>
        protected virtual void OnTick() { }

        public void Tick() {
            OnTick();
        }

        /// Summary
        /// Called When UI Destroyed
        /// </summary>
        protected virtual void OnDispose() { }

        public void Dispose() {
            OnDispose();
            GameObject.Destroy(gameObject);
        }

    }
}
