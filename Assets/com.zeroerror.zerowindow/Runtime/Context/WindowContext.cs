using System.Collections.Generic;
using UnityEngine;
using ZeroWin;

namespace ZeroWin {

    public class WinContext {

        public WinDomain Domain { get; private set; }
        public WinRepo Repo { get; private set; }
        public WinFactory Factory { get; private set; }
        public Dictionary<string, GameObject> WinAssets { get; private set; }
        public WinService Service { get; private set; }

        public WinContext() {
            Domain = new WinDomain();
            Repo = new WinRepo();
            Factory = new WinFactory();
            WinAssets = new Dictionary<string, GameObject>();
        }

        public void Inject(IList<GameObject> uiAssets, WinService service) {
            var count = uiAssets.Count;
            for (int i = 0; i < count; i++) {
                var ui = uiAssets[i];
                var uiName = ui.name;
                WinAssets.Add(uiName, ui);
                Debug.Log($"注入UI资产 {uiName}");
            }

            this.Service = service;

            Domain.Inject(this);
            Factory.Inject(this);
        }

    }

}