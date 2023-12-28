using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace RaindropFX {
    [Serializable]
    [PostProcess(typeof(SplitScreenRenderer), PostProcessEvent.AfterStack, "RaindropFX/SplitScreen")]
    public sealed class SplitScreen : PostProcessEffectSettings {
        [Tooltip("Render texture for player2(camera2).")]
        public TextureParameter player2View = new TextureParameter { value = null };

        [Tooltip("Mask image for split screen.")]
        public TextureParameter maskImage = new TextureParameter { value = null };
    }

    public sealed class SplitScreenRenderer : PostProcessEffectRenderer<SplitScreen> {

        Material mat = new Material(Shader.Find("Hidden/Custom/DropletBlur"));

        public override void Render(PostProcessRenderContext context) {

            if (mat == null) mat = new Material(Shader.Find("Hidden/Custom/DropletBlur"));

            if (settings.maskImage.value != null && settings.player2View.value != null) {
                mat.SetTexture("_BlurredMainTex", settings.player2View.value);
                mat.SetTexture("_MaskMap", settings.maskImage.value);
                mat.SetFloat("_focalize", 1.0f);
                context.command.Blit(context.source, context.destination, mat);
            } else {
                context.command.Blit(context.source, context.destination);
            }

        }

    }
}