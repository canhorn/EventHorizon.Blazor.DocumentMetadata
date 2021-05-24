namespace EventHorizon.Blazor.DocumentMetadata.Setup
{
    using System;
    using System.Threading.Tasks;
    using EventHorizon.Blazor.DocumentMetadata.Api;
    using EventHorizon.Blazor.DocumentMetadata.Collections;
    using Microsoft.Extensions.DependencyInjection;

    public class DocumentMetadataSetup
    {
        private readonly Func<IServiceProvider, IDocumentMetadataCollection, Task> _initialize;

        private readonly MetadataCache _cache = new();

        internal DocumentMetadataSetup(
            Func<IServiceProvider, IDocumentMetadataCollection, Task> initialize
        )
        {
            _initialize = initialize;
        }

        internal MetadataCache Initialize()
            => _cache;

        public async Task Setup(
            IServiceProvider serviceProvider
        ) => await _initialize(
            serviceProvider,
            serviceProvider.GetRequiredService<IDocumentMetadataCollection>()
        );
    }
}
