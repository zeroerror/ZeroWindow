using UnityEngine;

namespace ZeroWindow {

    public interface IWindowAPI {

        public WindowBase Show(string windowName, string layerName);
        public void ShowAll();

        public void Hide(string windowName);
        public void HideAll();

        public void Dispose(string windowName);
        public void DisposeAll();

    }

}