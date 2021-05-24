namespace EventHorizon.Blazor.DocumentMetadata.Builder
{
    using System.Collections.Generic;
    using EventHorizon.Blazor.DocumentMetadata.Api;
    using EventHorizon.Blazor.DocumentMetadata.Renderers;

    public class DocumentMetadataBuilder : IDocumentMetadataBuilder
    {
        private readonly ISet<Renderer> _renderers;
        private readonly string _pageName;

        public override string ToString()
        {
            return _pageName;
        }

        public DocumentMetadataBuilder(
            string pageName,
            ISet<Renderer> renderers
        )
        {
            _renderers = renderers;
            _pageName = pageName;
        }

        private IDocumentMetadataBuilder UpdateWith(
            ISet<Renderer> source, 
            in Renderer update
        )
        {
            source.Remove(update);
            source.Add(update);
            return this;
        }

        public IDocumentMetadataBuilder Base(
            string url
        ) => UpdateWith(_renderers, Renderer.BaseHref(url));

        public IDocumentMetadataBuilder Title(
            string title
        ) => UpdateWith(_renderers, Renderer.Title(title));

        public IDocumentMetadataBuilder Link(
            string name,
            string href,
            string title,
            string type
        ) => UpdateWith(_renderers, Renderer.Link(name, href, title, type));

        public IDocumentMetadataBuilder TitleFormat(
            string format
        ) => UpdateWith(_renderers, Renderer.TitleFormat(format));

        public IDocumentMetadataBuilder StyleSheet(
            string name,
            string url
        ) => UpdateWith(_renderers, Renderer.Stylesheet(name, url));

        public IDocumentMetadataBuilder Script(
            string name,
            string url,
            bool async = false,
            bool defer = false
        ) => UpdateWith(_renderers, Renderer.Script(url, async, defer));

        public IDocumentMetadataBuilder Viewport(
            string value
        ) => UpdateWith(_renderers, Renderer.Viewport(value));

        public IDocumentMetadataBuilder Charset(
            string value
        ) => UpdateWith(_renderers, Renderer.Charset(value));

        public IDocumentMetadataBuilder Meta(
            string name,
            string content
        ) => UpdateWith(_renderers, Renderer.Meta(name, content));

        public IDocumentMetadataBuilder OpenGraph(
            string property,
            string content
        ) => UpdateWith(_renderers, Renderer.OpenGraph(property, content));
    }
}
