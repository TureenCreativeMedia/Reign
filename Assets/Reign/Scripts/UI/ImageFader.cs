using NaughtyAttributes;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ImageFader
{
    public static IEnumerator FadeAlpha(Image TARGETGRAPHIC, float ALPHA, float DURATION)
    {
        Color Color_Image = TARGETGRAPHIC.color;
        float float_StartAlpha = TARGETGRAPHIC.color.a;
        float float_Elapsed = 0f;

        while (float_Elapsed < DURATION)
        {
            float_Elapsed += Time.unscaledDeltaTime;
            float float_Clamp = Mathf.Clamp01(float_Elapsed / DURATION);

            Color_Image.a = (Mathf.Lerp(float_StartAlpha, ALPHA, float_Clamp));
            TARGETGRAPHIC.color = Color_Image;

            yield return null;
        }

        Color_Image.a = ALPHA;
        TARGETGRAPHIC.color = Color_Image;
    }
}