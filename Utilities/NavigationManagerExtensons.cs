namespace EventHorizon.Blazor.DocumentMetadata.Utilities
{
    using Microsoft.AspNetCore.Components;

    public static class NavigationManagerExtensons
    {
        public static string GetCurrentPageName(
            this NavigationManager navigationManager
        ) => navigationManager.GetPageNameByLocation(navigationManager.Uri);

        public static string GetPageNameByLocation(
            this NavigationManager navigationManager,
            string location
        )
        {
            var uriFragment = navigationManager.ToAbsoluteUri(location).Fragment;
            if (!string.IsNullOrEmpty(uriFragment))
            {
                location = location.Replace(uriFragment, "");
            }
            return navigationManager.ToBaseRelativePath(location);
        }

        public static string ResolveUrl(
            this NavigationManager navigationManager,
            string url
        )
        {
            if (url.StartsWith("~/"))
            {
                var baseUrl = navigationManager.BaseUri;
                var absoluteUrl = baseUrl + url[2..];
                url = navigationManager.ToBaseRelativePath(absoluteUrl);
                url = navigationManager.ToAbsoluteUri(url).PathAndQuery;
            }
            return url;
        }
    }
}
