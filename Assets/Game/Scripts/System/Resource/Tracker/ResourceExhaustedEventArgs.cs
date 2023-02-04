using System;

public sealed class ResourceExhaustedEventArgs : EventArgs
{
    internal ResourceExhaustedEventArgs(Resource resource)
    {
        if (!resource.IsExhausted)
        {
            throw new ArgumentException($"Resource still had value {resource}", nameof(resource));
        }
        Resource = resource;
    }
        
    public Resource Resource { get; }
}