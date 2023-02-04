using System;

public sealed class ResourceValueChangedEventArgs : EventArgs
{

    public enum ChangeType
    {
        Increase,
        Decrease
    }
    internal ResourceValueChangedEventArgs(Resource resource, ChangeType changeType)
    {
        Value = resource;
        Type = changeType;
    }

    public Resource Value { get; }
    public ChangeType Type { get; }
}