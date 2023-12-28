using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.PostProcessing;
using RaindropFX;

namespace RaindropFX {

    public enum StepMode {
        origin,
        blur,
        colorLevel,
        finalRes
    }

    [Serializable]
    public sealed class StepModeParameter : ParameterOverride<StepMode> { }

    [Serializable]
    [PostProcess(typeof(RaindropFX_PPVRenderer), PostProcessEvent.AfterStack, "RaindropFX/CameraLensRain")]
    public sealed class RaindropFX_PPV : PostProcessEffectSettings {
        // system properties
        [Tooltip("Waterdrops will fade in/out automatically if you disable/enable this.")]
        public BoolParameter fadeout_fadein_switch = new BoolParameter { value = false };

        [Tooltip("Waterdrops will fade in/out with higher frame rate but lower accuracy.")]
        public BoolParameter fastMode = new BoolParameter { value = false };

        [Range(0.01f, 1), Tooltip("Control the speed of fadeout, the bigger, the faster.")]
        public FloatParameter fadeSpeed = new FloatParameter { value = 0.02f };

        [Tooltip("Please use droplet texture with alpha channel.")]
        public TextureParameter _raindropTex_alpha = new TextureParameter { value = null };

        [Tooltip("Use a grayscale image to specify the screen area affected by water droplets, " +
            "with black color representing culling area.")]
        public TextureParameter _rainMask_grayscale = new TextureParameter { value = null };

        [Tooltip("Enable this if you want to pixelize the raindrops.")]
        public BoolParameter pixelization = new BoolParameter { value = false };

        [Range(1, 1024), Tooltip("Pixelization the raindrop texture, set size of a pixel.")]
        public FloatParameter pixResolution = new FloatParameter { value = 1 };

        [Tooltip("Allow you to wipe the raindrops via GameObject.")]
        public BoolParameter wipeEffect = new BoolParameter { value = false };
        public TextureParameter wiper = new TextureParameter { value = null };

        [Tooltip("Specifies the size of the rendered texture.")]
        public BoolParameter forceRainTextureSize = new BoolParameter { value = true };

        [Tooltip("Specifies the size of the rendered texture.")]
        public Vector2Parameter calcRainTextureSize = new Vector2Parameter { value = new Vector2Int(800, 450) };

        [Range(0, 1), Tooltip("Time step of physical computing.")]
        public FloatParameter calcTimeStep = new FloatParameter { value = 0.1f };

        [Range(0, 10), Tooltip("Frame interval of texture rendering.")]
        public IntParameter refreshRate = new IntParameter { value = 1 };

        [Tooltip("Dynamic water droplets produce a tail when they slide if you enable this.")]
        public BoolParameter generateTrail = new BoolParameter { value = true };

        [Range(0, 10000), Tooltip("Max number of static raindrops.")]
        public IntParameter maxStaticRaindropNumber = new IntParameter { value = 5000 };

        [Range(0, 1000), Tooltip("Max number of dynamic raindrops.")]
        public IntParameter maxDynamicRaindropNumber = new IntParameter { value = 10 };

        [Tooltip("Random droplet size range.")]
        public Vector2Parameter raindropSizeRange = new Vector2Parameter { value = new Vector2(0.1f, 0.25f) };

        [Tooltip("Tint color for droplets.")]
        public ColorParameter tintColor = new ColorParameter { value = Color.white };

        [Range(1, 10), Tooltip("The larger the value, the thicker the color.")]
        public IntParameter tintWeight = new IntParameter { value = 1 };

        // physical properties
        [Tooltip("Enable this if you want to use wind.")]
        public BoolParameter useWind = new BoolParameter { value = false };

        [Range(0, 1), Tooltip("Enable wind turbulence.")]
        public FloatParameter windTurbulence = new FloatParameter { value = 0.1f };

