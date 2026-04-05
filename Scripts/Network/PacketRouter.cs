using Godot;
using SL.Scripts.Core;
using SL.Scripts.Enums;
using SL.Scripts.Event;
using System.Text;
using System.Text.Json;
namespace SL.Scripts.Network;

public partial class PacketRouter : Node
{
    public void Route(byte[] data)
    {
        string json = Encoding.UTF8.GetString(data);

        var packet = JsonSerializer.Deserialize<ServerPacket>(json);

        switch (ServerMessageTypeExtensions.FromStr(packet.type))
        {
            case ServerMessageType.PlayerConnected:
                var connectedData = JsonSerializer.Deserialize<JoinRoomPacket>(json);
                EventBus.Publish(new JoinRoomEvent
                {
                    RoomId = connectedData.room_id,
                    SeatIndex = connectedData.seat_index
                });
                break;

            case ServerMessageType.GameStarted:
                var startData = JsonSerializer.Deserialize<StartGamePacket>(json);
                EventBus.Publish(new StartGameEvent
                {
                    MatchId = startData.match_id
                });
                break;
            case ServerMessageType.PlayerJoinedRoom:
                var joinData = JsonSerializer.Deserialize<JoinRoomPacket>(json);
                EventBus.Publish(new JoinRoomEvent
                {
                    RoomId = joinData.room_id,
                    SeatIndex = joinData.seat_index
                });
                break;
            case ServerMessageType.RoomList:
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var roomListData = JsonSerializer.Deserialize<RoomListPacket>(json, options);
                EventBus.Publish(new RoomListEvent
                {
                    Rooms = roomListData.Rooms
                });
                break;

            case ServerMessageType.PlayerList:
                var playerListData = JsonSerializer.Deserialize<PlayerListPacket>(json);
                EventBus.Publish(new PlayerListEvent
                {
                    Players = playerListData.players
                });
                break;
        }
    }
}

