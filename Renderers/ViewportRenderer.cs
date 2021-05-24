namespace EventHorizon.Blazor.DocumentMetadata.Renderers
{
    public readonly partial struct Renderer
    {
        public static Renderer Viewport(string value)
        {
            return Meta("viewport", value);
        }
    }
}
