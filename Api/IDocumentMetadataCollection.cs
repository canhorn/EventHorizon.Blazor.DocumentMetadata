namespace EventHorizon.Blazor.DocumentMetadata.Api
{
    using EventHorizon.Blazor.DocumentMetadata.Collections;

    public interface IDocumentMetadataCollection
    {
        MetadataCache Cache { get; }
        IDocumentMetadataBuilder AddDefault();
        IDocumentMetadataBuilder AddPage(string pageName);
    }
}
