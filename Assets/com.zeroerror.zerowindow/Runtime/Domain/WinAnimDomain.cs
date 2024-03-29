using System;
using UnityEngine;
using UnityEngine.UI;
using ZeroWin.Generic;
using ZeroWin.Logger;

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
            Play(self, winAnimName, null);
        }

        public void PlayAnimWithTarget(GameObject self, string winAnimName, GameObject tar) {
            Play(self, winAnimName, tar);
        }

        void Play(GameObject self, string winAnimName, GameObject tar) {
            var animPlayerRepo = context.AnimPlayerRepo;
            var key = self.GetInstanceID();
            if (!animPlayerRepo.TryGet(key, winAnimName, out var animPlayer)) {
                var factory = context.Factory;
                animPlayer = factory.CreateAnimPlayer(winAnimName, self);
                animPlayerRepo.Add(key, animPlayer);
            }

            animPlayer.EnterPlayining();
            
            if (tar != null) {
                animPlayer.SetTarget(tar);
            }
        }

        public void SetAnimLoopType(GameObject self, string winAnimName, WinAnimLoopType loopType) {
            var animPlayerRepo = context.AnimPlayerRepo;
            var key = self.GetInstanceID();
            if (!animPlayerRepo.TryGet(key, winAnimName, out var animPlayer)) {
                WinLogger.LogWarning($"动画播放器不存在 名称 {winAnimName}  key {key}");
                return;
            }

            animPlayer.SetLoopType(loopType);
        }

        public void PauseAim(GameObject self, string winAnimName) {
            var animPlayerRepo = context.AnimPlayerRepo;
            var key = self.GetInstanceID();
            if (!animPlayerRepo.TryGet(key, winAnimName, out var animPlayer)) {
                WinLogger.LogWarning($"动画播放器不存在 名称 {winAnimName}  key {key}");
                return;
            }

            animPlayer.EnterPause();
        }

        public void ResumeAnim(GameObject self, string winAnimName) {
            var animPlayerRepo = context.AnimPlayerRepo;
            var key = self.GetInstanceID();
            if (!animPlayerRepo.TryGet(key, winAnimName, out var animPlayer)) {
                WinLogger.LogWarning($"动画播放器不存在 名称 {winAnimName}  key {key}");
                return;
            }

            animPlayer.ResumeAnim();
        }

        public void StopAnim(GameObject self, string winAnimName) {
            var animPlayerRepo = context.AnimPlayerRepo;
            var key = self.GetInstanceID();
            if (!animPlayerRepo.TryGet(key, winAnimName, out var animPlayer)) {
                WinLogger.LogWarning($"动画播放器不存在 名称 {winAnimName}  key {key}");
                return;
            }

            animPlayer.EnterStop();
        }

        public void SetUseCustomOffsetAngle(GameObject self, string winAnimName, bool useCustomOffsetAngle) {
            var animPlayerRepo = context.AnimPlayerRepo;
            var key = self.GetInstanceID();
            if (!animPlayerRepo.TryGet(key, winAnimName, out var animPlayer)) {
                WinLogger.LogWarning($"动画播放器不存在 名称 {winAnimName}  key {key}");
                return;
            }

            animPlayer.SetUseCustomOffsetAngle(useCustomOffsetAngle);
        }

        public void SetTarget(GameObject self, string winAnimName, GameObject target) {
            var animPlayerRepo = context.AnimPlayerRepo;
            var key = self.GetInstanceID();
            if (!animPlayerRepo.TryGet(key, winAnimName, out var animPlayer)) {
                WinLogger.LogWarning($"动画播放器不存在 名称 {winAnimName}  key {key}");
                return;
            }

            animPlayer.SetTarget(target);
        }

        public void SetEndAction(GameObject self, string winAnimName, Action action) {
            var animPlayerRepo = context.AnimPlayerRepo;
            var key = self.GetInstanceID();
            if (!animPlayerRepo.TryGet(key, winAnimName, out var animPlayer)) {
                WinLogger.LogWarning($"动画播放器不存在 名称 {winAnimName}  key {key}");
                return;
            }

            animPlayer.SetEndAction(action);
        }

    }

}