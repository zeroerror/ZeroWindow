using System;
using System.Collections.Generic;
using UnityEngine;

namespace ZeroWindow {

    public class WindowRepo {

        Dictionary<string, WindowBase> all;

        public WindowRepo() {
            all = new Dictionary<string, WindowBase>();
        }

        public void Add(WindowBase window) {
            all.Add(window.WindowName, window);
        }

        public void Remove(string uiName) {
            all.Remove(uiName);
        }

        public void ClearAll() {
            all.Clear();
        }

        public bool TryGet(string uiName, out WindowBase uiGO) {
            return all.TryGetValue(uiName, out uiGO);
        }

        public void ForeachAll(Action<WindowBase> action, Func<bool> isBreak = null) {
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