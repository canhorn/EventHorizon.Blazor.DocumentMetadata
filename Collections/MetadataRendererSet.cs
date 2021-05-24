namespace EventHorizon.Blazor.DocumentMetadata.Collections
{
    using System.Collections.Generic;
    using EventHorizon.Blazor.DocumentMetadata.Renderers;
    using EventHorizon.Blazor.DocumentMetadata.Utilities;

    public class MetadataRendererSet : HashSet<Renderer>
    {
        public MetadataRendererSet() 
            : base(MetadataRendererComparer.Default) { }
    }
}