        [Range(0.01f, 10), Tooltip("Adjust scale of wind turbulence.")]
        public FloatParameter windTurbScale = new FloatParameter { value = 1.0f };

        [Tooltip("Wind power adjustment.")]
        public Vector2Parameter wind = new Vector2Parameter { value = new Vector2(0.0f, 0.0f) };

        [Tooltip("Enable radial wind, mostly for driving simulation.")]
        public BoolParameter radialWind = new BoolParameter { value = false };

        [Tooltip("Gravity adjustment.")]
        public Vector2Parameter gravity = new Vector2Parameter { value = new Vector2(0.0f, -9.8f) };

        [Tooltip("Friction adjustment.")]
        public FloatParameter friction = new FloatParameter { value = 0.8f };

        [Range(0, 10), Tooltip("Screen blend effect intensity.")]
        public FloatParameter distortion = new FloatParameter { value = 0.6f };

        // Fog properties
        [Tooltip("If you want to use screen fog, enable this.")]
        public BoolParameter useFog = new BoolParameter { value = false };

        [Range(0.01f, 1), Tooltip("Screen fog effect intensity.")]
        public FloatParameter fogIntensity = new FloatParameter { value = 0.5f };

        [Range(1, 5), Tooltip("Controls the effect of water droplet wake on fog.")]
        public IntParameter fogIteration = new IntParameter { value = 2 };

        // screen blend properties
        [Range(0, 15), Tooltip("Edge softness of water droplets.")]
        public IntParameter edgeSoftness = new IntParameter { value = 1 };

        [Range(0f, 255f), Tooltip("Color level parameter.")]
        public FloatParameter _inBlack = new FloatParameter { value = 55.0f };

        [Range(0f, 255f), Tooltip("Color level parameter.")]
        public FloatParameter _inWhite = new FloatParameter { value = 180.0f };

        [Range(0f, 255f), Tooltip("Color level parameter.")]
        public FloatParameter _outWhite = new FloatParameter { value = 160.0f };

        [Range(0f, 255f), Tooltip("Color level parameter.")]
        public FloatParameter _outBlack = new FloatParameter { value = 5.0f };

        // foreground blur properties
        [Tooltip("Enable this if you want to blur waterdrops.")]
        public BoolParameter dropletBlur = new BoolParameter { value = false };

        [Range(0f, 10f), Tooltip("Adjust focal length.")]
        public FloatParameter _focalize = new FloatParameter { value = 1.0f };

        [Range(0f, 10f), Tooltip("Adjust blur strength.")]
        public IntParameter blurIteration = new IntParameter { value = 1 };

        // debug properties
        [Tooltip("Show steps.")]
        public StepModeParameter showSteps = new StepModeParameter { value = StepMode.finalRes };

        [Tooltip("Enable this if you want to see debug info.")]
        public BoolParameter debugLog = new BoolParameter { value = false };
    }

    public class RaindropFX_PPVRenderer : PostProcessEffectRenderer<RaindropFX_PPV> {

        #region parameters
        public RaindropGenerator solver;
        public PostProcessRenderContext context;

        RenderTexture temp;
        RenderTexture temp2;
        public RenderTexture tempWip;
        public RenderTexture wipDelta;

        bool initFlag = false;
        Wiper wiper = null;
        bool cnt;
        #endregion

        //------------------------------------------------------
        // Use this method to auto fadeout and fadein the effect
        // Example: RaindropFX.RaindropFX_PPVRenderer.SetEnable(true/false);
        //------------------------------------------------------
        public void SetEnable(bool set) {
            solver.fadeout_fadein_switch = set;
            if (!set) RaindropFX_Tools.PrintLog("fadeout...");
        }

