using System.Collections.Generic;
using UnityEngine;
using ZeroWin;

namespace ZeroWin {

    public class WinContext {

        public WinBaseDomain WinBaseDomain { get; private set; }
        public WinAnimDomain WinAnimDomain { get; private set; }

        public WinBaseRepo WinBaseRepo { get; private set; }
        public WinAnimPlayerRepo AnimPlayerRepo { get; private set; }

        public WinFactory Factory { get; private set; }
        public WinService WinService { get; private set; }
        
        public Dictionary<string, GameObject> WinAssets { get; private set; }

        public WinContext() {
            WinBaseDomain = new WinBaseDomain();
            WinAnimDomain = new WinAnimDomain();
            
            WinBaseRepo = new WinBaseRepo();
            AnimPlayerRepo = new WinAnimPlayerRepo();

            Factory = new WinFactory();
            WinAssets = new Dictionary<string, GameObject>();
        }

        public void Inject(IList<GameObject> uiAssets, WinService service) {
            var count = uiAssets.Count;
            for (int i = 0; i < count; i++) {
                var ui = uiAssets[i];
                var uiName = ui.name;
                WinAssets.Add(uiName, ui);
                Debug.Log($"外部注入资产 {uiName}");
            }

            this.WinService = service;

            WinBaseDomain.Inject(this);
            WinAnimDomain.Inject(this);
            Factory.Inject(this);
        }

    }

}