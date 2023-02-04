using System;

public sealed class TimeResource : Resource
{
    public TimeResource(ulong value)
    {
        if (value == 0)
        {
            throw new ArgumentOutOfRangeException(nameof(value), "value must be positive");
        }

        Value = value;
    }

    public ulong Value { get; set; }
    public override bool IsExhausted => Value == 0;

    public override string ToString()
        => $"{nameof(TimeResource)}: {Value.ToString()}";
}