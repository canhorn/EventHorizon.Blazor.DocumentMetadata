namespace EventHorizon.Blazor.DocumentMetadata.Utilities
{
    using System.Collections.Generic;
    using EventHorizon.Blazor.DocumentMetadata.Renderers;

    public class MetadataRendererComparer : IEqualityComparer<Renderer>
    {
        public static readonly IEqualityComparer<Renderer> Default = new MetadataRendererComparer();

        public bool Equals(Renderer x, Renderer y) => x.Equals(y);
        public int GetHashCode(Renderer obj) => obj.GetHashCode();

        MetadataRendererComparer() { }
    }
}
