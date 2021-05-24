namespace EventHorizon.Blazor.DocumentMetadata.Components
{
    using System;
    using System.Collections.Generic;
    using EventHorizon.Blazor.DocumentMetadata.Api;
    using EventHorizon.Blazor.DocumentMetadata.Renderers;
    using EventHorizon.Blazor.DocumentMetadata.Utilities;
    using Microsoft.AspNetCore.Components;
    using Microsoft.AspNetCore.Components.Rendering;

    public class EhzMetadataRenderComponent
        : ComponentBase,
        IObserver<IEnumerable<Renderer>>,
        IDisposable
    {
        private static readonly MarkupString TabInsideHeadElement = new(Environment.NewLine + new string(' ', 4));

        private Queue<Renderer> _renderers = new();
        private IDisposable? _unsubscribe;

        [Inject]
        public IObservable<IEnumerable<Renderer>> Metadata { get; set; } = null!;
        [Inject]
        public IDocumentMetadataService DocumentMetadataService { get; set; } = null!;

        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;

        [Parameter]
        public RenderFragment ChildContent { get; set; } = null!;

        protected override void OnInitialized()
        {
            base.OnInitialized();
            _unsubscribe = Metadata.Subscribe(this);

            _renderers = new Queue<Renderer>(DocumentMetadataService.GetRenderers(
                NavigationManager.GetCurrentPageName()
            ));
        }

        protected override void BuildRenderTree(
            RenderTreeBuilder builder
        )
        {
            int seq = 0;
            while (_renderers.TryDequeue(
                out Renderer renderer
            ))
            {
                builder.AddContent(
                    seq + 0,
                    TabInsideHeadElement
                );
                seq = renderer.Render(
                    builder,
                    seq + 1,
                    NavigationManager
                );
            }
            builder.OpenElement(
                seq + 2,
                "meta"
            );
            builder.AddAttribute(
                seq + 3,
                "data-ehzmetadatarenderer",
                true
            );
            builder.CloseElement();
        }

#pragma warning disable CA1816 // Dispose methods should call SuppressFinalize
        void IDisposable.Dispose()
#pragma warning restore CA1816 // Dispose methods should call SuppressFinalize
        {
            using (_unsubscribe) _unsubscribe = null;
            _renderers.Clear();
        }


        void IObserver<IEnumerable<Renderer>>.OnCompleted() { }
        void IObserver<IEnumerable<Renderer>>.OnError(Exception error) { }
        void IObserver<IEnumerable<Renderer>>.OnNext(IEnumerable<Renderer> value)
        {
            _renderers = new Queue<Renderer>(value);
            InvokeAsync(StateHasChanged);
        }
    }
}
