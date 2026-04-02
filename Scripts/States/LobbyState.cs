using Godot;
using SL.Scripts.Enums;
using SL.Scripts.Event;
using SL.Scripts.Managers;

namespace SL.Scripts.States;

public class LobbyState : GameState
{
    public LobbyState(StateMachine machine) : base(machine) { }

    public override async void Enter()
    {
        GD.Print("Enter Lobby ");

        await SceneManager.Instance.ChangeWorld(SceneType.Lobby);
    }
 public override void HandleEvent(object evt)
    {
        if (evt is JoinRoomEvent e)
        {
            Machine.Context.RoomId = e.RoomId;
            Machine.ChangeState(GameStateType.Room);
        }
    }

}