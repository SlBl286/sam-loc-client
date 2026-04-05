using Godot;
using SL.Scripts.Managers;
using System;
using System.Text;
namespace SL.Scripts.Network;

public partial class SocketClient : Node
{
    private WebSocketPeer _socket = new();

    public event Action<byte[]> PacketReceived;
    public event Action Connected;
    public event Action<int, string> Disconnected;
    public WebSocketPeer.State LastState { get; private set; } = WebSocketPeer.State.Closed;
    public override void _Ready()
    {

    }

    public override void _Process(double delta)
    {
        _socket.Poll();
        if (LastState != _socket.GetReadyState())
        {
            OnStateChanged(_socket.GetReadyState());
            LastState = _socket.GetReadyState();
        }
        if (_socket.GetReadyState() == WebSocketPeer.State.Open)
        {
            while (_socket.GetAvailablePacketCount() > 0)
            {
                var packet = _socket.GetPacket();
                PacketReceived?.Invoke(packet);
            }
        }

    }

    public void Connect(string url)
    {
        var err = _socket.ConnectToUrl(url);
        GD.Print("Connecting: ", err);
        if (err != Error.Ok)
        {
            GD.PrintErr("Failed to connect to WebSocket: ", err);
            return;
        }

    }

    public void Send(object data)
    {
        string json = System.Text.Json.JsonSerializer.Serialize(data);
        _socket.SendText(json);
    }
    public void Close()
    {
        _socket.Close();
    }
    private void OnStateChanged(WebSocketPeer.State state)
    {
        switch (state)
        {
            case WebSocketPeer.State.Connecting:
                GD.Print("WebSocket connecting...");
                break;
            case WebSocketPeer.State.Open:
                GD.Print("State:    WebSocket connected.");
                Connected?.Invoke();
                break;
            case WebSocketPeer.State.Closing:
                GD.Print("WebSocket closing...");
                break;
            case WebSocketPeer.State.Closed:
                Disconnected?.Invoke(_socket.GetCloseCode(), _socket.GetCloseReason());
                GD.Print("WebSocket closed.");
                break;
        }
    }
}