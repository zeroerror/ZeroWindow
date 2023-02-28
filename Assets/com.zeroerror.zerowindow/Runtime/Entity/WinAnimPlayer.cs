using UnityEngine;
using ZeroWin.Generic;

namespace ZeroWin {

    public class WinAnimPlayer {

        string animName;
        public string AnimName => animName;

        GameObject self;
        GameObject tar;

        WinAnimFSMState state;

        WinAnimModel animModel;

        public WinAnimPlayer(WinAnimModel animModel) {
            this.animModel = animModel;
        }

        public void SetSeltAndTar(GameObject self, GameObject tar = null) {
            this.self = self;
            this.tar = tar;
        }

        public void Reset() {
        }

        public void Tick(float dt) {
            if (state == WinAnimFSMState.Playing) {
            } else if (state == WinAnimFSMState.Pause) {
            } else if (state == WinAnimFSMState.Stop) {
            }
        }

        void PlayAnim(float dt) {

        }

        public void EnterPlayining() {
            state = WinAnimFSMState.Playing;
        }

        public void EnterPause() {
            state = WinAnimFSMState.Pause;
        }

        public void EnterStop() {
            state = WinAnimFSMState.Stop;
        }

    }
}
