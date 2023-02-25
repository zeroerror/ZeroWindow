using System.Collections.Generic;
using UnityEngine;

namespace ZeroWindow {

    public class WindowCore {

        WindowAPI api;
        public IWindowAPI API => api;

        WindowService service;
        WindowContext context;

        public WindowCore(Vector2 resolution, string layerName) {
            context = new WindowContext();
            api = new WindowAPI();
            service = new WindowService(resolution, layerName);
        }

        public void Inject(IList<GameObject> uiAssets) {
            context.Inject(uiAssets, service);
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