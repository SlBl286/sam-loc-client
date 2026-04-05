using Godot;
using SL.Scripts.Core;
using SL.Scripts.Enums;
using SL.Scripts.Event;
using SL.Scripts.Managers;

namespace SL.Scripts.States;

public class LobbyState : GameState
{
    public LobbyState(StateMachine machine) : base(machine) { }

    public override async void Enter()
    {

        await SceneManager.Instance.ChangeWorld(SceneType.Lobby);
        NetworkManager.Instance.Socket.Connect("ws://127.0.0.1:8080");
    }
    public override void HandleEvent(object evt)
    {
        if (evt is JoinRoomEvent jre)
        {
            Machine.Context.RoomId = jre.RoomId;
            Machine.ChangeState(GameStateType.Room);
        }
        if (evt is RoomListEvent )
        {
            
        }
        if (evt is PlayerListEvent)
        {
        }
    }

}