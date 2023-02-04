using System;

public sealed class BranchResource : Resource
{
    public BranchResource(uint value)
    {
        if (value == 0)
        {
            throw new ArgumentOutOfRangeException(nameof(value), "value must be positive");
        }

        Value = value;
    }

    public uint Value { get; set; }
    public override bool IsExhausted => Value == 0;

    public override string ToString()
        => $"{nameof(BranchResource)}: {Value.ToString()}";
}