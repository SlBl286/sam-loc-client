using Godot;
using SL.Scripts.Enums;
using SL.Scripts.Event;
using SL.Scripts.Managers;

namespace SL.Scripts.States;

public class LoginState : GameState
{
    public LoginState(StateMachine machine) : base(machine) { }

    public override void Enter()
    {
        SceneManager.Instance.ChangeWorld(SceneType.Login);
    }

    public override void HandleEvent(object evt)
    {
        if (evt is LoginSuccessEvent e)
        {
            Machine.Context.UserId = e.UserId;
            Machine.Context.Token = e.Token;

            Machine.ChangeState(GameStateType.Lobby);
        }
    }
}