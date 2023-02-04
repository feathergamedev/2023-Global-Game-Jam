using System;

public sealed class ResourceTracker
{
    private readonly ResourceSetting<uint> _branchSetting;
    private readonly ResourceSetting<uint> _energySetting;
    private readonly ResourceSetting<uint> _fertilizerSetting;

    private readonly ResourceSetting<ulong> _timeSetting;
    private readonly ResourceSetting<uint> _waterSetting;
    private BranchResource _currentBranch;
    private EnergyResource _currentEnergy;
    private FertilizerResource _currentFertilizer;

    private TimeResource _currentTime;
    private WaterResource _currentWater;

    public ResourceTracker
    (
        ResourceSetting<ulong> time,
        ResourceSetting<uint> energy,
        ResourceSetting<uint> water,
        ResourceSetting<uint> fertilizer,
        ResourceSetting<uint> branches
    )
    {
        _timeSetting = time;
        _energySetting = energy;
        _waterSetting = water;
        _fertilizerSetting = fertilizer;
        _branchSetting = branches;

        _currentTime = new TimeResource(_timeSetting.DefaultValue);
        _currentEnergy = new EnergyResource(_energySetting.DefaultValue);
        _currentWater = new WaterResource(_waterSetting.DefaultValue);
        _currentFertilizer = new FertilizerResource(_fertilizerSetting.DefaultValue);
        _currentBranch = new BranchResource(_branchSetting.DefaultValue);
    }

    public ulong Time => _currentTime.Value;
    public uint Energy => _currentEnergy.Value;
    public uint Fertilizer => _currentFertilizer.Value;
    public uint Water => _currentWater.Value;
    public uint Branch => _currentBranch.Value;
    public event EventHandler<ResourceValueChangedEventArgs> ResourceValueChanged;
    public event EventHandler<ResourceExhaustedEventArgs> ResourceExhausted;

    public void IncreaseTime(ulong value)
    {
        if (value == 0)
        {
            throw new ArgumentOutOfRangeException(nameof(value), "Should not be negative");
        }

        ulong newValue = Math.Max(Time + value, _timeSetting.UpperBound);
        var newTime = new TimeResource(newValue);
        ResourceValueChanged?.Invoke
            (this, new ResourceValueChangedEventArgs(_currentTime, newTime, ResourceValueChangedEventArgs.ChangeType.Increase));
        _currentTime = newTime;
    }

    public void DecreaseTime(ulong value)
    {
        if (value == 0)
        {
            throw new ArgumentOutOfRangeException(nameof(value), "Should not be negative");
        }

        ulong newValue = Math.Min(Time - value, _timeSetting.LowerBound);
        var newTime = new TimeResource(newValue);
        ResourceValueChanged?.Invoke
            (this, new ResourceValueChangedEventArgs(_currentTime, newTime, ResourceValueChangedEventArgs.ChangeType.Decrease));
        _currentTime = newTime;
        if (Time < _timeSetting.LowerBound)
        {
            ResourceExhausted?.Invoke(this, new ResourceExhaustedEventArgs(_currentTime));
        }
    }

    public void IncreaseEnergy(uint value)
    {
        if (value == 0)
        {
            throw new ArgumentOutOfRangeException(nameof(value), "Should not be negative");
        }

        uint newValue = Math.Max(Energy + value, _energySetting.UpperBound);
        var newEnergy = new EnergyResource(newValue);
        ResourceValueChanged?.Invoke
            (this, new ResourceValueChangedEventArgs(_currentEnergy, newEnergy, ResourceValueChangedEventArgs.ChangeType.Increase));
        _currentEnergy = newEnergy;
    }

