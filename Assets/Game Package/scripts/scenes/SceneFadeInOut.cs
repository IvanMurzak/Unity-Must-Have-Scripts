using UnityEngine;
using System.Collections;
using UnityEngine.Events;

namespace Game.Package
{
    [RequireComponent(typeof(GUITexture))]
    public class SceneFadeInOut : MonoBehaviour
    {
        private const float MIN_DIF = 0.005f;

        public float fadeSpeed = 1.5f;          // Speed that the screen fades to and from black.

        public UnityEvent onFadeBegin = new UnityEvent(), onFadeComplete = new UnityEvent();

        private bool IsFading = false;      // Whether or not the scene is still fading in.
        private new GUITexture guiTexture;
        private Color targetColor = Color.clear;

        public Color CurrentColor { get { return guiTexture.color; } }

        protected void Awake()
        {
            guiTexture = GetComponent<GUITexture>();
            guiTexture.pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);
            guiTexture.texture = Texture2D.whiteTexture;
            guiTexture.enabled = IsVisible();
        }
        
        protected void Update()
        {
            if (IsFading)
            {
                guiTexture.color = Color.Lerp(guiTexture.color, targetColor, fadeSpeed * Time.deltaTime);
                Color dif = guiTexture.color - targetColor;
                if (Mathf.Abs(dif.a) < MIN_DIF && Mathf.Abs(dif.r) < MIN_DIF && Mathf.Abs(dif.g) < MIN_DIF && Mathf.Abs(dif.b) < MIN_DIF)
                {
                    IsFading = false;
                    if (onFadeComplete != null) onFadeComplete.Invoke();
                }
            }
            guiTexture.enabled = IsVisible();
        }

        public bool IsVisible()
        {
            return guiTexture.color.a >= MIN_DIF;
        }

        public void FadeToColor(Color color)
        {
            targetColor = color;
            IsFading = true;

            if (onFadeBegin != null) onFadeBegin.Invoke();
        }

        public void FadeToClear()
        {
            FadeToColor(Color.clear);
        }

        public void FadeToBlack()
        {
            FadeToColor(Color.black);
        }

        public void FadeToWhite()
        {
            FadeToColor(Color.white);
        }

        public void ForceSetColor(Color color)
        {
            guiTexture.color = color;
            targetColor = color;
        }
    }
}