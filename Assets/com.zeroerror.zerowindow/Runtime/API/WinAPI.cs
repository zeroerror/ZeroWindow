namespace ZeroWin {

    public class WinAPI : IWinAPI {

        WinContext context;

        public WinAPI() {
        }

        public void Inject(WinContext context) {
            this.context = context;
        }

        WinBase IWinAPI.Show(string uiName, string layerName) {
            return context.WinBaseDomain.Show(uiName, layerName);
        }

        void IWinAPI.Hide(string uiName) {
            context.WinBaseDomain.Hide(uiName);
        }

        void IWinAPI.HideAll() {
            context.WinBaseDomain.HideAll();
        }

        void IWinAPI.ShowAll() {
            context.WinBaseDomain.DisplayAll();
        }

        void IWinAPI.DisposeAll() {
            context.WinBaseDomain.DisposeAllWin();
        }

        void IWinAPI.Dispose(string windowName) {
            context.WinBaseDomain.DisposeAllWin();
        }
    }

}