using System.Text.Json.Serialization;

namespace dotnet_rpg2.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RpgClass
    {
        Knight = 1,
        Mage = 2,
        Elf = 3
    }
}