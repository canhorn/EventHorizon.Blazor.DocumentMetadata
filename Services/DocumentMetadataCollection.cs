namespace EventHorizon.Blazor.DocumentMetadata.Services
{
    using System;
    using EventHorizon.Blazor.DocumentMetadata.Api;
    using EventHorizon.Blazor.DocumentMetadata.Builder;
    using EventHorizon.Blazor.DocumentMetadata.Collections;

    public class DocumentMetadataCollection
        : IDocumentMetadataCollection
    {

        public static readonly string DefaultMetadataCacheKey = $"default_{Guid.NewGuid()}";

        private readonly MetadataCache _cache = new();

        public MetadataCache Cache => _cache;

        public IDocumentMetadataBuilder AddDefault() => AddPage(DefaultMetadataCacheKey);
        public IDocumentMetadataBuilder AddPage(string pageName) => new DocumentMetadataBuilder(pageName, _cache.GetPageRenderers(pageName));
    }
}
