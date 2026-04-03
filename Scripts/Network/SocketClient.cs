using Godot;
using System;
using System.Text;
namespace SL.Scripts.Network;
public partial class SocketClient : Node
{
    private WebSocketPeer _socket = new();

    public event Action<byte[]> PacketReceived;
    public event Action Connected;
    public event Action Disconnected;

    public override void _Ready()
    {

    }

    public override void _Process(double delta)
    {
        _socket.Poll();
        if (_socket.GetReadyState() == WebSocketPeer.State.Open)
        {
            
            while (_socket.GetAvailablePacketCount() > 0)
            {
                var packet = _socket.GetPacket();
                PacketReceived?.Invoke(packet);
            }
        }

        if (_socket.GetReadyState() == WebSocketPeer.State.Closed)
        {
        }
    }

    public void Connect(string url)
    {
        var err = _socket.ConnectToUrl(url);
        GD.Print("Connecting: ", err);
        Connected?.Invoke();
    }

    public void Send(object data)
    {
        string json = System.Text.Json.JsonSerializer.Serialize(data);
        _socket.SendText(json);
    }
}