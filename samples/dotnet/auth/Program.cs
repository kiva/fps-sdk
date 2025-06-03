using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Net;
using System.Net.Http;
using System.Text;

// ---------------------------------------------------------------------------
// Data
// ---------------------------------------------------------------------------

string domain = "auth.staging.kiva.org";                // URI only, no protocol, no path   
string clientId = System.Environment.GetEnvironmentVariable("client_id");
string clientSecret = System.Environment.GetEnvironmentVariable("client_secret");
string audience = System.Environment.GetEnvironmentVariable("audience");
string scope = System.Environment.GetEnvironmentVariable("scope");

Console.WriteLine($"getting autho token using client_id '{clientId}'");
Console.WriteLine($"\tsecret: '{clientSecret}'");
Console.WriteLine($"\taudience: '{audience}'");
Console.WriteLine($"\tscope: '{scope}'");


// ---------------------------------------------------------------------------
// create the http client
using HttpClient client = new();
client.DefaultRequestHeaders.Accept.Clear();

// ---------------------------------------------------------------------------
// set accepted type to json
client.DefaultRequestHeaders.Accept.Add(
    new MediaTypeWithQualityHeaderValue("application/json"));


// ---------------------------------------------------------------------------
// set the remaining paraemters
// details are documented in
// https://kivapartnerhelpcenter.zendesk.com/hc/en-us/articles/360051231131-API-authentication-client-credential-flow-
// expectation with this example is that these values were set as environment variables
// ---------------------------------------------------------------------------

var parameters = new Dictionary<string, string> 
{
    { "client_id", clientId },
    { "client_secret", clientSecret },
    { "audience", audience },
    { "grant_type", "client_credentials" },
    { "scope", scope }
};



// ---------------------------------------------------------------------------
// since the API expects the details to be posted as x-www-form-urlencoded
// we have to properly encode each value
var encodedContent = new FormUrlEncodedContent(parameters);

// ---------------------------------------------------------------------------
// make the call
var response = await client.PostAsync($"https://{domain}/oauth/token", encodedContent);


// ---------------------------------------------------------------------------
// process the response
if (response.StatusCode == HttpStatusCode.OK) 
{
    var json = await response.Content.ReadAsStringAsync();
    Console.WriteLine($"\r\nResults returned: \r\n {json}\r\n");
} 
else
{
    var result = await response.Content.ReadAsStringAsync();
    Console.WriteLine($"error: {response.StatusCode}: {result}");
}
