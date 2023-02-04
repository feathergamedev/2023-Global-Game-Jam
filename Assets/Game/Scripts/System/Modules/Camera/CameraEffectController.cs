using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lapis.Utilities;

public class CameraEffectController : MonoSingleton<CameraEffectController>
{
    [SerializeField] private Color fadeInColor;
    [SerializeField] private Color fadeOutColor;

    private Texture2D texture2D;
    private GUIStyle guiStyle;

    private static float alpha = 0;

    private bool isFadeIn = false;
    private bool isFadeOut = false;
    private float duration = 1f;

    private void Awake()
    {
        texture2D = new Texture2D(1, 1);
        texture2D.SetPixel(0, 0, fadeInColor);
        texture2D.Apply();

        guiStyle = new GUIStyle();
        guiStyle.normal.background = texture2D;

        StartFadeIn();
    }

    public void OnGUI()
    {
        GUI.Label(new Rect(0, 0, Screen.width, Screen.height), texture2D, guiStyle);
    }

    public void Update()
    {
        if (isFadeIn)
        {
            if (alpha > 0)
                alpha -= UnityEngine.Time.deltaTime / duration;
            else
                isFadeIn = false;

            texture2D.SetPixel(0, 0, fadeInColor + new Color(0, 0, 0, alpha));
        }
        else if (isFadeOut)
        {
            if (alpha < 1)
                alpha += UnityEngine.Time.deltaTime / duration;
            else
                isFadeOut = false;

            texture2D.SetPixel(0, 0, fadeOutColor + new Color(0, 0, 0, alpha));
        }

        texture2D.Apply();
    }

    public void SetFadeInColor(Color color)
    {
        fadeInColor = color;
    }

    public void SetFadeOutColor(Color color)
    {
        fadeOutColor = color;
    }

    public void StartFadeIn(float duration = 1f)
    {
        isFadeIn = true;
        this.duration = duration;
        alpha = 1;

        texture2D.SetPixel(0, 0, fadeInColor);
        texture2D.Apply();
    }

    public void StartFadeOut(float duration = 1f)
    {
        isFadeOut = true;
        this.duration = duration;
        alpha = 0;

        texture2D.SetPixel(0, 0, fadeOutColor);
        texture2D.Apply();
    }
}
