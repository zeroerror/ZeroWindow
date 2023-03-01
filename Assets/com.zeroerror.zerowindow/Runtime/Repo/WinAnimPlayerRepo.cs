using System;
using System.Collections.Generic;
using ZeroWin.Logger;

namespace ZeroWin {

    public class WinAnimPlayerRepo {

        Dictionary<int, List<WinAnimPlayer>> all;

        public WinAnimPlayerRepo() {
            all = new Dictionary<int, List<WinAnimPlayer>>();
        }

        public void Add(int key, WinAnimPlayer player) {
            var animName = player.AnimName;
            if (!all.TryGetValue(key, out var list)) {
                list = new List<WinAnimPlayer>();
                all.Add(key, list);
            }

            list.Add(player);
            WinLogger.Log($"添加动画播放器 名称 {animName} key {key}");
        }

        public void ClearAnimByKey(int key) {
            if (all.TryGetValue(key, out var list)) {
                list.Clear();
                WinLogger.Log($"清空动画播放器 key {key}");
            }
        }

        public void ClearAll() {
            var e = all.Values.GetEnumerator();
            while (e.MoveNext()) {
                var list = e.Current;
                list.Clear();
            }
            WinLogger.Log($"清空所有动画播放器");
        }

        public bool TryGet(int key, string animName, out WinAnimPlayer player) {
            player = null;
            if (!all.TryGetValue(key, out var playerList)) {
                return false;
            }

            var count = playerList.Count;
            for (int i = 0; i < count; i++) {
                var p = playerList[i];
                if (p.AnimName == animName) {
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