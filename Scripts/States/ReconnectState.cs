
using Godot;
using SL.Scripts.Enums;
using SL.Scripts.Event;
using SL.Scripts.Managers;

namespace SL.Scripts.States;
public class ReconnectState : GameState
{
    public ReconnectState(StateMachine machine) : base(machine) { }

    public override void Enter()
    {
        GD.Print("Reconnecting...");

        NetworkManager.Instance.Socket.Connect("ws://127.0.0.1:8080");
    }

    public override void HandleEvent(object evt)
    {
        if (evt is SocketConnectedEvent)
        {
            Machine.ChangeState(GameStateType.Lobby);
        }
    }
}