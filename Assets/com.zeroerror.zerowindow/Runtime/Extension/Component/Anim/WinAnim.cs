using System.Collections;
using UnityEngine;
using ZeroWin.Generic;

namespace ZeroWin.Extension {

    public class WinAnim : MonoBehaviour {

        public WinAnimElementModel[] animElements;

        void Awake() {
        }

        public void ResetAllElement() {
            if (animElements == null) {
                return;
            }

            var len = animElements.Length;
            for (int i = 0; i < len; i++) {
                var element = animElements[i];
                if (element == null || element.AnimState == WinAnimFSMState.Stop) {
                    continue;
                }
                element.Reset();
            }
        }

        public void Reset(int index) {
            if (animElements?.Length <= index) {
                return;
            }

            animElements[index].Reset();
        }

        public bool TryGetAnimElement(string animElementName, out WinAnimElementModel animElement) {
            animElement = null;
            if (animElements == null) {
                return false;
            }

            var len = animElements?.Length;
            for (int i = 0; i < len; i++) {
                if (animElements[i].animElementName == animElementName) {
                    animElement = animElements[i];
                    return true;
                }
            }

            return false;
        }

    }

}
