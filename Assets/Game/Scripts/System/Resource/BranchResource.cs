public sealed class BranchResource : Resource
{
    public BranchResource(uint value)
    {
        Value = value;
    }

    public uint Value { get; set; }
    public override bool IsExhausted => Value == 0;

    public override string ToString()
        => $"{nameof(BranchResource)}: {Value.ToString()}";
}