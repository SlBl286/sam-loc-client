using SL.Scripts.Enums;
using SL.Scripts.Event;
using SL.Scripts.Managers;

namespace SL.Scripts.States;

public class GlobalState : GameState
{
    public GlobalState(StateMachine machine) : base(machine) { }

    public override void HandleEvent(object evt)
    {
        if (evt is SocketConnectedEvent)
        {
            NetworkManager.Instance.Socket.Send(new
            {
                type = ClientMessageType.Connected.ToString(),
                user_id = Machine.Context.UserId
            });
        }
        if (evt is SocketDisconnectedEvent dce)
        {
            if (dce.Code == 1006) // 1006 là code khi mất kết nối đột ngột
                Machine.ChangeState(GameStateType.Reconnect);
            else 
                Machine.ChangeState(GameStateType.Login); // quay về lobby nếu bị kick hoặc tự ngắt kết nối

        }

        if (evt is NetworkStateChangedEvent n)
        {
            if (n.State == NetworkState.ApiRequesting)
            {
                Machine.PushState(GameStateType.Loading);
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