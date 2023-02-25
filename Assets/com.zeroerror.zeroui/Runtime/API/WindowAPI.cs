namespace ZeroWindowFrame {

    public class WindowAPI : IWindowAPI {

        WindowContext context;

        public WindowAPI() {
        }

        public void Inject(WindowContext context) {
            this.context = context;
        }

        WindowBase IWindowAPI.Show(string uiName) {
            return context.Domain.Show(uiName);
        }

        void IWindowAPI.Hide(string uiName) {
            context.Domain.Hide(uiName);
        }

        void IWindowAPI.HideAll() {
            context.Domain.HideAll();
        }

        void IWindowAPI.ShowAll() {
            context.Domain.ShowAll();
        }

        void IWindowAPI.DisposeAll() {
            context.Domain.DisposeAllWindow();
        }

        void IWindowAPI.Dispose(string windowName) {
            context.Domain.DisposeAllWindow();
        }
    }

}