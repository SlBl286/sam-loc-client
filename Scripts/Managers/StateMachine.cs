using Godot;
using SL.Scripts.Core;
using SL.Scripts.Enums;
using SL.Scripts.Event;
using SL.Scripts.States;
using System.Collections.Generic;
namespace SL.Scripts.Managers;

using Godot;
using System.Collections.Generic;

public partial class StateMachine : Node
{
    public static StateMachine Instance;

    public StateContext Context { get; private set; }

    private Dictionary<GameStateType, GameState> _states = new();
private Stack<GameState> _stack = new();
    private GameState _current;
    private GameState _global;

    public override void _Ready()
    {
        Instance = this;

        Context = new StateContext();

        RegisterStates();
        SubscribeEvents();

        _global = new GlobalState(this);

        ChangeState(GameStateType.Login);
    }

    void RegisterStates()
    {
        _states[GameStateType.Login] = new LoginState(this);
        _states[GameStateType.Lobby] = new LobbyState(this);
        _states[GameStateType.Room] = new RoomState(this);
        _states[GameStateType.Game] = new GamePlayingState(this);
        _states[GameStateType.Loading] = new LoadingState(this);
        _states[GameStateType.Reconnect] = new ReconnectState(this);
    }

    void SubscribeEvents()
    {
        EventBus.Subscribe<object>(HandleEvent);
    }

    void HandleEvent(object evt)
    {
      _global?.HandleEvent(evt);

        if (_stack.Count > 0)
            _stack.Peek().HandleEvent(evt);
    }

    public void ChangeState(GameStateType type)
    {
       while (_stack.Count > 0)
            _stack.Pop().Exit();

        PushState(type);
    }
     public void PushState(GameStateType type)
    {
        var state = _states[type];

        _stack.Push(state);
        state.Enter();
    }

    public void PopState()
    {
        if (_stack.Count == 0)
            return;

        _stack.Pop().Exit();
    }
}