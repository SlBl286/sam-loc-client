using SL.Scripts.Enums;

namespace SL.Scripts.Event;

public class SocketDisconnectedEvent
{
    public int Code { get; set; }
    public string Reason { get; set; }
}

