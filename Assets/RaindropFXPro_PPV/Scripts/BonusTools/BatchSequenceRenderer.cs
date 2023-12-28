using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.PostProcessing;

namespace RaindropFX {
    [Serializable]
    [PostProcess(typeof(BatchSequenceRendererRenderer), PostProcessEvent.AfterStack, "RaindropFX/BatchSequenceRenderer")]
    public sealed class BatchSequenceRenderer : PostProcessEffectSettings {
        [Tooltip("Frame interval.")]
        public IntParameter interval = new IntParameter { value = 10 };
    }

    public sealed class BatchSequenceRendererRenderer : PostProcessEffectRenderer<BatchSequenceRenderer> {

        Material mat = new Material(Shader.Find("Hidden/Custom/ScreenBlendEffect_Ex"));
        private UnityEngine.Object[] textures = null;
        private int loopID = 0;
        private int clock = 0;
        private bool enable = true;

        public override void Render(PostProcessRenderContext context) {

            if (enable && Application.isPlaying) {

                if (textures == null)
                    textures = Resources.LoadAll("Pictures", typeof(Texture2D));

                if (loopID >= textures.Length) {
                    Debug.Log("finished!");
                    enable = false;
                    return ;
                }

                if (textures.Length > 0) {
                    Texture2D texture = (Texture2D)textures[loopID];

                    if (clock >= settings.interval.value) {
                        clock = 0;

                        loopID++;
                        Debug.Log("loaded: " + texture.name);
                    } else clock++;

                    // rendering to screen
                    mat.SetFloat("_Distortion", 0);
                    //mat.SetTexture("_HeightMap", settings.heightMap);
                    mat.SetVector("_MainTex_TexelSize", new Vector4(Screen.width, Screen.height, 0, 0));
                    mat.SetFloat("_ColBlendAmount", 1.0f);
                    mat.SetTexture("_AdditionalColTex", texture);
                    context.command.Blit(context.source, context.destination, mat);
                }

            } else context.command.Blit(context.source, context.destination);

        }

    }
}