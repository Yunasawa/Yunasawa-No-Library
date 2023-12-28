using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace RaindropFX {
    [Serializable]
    [PostProcess(typeof(ColorLevelRenderer), PostProcessEvent.AfterStack, "RaindropFX/ColorLevel")]
    public sealed class ColorLevel : PostProcessEffectSettings {
        [Range(0f, 255f), Tooltip("Color level parameter.")]
        public FloatParameter _inBlack = new FloatParameter { value = 0.0f };

        [Range(0f, 255f), Tooltip("Color level parameter.")]
        public FloatParameter _inWhite = new FloatParameter { value = 255.0f };

        [Range(0f, 255f), Tooltip("Color level parameter.")]
        public FloatParameter _outWhite = new FloatParameter { value = 255.0f };

        [Range(0f, 255f), Tooltip("Color level parameter.")]
        public FloatParameter _outBlack = new FloatParameter { value = 0.0f };
    }

    public sealed class ColorLevelRenderer : PostProcessEffectRenderer<ColorLevel> {

        Material mat = new Material(Shader.Find("Hidden/Custom/ColorLevel"));

        public override void Render(PostProcessRenderContext context) {

            if (mat == null) mat = new Material(Shader.Find("Hidden/Custom/ColorLevel"));

            mat.SetFloat("_inBlack", settings._inBlack);
            mat.SetFloat("_inWhite", settings._inWhite);
            mat.SetFloat("_outWhite", settings._outWhite);
            mat.SetFloat("_outBlack", settings._outBlack);
            context.command.Blit(context.source, context.destination, mat);
        }
    }
}