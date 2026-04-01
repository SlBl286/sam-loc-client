using SL.Scripts.Enums;
using SL.Scripts.Managers;

namespace SL.Scripts.States;
public class GamePlayingState : GameState
{
    public GamePlayingState(StateMachine machine) : base(machine) { }

    public override void Enter()
    {
        SceneManager.Instance.ChangeWorld(SceneType.GameTable);
    }


}