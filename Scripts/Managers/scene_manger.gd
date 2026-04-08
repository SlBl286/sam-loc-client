extends Node

class_name SceneManager
const SceneType = preload("res://Scripts/Enums/scene_type.gd").SceneType

static var Instance

var _world_layer: Node
var _ui_layer: Node
var _popup_layer: Node
var _loading_layer: Node
var _transition: Node

var _cache := {}
var _ui_stack: Array = []

var _scene_paths := {
    SceneType.LOGIN: "res://Scenes/Login/Login.tscn",
    SceneType.LOBBY: "res://Scenes/Lobby/Lobby.tscn",
    SceneType.ROOM: "res://Scenes/Room/Room.tscn",
    SceneType.GAME_TABLE: "res://Scenes/Game/Table.tscn",
    SceneType.RESULT: "res://Scenes/Game/Result.tscn",
    SceneType.LOADING: "res://Scenes/Loading/Loading.tscn"
}

func _ready():
    Instance = self

    _world_layer = get_node("/root/Main/WorldLayer")
    _ui_layer = get_node("/root/Main/UILayer")
    _popup_layer = get_node("/root/Main/PopupLayer")
    _loading_layer = get_node("/root/Main/LoadingLayer")

    var trans_scene = load("res://scenes/transition/Transition.tscn")
    _transition = trans_scene.instantiate()
    add_child(_transition)

# =========================
# Load Scene
# =========================

func load_scene(path: String) -> PackedScene:
    if _cache.has(path):
        return _cache[path]

    var scene = load(path)
    _cache[path] = scene
    return scene

# =========================
# UI Scene
# =========================

func change_ui(type):
    if not _scene_paths.has(type):
        return

    var path = _scene_paths[type]

    clear_layer(_ui_layer)

    var scene = load_scene(path)
    var instance = scene.instantiate()

    _ui_layer.add_child(instance)

    await instance.ready

# =========================
# UI Stack
# =========================

func push_ui(type):
    if not _scene_paths.has(type):
        return null

    var path = _scene_paths[type]

    var scene = load_scene(path)
    var instance = scene.instantiate()

    _ui_layer.add_child(instance)
    _ui_stack.append(instance)

    return instance

func pop_ui():
    if _ui_stack.is_empty():
        return

    var scene = _ui_stack.pop_back()
    scene.queue_free()

# =========================
# World Scene
# =========================

func change_world(type):
    if not _scene_paths.has(type):
        return

    var path = _scene_paths[type]

    await _transition.fade_in()

    var scene = await load_scene_async(path)

    clear_layer(_world_layer)

    var current_world = scene.instantiate()
    _world_layer.add_child(current_world)

    await _transition.fade_out()

# =========================
# Popup
# =========================

func show_popup(path: String):
    var scene = load_scene(path)
    var instance = scene.instantiate()

    _popup_layer.add_child(instance)
    return instance

func hide_popup():
    clear_layer(_popup_layer)

# =========================
# Loading
# =========================

func show_loading():
    clear_layer(_loading_layer)

    var scene = load_scene(_scene_paths[SceneType.LOADING])
    var instance = scene.instantiate()

    _loading_layer.add_child(instance)
    return instance

func hide_loading():
    clear_layer(_loading_layer)

# =========================
# Helpers
# =========================

func clear_layer(layer: Node):
    for child in layer.get_children():
        child.queue_free()

# =========================
# Async Load
# =========================

func load_scene_async(path: String) -> PackedScene:
    ResourceLoader.load_threaded_request(path)

    while true:
        var status = ResourceLoader.load_threaded_get_status(path)
        if status == ResourceLoader.THREAD_LOAD_LOADED:
            return ResourceLoader.load_threaded_get(path)
        elif status == ResourceLoader.THREAD_LOAD_FAILED:
            return null
        await get_tree().process_frame
    
    return null
