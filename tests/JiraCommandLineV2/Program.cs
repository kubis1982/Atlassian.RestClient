using Kubis1982.Atlassian.Jira.RestClient.V2;
using Kubis1982.Atlassian.Jira.RestClient.V2.Models;
using Kubis1982.Atlassian.RestClient;

// Configuration
var domain = ""; // without .atlassian.net
var email = ""; // Your email address associated with your Jira account
var apiToken = ""; // Generate an API token from https://id.atlassian.com/manage-profile/security/api-tokens

// Create authentication provider
var basicAuthProvider = new BasicAuthProvider(email, apiToken);

// Initialize client
var jiraClient = JiraRestClient.Create(domain, basicAuthProvider);

// Get all categories
var myself = await jiraClient.Rest.Api.Two.Myself.GetAsync();

Console.WriteLine(myself?.EmailAddress);
Console.WriteLine(myself?.DisplayName);

try
{
    var worklogRequest = new Worklog
    {
        TimeSpentSeconds = 120,
        Started = DateTimeOffset.UtcNow,
        Comment = "Test"
    };

    var worklog = await jiraClient.Rest.Api.Two.Issue["PIQ-2816"].Worklog.PostAsync(worklogRequest);
}
catch (Exception exc)
{
    Console.WriteLine(exc);
}
