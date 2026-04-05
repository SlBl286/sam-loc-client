
using System;

namespace SL.Scripts.Enums;
public enum ServerMessageType
{
    PlayerConnected,
    RoomList,
    PlayerList,
    Error,
    PlayerJoinedRoom,
    GameStarted,

}

public static class ServerMessageTypeExtensions
{
    public static string ToStr(this ServerMessageType type)
    {
        return type.ToString(); // phải trùng với Rust
    }

    public static ServerMessageType? FromStr(string str)
    {
        if (Enum.TryParse<ServerMessageType>(str, out var result))
            return result;

        return null;
    }
}