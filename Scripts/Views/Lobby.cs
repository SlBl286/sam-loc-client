using Godot;
using SL.Scripts.Core;
using SL.Scripts.Event;
using SL.Scripts.Managers;
using System;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text.Json;

namespace SL.Scripts.Views;

public partial class Lobby : Control
{
	private Label _username;
	private Button _createroomBtn;
	private Button _quitBtn;

	private HFlowContainer _roomListContainer;

	public override void _Ready()
	{

		_username = GetNode<Label>("Panel/Username");
		_username.Text = StateMachine.Instance.Context.Username;

		_createroomBtn = GetNode<Button>("Panel/CreateBtn");
		_quitBtn = GetNode<Button>("Panel/QuitBtn");

		_createroomBtn.Pressed += OnBtnPress;
		_quitBtn.Pressed += OnQuitPress;

		_roomListContainer = GetNode<HFlowContainer>("Panel/RoomListContainer");

		EventBus.Subscribe<RoomListEvent>(OnSyncRooms);

	}

	public void OnBtnPress()
	{
		NetworkManager.Instance.Socket.Send(new { type = "CreateRoom", room_name = "Test Room", max_players = 5 });
	}

	public void OnQuitPress()
	{	
		NetworkManager.Instance.Socket.Close();
		GetTree().Quit();
	}
	public void OnSyncRooms(RoomListEvent e)
	{
		GD.Print("Syncing rooms: ", e.Rooms.Count);
		e.Rooms.ForEach(room =>
		{
			var roomLabel = new Label();
			roomLabel.Text = $"{room.Name} ({room.Players.Count}/{room.MaxPlayers})";
			roomLabel.AddThemeFontSizeOverride("font_size", 18);
			_roomListContainer.AddChild(roomLabel);
		});
	}

	public override void _ExitTree()
	{
		EventBus.Unsubscribe<RoomListEvent>(OnSyncRooms);

	}





}