using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace RaindropFX {
    public static class RaindropFX_Tools {

        public static bool debugLog = true;

        public static void PrintLog(string message) {
            if (debugLog) Debug.Log("RaindropFX info: " + message);
        }

        public static float PerlinNoiseSampler(Vector2 position, float scale) {
            float value = Mathf.PerlinNoise(position.x * scale, position.y * scale) * 2.0f - 1.0f;
            return value;
        }

        public static void RenderTextureToTexture2D(ref RenderTexture rTex, ref Texture2D dest) {
            RenderTexture act = RenderTexture.active;
            RenderTexture.active = rTex;
            dest.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
            dest.Apply();
            RenderTexture.active = act;

            //Resources.UnloadUnusedAssets();
            //System.GC.Collect();
        }

        public static Vector2Int GetViewSize() {
#if UNITY_EDITOR
            PrintLog("Editor Mode");
            System.Type T = System.Type.GetType("UnityEditor.GameView,UnityEditor");
            System.Reflection.MethodInfo GetMainGameView = T.GetMethod("GetMainGameView", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
            System.Object Res = GetMainGameView.Invoke(null, null);
            var gameView = (UnityEditor.EditorWindow)Res;
            var prop = gameView.GetType().GetProperty("currentGameViewSize", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var gvsize = prop.GetValue(gameView, new object[0] { });
            var gvSizeType = gvsize.GetType();
            int height = (int)gvSizeType.GetProperty("height", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance).GetValue(gvsize, new object[0] { });
            int width = (int)gvSizeType.GetProperty("width", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance).GetValue(gvsize, new object[0] { });
            return new Vector2Int(width, height);
#else
            return new Vector2Int(Screen.width, Screen.height);
#endif
        }

        public static Vector2 RotateAround(Vector2 targetPoint, Vector2 rotCenter, float theta) {
            float cx = rotCenter.x, cy = rotCenter.y;
            float px = targetPoint.x, py = targetPoint.y;

            float s = Mathf.Sin(theta);
            float c = Mathf.Cos(theta);
            px -= cx;
            py -= cy;
            
            float xnew = px * c + py * s;
            float ynew = -px * s + py * c;
            px = xnew + cx;
            py = ynew + cy;

            return new Vector2(px, py);
        }

        //----------------
        // Gaussian blur
        //----------------
        public static void Blur(PostProcessRenderContext context, UnityEngine.Rendering.RenderTargetIdentifier src, 
                           UnityEngine.Rendering.RenderTargetIdentifier dst, int nIterations, 
                           Material gaussianMat, Vector2Int calcTexSize) {
            if (nIterations > 0) {
                var tmp0 = RenderTexture.GetTemporary(calcTexSize.x, calcTexSize.y, 0);
                var tmp1 = RenderTexture.GetTemporary(calcTexSize.x, calcTexSize.y, 0);
                var iters = Mathf.Clamp(nIterations, 0, 15);
                context.command.Blit(src, tmp0);
                for (var i = 0; i < iters; i++) {
                    for (var pass = 1; pass < 3; pass++) {
                        tmp1.DiscardContents();
                        tmp0.filterMode = FilterMode.Bilinear;
                        context.command.Blit(tmp0, tmp1, gaussianMat, pass);
                        var tmpSwap = tmp0;
                        tmp0 = tmp1;
                        tmp1 = tmpSwap;
                    }
                }
                context.command.Blit(tmp0, dst);
                RenderTexture.ReleaseTemporary(tmp0);
                RenderTexture.ReleaseTemporary(tmp1);
            } else {
                context.command.Blit(src, dst);
            }
        }

        public static void Blur(Texture2D src, RenderTexture dst, int nIterations, Material gaussianMat) {
            var tmp0 = RenderTexture.GetTemporary(src.width, src.height, 0);
            var tmp1 = RenderTexture.GetTemporary(src.width, src.height, 0);
            var iters = Mathf.Clamp(nIterations, 0, 15);
            Graphics.Blit(src, tmp0);
            for (var i = 0; i < iters; i++) {
                for (var pass = 1; pass < 3; pass++) {
                    tmp1.DiscardContents();
                    tmp0.filterMode = FilterMode.Bilinear;
                    Graphics.Blit(tmp0, tmp1, gaussianMat, pass);
                    var tmpSwap = tmp0;
                    tmp0 = tmp1;
                    tmp1 = tmpSwap;
                }
            }
            Graphics.Blit(tmp0, dst);
            RenderTexture.ReleaseTemporary(tmp0);
            RenderTexture.ReleaseTemporary(tmp1);
        }

        public static void Blur(RenderTexture src, RenderTexture dst, int nIterations, Material gaussianMat) {
            var tmp0 = RenderTexture.GetTemporary(src.width, src.height, 0, src.format);
            var tmp1 = RenderTexture.GetTemporary(src.width, src.height, 0, src.format);
            var iters = Mathf.Clamp(nIterations, 0, 15);
            Graphics.Blit(src, tmp0);
            for (var i = 0; i < iters; i++) {
                for (var pass = 1; pass < 3; pass++) {
                    tmp1.DiscardContents();
                    tmp0.filterMode = FilterMode.Bilinear;
                    Graphics.Blit(tmp0, tmp1, gaussianMat, pass);
                    var tmpSwap = tmp0;
                    tmp0 = tmp1;
                    tmp1 = tmpSwap;
                }
            }
            Graphics.Blit(tmp0, dst);
            RenderTexture.ReleaseTemporary(tmp0);
            RenderTexture.ReleaseTemporary(tmp1);
        }

        public static Texture2D RenderTexToTex2D(RenderTexture input) {
            int width = input.width, height = input.height;
            Texture2D tex2D = new Texture2D(width, height, TextureFormat.ARGB32, false);
            RenderTexture temp = RenderTexture.active;
            RenderTexture.active = input;
            tex2D.ReadPixels(new Rect(0, 0, width, height), 0, 0);
            tex2D.Apply();
            RenderTexture.active = temp;

            return tex2D;
        }
    }
    
}