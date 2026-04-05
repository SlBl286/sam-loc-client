
using System;

namespace SL.Scripts.Enums;
public enum ClientMessageType
{
    Connected,
    Disconnected,
    CreateRoom,
    JoinRoom,
    StartGame,

}

public static class ClientMessageTypeExtensions
{
    public static string ToStr(this ClientMessageType type)
    {
        return type.ToString(); // phải trùng với Rust
    }

    public static ClientMessageType? FromStr(string str)
    {
        if (Enum.TryParse<ClientMessageType>(str, out var result))
            return result;

        return null;
    }
}