using System.Collections.Generic;
using UnityEngine;

namespace ZeroWin {

    public class WinCore {

        WinAPI api;
        public IWinAPI API => api;

        WinService service;
        WinContext context;

        public WinCore(Vector2 resolution, string layerName) {
            context = new WinContext();
            api = new WinAPI();
            service = new WinService(resolution, layerName);
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