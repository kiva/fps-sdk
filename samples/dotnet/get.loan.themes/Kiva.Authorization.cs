using System.Text.Json.Serialization;

record class KivaAuthorization(
        [property: JsonPropertyName("access_token")] string AuthToken,
        [property: JsonPropertyName("partnerId")] string PartnerId
    );
    
