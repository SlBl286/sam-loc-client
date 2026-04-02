using Godot;
using SL.Scripts.Enums;
using SL.Scripts.Views;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace SL.Scripts;

public partial class SceneManager : Node
{
	public static SceneManager Instance;

	private Node _worldLayer;
	private Node _uiLayer;
	private Node _popupLayer;
	private Node _loadingLayer;
	private Transition _transition;
	private Dictionary<string, PackedScene> _cache = new();
	private Stack<Node> _uiStack = new();
	private Dictionary<SceneType, string> _scenePaths = new()
{
	{ SceneType.Login, "res://Scenes/Login/Login.tscn" },
	{ SceneType.Lobby, "res://Scenes/Lobby/Lobby.tscn" },
	{ SceneType.Room, "res://Scenes/Room/Room.tscn" },
	{ SceneType.GameTable, "res://Scenes/Game/Table.tscn" },
	{ SceneType.Result, "res://Scenes/Game/Result.tscn" },
	{ SceneType.Loading, "res://Scenes/Loading/Loading.tscn" }
};

	public override void _Ready()
	{
		Instance = this;

		_worldLayer = GetNode("/root/Main/WorldLayer");
		_uiLayer = GetNode("/root/Main/UILayer");
		_popupLayer = GetNode("/root/Main/PopupLayer");
		_loadingLayer = GetNode("/root/Main/LoadingLayer");

		var transScene = GD.Load<PackedScene>("res://scenes/transition/Transition.tscn");
		_transition = transScene.Instantiate<Transition>();
		AddChild(_transition);
	}

	PackedScene LoadScene(string path)
	{
		if (_cache.ContainsKey(path))
			return _cache[path];

		var scene = GD.Load<PackedScene>(path);
		_cache[path] = scene;
		return scene;
	}

	// =========================
	// UI Scene
	// =========================

	public async Task ChangeUI(SceneType type)
	{
		if (!_scenePaths.ContainsKey(type))
			return;
		string path = _scenePaths[type];

		ClearLayer(_uiLayer);

		var scene = LoadScene(path);
		var instance = scene.Instantiate();

		_uiLayer.AddChild(instance);

		await ToSignal(instance, "ready");
	}

	// =========================
	// UI Stack (push / pop)
	// =========================

	public Node PushUI(SceneType type)
	{
		if (!_scenePaths.ContainsKey(type))
			return null;

		string path = _scenePaths[type];

		var scene = LoadScene(path);
		var instance = scene.Instantiate();

		_uiLayer.AddChild(instance);
		_uiStack.Push(instance);

		return instance;
	}

	public void PopUI()
	{
		if (_uiStack.Count == 0)
			return;

		var scene = _uiStack.Pop();
		scene.QueueFree();
	}

	// =========================
	// World Scene
	// =========================

	public async Task ChangeWorld(SceneType type)
	{
		if (!_scenePaths.ContainsKey(type))
			return;

		string path = _scenePaths[type];


		await _transition.FadeIn();

		// Async load
		var scene = await LoadSceneAsync(path);

		// Remove scene cũ
		ClearLayer(_worldLayer);

		// Add scene mới
		var _currentWorld = scene.Instantiate();
		_worldLayer.AddChild(_currentWorld);

		// Fade out
		await _transition.FadeOut();
	}

	// =========================
	// Popup
	// =========================

	public Node ShowPopup(string path)
	{
		var scene = LoadScene(path);
		var instance = scene.Instantiate();

		_popupLayer.AddChild(instance);
		return instance;
	}

	public void HidePopup()
	{
		ClearLayer(_popupLayer);
	}

	// =========================
	// Loading Screen
	// =========================

	public Node ShowLoading()
	{
		ClearLayer(_loadingLayer);

		var scene = LoadScene(_scenePaths[SceneType.Loading]);
		var instance = scene.Instantiate();

		_loadingLayer.AddChild(instance);
		return instance;
	}

	public void HideLoading()
	{
		ClearLayer(_loadingLayer);
	}

	// =========================
	// Helpers
	// =========================

	void ClearLayer(Node layer)
	{
		foreach (Node child in layer.GetChildren())
			child.QueueFree();
	}

	private async Task<PackedScene> LoadSceneAsync(string path)
	{
		ResourceLoader.LoadThreadedRequest(path);

		while (true)
		{
			var status = ResourceLoader.LoadThreadedGetStatus(path);

			if (status == ResourceLoader.ThreadLoadStatus.Loaded)
			{
				return ResourceLoader.LoadThreadedGet(path) as PackedScene;
			}

			await ToSignal(GetTree(), "process_frame");
		}
	}
}