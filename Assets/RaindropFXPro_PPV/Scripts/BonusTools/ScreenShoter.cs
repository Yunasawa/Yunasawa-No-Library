using System;
using UnityEngine;
using System.IO;
using UnityEngine.Rendering.PostProcessing;

namespace RaindropFX {
    [Serializable]
    [PostProcess(typeof(ScreenShoterRenderer), PostProcessEvent.AfterStack, "RaindropFX/ScreenShoter")]
    public sealed class ScreenShoter : PostProcessEffectSettings {
        [Tooltip("Frame interval.")]
        public IntParameter interval = new IntParameter { value = 10 };
    }

    public sealed class ScreenShoterRenderer : PostProcessEffectRenderer<ScreenShoter> {

        private int loopID = 0;
        private int clock = 0;

        public override void Render(PostProcessRenderContext context) {
            if (Application.isPlaying) {
                if (clock >= settings.interval.value) {
                    clock = 0;

                    // save ss to desk
                    CapruerScreen();
                    loopID++;
                } else clock++;
            }

            context.command.Blit(context.source, context.destination);
        }

        private void CapruerScreen() {
            string fullPath = string.Format(Application.streamingAssetsPath + "\\Output\\ScreenShot_{0}.png", loopID);
            ScreenCapture.CaptureScreenshot(fullPath, 0);
        }

    }
}