using System;

public sealed class OutOfResourcesEventArgs : EventArgs
{
    internal OutOfResourcesEventArgs(Resource resource)
    {
        if (!resource.IsExhausted)
        {
            throw new ArgumentException($"Resource still had value {resource}", nameof(resource));
        }
    }
        
    public Resource Resource { get; }
}