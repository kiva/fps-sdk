using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


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

// ---------------------------------------------------------------------------
//    The expectation is the JSON is in the format like:
//    
//    {   
//         "repayments": 
//         [
//             {"loan_id":"loan_id_1","amount":480.00, "client_id":"client_id_1"},
//             {"loan_id":"loan_id_2","amount":500.00, "client_id":"client_id_2"}
//         ]
//    } 
//    
//    You can use the classes in the file Repayment.Classes.cs to generate this json as well
// ---------------------------------------------------------------------------
StringContent GetJsonData()
{
    string allText = File.ReadAllText(dataFileName);
    var content = new StringContent(allText, Encoding.UTF8, "application/json");
    return content;
}

// ---------------------------------------------------------------------------
//  Recreates the Json using the data classes provided
// ---------------------------------------------------------------------------
StringContent GetDataFromClass()
{
    RepaymentHeader loan = new();
    loan.Repayments.Add(new Repayment { LoanId = "loan_id_1", Amount = 480.00M, ClientId = "client_id_1" });
    
    string json = JsonSerializer.Serialize(loan);
    
    Console.WriteLine($"Loan object serizlied to json: {json}");
    
    var content = new StringContent(json, Encoding.UTF8, "application/json");
    return content;
}

// ---------------------------------------------------------------------------
async Task DoRepayment()
{
    // var content = GetJsonData();
    // uncomment the line below (and comment out the above line) to use the data classes instead of json data file
    var content = GetDataFromClass();
    using HttpClient client = new();
    client.DefaultRequestVersion = new Version(2, 0);
    client.DefaultRequestHeaders.Accept.Clear();
    client.DefaultRequestHeaders.Accept.Add(
        new MediaTypeWithQualityHeaderValue("application/json"));
    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", BearerToken);

    string url = $"https://{PartnerDomain}/v3/partner/{PartnerId}/repayments";
    Console.WriteLine($"using url: {url}");
    
    var response = await client.PostAsync(url, content);
    
    var json = await response.Content.ReadAsStringAsync();
    
    if (response.StatusCode == HttpStatusCode.OK) 
    {
        Console.WriteLine($"\r\nRepayments returned: \r\n {json}\r\n");
    } 
    else 
    {
        Console.WriteLine($"error: {response.StatusCode}: {json}");
    }
}


// ---------------------------------------------------------------------------
// Program execution
// ---------------------------------------------------------------------------

Console.WriteLine("Kiva Partner API for repayment on loans");
Console.WriteLine("\tExample will use 'data.json' in current directory unless the file name is passed in as command line argument");
Console.WriteLine("\teg: ");
Console.WriteLine("\t\tdotnet run --project repayments/repayments.csproj data.json\r\n");

if (args.Length > 0)
{
    foreach (string arg in args)
    {
        dataFileName = arg;
        break;
    }
}


Console.WriteLine("\t -- Step 1 Getting authorization token");
await GetAuthorizationToken();
Console.WriteLine($"\t Authorization Received for Partner: {PartnerId}\r\n");

Console.WriteLine("\t -- Step 2 make repayments");
await DoRepayment();

Console.WriteLine("\t -- Completed.  See above for more details.");




