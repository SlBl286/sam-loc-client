extends CanvasLayer

@onready var _rect = $Overlay

func fade_in(duration: float = 0.3):
	_rect.mouse_filter = Control.MOUSE_FILTER_STOP

	var tween = create_tween().bind_node(self)

	tween.tween_property(_rect,"modulate:a",1.0,duration)
	await tween.finished;

func fade_out(duration: float = 0.3):
	var tween = create_tween()
	tween.tween_property(_rect, "modulate:a", 0.0, duration)
	await tween.finished
	_rect.mouse_filter = Control.MOUSE_FILTER_IGNORE
