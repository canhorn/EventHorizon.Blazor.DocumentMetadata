namespace EventHorizon.Blazor.DocumentMetadata.Renderers
{
    using Microsoft.AspNetCore.Components;
    using Microsoft.AspNetCore.Components.Rendering;

    public readonly partial struct Renderer
    {
        public static Renderer Link(string name, string href, string title, string type)
        {
            return new Renderer(
                RendererFlag.Link | RendererFlag.UniqueByName, 
                href, 
                name, 
                title: title, 
                type: type
            );
        }

        public int LinkRender(RenderTreeBuilder renderTreeBuilder, int seq, NavigationManager _)
        {
            renderTreeBuilder.OpenElement(seq + 0, "link");
            renderTreeBuilder.AddAttribute(seq + 1, "rel", _name);
            renderTreeBuilder.AddAttribute(seq + 2, "href", _mainAttributeValue);
            renderTreeBuilder.AddAttribute(seq + 3, "title", _title);
            renderTreeBuilder.AddAttribute(seq + 4, "type", _type);
            renderTreeBuilder.CloseElement();
            return seq + 5;
        }
    }
}
