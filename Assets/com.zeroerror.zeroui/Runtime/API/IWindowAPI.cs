using UnityEngine;

namespace ZeroWindowFrame {

    public interface IWindowAPI {

        public WindowBase Show(string windowName);
        public void ShowAll();

        public void Hide(string windowName);
        public void HideAll();

        public void Dispose(string windowName);
        public void DisposeAll();

    }

}