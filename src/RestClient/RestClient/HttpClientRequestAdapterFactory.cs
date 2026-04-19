namespace Kubis1982.Atlassian.RestClient
{
    using Microsoft.Kiota.Abstractions.Authentication;
    using Microsoft.Kiota.Http.HttpClientLibrary;
    using Microsoft.Kiota.Serialization.Json;
    using System.Text.Json;

    public static class HttpClientRequestAdapterFactory
    {
        public static HttpClientRequestAdapter Create(string baseUrl, IAuthenticationProvider authenticationProvider, HttpClient? httpClient = null)
        {
            // Configure JSON options with custom DateTime converter
            var jsonOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
            jsonOptions.Converters.Add(new DateTimeOffsetConverter());
            jsonOptions.Converters.Add(new NullableDateTimeOffsetConverter());

            // Create serialization context
            var context = new KiotaJsonSerializationContext(jsonOptions);
            var writerFactory = new JsonSerializationWriterFactory(context);

            return new HttpClientRequestAdapter(authenticationProvider, serializationWriterFactory: writerFactory, httpClient: httpClient)
            {
                BaseUrl = baseUrl
            };
        }
    }
}
