namespace EventHorizon.Blazor.DocumentMetadata.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using EventHorizon.Blazor.DocumentMetadata.Api;
    using EventHorizon.Blazor.DocumentMetadata.Collections;
    using EventHorizon.Blazor.DocumentMetadata.Renderers;
    using EventHorizon.Blazor.DocumentMetadata.Utilities;

    public class DocumentMetadataService
        : IDocumentMetadataService
    {
        private readonly MetadataCache _cache;

        public DocumentMetadataService(
            IDocumentMetadataCollection metadataCollection
        )
        {
            _cache = metadataCollection.Cache;
        }

        public IEnumerable<Renderer> GetRenderers(string pageName) =>
            GetRenderers(
                _cache,
                DocumentMetadataCollection.DefaultMetadataCacheKey,
                pageName
            ).Where(
                Enumerable.Any
            ).Aggregate(

                MergeRenderers
            );

        IEnumerable<Renderer> MergeRenderers(
                IEnumerable<Renderer> prev,
                IEnumerable<Renderer> next
        ) => prev.Except(next, MetadataRendererComparer.Default)
            .Concat(next);

        static IEnumerable<IEnumerable<Renderer>> GetRenderers(
            MetadataCache cache,
            params string[] keys
        ) => keys.Select(x => GetRenderers(cache, x));

        static IEnumerable<Renderer> GetRenderers(
            MetadataCache cache,
            string key
        ) => cache.TryGetPageRenderers(key, out ISet<Renderer> renderers) ? renderers : Enumerable.Empty<Renderer>();
    }
}
