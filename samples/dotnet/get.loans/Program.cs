using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;


// ---------------------------------------------------------------------------
// Data
// ---------------------------------------------------------------------------


string PartnerId = "";
string BearerToken = "";
string AuthDomain = "auth.staging.kiva.org";                // URI only, no protocol, no path   
string PartnerDomain = "partnerapi.staging.kiva.org";       // same as above, no protocol, no path


// ---------------------------------------------------------------------------
//   functions


// ---------------------------------------------------------------------------
// Please see the auth sample for discussion of how the authorization is expected to work
async Task GetAuthorizationToken()
{
    using HttpClient client = new();
    client.DefaultRequestHeaders.Accept.Clear();
    client.DefaultRequestHeaders.Accept.Add(
        new MediaTypeWithQualityHeaderValue("application/json"));

    var parameters = new Dictionary<string, string> {
        { "client_id", System.Environment.GetEnvironmentVariable("client_id") },
        { "client_secret", System.Environment.GetEnvironmentVariable("client_secret") },
        { "audience", System.Environment.GetEnvironmentVariable("audience") },
        { "grant_type", "client_credentials" },
        { "scope", System.Environment.GetEnvironmentVariable("scope") }
    };

    var encodedContent = new FormUrlEncodedContent(parameters);

    var response = await client.PostAsync($"https://{AuthDomain}/oauth/token", encodedContent);

    if (response.StatusCode == HttpStatusCode.OK)
    {
        using Stream responseBody = await response.Content.ReadAsStreamAsync();
        var kivaAuthorization = await JsonSerializer.DeserializeAsync<KivaAuthorization>(responseBody);
        
        // TODO: parse out auth token and partner id
        PartnerId = kivaAuthorization.PartnerId;
        BearerToken = kivaAuthorization.AuthToken;

    } 
    else 
    {
        string result = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"error: {response.StatusCode}: {result}");
        System.Environment.Exit(1);
    }

}

// ---------------------------------------------------------------------------
async Task GetLoans()
{
    using HttpClient client = new();
    client.DefaultRequestHeaders.Accept.Clear();
    client.DefaultRequestHeaders.Accept.Add(
        new MediaTypeWithQualityHeaderValue("application/json"));
    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", BearerToken);

    var response = await client.GetAsync($"https://{PartnerDomain}/v3/partner/{PartnerId}/loans");
    
    var json = await response.Content.ReadAsStringAsync();
    
    if (response.StatusCode == HttpStatusCode.OK) 
    {
        Console.WriteLine($"\r\nGet Loans returned: \r\n {json}\r\n");
    } 
    else 
    {
        Console.WriteLine($"error: {response.StatusCode}: {json}");
    }
}


// ---------------------------------------------------------------------------
// Program execution
// ---------------------------------------------------------------------------

Console.WriteLine("Kiva Partner API for listing loans");
Console.WriteLine("    -- Step 1 Getting authorization token");
await GetAuthorizationToken();


Console.WriteLine("    -- Step 2 listing the loans for the partner");
await GetLoans();