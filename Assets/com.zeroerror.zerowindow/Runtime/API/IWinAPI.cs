using UnityEngine;

namespace ZeroWin {

    public interface IWinAPI {

        public WinBase Show(string windowName, string layerName);
        public void ShowAll();

        public void Hide(string windowName);
        public void HideAll();

        public void Dispose(string windowName);
        public void DisposeAll();

    }

}