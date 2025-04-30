using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;

string PartnerId = "";
string BearerToken = "";
string dataFileName = "data.json";
string AuthDomain = "auth-stage.dk1.kiva.org"; 
string PartnerDomain = "partner-api-stage.dk1.kiva.org";
ThemeList themes = null;                                        // loaded by GetThemes()

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

// ---------------------------------------------------------------------------
async Task GetThemes()
{
    // https://partner-api-stage.dk1.kiva.org/v3/partner/{{PARTNER_ID}}/config/themes
    using HttpClient client = new();
    client.DefaultRequestHeaders.Accept.Clear();
    client.DefaultRequestHeaders.Accept.Add(
        new MediaTypeWithQualityHeaderValue("application/json"));
    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", BearerToken);
    
    var response = await client.GetAsync($"https://{PartnerDomain}/v3/partner/{PartnerId}/config/themes");
    
    if (response.StatusCode == HttpStatusCode.OK)
    {
        using Stream responseBody = await response.Content.ReadAsStreamAsync();
        themes = await JsonSerializer.DeserializeAsync<ThemeList>(responseBody);
        
    } 
    else 
    {
        var result = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"error: {response.StatusCode}: {result}");
        throw new ApplicationException();
    }
}


Console.WriteLine("\r\nKiva Partner API example for retrieving loan themes.");
Console.WriteLine("Please note that credentials need to be set in the environment variables.");
Console.WriteLine("  see `https://fps-sdk-portal.web.app/docs/overview/draftloans/` for more information");

Console.WriteLine("\r\n\r\n\t -- Step 1 Getting authorization token");
await GetAuthorizationToken();
Console.WriteLine($"\t Authorization Received for Partner: {PartnerId}");

Console.WriteLine("\r\n\t -- Step 2 Get Themes");
await GetThemes();

if (0 < themes?.Themes.Count)
{
    Console.WriteLine($"\r\nFound {themes.Themes.Count} themes:");
    foreach (Theme t in themes.Themes) 
    {
        Console.WriteLine($"\t{t.ThemeTypeId} - {t.ThemeType}");
    }
}