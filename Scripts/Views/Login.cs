using Godot;
using SL.Scripts.Core;
using SL.Scripts.Event;
using SL.Scripts.Managers;
namespace SL.Scripts.Views;
public partial class Login: Control
{
	private LineEdit _username;
	private LineEdit _password;
	private Button _loginBtn;
	private Label _error;

	public override void _Ready()
	{
		_username = GetNode<LineEdit>("Panel/VBoxContainer/UsernameInput");
		_password = GetNode<LineEdit>("Panel/VBoxContainer/PasswordInput");
		_loginBtn = GetNode<Button>("Panel/VBoxContainer/LoginBtn");
		_error = GetNode<Label>("Panel/VBoxContainer/ErrorLabel");

		_loginBtn.Pressed += OnLoginPressed;
		
		EventBus.Subscribe<ApiErrorEvent>(OnLoginError);
		EventBus.Subscribe<LoginSuccessEvent>(OnLoginSuccess);
	}

	public override void _ExitTree()
	{
		EventBus.Unsubscribe<ApiErrorEvent>(OnLoginError);
		EventBus.Unsubscribe<LoginSuccessEvent>(OnLoginSuccess);
	}

	void OnLoginPressed()
	{
		string username = _username.Text.Trim();
		string password = _password.Text.Trim();

		if (username == "" || password == "")
		{
			_error.Text = "Nhập username và password";
			return;
		}

		_error.Text = "Đang đăng nhập...";

		NetworkManager.Instance.Api.Login(username, password);
	}

	void OnLoginError(ApiErrorEvent e)
	{
		_error.Text = "Login failed";
	}

	void OnLoginSuccess(LoginSuccessEvent e)
	{
		_error.Text = "Login success";
	}
}