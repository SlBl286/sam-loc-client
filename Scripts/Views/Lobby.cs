using Godot;
using SL.Scripts.Core;
using SL.Scripts.Managers;
using System;
using System.Reflection.PortableExecutable;
using System.Text.Json;

namespace SL.Scripts.Views;

public partial class Lobby : Control
{
	private Label _username;
	private Button _createroomBtn;

	public override void _Ready()
	{

		_username = GetNode<Label>("Panel/Username");
		_createroomBtn = GetNode<Button>("Panel/Button");
		_username.Text = StateMachine.Instance.Context.Username;

		_createroomBtn.Pressed += OnBtnPress;
	}

	public void OnBtnPress()
	{
		NetworkManager.Instance.Socket.Send(JsonSerializer.Serialize(new { Type = "Login", Data = new { StateMachine.Instance.Context.UserId } }));
	}

	public override void _ExitTree()
	{

	}





}