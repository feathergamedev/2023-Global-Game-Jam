using System;

using UnityEngine;

public sealed class cwouyangMain : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        var tracker = new ResourceTracker(100, 100, 100);
        tracker.IncreaseWater(10);
        tracker.DecreaseWater(5);
        
        
        tracker.ResourceValueChanged += (sender, args) =>
        {
            switch (args.Value)
            {
                case WaterResource w:
                    // Update value to UI or somewhere
                    // args.Value;
                    if (args.Type == ResourceValueChangedEventArgs.ChangeType.Increase)
                    {
                       // do something 
                    }
                    else if (args.Type == ResourceValueChangedEventArgs.ChangeType.Decrease)
                    {
                       // do something 
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
    }
}