namespace EventHorizon.Blazor.DocumentMetadata.Renderers
{
    using EventHorizon.Blazor.DocumentMetadata.Utilities;
    using Microsoft.AspNetCore.Components;
    using Microsoft.AspNetCore.Components.Rendering;

    public readonly partial struct Renderer
    {
        public static Renderer BaseHref(string url)
        {
            return new Renderer(RendererFlag.BaseHref | RendererFlag.UniqueByType, url);
        }
        public int BaseHrefRender(RenderTreeBuilder renderTreeBuilder, int seq, NavigationManager navigationManager)
        {
            renderTreeBuilder.OpenElement(seq + 0, "base");
            renderTreeBuilder.AddAttribute(seq + 1, "href", navigationManager.ResolveUrl(_mainAttributeValue));
            renderTreeBuilder.CloseElement();
            return seq + 2;
        }
    }
}
