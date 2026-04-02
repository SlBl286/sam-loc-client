using Godot;
using SL.Scripts.Core;
using System;

namespace SL.Scripts.Views;

public partial class Lobby : Control
{

	// public override void _Ready()
	// {
	

	// 	EventBus.Subscribe<ApiErrorEvent>(OnLoginError);
	// 	EventBus.Subscribe<LoginSuccessEvent>(OnLoginSuccess);
	// }

	// public override void _ExitTree()
	// {
	// 	EventBus.Unsubscribe<ApiErrorEvent>(OnLoginError);
	// 	EventBus.Unsubscribe<LoginSuccessEvent>(OnLoginSuccess);
	// }

	// void OnLoginPressed()
	// {
	// 	string username = _username.Text.Trim();
	// 	string password = _password.Text.Trim();

	// 	if (username == "" || password == "")
	// 	{
	// 		_error.Text = "Nhập username và password";
	// 		return;
	// 	}

	// 	_error.Text = "Đang đăng nhập...";
	// 	_loginBtn.Disabled = true;
	// 	NetworkManager.Instance.Api.Login(username, password);
	// }

	// void OnLoginError(ApiErrorEvent e)
	// {
	// 	_error.Text = "Login failed";
	// 	_loginBtn.Disabled = true;

	// }

	// void OnLoginSuccess(LoginSuccessEvent e)
	// {
	// 	_loginBtn.Disabled = false;
	// 	StateMachine.Instance.ChangeState(GameStateType.Lobby);
	// }
}