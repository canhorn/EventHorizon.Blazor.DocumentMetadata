namespace EventHorizon.Blazor.DocumentMetadata.Renderers
{
    using Microsoft.AspNetCore.Components;
    using Microsoft.AspNetCore.Components.Rendering;

    public readonly partial struct Renderer
    {

        public static Renderer OpenGraph(string name, string content)
        {
            return new Renderer(RendererFlag.OpenGraph | RendererFlag.UniqueByName, content, name);
        }

        public int OpenGraphRender(RenderTreeBuilder renderTreeBuilder, int seq, NavigationManager _)
        {
            renderTreeBuilder.OpenElement(seq + 0, "meta");
            renderTreeBuilder.AddAttribute(seq + 1, "property", _name);
            renderTreeBuilder.AddAttribute(seq + 2, "content", _mainAttributeValue);
            renderTreeBuilder.CloseElement();
            return seq + 3;
        }
    }
}
