using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;

/*
   Data
*/

string PartnerId = "";
string BearerToken = "";
string dataFileName = "data.json";
string AuthDomain = "auth-stage.kiva.org"; 
string PartnerDomain = "partner-api-stage.kiva.org";


// ---------------------------------------------------------------------------
// functions
// ---------------------------------------------------------------------------

// ---------------------------------------------------------------------------
// Please see the auth sample project for discussion how authorization works
async Task GetAuthorizationToken()
{
    using HttpClient client = new();
    client.DefaultRequestHeaders.Accept.Clear();
    client.DefaultRequestHeaders.Accept.Add(
        new MediaTypeWithQualityHeaderValue("application/json"));

    var parameters = new Dictionary<string, string> 
    {
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

async Task PostJournal()
{

    using HttpClient client = new();
    client.DefaultRequestHeaders.Accept.Clear();
    client.DefaultRequestHeaders.Accept.Add(
        new MediaTypeWithQualityHeaderValue("application/json"));
    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", BearerToken);

    Journal journal = new()
    {
        Body = "This is the journal message sent to the borrower",
        ImageUrl = "https://www.kiva.org/img/w800/1615031.jpg",
        InternalClientId = "123456",
        InternalLoanId = "123456",
        Subject = "This is the subject of the journal"
    };
    List<Journal> journals = new();
    journals.Add(journal);

    var response = await client.PostAsJsonAsync($"https://{PartnerDomain}/v3/partners/{PartnerId}/journals", journals);

    if (response.StatusCode == HttpStatusCode.Created)
    {
        Console.WriteLine("Journal posted");
    } 
    else 
    {
        string result = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"error: {response.StatusCode}: {result}");
        System.Environment.Exit(1);
    }
 
}


// ---------------------------------------------------------------------------
// Program execution
// ---------------------------------------------------------------------------

Console.WriteLine("Kiva Partner API example for journal");

Console.WriteLine("\t -- Step 1 Getting authorization token");
await GetAuthorizationToken();

Console.WriteLine("\t -- Step 2 Posting journal");
await PostJournal();

Console.WriteLine("\t -- Step 3 Done");