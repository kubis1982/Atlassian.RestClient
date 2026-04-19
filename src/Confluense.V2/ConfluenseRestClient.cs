namespace Kubis1982.Atlassian.Confluense.RestClient
{
    using Kubis1982.Atlassian.RestClient;
    using Microsoft.Kiota.Abstractions.Authentication;
    using Microsoft.Kiota.Http.HttpClientLibrary;

    public partial class ConfluenseRestClient
    {
        public static ConfluenseRestClient Create(string domain, IAuthenticationProvider authenticationProvider, HttpClient? httpClient = null)
        {
            HttpClientRequestAdapter httpClientRequestAdapter = HttpClientRequestAdapterFactory.Create($"https://{domain}/wiki/api/v2", authenticationProvider, httpClient);
            return new ConfluenseRestClient(httpClientRequestAdapter);
        }
    }
}