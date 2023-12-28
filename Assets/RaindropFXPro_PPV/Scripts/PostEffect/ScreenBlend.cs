using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace RaindropFX {
    [Serializable]
    [PostProcess(typeof(ScreenBlendRenderer), PostProcessEvent.AfterStack, "RaindropFX/ScreenBlend")]
    public sealed class ScreenBlend : PostProcessEffectSettings {
        [Range(0, 10), Tooltip("Screen distortion intensity.")]
        public FloatParameter distortion = new FloatParameter { value = 0.15f };

        [Range(0, 1), Tooltip("Color map blend intensity.")]
        public FloatParameter colorIntensity = new FloatParameter { value = 0.15f };

        [Tooltip("Grayscale image for screen blend.")]
        public TextureParameter heightMap = new TextureParameter { value = null };

        [Tooltip("Color image for screen blend.")]
        public TextureParameter colorMap = new TextureParameter { value = null };
    }

    public sealed class ScreenBlendRenderer : PostProcessEffectRenderer<ScreenBlend> {

        Material mat = new Material(Shader.Find("Hidden/Custom/ScreenBlendEffect_Ex"));

        public override void Render(PostProcessRenderContext context) {

            if (mat == null) mat = new Material(Shader.Find("Hidden/Custom/ScreenBlendEffect_Ex"));

            if (settings.heightMap != null) {
                mat.SetFloat("_Distortion", settings.distortion);
                mat.SetTexture("_HeightMap", settings.heightMap);
                mat.SetVector("_MainTex_TexelSize", new Vector4(Screen.width, Screen.height, 0, 0));
                mat.SetTexture("_AdditionalColTex", settings.colorMap);
                mat.SetFloat("_ColBlendAmount", settings.colorIntensity.value);
                context.command.Blit(context.source, context.destination, mat);
            } else {
                context.command.Blit(context.source, context.destination);
            }

        }

    }
}