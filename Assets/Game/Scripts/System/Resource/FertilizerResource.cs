public sealed class FertilizerResource : Resource
{
    public FertilizerResource(uint value)
    {
        Value = value;
    }

    public uint Value { get; set; }

    public override bool IsExhausted => Value == 0;

    public override string ToString()
        => $"{nameof(FertilizerResource)}: {Value.ToString()}";
}