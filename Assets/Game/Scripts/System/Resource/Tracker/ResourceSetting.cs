using System;

using JetBrains.Annotations;

public readonly struct ResourceSetting<T> where T : IComparable<T>
{
    [NotNull] public T LowerBound { get; }
    [NotNull] public T UpperBound { get; }
    [NotNull] public T DefaultValue { get; }

    public ResourceSetting([NotNull] T lowerBound, [NotNull] T upperBound, [NotNull] T defaultValue)
    {
        if (lowerBound == null)
        {
            throw new ArgumentNullException(nameof(lowerBound));
        }

        if (upperBound == null)
        {
            throw new ArgumentNullException(nameof(upperBound));
        }

        if (defaultValue == null)
        {
            throw new ArgumentNullException(nameof(defaultValue));
        }

        if (lowerBound.CompareTo(upperBound) > 0)
        {
            throw new ArgumentException
                ($"{nameof(lowerBound)} {lowerBound} should be smaller or equal to {nameof(upperBound)} {upperBound}");
        }

        if (defaultValue.CompareTo(upperBound) > 0 || defaultValue.CompareTo(lowerBound) < 0)
        {
            throw new ArgumentException($"{nameof(defaultValue)} {defaultValue} should be in the range [{lowerBound}, {upperBound}]");
        }

        LowerBound = lowerBound;
        UpperBound = upperBound;
        DefaultValue = defaultValue;
    }
}