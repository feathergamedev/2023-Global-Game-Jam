public static class TierComputer
{
    public enum Tier
    {
        F,
        C,
        B,
        A,
        S
    }

    public static Tier Run(ResourceTracker tracker)
    {
        ulong totalResources = tracker.Time + tracker.Energy + tracker.Water + tracker.Fertilizer;
        return totalResources > 100 ? Tier.S : Tier.A;
    }
}