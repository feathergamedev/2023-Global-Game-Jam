public sealed class WaterResource : Resource
{
    public WaterResource(uint value)
    {
        Value = value;
    }

    public uint Value { get; set; }
    public override bool IsExhausted => Value == 0;

    public override string ToString()
        => $"{nameof(WaterResource)}: {Value.ToString()}";
}