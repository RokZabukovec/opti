using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Flock.Models;

public class Command
{
    [JsonProperty(PropertyName = "command")]
    public string Name { get; set; }
    public string Description { get; set; }
}