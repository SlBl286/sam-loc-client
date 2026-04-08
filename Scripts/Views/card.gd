extends Node2D

@onready var _fontSprite = $Front
@onready var _backSprite = $Back
var cardValue = 0
func _ready():
	_fontSprite.frame = cardValue;
	
