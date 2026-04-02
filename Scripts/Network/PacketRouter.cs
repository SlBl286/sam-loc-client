using Godot;
using SL.Scripts.Core;
using SL.Scripts.Event;
using SL.Scripts.Models;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
namespace SL.Scripts.Network;

public partial class PacketRouter : Node
{
    public void Route(byte[] data)
    {
        string json = Encoding.UTF8.GetString(data);

        var packet = JsonSerializer.Deserialize<ServerPacket>(json);

        switch (packet.Type)
        {
            case "join_room":
                var joinData = JsonSerializer.Deserialize<JoinRoomData>(packet.Data.ToString());
                EventBus.Publish(new JoinRoomEvent
                {
                    RoomId = joinData.RoomId,
                    SeatIndex = joinData.SeatIndex
                });
                break;

            case "start_game":
                var startData = JsonSerializer.Deserialize<StartGameData>(packet.Data.ToString());
                EventBus.Publish(new StartGameEvent
                {
                    MatchId = startData.MatchId
                });
                break;
            case "room_list":
                    var roomListData = JsonSerializer.Deserialize<RoomListPacket>(json);
                EventBus.Publish(new RoomListEvent
                {
                    Rooms = roomListData.Rooms
                });
                break;
        }
    }
}

internal class RoomListPacket
{
    public List<RoomInfo> Rooms { get; set; }
}

internal class StartGameData
{
    public int MatchId { get; set; }
}

internal class JoinRoomData
{
    public int RoomId { get; set; }
    public int SeatIndex { get; set; }
}