    using System;

using UnityEngine;

public sealed class cwouyangMain : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        var tracker = new ResourceTracker(
            time: new ResourceSetting<ulong>(0, 100, 50), 
            energy: new ResourceSetting<uint>(0, 100, 50), 
            water: new ResourceSetting<uint>(0, 100, 50), 
            fertilizer: new ResourceSetting<uint>(0, 100, 50), 
            branches: new ResourceSetting<uint>(0, 100, 50));
        
        tracker.ResourceValueChanged += (sender, args) =>
        {
            switch (args.NewValue)
            {
                case WaterResource w:
                    // Update value to UI or somewhere
                    // args.Value;
                    if (args.Type == ResourceValueChangedEventArgs.ChangeType.Increase)
                    {
                        Debug.Log($"Water increase from {((WaterResource)(args.OldValue)).Value} to {w.Value}");
                    }
                    else if (args.Type == ResourceValueChangedEventArgs.ChangeType.Decrease)
                    {
                        Debug.Log($"Water decrease from {((WaterResource)(args.OldValue)).Value} to {w.Value}");
                    }
                    break;
                case TimeResource t:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        };
        
        tracker.ResourceExhausted += (sender, args) =>
        {
            switch (args.Resource)
            {
                case WaterResource w:
                    // do something 
                    break;
                case TimeResource t:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        };
        
        tracker.IncreaseWater(10);
        tracker.DecreaseWater(5);
    }
}