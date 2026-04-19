using Kubis1982.Atlassian.Jira.RestClient;
using Kubis1982.Atlassian.Jira.RestClient.Models;
using Kubis1982.Atlassian.RestClient;
using Microsoft.Kiota.Abstractions.Serialization;
using Microsoft.Kiota.Http.HttpClientLibrary;

// Configuration
var domain = ""; // without .atlassian.net
var email = ""; // Your email address associated with your Jira account
var apiToken = ""; // Generate an API token from https://id.atlassian.com/manage-profile/security/api-tokens

// Create authentication provider
var basicAuthProvider = new BasicAuthProvider(email, apiToken);

// Initialize client
var jiraClient = JiraRestClient.Create(domain, basicAuthProvider);

// Get all categories
var myself = await jiraClient.Rest.Api.Three.Myself.GetAsync();

Console.WriteLine(myself?.EmailAddress);
Console.WriteLine(myself?.DisplayName);

try
{
    var worklogRequest = new Worklog
    {
        TimeSpentSeconds = 60,
        Started = DateTimeOffset.UtcNow,
        Comment = new UntypedObject(new Dictionary<string, UntypedNode>
        {
            ["type"] = new UntypedString("doc"),
            ["version"] = new UntypedInteger(1),
            ["content"] = new UntypedArray(
                        [
                            new UntypedObject(new Dictionary<string, UntypedNode>
                            {
                                ["type"] = new UntypedString("paragraph"),
                                ["content"] = new UntypedArray(
                                [
                                    new UntypedObject(new Dictionary<string, UntypedNode>
                                    {
                                        ["type"] = new UntypedString("text"),
                                        ["text"] = new UntypedString("Test")
                                    })
                                ])
                            })
                        ])
        })
    };

    var worklog = await jiraClient.Rest.Api.Three.Issue["PIQ-2816"].Worklog.PostAsync(worklogRequest);
}
catch (Exception exc)
{
    Console.WriteLine(exc);
}
