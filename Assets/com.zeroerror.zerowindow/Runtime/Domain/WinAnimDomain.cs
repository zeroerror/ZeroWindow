using UnityEngine;
using UnityEngine.UI;
using ZeroWin.Generic;

namespace ZeroWin {

    public class WinAnimDomain {

        WinContext context;

        public WinAnimDomain() { }

        public void Inject(WinContext context) {
            this.context = context;
        }

        public void TickAllAnimPlayer(float dt) {
            var animPlayerRepo = context.AnimPlayerRepo;
            animPlayerRepo.ForeachAll(animPlayer => {
                animPlayer.Tick(dt);
            });
        }

        public void PlayAnim(string winAnimName, GameObject self) {
            Play(winAnimName, self, null);
        }

        public void PlayAnimWithTarget(string winAnimName, GameObject self, GameObject tar) {
            Play(winAnimName, self, tar);
        }

        void Play(string winAnimName, GameObject self, GameObject tar) {
            var animPlayerRepo = context.AnimPlayerRepo;
            if (!animPlayerRepo.TryGet(winAnimName, out var animPlayer)) {
                var factory = context.Factory;
                animPlayer = factory.CreateAnimPlayer(winAnimName, self);
                animPlayer.SetTarget(tar);
                animPlayerRepo.Add(animPlayer);
            }

            animPlayer.EnterPlayining();
        }

    }

}