using UnityEngine;
using UnityEngine.UI;

namespace ZeroWindow {

    public class WinText : Text {

        [SerializeField]
        int textID;
        public int TextID => textID;
        public void SetTextID(int id) => textID = id;

    }

}