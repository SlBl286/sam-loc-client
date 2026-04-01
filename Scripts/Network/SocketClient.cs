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
       _socket.ConnectToUrl("ws://127.0.0.1:8080");
       Connected?.Invoke();
    }

    public override void _Process(double delta)
    {
        if (_socket.GetReadyState() == WebSocketPeer.State.Open)
        {
            _socket.Poll();

            while (_socket.GetAvailablePacketCount() > 0)
            {
                var packet = _socket.GetPacket();
                PacketReceived?.Invoke(packet);
            }
        }

        if (_socket.GetReadyState() == WebSocketPeer.State.Closed)
        {
            Disconnected?.Invoke();
            return;
        }
    }

    public void Connect(string url)
    {
        var err = _socket.ConnectToUrl(url);
        GD.Print("Connecting: ", err);
    }

    public void Send(object data)
    {
        string json = System.Text.Json.JsonSerializer.Serialize(data);
        _socket.SendText(json);
    }
}