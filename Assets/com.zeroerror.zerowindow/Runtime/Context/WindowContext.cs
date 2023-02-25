using System.Collections.Generic;
using UnityEngine;
using ZeroWindow;

namespace ZeroWindow {

    public class WindowContext {

        public WindowDomain Domain { get; private set; }
        public WindowRepo Repo { get; private set; }
        public WindowFactory Factory { get; private set; }
        public Dictionary<string, GameObject> WindowAssets { get; private set; }
        public WindowService Service { get; private set; }

        public WindowContext() {
            Domain = new WindowDomain();
            Repo = new WindowRepo();
            Factory = new WindowFactory();
            WindowAssets = new Dictionary<string, GameObject>();
        }

        public void Inject(IList<GameObject> uiAssets, WindowService service) {
            var count = uiAssets.Count;
            for (int i = 0; i < count; i++) {
                var ui = uiAssets[i];
                var uiName = ui.name;
                WindowAssets.Add(uiName, ui);
                Debug.Log($"注入UI资产 {uiName}");
            }

            this.Service = service;

            Domain.Inject(this);
            Factory.Inject(this);
        }

    }

}