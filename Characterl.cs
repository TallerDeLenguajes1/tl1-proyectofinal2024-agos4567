using System.Text.Json.Serialization;

public class Character
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("gender")]
    public string Gender { get; set; }

    [JsonPropertyName("house")]
    public string House { get; set; }

    [JsonPropertyName("dateOfBirth")]
    public string DateOfBirth { get; set; }

    [JsonPropertyName("ancestry")]
    public string Ancestry { get; set; }

  
    [JsonPropertyName("image")]
    public string Image { get; set; }
}
