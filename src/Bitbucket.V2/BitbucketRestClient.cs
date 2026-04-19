namespace Kubis1982.Atlassian.Bitbucket.RestClient
{
    using Kubis1982.Atlassian.RestClient;
    using Microsoft.Kiota.Abstractions.Authentication;
    using Microsoft.Kiota.Http.HttpClientLibrary;

    public partial class BitbucketRestClient
    {
        public static BitbucketRestClient Create(IAuthenticationProvider authenticationProvider, HttpClient? httpClient = null)
        {
            HttpClientRequestAdapter httpClientRequestAdapter = HttpClientRequestAdapterFactory.Create("https://api.bitbucket.org/2.0", authenticationProvider, httpClient);
            return new BitbucketRestClient(httpClientRequestAdapter);
        }
    }
}