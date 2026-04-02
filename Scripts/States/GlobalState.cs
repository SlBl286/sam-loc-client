using SL.Scripts.Enums;
using SL.Scripts.Event;
using SL.Scripts.Managers;

namespace SL.Scripts.States;

public class GlobalState : GameState
{
    public GlobalState(StateMachine machine) : base(machine) { }

    public override void HandleEvent(object evt)
    {
        if (evt is SocketDisconnectedEvent)
        {
            Machine.ChangeState(GameStateType.Reconnect);
        }

        if (evt is NetworkStateChangedEvent n)
        {
            if (n.State == NetworkState.ApiRequesting)
            {
                Machine.ChangeState(GameStateType.Loading);
            }
            else if (n.State == NetworkState.ApiRequestSucess)
            {
                Machine.PopState(GameStateType.Loading); // tắt loading
            }
            
        }
        if (evt is ShowPopupEvent p)
        {
            Machine.PushState(GameStateType.Popup);
        }
    }
}