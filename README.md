# EventHorizon SEO Document Metadata

This package helps to address the issue of setting the Metadata tags of the head, on initial page request and on page navigation. 

Since the Blazor framework does not provide any way to change a document's metadata at runtime (for document metadata: page title, meta tags, and other SEO-related data). This utility helps to address this issue.

This library is heavily influence by <a href="https://github.com/DevExpress/Blazor/tree/master/tools/DevExpress.Blazor.DocumentMetadata">https://github.com/DevExpress/Blazor/tree/master/tools/DevExpress.Blazor.DocumentMetadata</a>, this package is a modernized, configuration up front, and easy dynamic updating version.

## How to Used

1. Add the Package to our project: <a href="https://www.nuget.org/packages/EventHorizon.Blazor.DocumentMetadata/">EventHorizon.Blazor.DocumentMetadata</a>

    ~~~ bash
    dotnet add package EventHorizon.Blazor.DocumentMetadata
    ~~~

2. Add the `DocumentMetadata` component to a page header:

    ~~~ html
    <head>
        @(await Html.RenderComponentAsync<EhzMetadataRenderComponent>(RenderMode.ServerPrerendered))
        ...
    </head>
    ~~~

2. Call the `AddDocumentMetadata()` extension method to register default document metadata:

    ~~~ csharp
    using EventHorizon.Blazor.DocumentMetadata;
    namespace BlazorDemo.ServerSide
    {
        public class Startup
        {
            public void ConfigureServices(IServiceCollection services)
            {
                services.AddDocumentMetadata(async (IServiceProvider serviceProvider,
                    IDocumentMetadataRegistrator registrator) => {
                    registrator.Default()
                        .Title("Blazor UI Components");

                    registrator.AddPage("/not-found")
                        .Title("Not Found")
                        .Meta("title", "Not Found Title")
                        .OpenGraph("og:title", "Not Found Open Graph")
                        .OpenGraph("twitter:title", "Not Found Twitter");
                    ...
                });
            }
            ...
        }
    }
    ~~~
    
    Note, the `registrator.Default()` method returns a document metadata builder. Use this builder to register metadata defaults for all pages:
 
    ~~~ cs
    registrator.Default()
      .Base("~/")
      .Charset("utf-8")
      .Viewport("width=device-width, initial-scale=1.0")
      .OpenGraph("url", "https://example.com")
      ...
    ~~~

3. Call the `UseDocumentMetadata()` extension method to run setup on the internal Metadata Collection: 

    ~~~ csharp 
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseDocumentMetadata();
        ...
    }
    ~~~

4. Using the `DocumentMetadataSetup` service you can trigger a re-run of the logic of your initialize.

    ~~~ csharp 
    private async Task ReRunSetup(IServiceProvider services) {
        var setupService = services.GetRequiredService<DocumentMetadataSetup>();
        await setupService.Setup();
    }
    ~~~

5. You can also add Pages at runtime using the `IDocumentMetadataCollection` service, you can get this too through DI.

    ~~~ csharp
    private async Task ReRunSetup(IServiceProvider services) {
        var collection = services.GetRequiredService<IDocumentMetadataCollection>();
        collection.AddPage("/dynamic-page")
            .Title("Dynamic Page")
            .Meta("title", "Dynamic Page Title")
            .OpenGraph("og:title", "Dynamic Page Open Graph Title")
            .OpenGraph("twitter:title", "Dynamic Page Twitter Title");
    }
    ~~~

## Examples

An example of a Blazor application with the use of the SEO Metadata tool:

[canhorn/Blazor.Contentful.Blog.Starter](https://github.com/canhorn/Blazor.Contentful.Blog.Starter)
