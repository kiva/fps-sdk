
using System.Text.Json.Serialization;

public class ActivityItem
{
    [JsonPropertyName("activity")]
    public string Activity { get; set; } = string.Empty;

    [JsonPropertyName("activity_id")]
    public int ActivityId { get; set; } = -1;
}

public class ActivityList
{
    [JsonPropertyName("as_of_date_time")]
    public DateTime AsOfDateTime { get; set; } = DateTime.Now;

    [JsonPropertyName("activities")]
    public List<ActivityItem> Activities { get; set; } = new ();
}

public class Locale
{
    [JsonPropertyName("currency")]
    public string Currency { get; set; }

    [JsonPropertyName("language_code")]
    public string LanguageCode { get; set; }

    [JsonPropertyName("language_name")]
    public string LanguageName { get; set; }

    [JsonPropertyName("language_id")]
    public int LanguageId { get; set; }
}

public class LocaleList
{
    [JsonPropertyName("as_of_date_time")]
    public DateTime AsOfDateTime { get; set; } = DateTime.Now;

    [JsonPropertyName("locales")]
    public List<Locale> Locales { get; set; } = new ();
}

public class ThemeList
{
    [JsonPropertyName("as_of_date_time")]
    public DateTime AsOfDateTime { get; set; } = DateTime.Now;

    [JsonPropertyName("themes")]
    public List<Theme> Themes { get; set; } = new ();
}

public class Theme
{
    [JsonPropertyName("theme_type_id")]
    public int ThemeTypeId { get; set; }

    [JsonPropertyName("theme_type")]
    public string ThemeType { get; set; }
}

public class Location
{
    [JsonPropertyName("country")]
    public string Country { get; set; }

    [JsonPropertyName("location")]
    public string LocationName { get; set; }

    [JsonPropertyName("full_name")]
    public string FullName { get; set; }
}

public class LocationList
{
    [JsonPropertyName("as_of_date_time")]
    public DateTime AsOfDateTime { get; set; } = DateTime.Now;

    [JsonPropertyName("locations")]
    public List<Location> Locations { get; set; } = new ();
}

public class Entrep
{
    [JsonPropertyName("amount")]
    public int Amount { get; set; }

    [JsonPropertyName("client_id")]
    public string ClientId { get; set; }

    [JsonPropertyName("first_name")]
    public string FirstName { get; set; }

    [JsonPropertyName("gender")]
    public string Gender { get; set; }

    [JsonPropertyName("last_name")]
    public string LastName { get; set; }

    [JsonPropertyName("loan_id")]
    public string LoanId { get; set; }
}

public class LoanDraft
{
    [JsonPropertyName("activity_id")]
    public int ActivityId { get; set; }

    [JsonPropertyName("client_waiver_signed")]
    public bool ClientWaiverSigned { get; set; }

    [JsonPropertyName("currency")]
    public string Currency { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("description_language_id")]
    public int DescriptionLanguageId { get; set; }

    [JsonPropertyName("disburse_time")]
    public string DisburseTime { get; set; }

    [JsonPropertyName("entreps")]
    public List<Entrep> Entreps { get; set; } = new ();

    [JsonPropertyName("group_name")]
    public string GroupName { get; set; }

    [JsonPropertyName("image_encoded")]
    public string ImageEncoded { get; set; }

    [JsonPropertyName("internal_client_id")]
    public string InternalClientId { get; set; }

    [JsonPropertyName("internal_loan_id")]
    public string InternalLoanId { get; set; }

    [JsonPropertyName("loanuse")]
    public string Loanuse { get; set; }

    [JsonPropertyName("location")]
    public string Location { get; set; }

    [JsonPropertyName("not_pictured")]
    public List<bool> NotPictured { get; set; } = new ();

    [JsonPropertyName("schedule")]
    public List<Schedule> Schedule { get; set; } = new ();

    [JsonPropertyName("theme_type_id")]
    public int ThemeTypeId { get; set; }
}

public class Schedule
{
    [JsonPropertyName("date")]
    public string Date { get; set; }

    [JsonPropertyName("interest")]
    public int Interest { get; set; }

    [JsonPropertyName("principal")]
    public int Principal { get; set; }
}



