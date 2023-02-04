using System;

using JetBrains.Annotations;

public sealed class ResourceValueChangedEventArgs : EventArgs
{
    public enum ChangeType
    {
        Increase,
        Decrease
    }

    internal ResourceValueChangedEventArgs([NotNull] Resource oldValue, [NotNull] Resource newValue, ChangeType changeType)
    {
        OldValue = oldValue ?? throw new ArgumentNullException(nameof(oldValue));
        NewValue = newValue ?? throw new ArgumentNullException(nameof(newValue));
        Type = changeType;

        if (OldValue.GetType() != NewValue.GetType())
        {
            throw new ArgumentException($"Type of {nameof(oldValue)} and {nameof(newValue)} should be the same");
        }
    }

    public Resource OldValue { get; }
    public Resource NewValue { get; }
    public ChangeType Type { get; }
}