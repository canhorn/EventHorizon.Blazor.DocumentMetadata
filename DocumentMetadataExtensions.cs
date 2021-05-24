namespace EventHorizon.Blazor.DocumentMetadata
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EventHorizon.Blazor.DocumentMetadata.Api;
    using EventHorizon.Blazor.DocumentMetadata.Observers;
    using EventHorizon.Blazor.DocumentMetadata.Renderers;
    using EventHorizon.Blazor.DocumentMetadata.Services;
    using EventHorizon.Blazor.DocumentMetadata.Setup;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;

    public static class DocumentMetadataExtensions
    {
        public static IServiceCollection AddDocumentMetadata(
            this IServiceCollection services,
            Func<IServiceProvider, IDocumentMetadataCollection, Task> initialize
        )
        {
            services.AddMvc();
            services.AddSingleton(
                new DocumentMetadataSetup(initialize)
            );
            services.AddSingleton<IDocumentMetadataCollection, DocumentMetadataCollection>();

            services.AddScoped<IDocumentMetadataService, DocumentMetadataService>();
            services.AddScoped<IObservable<IEnumerable<Renderer>>, DocumentMetadataObserver>();
            return services;
        }

        public static IApplicationBuilder UseDocumentMetadata(
            this IApplicationBuilder app
        )
        {
            using var appScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            appScope.ServiceProvider.GetRequiredService<DocumentMetadataSetup>().Setup(
                appScope.ServiceProvider
            ).GetAwaiter().GetResult();
            return app;
        }
    }
}
