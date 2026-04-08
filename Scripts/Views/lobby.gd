extends Control


@onready var username = $Panel/Username

func _ready() -> void:
	username.text = ""
