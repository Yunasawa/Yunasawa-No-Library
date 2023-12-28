using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace RaindropFX {
    [Serializable]
    [PostProcess(typeof(BlurRenderer), PostProcessEvent.AfterStack, "RaindropFX/Blur")]
    public sealed class Blur : PostProcessEffectSettings {
        [Range(0, 15), Tooltip("Blur effect intensity.")]
        public IntParameter BlurRadius = new IntParameter { value = 1 };
    }

    public sealed class BlurRenderer : PostProcessEffectRenderer<Blur> {

        Material mat = new Material(Shader.Find("Hidden/Custom/GaussianBlur"));

        public override void Render(PostProcessRenderContext context) {

            if (settings.BlurRadius > 0) {
                var tmp0 = RenderTexture.GetTemporary(Screen.width, Screen.height, 0);
                var tmp1 = RenderTexture.GetTemporary(Screen.width, Screen.height, 0);
                var iters = Mathf.Clamp(settings.BlurRadius, 0, 15);
                context.command.Blit(context.source, tmp0);
                for (var i = 0; i < iters; i++) {
                    for (var pass = 1; pass < 3; pass++) {
                        tmp1.DiscardContents();
                        tmp0.filterMode = FilterMode.Bilinear;
                        context.command.Blit(tmp0, tmp1, mat, pass);
                        var tmpSwap = tmp0;
                        tmp0 = tmp1;
                        tmp1 = tmpSwap;
                    }
                }
                context.command.Blit(tmp0, context.destination);
                RenderTexture.ReleaseTemporary(tmp0);
                RenderTexture.ReleaseTemporary(tmp1);
            } else {
                context.command.Blit(context.source, context.destination);
            }

        }

    }
}