using SL.Scripts.Event;
using SL.Scripts.Managers;

namespace SL.Scripts.States;
public class PopupState : GameState
{
    public PopupState(StateMachine machine) : base(machine) { }

    public override void Enter()
    {
        SceneManager.Instance.ShowPopup("res://scenes/popup/Popup.tscn");
    }

    public override void Exit()
    {
        SceneManager.Instance.HidePopup();
    }

    public override void HandleEvent(object evt)
    {
        if (evt is ClosePopupEvent)
        {
            Machine.PopState();
        }
    }
}