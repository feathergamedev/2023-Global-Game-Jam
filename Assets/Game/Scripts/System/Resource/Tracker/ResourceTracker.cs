using System;

public sealed class ResourceTracker
{
    public event EventHandler<ResourceValueChangedEventArgs> ResourceValueChanged;
    public event EventHandler<OutOfResourcesEventArgs> ResourceExhausted;

    private TimeResource _currentTime;
    private WaterResource _currentWater;
    private BranchResource _currentBranches;

    public ResourceTracker(ulong time, uint water, uint branches)
    {
        _currentTime = new TimeResource(time);
        _currentWater = new WaterResource(water);
        _currentBranches = new BranchResource(branches);
    }

    public void IncreaseWater(uint value)
    {
        if (value == 0)
        {
            throw new ArgumentOutOfRangeException(nameof(value), "Should not be negative");
        }

        ResourceValueChanged?.Invoke(this, new ResourceValueChangedEventArgs(new WaterResource(value), ResourceValueChangedEventArgs.ChangeType.Increase));
    }

    public void DecreaseWater(uint value)
    {
        if (value == 0)
        {
            throw new ArgumentOutOfRangeException(nameof(value), "Should not be negative");
        }

        ResourceValueChanged?.Invoke(this, new ResourceValueChangedEventArgs(new WaterResource(value), ResourceValueChangedEventArgs.ChangeType.Decrease));
    }

}