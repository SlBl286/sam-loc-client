namespace SL.Scripts.Core;
public class StateContext
{
    // Player
    public int UserId;
    public string Username;
    public string Token;

    // Lobby
    public int SelectedRoomId;

    // Room
    public int RoomId;
    public int SeatIndex;

    // Game
    public int MatchId;
    public bool IsHost;

    // Network
    public bool Reconnecting;
}