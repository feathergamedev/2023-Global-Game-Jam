using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SoundEffectPair
{
    public ESoundEffectType Type;
    public AudioClip AudioClip;

    [Range(0f, 1f)]
    public float AudioVolume;
}

[CreateAssetMenu(fileName = "SoundEffectTable", menuName = "Lapis/Audio/SoundEffect Table")]
public class SoundEffectTable : ScriptableObject
{
    public List<SoundEffectPair> SoundEffectPairs;

    public AudioClip FindClip(ESoundEffectType type)
    {
        var pair = SoundEffectPairs.Find(x => x.Type == type);
        return pair.AudioClip;
    }
}
