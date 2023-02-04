using System;

using JetBrains.Annotations;

using UnityEngine;

using Object = UnityEngine.Object;

public class TerrainObject
{
    private readonly GameObject _prefab;
    private readonly Transform _root;

    public TerrainObject([NotNull] GameObject prefab, [NotNull] Transform root, Vector2 position)
    {
        if (prefab == null)
        {
            throw new ArgumentNullException(nameof(prefab));
        }

        if (root == null)
        {
            throw new ArgumentNullException(nameof(root));
        }

        if (position.x < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(position), "X position is invalid");
        }

        if (position.y < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(position), "Y Position is invalid");
        }

        _prefab = prefab;
        _root = root;
        Position = position;
    }

    public Vector2 Position { get; set; }
    public event Action<EncounterEventData> OnCollidedEvent;

    public void Instantiate()
    {
        GameObject obj = Object.Instantiate(_prefab, new Vector3(Position.x, Position.y), new Quaternion());
        obj.transform.parent = _root;

        var component = obj.GetComponent<EncounterObject>();
        component.Init((_, data) => { OnCollidedEvent?.Invoke(data); });
    }
}