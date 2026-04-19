namespace Kubis1982.Atlassian.Jira.RestClient
{
    using Kubis1982.Atlassian.RestClient;
    using Microsoft.Kiota.Abstractions.Authentication;
    using Microsoft.Kiota.Http.HttpClientLibrary;

    public partial class JiraRestClient
    {
        public static JiraRestClient Create(string domain, IAuthenticationProvider authenticationProvider, HttpClient? httpClient = null)
        {
            HttpClientRequestAdapter httpClientRequestAdapter = HttpClientRequestAdapterFactory.Create($"https://{domain}.atlassian.net", authenticationProvider, httpClient);
            return new JiraRestClient(httpClientRequestAdapter);
        }
    }
}