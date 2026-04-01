using SL.Scripts.Core;
using SL.Scripts.Enums;
using SL.Scripts.Managers;

namespace SL.Scripts.States;

public abstract class GameState
{
    protected StateMachine Machine;

    protected GameState(StateMachine machine)
    {
        Machine = machine;
    }

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void HandleEvent(object evt) { }
}