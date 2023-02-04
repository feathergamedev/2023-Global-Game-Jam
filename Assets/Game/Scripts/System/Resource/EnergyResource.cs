public sealed class EnergyResource : Resource
{
    public EnergyResource(uint value)
    {
        Value = value;
    }

    public uint Value { get; set; }

    public override bool IsExhausted => Value == 0;

    public override string ToString()
        => $"{nameof(EnergyResource)}: {Value.ToString()}";
}