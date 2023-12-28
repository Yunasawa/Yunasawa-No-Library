using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RaindropFX;

namespace RaindropFX {
    [ExecuteInEditMode]
    public class MaterialLinker : MaterialLinkerBase {
        #region properties
        public string normalTexPropertieName = "_BumpMap";
        public string fogMaskPropertieName = "_FogMaskMap";
        #endregion

        private void Update() {
            Solve();
            if (calcRainTex != null && fogMask != null) {
                targetMat.SetTexture(normalTexPropertieName, calcRainTex);
                targetMat.SetTexture(fogMaskPropertieName, fogMask);
            }
        }

    }

}