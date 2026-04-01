using Godot;
using SL.Scripts.Enums;
using SL.Scripts.Event;
using SL.Scripts.Managers;

namespace SL.Scripts.States;

public class RoomState : GameState
{
    public RoomState(StateMachine machine) : base(machine) { }

    public override void Enter()
    {
        GD.Print("Join Room: " + Machine.Context.SelectedRoomId);

        SceneManager.Instance.ChangeWorld(SceneType.Room);
    }
    public override void HandleEvent(object evt)
    {
        if (evt is StartGameEvent e)
        {
            Machine.Context.MatchId = e.MatchId;
            Machine.ChangeState(GameStateType.Game);
        }
    }
}