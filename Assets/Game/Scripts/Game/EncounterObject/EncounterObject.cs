using System;

using UnityEngine;

public sealed class EncounterObject : MonoBehaviour
{
    public Collider2D Collider2D;
    public SpriteRenderer SpriteRenderer;
    public EncounterEventData EncounterEventData;

    private bool _enable;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (!_enable)
        {
            return;
        }
        
        HandleCollided();
        _OnCollidedEventHandler?.Invoke(EncounterEventData);

        void HandleCollided()
        {
            switch (EncounterEventData.Type)
            {
                case EncounterType.Water:
				    AudioManager.Instance.PlaySFX(ESoundEffectType.GetProp);
                    Consume();
                    Debug.Log("Trigger Water " + EncounterEventData.EffectValue);
                    break;
                case EncounterType.Fertilizer:
				    AudioManager.Instance.PlaySFX(ESoundEffectType.GetProp);
                    Destroy(gameObject);
                    Debug.Log("Trigger Fertilizer " + EncounterEventData.EffectValue);
                    break;
                case EncounterType.Block:
				    AudioManager.Instance.PlaySFX(ESoundEffectType.HitObstacle);
                    Debug.Log("Trigger Block");
                    break;
                case EncounterType.Time:
				    AudioManager.Instance.PlaySFX(ESoundEffectType.GetProp);
                    Destroy(gameObject);
                    Debug.Log("Trigger Time");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        void Consume()
        {
            _enable = false;
            Color color = SpriteRenderer.color;
            color.a = 0.3f;
            SpriteRenderer.color = color;
        }
    }

    private event Action<EncounterEventData> _OnCollidedEventHandler;

    public void Init(Action<EncounterEventData> collidedEventHandler = null)
    {
        _OnCollidedEventHandler = collidedEventHandler;
        _enable = true;
    }
}