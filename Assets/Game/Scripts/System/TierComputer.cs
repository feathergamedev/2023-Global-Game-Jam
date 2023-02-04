public static class TierComputer
{
    public enum Tier
    {
        SSR,
        SR
    }

    public static Tier Run(ResourceTracker tracker)
    {
        ulong totalResources = tracker.Time + tracker.Energy + tracker.Water + tracker.Fertilizer;
        return totalResources > 100 ? Tier.SSR : Tier.SR;
    }
}