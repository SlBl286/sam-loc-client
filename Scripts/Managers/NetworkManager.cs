using Godot;
using SL.Scripts.Core;
using SL.Scripts.Enums;
using SL.Scripts.Event;
using SL.Scripts.Network;
namespace SL.Scripts.Managers;

public partial class NetworkManager : Node
{
    public static NetworkManager Instance;

    public ApiClient Api { get; private set; }
    public SocketClient Socket { get; private set; }
    public PacketRouter Router { get; private set; }
    public NetworkState State { get; private set; } = NetworkState.Disconnected;

    public override void _Ready()
    {
        Instance = this;

        Api = new ApiClient();
        Socket = new SocketClient();
        Router = new PacketRouter();

        AddChild(Api);
        AddChild(Socket);
        AddChild(Router);

        Socket.PacketReceived += Router.Route;
        Socket.Connected += OnConnected;
        Socket.Disconnected += OnDisconnected;
    }
    public void SetState(NetworkState state)
    {
        State = state;

        EventBus.Publish(new NetworkStateChangedEvent
        {
            State = state
        });
    }
    void OnConnected()
    {
        SetState(NetworkState.Connected);
     
    }

    void OnDisconnected()
    {
        SetState(NetworkState.Disconnected);
       
    }
}