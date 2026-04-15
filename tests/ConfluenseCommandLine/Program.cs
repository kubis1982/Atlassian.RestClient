using Kubis1982.Atlassian.Confluense.RestClient;
using Microsoft.Kiota.Abstractions;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;
using System.Text;

// Configuration
var domain = ""; // without .atlassian.net
var email = ""; // Your email address associated with your Confluense account
var apiToken = ""; // Generate an API token from https://id.atlassian.com/manage-profile/security/api-tokens

var basicAuthProvider = new BasicAuthProvider(email, apiToken);
var httpClient = new HttpClient()
{
    Timeout = TimeSpan.FromSeconds(30)
};

// Create request adapter
var requestAdapter = new HttpClientRequestAdapter(basicAuthProvider, httpClient: httpClient)
{
    BaseUrl = $"https://{domain}.atlassian.net/wiki/api/v2"
};

// Initialize client
var confluenceClient = new ConfluenseRestClient(requestAdapter);

// Get pages
var pages = await confluenceClient.Pages.GetAsPagesGetResponseAsync(n => n.QueryParameters.Limit = 5);

Console.WriteLine(pages?.Results?.Count);

file class BasicAuthProvider(string username, string appPassword) : IAuthenticationProvider
{
    private readonly string _username = username ?? throw new ArgumentNullException(nameof(username));
    private readonly string _appPassword = appPassword ?? throw new ArgumentNullException(nameof(appPassword));

    public Task AuthenticateRequestAsync(RequestInformation request, Dictionary<string, object>? additionalAuthenticationContext = null, CancellationToken cancellationToken = default)
    {
        var credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_username}:{_appPassword}"));
        request.Headers.Add("Authorization", $"Basic {credentials}");
        return Task.CompletedTask;
    }
}
