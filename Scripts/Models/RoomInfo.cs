using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SL.Scripts.Models;

public class RoomInfo
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("players")]
    public List<long> Players { get; set; }
    [JsonPropertyName("max_players")]

    public int MaxPlayers { get; set; }
    [JsonPropertyName("status")]
    public string Status { get; set; }
}