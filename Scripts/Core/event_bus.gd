extends Node
class_name EventBus

static var Instance
var _events: Dictionary = {}

func _ready():
    Instance = self

func subscribe(event_type: String, callback: Callable) -> void:
    if not _events.has(event_type):
        _events[event_type] = []
    _events[event_type].append(callback)


func unsubscribe(event_type: String, callback: Callable) -> void:
    if not _events.has(event_type):
        return
    _events[event_type].erase(callback)


func publish(event_type: String, evt) -> void:
    if _events.has(event_type):
        for cb in _events[event_type]:
            cb.call(evt)

    # 🔥 gọi global listener (giống object)
    if _events.has("object"):
        for cb in _events["object"]:
            cb.call(evt)


func clear() -> void:
    _events.clear()