        //---------------------------------------------
        // Use this method to set up the wind
        //---------------------------------------------
        public void SetWind(bool isEnabled, Vector2 windForce, float turbulenceWeight, float turbScale) {
            settings.useWind.value = isEnabled;
            settings.wind.value = windForce;
            settings.windTurbulence.value = turbulenceWeight;
            settings.windTurbScale.value = turbScale;
            RaindropFX_Tools.PrintLog("Set wind to: " + (isEnabled ? ("enabled, power: " + windForce + " turbulence: " + 
                                     turbulenceWeight + "turbScale: " + turbScale) :  "disabled"));
        }

        //---------------------------------
        // Calculate raindrops animation
        //---------------------------------
        public override void Render(PostProcessRenderContext context) {
            this.context = context;
            
            if (settings._raindropTex_alpha.value != null) {
                Constraints();
                solver.UpdateProps(ref settings.fadeout_fadein_switch.value, ref settings.fastMode.value, ref settings.fadeSpeed.value,
                                   ref settings.forceRainTextureSize.value, ref settings.calcTimeStep.value, ref settings.refreshRate.value,
                                   ref settings.generateTrail.value, ref settings.maxStaticRaindropNumber.value, ref settings.maxDynamicRaindropNumber.value,
                                   ref settings.raindropSizeRange.value, ref settings.useWind.value, ref settings.windTurbulence.value, 
                                   ref settings.windTurbScale.value, ref settings.wind.value, ref settings.gravity.value, ref settings.friction.value, 
                                   ref settings.distortion.value, ref settings.useFog.value, ref settings.fogIntensity.value, ref settings.fogIteration.value, 
                                   ref settings.edgeSoftness.value, ref settings._inBlack.value, ref settings._inWhite.value, ref settings._outWhite.value, 
                                   ref settings._outBlack.value, ref settings.dropletBlur.value, ref settings._focalize.value, ref settings.blurIteration.value, 
                                   ref settings.tintColor.value, ref settings.tintWeight.value, ref settings.radialWind.value, ref settings._rainMask_grayscale.value,
                                   ref settings.pixelization.value, ref settings.pixResolution.value);
                solver.CalcRainTex();

                if (solver.staticRaindrops.Count + solver.dynamicRaindrops.Count <= 0 && solver.maxDynamicRaindropNumber + solver.maxStaticRaindropNumber > 0) {
                    context.command.Blit(context.source, context.destination);
                    return;
                }

                if (settings.wipeEffect.value && settings.wiper.value != null) {
                    var tempD = RenderTexture.GetTemporary(solver.calcTexSize.x, solver.calcTexSize.y, 0);
                    if (cnt) context.command.Blit(wipDelta, tempD);
                    else RaindropFX_Tools.Blur(context, wipDelta, tempD, 1,
                        solver.blur_material, solver.calcTexSize); cnt = !cnt;
                    solver.time_material.SetFloat("_ratio", (float)solver.calcTexSize.x / solver.calcTexSize.y);
                    solver.time_material.SetTexture("_WipeTex", settings.wiper.value);
                    context.command.Blit(tempD, wipDelta, solver.time_material);

                    solver.wipe_material.SetTexture("_WipeTex", settings.wiper.value);
                    context.command.Blit(solver.calcRainTex, tempWip, solver.wipe_material);

                    wiper.GetWiped(this);
                    RenderTexture.ReleaseTemporary(tempD);
                } else if (settings.wipeEffect.value) {
                    RaindropFX_Tools.PrintLog("wiper is null!");
                }

                // apply blur effect to solver.calcRainTex
                RaindropFX_Tools.Blur(context, solver.calcRainTex, temp, settings.edgeSoftness.value, solver.blur_material, solver.calcTexSize);

                // apply color level to solver.calcRainTex
                solver.SetLevelMat();
                context.command.Blit(temp, temp2, solver.level_material);

                // pixelize the raindrop
                if (solver.pixelization) {
                    solver.blend_material.SetFloat("_PixelSize", solver.pixResolution);
                } else solver.blend_material.SetFloat("_PixelSize", -1);

                // convert height map to normal map and create screen blend effect
                solver.blend_material.SetInt("_IsEnableWip", settings.wipeEffect.value ? 1 : 0);
                solver.SetScreenBlendMat(ref temp2, ref temp, ref wipDelta);
                
                // output final result
                if (settings.showSteps.value == StepMode.origin) context.command.Blit(solver.calcRainTex, context.destination);
                else if (settings.showSteps.value == StepMode.blur) context.command.Blit(temp, context.destination);
                else if (settings.showSteps.value == StepMode.colorLevel) context.command.Blit(temp2, context.destination);
                else

                // blur droplet
                if (settings.dropletBlur.value) {
                    var temp3 = RenderTexture.GetTemporary(solver.calcTexSize.x, solver.calcTexSize.y, 0);
                    var temp4 = RenderTexture.GetTemporary(solver.calcTexSize.x, solver.calcTexSize.y, 0);

                    context.command.Blit(context.source, temp3, solver.blend_material);
                    RaindropFX_Tools.Blur(context, temp3, temp4, settings.blurIteration.value, solver.blur_material, solver.calcTexSize);

                    solver.SetDropblurMat(ref temp4, ref temp);
                    context.command.Blit(temp3, context.destination, solver.dropblur_material);

                    RenderTexture.ReleaseTemporary(temp3);
                    RenderTexture.ReleaseTemporary(temp4);
                } else
                    context.command.Blit(context.source, context.destination, solver.blend_material);
            
            } else {
                RaindropFX_Tools.PrintLog("raindrop tex is null!");
                context.command.Blit(context.source, context.destination);
            }
        }

