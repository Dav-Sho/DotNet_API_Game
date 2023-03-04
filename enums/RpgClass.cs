using System.Text.Json.Serialization;

namespace game_rpg.enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RpgClass
    {
        Kinght = 1,
        Mage = 2,
        Cleric = 3
    }
}