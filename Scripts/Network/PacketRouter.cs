using Godot;
using SL.Scripts.Core;
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

        switch (packet.Type)
        {
            case "join_room":
                EventBus.Publish(new JoinRoomEvent
                {
                    RoomId = packet.RoomId,
                    SeatIndex = packet.Seat
                });
                break;

            case "start_game":
                EventBus.Publish(new StartGameEvent
                {
                    MatchId = packet.MatchId
                });
                break;
        }
    }
}