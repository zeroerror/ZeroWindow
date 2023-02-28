using UnityEngine;
using UnityEngine.UI;

namespace ZeroWin.Extension {

    public class WinText : Text {

        [SerializeField]
        int textID;
        public int TextID => textID;
        public void SetTextID(int id) => textID = id;

    }

}