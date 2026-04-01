
namespace SL.Scripts.Network;
public class ServerPacket
{
    public string Type { get; set; }

    public int RoomId { get; set; }
    public int Seat { get; set; }
    public int MatchId { get; set; }
}