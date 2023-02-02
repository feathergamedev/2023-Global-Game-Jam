using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Lapis.Utilities;

public class ScreenEffectManager : MonoSingleton<ScreenEffectManager>
{
    private Camera camera;

    private void Update()
    {

    }

    public void ShakeScreeen(float duration = 1, float strength = 0.1f, int vibrato = 15, float randomness = 45)
    {
        if (camera != Camera.main)
            camera = Camera.main;

        camera.DOShakePosition(duration, strength, vibrato, randomness, true);
    }
}
