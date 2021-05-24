namespace EventHorizon.Blazor.DocumentMetadata.Api
{
    using System.Collections.Generic;
    using EventHorizon.Blazor.DocumentMetadata.Renderers;

    public interface IDocumentMetadataService
    {
        IEnumerable<Renderer> GetRenderers(string pageName);
    }
}
