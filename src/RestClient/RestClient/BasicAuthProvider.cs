namespace Kubis1982.Atlassian.RestClient
{
    using Microsoft.Kiota.Abstractions;
    using Microsoft.Kiota.Abstractions.Authentication;
    using System.Text;

    public class BasicAuthProvider(string username, string password) : IAuthenticationProvider
    {
        private readonly string _username = username ?? throw new ArgumentNullException(nameof(username));
        private readonly string _password = password ?? throw new ArgumentNullException(nameof(password));

        public Task AuthenticateRequestAsync(RequestInformation request, Dictionary<string, object>? additionalAuthenticationContext = null, CancellationToken cancellationToken = default)
        {
            var credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_username}:{_password}"));
            request.Headers.Add("Authorization", $"Basic {credentials}");
            return Task.CompletedTask;
        }
    }
}
