using Godot;
using System;

public partial class Card : Node2D
{

	private Sprite2D _frontSprite;
	private Sprite2D _backSprite;
	[Export]
	public bool _isFaceUp = false;
	[Export]
	public int cardValue = 0;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_frontSprite = GetNode<Sprite2D>("Front");
		_backSprite = GetNode<Sprite2D>("Back");
		_frontSprite.Frame = cardValue;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
}
