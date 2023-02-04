public sealed class TimeResource : Resource
{
    public TimeResource(ulong value)
    {
        Value = value;
    }

    public ulong Value { get; set; }
    public override bool IsExhausted => Value == 0;

    public override string ToString()
        => $"{nameof(TimeResource)}: {Value.ToString()}";
}