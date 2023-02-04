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
    public event EventHandler<OutOfResourcesEventArgs> ResourceExhausted;

    public void IncreaseTime(ulong value)
    {
        if (value == 0)
        {
            throw new ArgumentOutOfRangeException(nameof(value), "Should not be negative");
        }

        ulong newValue = Time + value;
        if (newValue > _timeSetting.UpperBound)
        {
            throw new InvalidOperationException($"New time value {newValue} exceeds upper bound {_timeSetting.UpperBound}");
        }

        _currentTime = new TimeResource(newValue);
        ResourceValueChanged?.Invoke
            (this, new ResourceValueChangedEventArgs(_currentTime, ResourceValueChangedEventArgs.ChangeType.Increase));
    }

    public void DecreaseTime(ulong value)
    {
        if (value == 0)
        {
            throw new ArgumentOutOfRangeException(nameof(value), "Should not be negative");
        }

        ulong newValue = Time - value;
        if (newValue < _timeSetting.LowerBound)
        {
            throw new InvalidOperationException($"New time value {newValue} exceeds lower bound {_timeSetting.LowerBound}");
        }

        _currentTime = new TimeResource(newValue);
        ResourceValueChanged?.Invoke
            (this, new ResourceValueChangedEventArgs(_currentTime, ResourceValueChangedEventArgs.ChangeType.Decrease));
    }

    public void IncreaseEnergy(uint value)
    {
        if (value == 0)
        {
            throw new ArgumentOutOfRangeException(nameof(value), "Should not be negative");
        }

        uint newValue = Energy + value;
        if (newValue > _energySetting.UpperBound)
        {
            throw new InvalidOperationException($"New time value {newValue} exceeds upper bound {_energySetting.UpperBound}");
        }

        _currentEnergy = new EnergyResource(newValue);
        ResourceValueChanged?.Invoke
            (this, new ResourceValueChangedEventArgs(_currentEnergy, ResourceValueChangedEventArgs.ChangeType.Increase));
    }

    public void DecreaseEnergy(uint value)
    {
        if (value == 0)
        {
            throw new ArgumentOutOfRangeException(nameof(value), "Should not be negative");
        }

        uint newValue = Energy - value;
        if (newValue < _energySetting.LowerBound)
        {
            throw new InvalidOperationException($"New time value {newValue} exceeds lower bound {_energySetting.LowerBound}");
        }

        _currentEnergy = new EnergyResource(newValue);
        ResourceValueChanged?.Invoke
            (this, new ResourceValueChangedEventArgs(_currentEnergy, ResourceValueChangedEventArgs.ChangeType.Decrease));
    }

    public void IncreaseWater(uint value)
    {
        if (value == 0)
        {
            throw new ArgumentOutOfRangeException(nameof(value), "Should not be negative");
        }

        uint newValue = Water + value;
        if (newValue > _waterSetting.UpperBound)
        {
            throw new InvalidOperationException($"New time value {newValue} exceeds upper bound {_waterSetting.UpperBound}");
        }

        _currentWater = new WaterResource(newValue);
        ResourceValueChanged?.Invoke
            (this, new ResourceValueChangedEventArgs(_currentWater, ResourceValueChangedEventArgs.ChangeType.Increase));
    }

    public void DecreaseWater(uint value)
    {
        if (value == 0)
        {
            throw new ArgumentOutOfRangeException(nameof(value), "Should not be negative");
        }

        uint newValue = Water - value;
        if (newValue < _waterSetting.LowerBound)
        {
            throw new InvalidOperationException($"New time value {newValue} exceeds lower bound {_waterSetting.LowerBound}");
        }

        _currentWater = new WaterResource(newValue);
        ResourceValueChanged?.Invoke
            (this, new ResourceValueChangedEventArgs(_currentWater, ResourceValueChangedEventArgs.ChangeType.Decrease));
    }

    public void IncreaseFertilizer(uint value)
    {
        if (value == 0)
        {
            throw new ArgumentOutOfRangeException(nameof(value), "Should not be negative");
        }

        uint newValue = Fertilizer + value;
        if (newValue > _fertilizerSetting.UpperBound)
        {
            throw new InvalidOperationException($"New time value {newValue} exceeds upper bound {_fertilizerSetting.UpperBound}");
        }

        _currentFertilizer = new FertilizerResource(newValue);
        ResourceValueChanged?.Invoke
            (this, new ResourceValueChangedEventArgs(_currentFertilizer, ResourceValueChangedEventArgs.ChangeType.Increase));
    }

    public void DecreaseFertilizer(uint value)
    {
        if (value == 0)
        {
            throw new ArgumentOutOfRangeException(nameof(value), "Should not be negative");
        }

        uint newValue = Fertilizer - value;
        if (newValue < _fertilizerSetting.LowerBound)
        {
            throw new InvalidOperationException($"New time value {newValue} exceeds lower bound {_fertilizerSetting.LowerBound}");
        }

        _currentFertilizer = new FertilizerResource(newValue);
        ResourceValueChanged?.Invoke
            (this, new ResourceValueChangedEventArgs(_currentFertilizer, ResourceValueChangedEventArgs.ChangeType.Decrease));
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

        _currentBranch = new BranchResource(newValue);
        ResourceValueChanged?.Invoke
            (this, new ResourceValueChangedEventArgs(_currentBranch, ResourceValueChangedEventArgs.ChangeType.Decrease));
    }
}