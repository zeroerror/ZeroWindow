using System;
using System.Collections.Generic;
using UnityEngine;

namespace ZeroWin {

    public class WinRepo {

        Dictionary<string, WinBase> all;

        public WinRepo() {
            all = new Dictionary<string, WinBase>();
        }

        public void Add(WinBase window) {
            all.Add(window.WinName, window);
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