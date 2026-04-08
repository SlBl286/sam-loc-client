extends Node
class_name StateMachine

var GameStateType = preload("res://Scripts/Enums/game_state_type.gd").GameStateType
var GlobalState = preload("res://Scripts/States/global_state.gd").GlobalState
var LoginState = preload("res://Scripts/States/login_state.gd").LoginState

static var Instance

var Context: preload("res://Scripts/Core/state_context.gd")

var _states := {}
var _stack: Array = []
var _current: GameState
var _global: GameState

func _ready():
    Instance = self

    Context = StateContext.new()

    register_states()
    subscribe_events()

    _global = GlobalState.new(self)

    change_state(GameStateType.LOGIN)

# =========================
# Register
# =========================

func register_states():
    _states[GameStateType.LOGIN] = LoginState.new(self)
    _states[GameStateType.LOBBY] = LobbyState.new(self)
    _states[GameStateType.ROOM] = RoomState.new(self)
    _states[GameStateType.GAME] = GamePlayingState.new(self)
    _states[GameStateType.LOADING] = LoadingState.new(self)
    _states[GameStateType.RECONNECT] = ReconnectState.new(self)

# =========================
# EventBus
# =========================

func subscribe_events():
    EventBus.Instance.subscribe("event", _handle_event)

func _handle_event(evt):
    if _global:
        _global.handle_event(evt)

    if not _stack.is_empty():
        _stack[-1].handle_event(evt)

# =========================
# State control
# =========================

func change_state(type):
    while not _stack.is_empty():
        var s = _stack.pop_back()
        s.exit()

    push_state(type)

func push_state(type):
    var state: GameState = _states[type]

    _stack.append(state)
    _current = state

    state.enter()

func pop_state(type):
    if _stack.is_empty():
        return

    if _current == _states[type]:
        var s = _stack.pop_back()
        s.exit()

        _current =  null if _stack.is_empty() else _stack[-1]