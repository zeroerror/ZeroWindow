namespace ZeroWin.Logger {

    public static class WinLogger {

        public static bool isEnabled = true;

        public static void Log(string msg) {
            if(!isEnabled) return;
            UnityEngine.Debug.Log(msg);
        }

        public static void LogWarning(string msg) {
            if(!isEnabled) return;
            UnityEngine.Debug.LogWarning(msg);
        }

        public static void LogError(string msg) {
            if(!isEnabled) return;
            UnityEngine.Debug.LogError(msg);
        }

    }

}