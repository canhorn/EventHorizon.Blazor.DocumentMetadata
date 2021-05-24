namespace EventHorizon.Blazor.DocumentMetadata.Observers
{
    using System;
    using System.Collections.Generic;
    using EventHorizon.Blazor.DocumentMetadata.Api;
    using EventHorizon.Blazor.DocumentMetadata.Renderers;
    using EventHorizon.Blazor.DocumentMetadata.Utilities;
    using Microsoft.AspNetCore.Components;
    using Microsoft.AspNetCore.Components.Routing;

    public class DocumentMetadataObserver
        : IObservable<IEnumerable<Renderer>>,
        IDisposable
    {
        private readonly struct Unsubscribe : IDisposable
        {
            private readonly DocumentMetadataObserver _observable;
            private readonly Action<IEnumerable<Renderer>> _subscriber;
            internal Unsubscribe(DocumentMetadataObserver observable, Action<IEnumerable<Renderer>> subscriber)
            {
                _subscriber = subscriber;
                _observable = observable;
                _observable._listeners += subscriber;
            }
            public void Dispose()
            {
                if (_observable._listeners != null)
                {
#pragma warning disable CS8601 // Possible null reference assignment.
                    _observable._listeners -= _subscriber;
#pragma warning restore CS8601 // Possible null reference assignment.
                }
            }
        }

        private readonly IDocumentMetadataService _metadataService;
        private readonly NavigationManager _navigationManager;

        private Action<IEnumerable<Renderer>> _listeners = render => { };

        public DocumentMetadataObserver(
            IDocumentMetadataService metadataService,
            NavigationManager navigationManager
        )
        {
            _metadataService = metadataService;
            _navigationManager = navigationManager;
            _navigationManager.LocationChanged += OnLocationChanged;
        }


        public IDisposable Subscribe(IObserver<IEnumerable<Renderer>> observer) => SubscribeListener(observer.OnNext, observer.OnCompleted);

        IDisposable SubscribeListener(Action<IEnumerable<Renderer>> callback, Action onCompleted)
        {
            NotifyListener(callback);
            onCompleted();
            return new Unsubscribe(this, callback);
        }

        void OnLocationChanged(object? sender, LocationChangedEventArgs e) => OnServiceUpdateCompleted();
        void OnServiceUpdateCompleted()
        {
            if (_listeners != null)
                NotifyListener(_listeners);
        }
        void NotifyListener(Action<IEnumerable<Renderer>> callback)
        {
            Console.WriteLine("NotifiyListeners");
            callback(_metadataService.GetRenderers(_navigationManager.GetCurrentPageName()));
        }

#pragma warning disable CA1816 // Dispose methods should call SuppressFinalize
        void IDisposable.Dispose()
#pragma warning restore CA1816 // Dispose methods should call SuppressFinalize
        {
            _navigationManager.LocationChanged -= OnLocationChanged;
        }
    }
}
