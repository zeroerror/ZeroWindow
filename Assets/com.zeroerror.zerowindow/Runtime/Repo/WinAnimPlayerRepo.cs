using System;
using System.Collections.Generic;

namespace ZeroWin {

    public class WinAnimPlayerRepo {

        Dictionary<string, List<WinAnimPlayer>> all;

        public WinAnimPlayerRepo() {
            all = new Dictionary<string, List<WinAnimPlayer>>();
        }

        public void Add(WinAnimPlayer player) {
            var animName = player.AnimName;
            if (!all.TryGetValue(animName, out var list)) {
                list = new List<WinAnimPlayer>();
                all.Add(animName, list);
            }

            list.Add(player);
        }

        public void Remove(string animName) {
            all.Remove(animName);
        }

        public void ClearAll() {
            all.Clear();
        }

        public bool TryGet(string animName, out WinAnimPlayer player) {
            player = null;
            if (!all.TryGetValue(animName, out var playerList)) {
                return false;
            }

            var count = playerList.Count;
            for (int i = 0; i < count; i++) {
                var p = playerList[i];
                if (p.State == WinAnimFSMState.Stop) {
                    player = p;
                    return true;
                }
            }

            return false;
        }

        public void ForeachAll(Action<WinAnimPlayer> action) {
            var e = all.Values.GetEnumerator();
            while (e.MoveNext()) {
                var playerList = e.Current;
                playerList.ForEach((player) => {
                    action.Invoke(player);
                });
            }
            return;
        }

    }

}