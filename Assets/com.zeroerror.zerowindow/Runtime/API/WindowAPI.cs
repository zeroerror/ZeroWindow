namespace ZeroWin {

    public class WinAPI : IWinAPI {

        WinContext context;

        public WinAPI() {
        }

        public void Inject(WinContext context) {
            this.context = context;
        }

        WinBase IWinAPI.Show(string uiName, string layerName) {
            return context.Domain.Show(uiName, layerName);
        }

        void IWinAPI.Hide(string uiName) {
            context.Domain.Hide(uiName);
        }

        void IWinAPI.HideAll() {
            context.Domain.HideAll();
        }

        void IWinAPI.ShowAll() {
            context.Domain.DisplayAll();
        }

        void IWinAPI.DisposeAll() {
            context.Domain.DisposeAllWin();
        }

        void IWinAPI.Dispose(string windowName) {
            context.Domain.DisposeAllWin();
        }
    }

}