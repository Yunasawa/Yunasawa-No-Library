using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RaindropFX;

namespace RaindropFX {
    public class MaterialLinkerBase : MonoBehaviour {
        #region read-only properties
        [HideInInspector]
        public RenderTexture calcRainTex = null;
        [HideInInspector]
        public RenderTexture fogMask = null;
        public Material targetMat = null;
        #endregion
        
        #region raindrop solver properties
        private bool initFlag = false;
        private RaindropGenerator solver = new RaindropGenerator();

        public bool fadeout_fadein_switch = false;
        public bool fastMode = true;
        public float fadeSpeed = 0.02f;

        public Texture2D _raindropTex_alpha;
        public Texture _rainMask_grayscale;

        [HideInInspector]
        public bool pixelization = false;
        [HideInInspector]
        public float pixResolution = 1;

        public bool forceRainTextureSize = true;
        public Vector2Int calcRainTextureSize = new Vector2Int(800, 450);
        public float calcTimeStep = 0.1f;
        public int refreshRate = 1;
        public bool generateTrail = true;
        public int maxStaticRaindropNumber = 5000;
        public int maxDynamicRaindropNumber = 10;
        public Vector2 raindropSizeRange = new Vector2(0.1f, 0.25f);
        [HideInInspector]
        public float maxRandomDynamicForce = 3.5f;
        [HideInInspector]
        public Color tintColor = Color.white;
        [HideInInspector]
        public int tintWeight = 1;

        // physical properties
        public bool useWind = false;
        public bool radialWind = false;
        public float windTurbulence = 0.1f;
        public float windTurbScale = 1.0f;
        public Vector2 wind = new Vector2(0.0f, 0.0f);
        public Vector2 gravity = new Vector2(0.0f, -9.8f);
        public float friction = 0.8f;

        // Fog properties
        [HideInInspector]
        public bool useFog = false;
        [HideInInspector]
        public float fogIntensity = 0.5f;
        [HideInInspector]
        public int fogIteration = 2;
        //[HideInInspector]
        //public Color fogTint = Color.white;

        // screen blend properties
        [HideInInspector]
        public float distortion = 0.6f;
        public int edgeSoftness = 1;
        public float _inBlack = 55.0f;
        public float _inWhite = 180.0f;
        public float _outWhite = 160.0f;
        public float _outBlack = 5.0f;

        // foreground blur properties
        [HideInInspector]
        public bool dropletBlur = false;
        [HideInInspector]
        public float _focalize = 1.0f;
        [HideInInspector]
        public int blurIteration = 1;

        private Vector2 fadeSpeedRange = new Vector2(0.25f, 0.5f);
        private Vector2 shrinkSpeed = new Vector2(0.01f, 0.02f);
        private Vector2 loseWeightRange = new Vector2(0.01f, 0.02f);
        private float killSize = 0.05f;

        public bool debugLog = false;
        #endregion

        //---------------------------------------------------------------------------------------
        // Call this method to get animated screen rain texture and fog mask in every time step
        //---------------------------------------------------------------------------------------
        public void Solve() {
            if (_raindropTex_alpha != null) {
                Constraints();
                SendPropertiesToSolver();
                solver.CalcRainTex();
                solver.GenerateTextures(ref calcRainTex, ref fogMask);
            } else {
                if (solver.raindropTex_alpha == null) RaindropFX_Tools.PrintLog("raindrop tex is null!");
            }
        }

        //-------------------------------------
        // send properties to raindrop solver
        //-------------------------------------
        private void SendPropertiesToSolver() {
            solver.UpdateProps(ref fadeout_fadein_switch, ref fastMode, ref fadeSpeed, ref forceRainTextureSize, ref calcTimeStep,
                               ref refreshRate, ref generateTrail, ref maxStaticRaindropNumber, ref maxDynamicRaindropNumber,
                               ref raindropSizeRange, ref useWind, ref windTurbulence, ref windTurbScale, ref wind, ref gravity,
                               ref friction, ref distortion, ref useFog, ref fogIntensity, ref fogIteration, ref edgeSoftness,
                               ref _inBlack, ref _inWhite, ref _outWhite, ref _outBlack, ref dropletBlur, ref _focalize,
                               ref blurIteration, ref tintColor, ref tintWeight, ref radialWind, ref _rainMask_grayscale, 
                               ref pixelization, ref pixResolution);
        }

        //---------------------------------
        // Initialize raindrop solver
        //---------------------------------
        private void InitSys() {
            solver.Init(_raindropTex_alpha, calcRainTextureSize);
        }

        //---------------------------------
        // Parameter security detection
        //---------------------------------
        private void Constraints() {
            if (targetMat == null) targetMat = this.GetComponent<Renderer>().sharedMaterial;
            if (solver == null) solver = new RaindropGenerator();
            if (solver.calcRainTex == null) initFlag = true;
            if (initFlag) { InitSys(); initFlag = false; }

            if (calcRainTextureSize.x < 0) calcRainTextureSize.x = 1;
            if (calcRainTextureSize.y < 0) calcRainTextureSize.y = 1;
            if (calcTimeStep < 0) calcTimeStep = 0;
            else if (calcTimeStep > 1) calcTimeStep = 1;
            if (refreshRate < 0) refreshRate = 0;
            else if (refreshRate > 10) refreshRate = 10;

            if (maxDynamicRaindropNumber < 0) maxDynamicRaindropNumber = 0;
            else if (maxDynamicRaindropNumber > 1000) maxDynamicRaindropNumber = 1000;
            if (maxStaticRaindropNumber < 0) maxStaticRaindropNumber = 0;
            else if (maxStaticRaindropNumber > 10000) maxStaticRaindropNumber = 10000;
            if (raindropSizeRange.x <= 0.01f) raindropSizeRange.x = 0.01f;
            if (raindropSizeRange.y < raindropSizeRange.x) raindropSizeRange.y = raindropSizeRange.x;

            if (tintWeight < 1) tintWeight = 1;
            else if (tintWeight > 10) tintWeight = 10;

            if (fadeSpeed < 0.01f) fadeSpeed = 0.01f;
            else if (fadeSpeed > 1) fadeSpeed = 1;
            if (distortion < 0) distortion = 0;
            else if (distortion > 10) distortion = 10;
            if (fogIntensity < 0.01f) fogIntensity = 0.01f;
            else if (fogIntensity > 1) fogIntensity = 1;
            if (edgeSoftness < 0) edgeSoftness = 0;
            else if (edgeSoftness > 15) edgeSoftness = 15;

            if (_inBlack < 0) _inBlack = 0;
            else if (_inBlack > 255) _inBlack = 255;
            if (_inWhite < 0) _inWhite = 0;
            else if (_inWhite > 255) _inWhite = 255;
            if (_outBlack < 0) _outBlack = 0;
            else if (_outBlack > 255) _outBlack = 255;
            if (_outWhite < 0) _outWhite = 0;
            else if (_outWhite > 255) _outWhite = 255;

            if (windTurbulence < 0) windTurbulence = 0;
            else if (windTurbulence > 1) windTurbulence = 1;
            if (windTurbScale < 0.01f) windTurbScale = 0.01f;
            else if (windTurbScale > 10) windTurbScale = 10;

            if (_focalize < 0) _focalize = 0;
            else if (_focalize > 10) _focalize = 10;

            if (blurIteration < 0) blurIteration = 0;
            else if (blurIteration > 10) blurIteration = 10;

            if (pixResolution < 1) pixResolution = 1;
            else if (pixResolution > 1024) pixResolution = 1024;

            RaindropFX_Tools.debugLog = this.debugLog;
        }
    }

}