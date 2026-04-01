using SL.Scripts.Managers;

namespace SL.Scripts.States;
public class LoadingState : GameState
{
    public LoadingState(StateMachine machine) : base(machine) { }

    public override void Enter()
    {
        SceneManager.Instance.ShowLoading();
    }

    public override void Exit()
    {
        SceneManager.Instance.HideLoading();
    }
}