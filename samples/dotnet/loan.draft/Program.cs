using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


string PartnerId = "";
string BearerToken = "";
string dataFileName = "data.json";
string AuthDomain = "auth-stage.kiva.org"; 
string PartnerDomain = "partner-api-stage.kiva.org";
ActivityList activities = null;                                 // loaded by GetActivities()
LocaleList locales = null;                                      // loaded by GetLocales()
ThemeList themes = null;                                        // loaded by GetThemes()
LocationList locations = null;                                  // loaded by GetLocations()

// ---------------------------------------------------------------------------
//   functions
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

async Task GetActivities()
{
    // https://partner-api-stage.dk1.kiva.org/v3/partner/{{PARTNER_ID}}/config/activities
    
    using HttpClient client = new();
    client.DefaultRequestHeaders.Accept.Clear();
    client.DefaultRequestHeaders.Accept.Add(
        new MediaTypeWithQualityHeaderValue("application/json"));
    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", BearerToken);
    
    var response = await client.GetAsync($"https://{PartnerDomain}/v3/partner/{PartnerId}/config/activities");
    
    if (response.StatusCode == HttpStatusCode.OK) 
    {
        using Stream responseBody = await response.Content.ReadAsStreamAsync();
        activities = await JsonSerializer.DeserializeAsync<ActivityList>(responseBody);
        Console.WriteLine($"Found {activities.Activities.Count} activities");
    } 
    else 
    {
        var result = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"error: {response.StatusCode}: {result}");
        throw new ApplicationException();
    }
}

// ---------------------------------------------------------------------------
async Task GetLocales()
{
    // https://partner-api-stage.dk1.kiva.org/v3/partner/{{PARTNER_ID}}/config/locales
    using HttpClient client = new();
    client.DefaultRequestHeaders.Accept.Clear();
    client.DefaultRequestHeaders.Accept.Add(
        new MediaTypeWithQualityHeaderValue("application/json"));
    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", BearerToken);
    
    var response = await client.GetAsync($"https://{PartnerDomain}/v3/partner/{PartnerId}/config/locales");
    
    if (response.StatusCode == HttpStatusCode.OK)
    {
        using Stream responseBody = await response.Content.ReadAsStreamAsync();
        locales = await JsonSerializer.DeserializeAsync<LocaleList>(responseBody);
        Console.WriteLine($"Found {locales.Locales.Count} locales");
    } 
    else 
    {
        var result = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"error: {response.StatusCode}: {result}");
        throw new ApplicationException();
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
        Console.WriteLine($"Found {themes.Themes.Count} themes");
    } 
    else 
    {
        var result = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"error: {response.StatusCode}: {result}");
        throw new ApplicationException();
    }
}

// ---------------------------------------------------------------------------
async Task GetLocations()
{
    // https://partner-api-stage.dk1.kiva.org/v3/partner/{{PARTNER_ID}}/config/locations
    using HttpClient client = new();
    client.DefaultRequestHeaders.Accept.Clear();
    client.DefaultRequestHeaders.Accept.Add(
        new MediaTypeWithQualityHeaderValue("application/json"));
    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", BearerToken);
    
    var response = await client.GetAsync($"https://{PartnerDomain}/v3/partner/{PartnerId}/config/locations");
    
    if (response.StatusCode == HttpStatusCode.OK)
    {
        // var json = await response.Content.ReadAsStringAsync();
        // Console.WriteLine($"results: {json}");
        
        using Stream responseBody = await response.Content.ReadAsStreamAsync();
        locations = await JsonSerializer.DeserializeAsync<LocationList>(responseBody);
        Console.WriteLine($"Found {locations.Locations.Count} locations");
    } 
    else 
    {
        var result = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"error: {response.StatusCode}: {result}");
        throw new ApplicationException();
    }
}

// ---------------------------------------------------------------------------
async Task PostLoanDraft()
{
    // create the loan
    LoanDraft loan = new();
    loan.ThemeTypeId = themes.Themes[0].ThemeTypeId;
    loan.Location = locations.Locations[1].FullName;
    loan.ActivityId = activities.Activities[0].ActivityId;
    loan.DescriptionLanguageId = locales.Locales[0].LanguageId;
    loan.Currency = locales.Locales[0].Currency;
    loan.InternalLoanId = "888888";
    loan.InternalClientId = "888888";
    loan.Description = "Test loan";
    loan.DisburseTime = "2023-12-01";
    loan.GroupName = "No Group";

    Entrep entrep = new Entrep()
    {
        Amount = 500,
        ClientId = "88",
        FirstName = "Test",
        Gender = "unknown",
        LastName = "Borrower",
        LoanId = "88"
    };
    
    loan.Entreps.Add(entrep);
    loan.NotPictured.Add(false);

    Schedule schedule = new Schedule()
    {
        Date = "2023-12-31",
        Interest = 50,
        Principal = 500
    };
    
    loan.Schedule.Add(schedule);

    if (File.Exists("pic.jpg"))
    {
        var imageContents = File.ReadAllBytes("pic.jpg");
        loan.ImageEncoded = $"data:image/png;base64,{Convert.ToBase64String(imageContents)}";
    }

    JsonSerializerOptions options = new JsonSerializerOptions
    {  
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
    };
    
    // convert to serialized data
    string loanJson = JsonSerializer.Serialize(loan, options);
    
    var content = new StringContent(loanJson, Encoding.UTF8, "application/json");
    
    // post it
    using HttpClient client = new();
    client.DefaultRequestHeaders.Accept.Clear();
    client.DefaultRequestHeaders.Accept.Add(
        new MediaTypeWithQualityHeaderValue("application/json"));
    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", BearerToken);

    var response = await client.PostAsync($"https://{PartnerDomain}/v3/partner/{PartnerId}/loan_draft",
        content);
    
    if (response.StatusCode == HttpStatusCode.OK)
    {
        var result = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Loan Posted: {result}");
    } 
    else 
    {
        var result = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"error: {response.StatusCode}: {result}");
        throw new ApplicationException();
    }
}



// ---------------------------------------------------------------------------
// Program execution
// ---------------------------------------------------------------------------


Console.WriteLine("Kiva Partner API example loans draft");
Console.WriteLine("\r\n\tsee  https://fps-sdk-portal.web.app/docs/overview/draftloans/ for more information");

Console.WriteLine("\r\n\r\n\t -- Step 1 Getting authorization token");
await GetAuthorizationToken();
Console.WriteLine($"\t Authorization Received for Partner: {PartnerId}");

Console.WriteLine("\r\n\t -- Step 2a Get Activities");
await GetActivities();

Console.WriteLine("\r\n\t -- Step 2b Get Locales");
await GetLocales();

Console.WriteLine("\r\n\t -- Step 2c Get Themes");
await GetThemes();

Console.WriteLine("\r\n\t -- Step 2d Get Locations");
await GetLocations();

Console.WriteLine("\r\n\t -- Step 3 Post the loan");
await PostLoanDraft();

Console.WriteLine("\t -- Done!");

