using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Game.Package
{
    [RequireComponent(typeof(Canvas))]
    public class SceneFadeInOut : MonoBehaviour
    {
        private const float MIN_DIF = 0.005f;

        public float fadeSpeed = 1.5f;          // Speed that the screen fades to and from black.

        public UnityEvent onFadeBegin = new UnityEvent(), onFadeComplete = new UnityEvent();

        private bool IsFading = false;      // Whether or not the scene is still fading in.
        private Canvas canvas;
        private Image image;
        private Color targetColor = Color.clear;

        public Color CurrentColor { get { return image.color; } }

        protected void Awake()
        {
            canvas = GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.sortingOrder = 32767;

            image = new GameObject("FadeSpace").AddComponent<Image>();
            image.sprite = Sprite.Create(Texture2D.whiteTexture, new Rect(0,0,1,1), new Vector2(0.5f, 0.5f));
            image.gameObject.transform.SetParent(transform);
            image.rectTransform.anchorMin = Vector2.zero;
            image.rectTransform.anchorMax = Vector2.one;
            image.rectTransform.pivot = new Vector2(0.5f, 0.5f);
            image.rectTransform.offsetMax = Vector2.zero;
            image.rectTransform.offsetMin = Vector2.zero;
            image.raycastTarget = false;
            image.enabled = IsVisible();
        }
        
        protected void Update()
        {
            if (IsFading)
            {
                image.color = Color.Lerp(image.color, targetColor, fadeSpeed * Time.deltaTime);
                Color dif = image.color - targetColor;
                if (Mathf.Abs(dif.a) < MIN_DIF && Mathf.Abs(dif.r) < MIN_DIF && Mathf.Abs(dif.g) < MIN_DIF && Mathf.Abs(dif.b) < MIN_DIF)
                {
                    IsFading = false;
                    if (onFadeComplete != null) onFadeComplete.Invoke();
                }
            }
            image.enabled = IsVisible();
        }

        public bool IsVisible()
        {
            return image.color.a >= MIN_DIF;
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
            image.color = color;
            targetColor = color;
        }
    }
}