using System;
using System.Collections.Generic;

namespace ZeroWin {

    public class WinBaseRepo {

        Dictionary<string, WinBase> all;

        public WinBaseRepo() {
            all = new Dictionary<string, WinBase>();
        }

        public void Add(WinBase winBase) {
            all.Add(winBase.WinBaseName, winBase);
        }

        public void Remove(string uiName) {
            all.Remove(uiName);
        }

        public void ClearAll() {
            all.Clear();
        }

        public bool TryGet(string uiName, out WinBase uiGO) {
            return all.TryGetValue(uiName, out uiGO);
        }

        public void ForeachAll(Action<WinBase> action, Func<bool> isBreak = null) {
            var e = all.Values.GetEnumerator();

            if (isBreak == null) {
                while (e.MoveNext()) {
                    var ui = e.Current;
                    action.Invoke(ui);
                }
                return;
            }

            if (isBreak != null) {
                while (e.MoveNext()) {
                    var ui = e.Current;
                    action.Invoke(ui);
                    if (isBreak.Invoke()) {
                        break;
                    }
                }
            }
        }

    }

}