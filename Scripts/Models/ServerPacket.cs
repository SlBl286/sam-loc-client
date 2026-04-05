
using System.Collections.Generic;
using System.Text.Json.Serialization;
using SL.Scripts.Enums;
using SL.Scripts.Models;

namespace SL.Scripts.Network;

public class ServerPacket
{
    public string type { get; set; }

}

public class RoomListPacket : ServerPacket
{
    [JsonPropertyName("rooms")]
    public List<RoomInfo> Rooms { get; set; }
}
public class PlayerListPacket : ServerPacket
{
    public List<PlayerInfo> players { get; set; }
}

public class StartGamePacket : ServerPacket
{
    public int match_id { get; set; }
}

public class JoinRoomPacket : ServerPacket
{
    public int room_id { get; set; }
    public int seat_index { get; set; }
}