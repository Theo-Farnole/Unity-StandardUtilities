using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Utils.Managers
{
    public static class FadeSystem
    {
        #region Fields
        private static Image _fadeImage;
        #endregion

        #region Methods
        static void CreateFadeImage()
        {
            if (_fadeImage != null)
                return;

            GameObject init = new GameObject();
            init.name = "Fader";

            Canvas myCanvas = init.AddComponent<Canvas>();
            myCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
            init.AddComponent<CanvasGroup>();
            _fadeImage = init.AddComponent<Image>();

            GameObject.DontDestroyOnLoad(init);
        }

        #region Fade
        public static void Fade(FadeType fadeType, float duration)
        {
            Fade(fadeType, duration, Color.black);
        }

        public static void Fade(FadeType fadeType, float duration, Color color)
        {
            CreateFadeImage();

            _fadeImage.color = color;
            _fadeImage.Fade(fadeType, duration / 2);
        }
        #endregion

        #region Fade Blink
        public static void FadeBlink(float duration)
        {
            FadeBlink(duration, Color.black);
        }

        public static void FadeBlink(float duration, Color color)
        {
            Fade(FadeType.FadeIn, duration / 2, color);
            _fadeImage.ExecuteAfterTime(duration / 2, () => Fade(FadeType.FadeOut, duration / 2, color));
        }
        #endregion

        #region Fade Blink Scene
        public static void FadeBlinkScene(float duration)
        {
            FadeBlinkScene(duration, Color.black);
        }

        public static void FadeBlinkScene(float duration, Color color)
        {
            Fade(FadeType.FadeIn, duration / 2, color);

            SceneManager.OnSceneActivation += () =>
            {
                Fade(FadeType.FadeOut, duration / 2, color);
            };
        }
        #endregion
        #endregion
    }
}
