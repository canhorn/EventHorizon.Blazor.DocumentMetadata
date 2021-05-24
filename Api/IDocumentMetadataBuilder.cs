namespace EventHorizon.Blazor.DocumentMetadata.Api
{
    public interface IDocumentMetadataBuilder
    {
        IDocumentMetadataBuilder Base(string url);
        IDocumentMetadataBuilder Title(string title);
        IDocumentMetadataBuilder Link(string name, string href, string title = "", string type = "");
        IDocumentMetadataBuilder TitleFormat(string format);
        IDocumentMetadataBuilder Charset(string value);
        IDocumentMetadataBuilder Viewport(string value);
        IDocumentMetadataBuilder Meta(string name, string content);
        IDocumentMetadataBuilder OpenGraph(string property, string content);
        IDocumentMetadataBuilder StyleSheet(string name, string styleSheetUrl);
        IDocumentMetadataBuilder Script(string name, string scriptUrl, bool async = false, bool defer = false);
    }
}
