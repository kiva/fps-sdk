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
// string AuthDomain = "auth.k1.kiva.org";                     // for production, use this instead
string PartnerDomain = "partnerapi.staging.kiva.org";       // same as above, no protocol, no path
// string PartnerDomain = "partner-api.k1.kiva.org"; 

// for the loans endpoint, the status is required. See docs for valid values
string loanStaus = "payingBack"; 
int offset = 0;
int limit = 100; 

// Valid loan status values
string[] validLoanStatuses = new string[] {
    "deleted", "issue", "payingBack", "issue_revising", "issue_approving",
    "reviewed", "fundRaising", "refunded", "raised", "ended", "defaulted",
    "expired", "inactive_expired"
};

// ---------------------------------------------------------------------------
//   functions

void ShowHelpAndExit()
{
    Console.WriteLine("\r\nUsage: dotnet run [--loanStatus <status>] [--limit <number>]");
    Console.WriteLine("  --loanStatus: Optional. Valid values: " + string.Join(", ", validLoanStatuses));
    Console.WriteLine("  --limit: Optional. Integer between 0 and 3000.");
	Console.WriteLine("\r\n");
    Environment.Exit(1);
}

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

    Console.WriteLine($"Using client_id: {parameters["client_id"]}");
    Console.WriteLine($"Using audience: {parameters["audience"]}");
    Console.WriteLine($"Using scope: {parameters["scope"]}");
        
    
    var encodedContent = new FormUrlEncodedContent(parameters);

    var response = await client.PostAsync($"https://{AuthDomain}/oauth/token", encodedContent);

    if (response.StatusCode == HttpStatusCode.OK)
    {
        using Stream responseBody = await response.Content.ReadAsStreamAsync();
        var kivaAuthorization = await JsonSerializer.DeserializeAsync<KivaAuthorization>(responseBody);        
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

	// note:  filter parameters, see https://partnerapi.staging.kiva.org/swagger-ui/index.html#/partners/loansRoute
	Console.WriteLine($"Getting loans for partner {PartnerId} with status {loanStaus}, offset {offset}, limit {limit}");
    var response = await client.GetAsync($"https://{PartnerDomain}/v3/partner/{PartnerId}/loans?status={loanStaus}&offset={offset}&limit={limit}");
    
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

// Parse command line arguments
for (int i = 0; i < args.Length; i++)
{
    if (args[i] == "--loanStatus" && i + 1 < args.Length)
    {
        string statusArg = args[i + 1];
        if (Array.IndexOf(validLoanStatuses, statusArg) == -1)
        {
            Console.WriteLine($"Invalid loanStatus: {statusArg}");
            ShowHelpAndExit();
        }
        loanStaus = statusArg;
        i++;
    }
    else if (args[i] == "--limit" && i + 1 < args.Length)
    {
        if (!int.TryParse(args[i + 1], out int parsedLimit) || parsedLimit < 0 || parsedLimit > 3000)
        {
            Console.WriteLine($"Invalid limit: {args[i + 1]}");
            ShowHelpAndExit();
        }
        limit = parsedLimit;
        i++;
    }
    else
    {
        Console.WriteLine($"Unknown or incomplete argument: {args[i]}");
        ShowHelpAndExit();
    }
}

Console.WriteLine("Kiva Partner API for listing loans");
Console.WriteLine("    -- Step 1 Getting authorization token");
await GetAuthorizationToken();


Console.WriteLine("    -- Step 2 listing the loans for the partner");
await GetLoans();
