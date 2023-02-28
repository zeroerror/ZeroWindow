using System.Collections.Generic;
using UnityEngine;
using ZeroWin.Extension;

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
            WinExtension.Inject(context);
        }

        public void Tick(float dt) {
            var winAnimDomain = context.WinAnimDomain;
            winAnimDomain.TickAllAnimPlayer(dt);
        }

        public void Dispose() {
            // Static Dispose
            WinExtension.Dispose();
        }

    }

}