        //---------------------------------
        // Initialize raindrop solver
        //---------------------------------
        private void InitSys() {
            solver.Init(
                (Texture2D)settings._raindropTex_alpha.value, new
                Vector2Int((int)settings.calcRainTextureSize.value.x,
                (int)settings.calcRainTextureSize.value.y)
            );
            RaindropFX_Tools.debugLog = settings.debugLog.value;
            RaindropFX_Tools.PrintLog("now calc tex size: " + "(" + solver.calcTexSize.x + ", " + solver.calcTexSize.y + ")");
        }

        //---------------------------------
        // Parameter security detection
        //---------------------------------
        private void Constraints() {
            if (solver == null) solver = new RaindropGenerator();
            if (solver.calcRainTex == null) initFlag = true;
            if (initFlag) { InitSys(); initFlag = false; }
            if (wiper == null) wiper = GameObject.FindObjectOfType(typeof(Wiper)) as Wiper;
            if (temp == null) temp = new RenderTexture(solver.calcTexSize.x, solver.calcTexSize.y, 16);
            if (temp2 == null) temp2 = new RenderTexture(solver.calcTexSize.x, solver.calcTexSize.y, 16);
            if (tempWip == null) tempWip = new RenderTexture(solver.calcTexSize.x, solver.calcTexSize.y, 16);
            if (wipDelta == null) wipDelta = new RenderTexture(solver.calcTexSize.x, solver.calcTexSize.y, 16);

            if (settings.calcRainTextureSize.value.x < 0) settings.calcRainTextureSize.value.x = 1;
            if (settings.calcRainTextureSize.value.y < 0) settings.calcRainTextureSize.value.y = 1;
            if (settings.maxDynamicRaindropNumber.value < 0) settings.maxDynamicRaindropNumber.value = 0;
            if (settings.calcTimeStep.value < 0.1f) settings.calcTimeStep.value = 0.1f;
            if (settings.raindropSizeRange.value.x <= 0.01f) settings.raindropSizeRange.value.x = 0.01f;
            if (settings.raindropSizeRange.value.y < settings.raindropSizeRange.value.x)
                settings.raindropSizeRange.value.y = settings.raindropSizeRange.value.x;
            // Raindrop_PPV.sFriction = settings.friction;
            RaindropFX_Tools.debugLog = settings.debugLog.value;
        }
        
    }
}