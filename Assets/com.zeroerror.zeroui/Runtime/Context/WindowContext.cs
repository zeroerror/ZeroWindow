using System.Collections.Generic;
using UnityEngine;
using ZeroWindowFrame;

namespace ZeroWindowFrame {

    public class WindowContext {

        public WindowDomain Domain { get; private set; }
        public WindowRepo Repo { get; private set; }
        public WindowFactory Factory { get; private set; }
        public Dictionary<string, GameObject> WindowAssets { get; private set; }

        public WindowEntity WindowEntity { get; set; }

        public WindowContext() {
            Domain = new WindowDomain();
            Repo = new WindowRepo();
            Factory = new WindowFactory();
            WindowAssets = new Dictionary<string, GameObject>();
            
            WindowEntity = new WindowEntity();
        }

        public void Inject(IList<GameObject> uiAssets) {
            var count = uiAssets.Count;
            for (int i = 0; i < count; i++) {
                var ui = uiAssets[i];
                var uiName = ui.name;
                WindowAssets.Add(uiName, ui);
                Debug.Log($"注入UI资产 {uiName}");
            }

            Domain.Inject(this);
            Factory.Inject(this);
        }

    }

}