using Godot;
using SL.Scripts.Core;
using SL.Scripts.Managers;
using System;
using System.Reflection.PortableExecutable;

namespace SL.Scripts.Views;

public partial class Lobby : Control
{
		private Label _username;

	public override void _Ready()
	{
	
    _username = GetNode<Label>("Panel/Username");

		_username.Text = StateMachine.Instance.Context.Username;
	}

	public override void _ExitTree()
	{
	
	}





}