    public void DecreaseEnergy(uint value)
    {
        if (value == 0)
        {
            throw new ArgumentOutOfRangeException(nameof(value), "Should not be negative");
        }

        uint newValue = Math.Min(Energy - value, _energySetting.LowerBound);
        var newEnergy = new EnergyResource(newValue);
        ResourceValueChanged?.Invoke
            (this, new ResourceValueChangedEventArgs(_currentEnergy, newEnergy, ResourceValueChangedEventArgs.ChangeType.Decrease));
        _currentEnergy = newEnergy;
        if (Energy < _energySetting.LowerBound)
        {
            ResourceExhausted?.Invoke(this, new ResourceExhaustedEventArgs(_currentEnergy));
        }
    }

    public void IncreaseWater(uint value)
    {
        if (value == 0)
        {
            throw new ArgumentOutOfRangeException(nameof(value), "Should not be negative");
        }

        uint newValue = Math.Max(Water + value, _waterSetting.UpperBound);
        var newWater = new WaterResource(newValue);
        ResourceValueChanged?.Invoke
            (this, new ResourceValueChangedEventArgs(_currentWater, newWater, ResourceValueChangedEventArgs.ChangeType.Increase));
        _currentWater = newWater;
    }

    public void DecreaseWater(uint value)
    {
        if (value == 0)
        {
            throw new ArgumentOutOfRangeException(nameof(value), "Should not be negative");
        }

        uint newValue = Math.Min(Water - value, _waterSetting.LowerBound);
        var newWater = new WaterResource(newValue);
        ResourceValueChanged?.Invoke
            (this, new ResourceValueChangedEventArgs(_currentWater, newWater, ResourceValueChangedEventArgs.ChangeType.Decrease));
        _currentWater = newWater;
        if (Water < _waterSetting.LowerBound)
        {
            ResourceExhausted?.Invoke(this, new ResourceExhaustedEventArgs(_currentWater));
        }
    }

    public void IncreaseFertilizer(uint value)
    {
        if (value == 0)
        {
            throw new ArgumentOutOfRangeException(nameof(value), "Should not be negative");
        }

        uint newValue = Math.Max(Fertilizer + value, _fertilizerSetting.UpperBound);
        var newFertilizer = new FertilizerResource(newValue);
        ResourceValueChanged?.Invoke
            (this, new ResourceValueChangedEventArgs(_currentFertilizer, newFertilizer, ResourceValueChangedEventArgs.ChangeType.Increase));
        _currentFertilizer = newFertilizer;
    }

    public void DecreaseFertilizer(uint value)
    {
        if (value == 0)
        {
            throw new ArgumentOutOfRangeException(nameof(value), "Should not be negative");
        }

        uint newValue = Math.Min(Fertilizer - value, _fertilizerSetting.LowerBound);
        var newFertilizer = new FertilizerResource(newValue);
        ResourceValueChanged?.Invoke
            (this, new ResourceValueChangedEventArgs(_currentFertilizer, newFertilizer, ResourceValueChangedEventArgs.ChangeType.Decrease));
        _currentFertilizer = newFertilizer;
        if (Fertilizer < _fertilizerSetting.LowerBound)
        {
            ResourceExhausted?.Invoke(this, new ResourceExhaustedEventArgs(_currentFertilizer));
        }
    }

    public void DecreaseBranch(uint value)
    {
        if (value == 0)
        {
            throw new ArgumentOutOfRangeException(nameof(value), "Should not be negative");
        }

        uint newValue = Branch - value;
        if (newValue < _branchSetting.LowerBound)
        {
            throw new InvalidOperationException($"New time value {newValue} exceeds lower bound {_branchSetting.LowerBound}");
        }

        var newBranch = new BranchResource(newValue);
        ResourceValueChanged?.Invoke
            (this, new ResourceValueChangedEventArgs(_currentBranch, newBranch, ResourceValueChangedEventArgs.ChangeType.Decrease));
        _currentBranch = newBranch;
        if (Branch < _branchSetting.LowerBound)
        {
            ResourceExhausted?.Invoke(this, new ResourceExhaustedEventArgs(_currentBranch));
        }
    }
}