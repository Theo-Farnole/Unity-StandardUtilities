using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Utils
{
    [RequireComponent(typeof(Graphic))]
    public class FadeAfterSeconds : MonoBehaviour
    {
        [SerializeField] private float _timeBeforeFade = 1;
        [Space]
        [SerializeField] private FadeType _fadeType = FadeType.FadeIn;
        [SerializeField] private float _fadeDuration = 1;


        void Start()
        {
            Graphic graphic = GetComponent<Graphic>();

            switch (_fadeType)
            {
                case FadeType.FadeIn:
                    graphic.SetAlpha(0);
                    break;

                case FadeType.FadeOut:
                    graphic.SetAlpha(1);
                    break;
            }

            this.ExecuteAfterTime(_timeBeforeFade, () => graphic.Fade(_fadeType, _fadeDuration));
        }
    }
}
