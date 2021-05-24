namespace EventHorizon.Blazor.DocumentMetadata.Collections
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using EventHorizon.Blazor.DocumentMetadata.Renderers;

    public class MetadataCache
        : ConcurrentDictionary<string, ISet<Renderer>>
    {
        public ISet<Renderer> GetPageRenderers(string pageName)
        {
            return GetOrAdd(
                GetFixedPageName(pageName),
                (_) => new MetadataRendererSet()
            );
        }

        public bool TryGetPageRenderers(
            string pageName,
            out ISet<Renderer> _
        ) => TryGetValue(GetFixedPageName(pageName), out _!);

        private static string GetFixedPageName(
            string route
        )
        {
            if (route.Length == 0 || route[0] != '/')
            {
                return route.Insert(0, "/");
            }
            else
            {
                return route;
            }
        }
    }
}
