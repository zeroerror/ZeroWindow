using System;
using System.Collections.Generic;

namespace ZeroWin {

    public class WinAnimPlayerRepo {

        Dictionary<string, WinAnimPlayer> all;

        public WinAnimPlayerRepo() {
            all = new Dictionary<string, WinAnimPlayer>();
        }

        public void Add(WinAnimPlayer player) {
            all.Add(player.AnimName, player);
        }

        public void Remove(string animName) {
            all.Remove(animName);
        }

        public void ClearAll() {
            all.Clear();
        }

        public bool TryGet(string animName, out WinAnimPlayer player) {
            return all.TryGetValue(animName, out player);
        }

        public void ForeachAll(Action<WinAnimPlayer> action, Func<bool> isBreak = null) {
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