namespace EventHorizon.Blazor.DocumentMetadata.Renderers
{
    using System;
    using Microsoft.AspNetCore.Components;
    using Microsoft.AspNetCore.Components.Rendering;

    public readonly partial struct Renderer
    {

        public static Renderer Title(string value)
        {
            return new Renderer(RendererFlag.Title | RendererFlag.UniqueByType, value);
        }

        public static Renderer TitleFormat(string value)
        {
            return new Renderer(RendererFlag.TitleFormat | RendererFlag.UniqueByType, value);
        }

        public int TitleRender(RenderTreeBuilder renderTreeBuilder, int seq, NavigationManager _)
        {
            renderTreeBuilder.OpenElement(seq + 0, "title");
            renderTreeBuilder.AddContent(seq + 1, string.Format("{0}", _mainAttributeValue));
            renderTreeBuilder.CloseElement();
            return seq + 2;
        }
    }
}
