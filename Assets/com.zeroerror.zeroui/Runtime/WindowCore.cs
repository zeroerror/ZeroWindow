using System.Collections.Generic;
using UnityEngine;

namespace ZeroWindowFrame {

    public class WindowCore {

        WindowAPI api;
        public IWindowAPI API => api;

        WindowContext context;

        public WindowCore() {
            context = new WindowContext();
            api = new WindowAPI();
        }

        public void Inject(IList<GameObject> uiAssets) {
            context.Inject(uiAssets);
            api.Inject(context);
        }

        public void Tick() {
            var repo = context.Repo;
            repo.ForeachAll(ui => {
                ui.Tick();
            });
        }

    }

}