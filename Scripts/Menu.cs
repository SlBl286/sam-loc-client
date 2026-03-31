using Godot;
using System;
using System.Text.Json;

public partial class Menu : Node2D
{
    public override void _Ready()
    {
        var ws = new WebSocketPeer();

        ws.ConnectToUrl("ws://127.0.0.1:8080");
        var packet = new { action = "ping", data = new { } };

        ws.SendText(JsonSerializer.Serialize(packet));
    }

}
