using UnityEngine;
using ZeroWin.Generic;

namespace ZeroWin.Extension {

    public class WinAnim : MonoBehaviour {

        public WinAnimElementModel[] elements;

        public bool TryGetAnimElement(string animElementName, out WinAnimElementModel animElement) {
            animElement = null;
            if (elements == null) {
                return false;
            }

            var len = elements.Length;
            for (int i = 0; i < len; i++) {
                if (elements[i].elementName == animElementName) {
                    animElement = elements[i];
                    return true;
                }
            }

            return false;
        }

    }

}
