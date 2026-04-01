using System.Text.Json;
using Godot;
namespace SL.Scripts;
public partial class Menu : Node2D
{
    WebSocketPeer ws;
    public override void _Ready()
    {
        ws = new();
        var err = ws.ConnectToUrl("ws://127.0.0.1:8080");
        GD.Print("Connect result: ", err);
    }
    public override void _Process(double delta)
    {
        ws.Poll();

        var state = ws.GetReadyState();

        if (state == WebSocketPeer.State.Open)
        {
           

            if (ws.GetAvailablePacketCount() > 0)
            {
                var pkt = ws.GetPacket();
                GD.Print("Received: ", pkt.GetStringFromUtf8());
            }
        }
        else if (state == WebSocketPeer.State.Closed)
        {
            GD.Print("Closed: ", ws.GetCloseCode());
        }
    }

    public  void _On_Btn_Pressed()
    {
            var packet = new { action = "ping", data = new { } };

        ws.SendText(JsonSerializer.Serialize(packet));

    }
    
}

