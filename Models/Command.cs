using Newtonsoft.Json;

namespace opti.Models;

public class Command
{
    [JsonProperty(PropertyName = "command")]
    public string Name { get; set; }
    public string Description { get; set; }
}