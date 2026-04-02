using Godot;
using SL.Scripts.Core;
using SL.Scripts.Enums;
using SL.Scripts.Event;
using SL.Scripts.Managers;
using System.Text;
using System.Text.Json;
namespace SL.Scripts.Network;
public partial class ApiClient : Node
{
    private HttpRequest _http;
    private string _baseUrl = "http://127.0.0.1:8081";

    public override void _Ready()
    {
        _http = new HttpRequest();
        AddChild(_http);

        _http.RequestCompleted += OnResponse;
    }

    public void Login(string username, string password)
    {
        NetworkManager.Instance.SetState(NetworkState.ApiRequesting);

        Post("/login", new
        {
            username,
            password
        });
    }

    public void GetProfile()
    {
        NetworkManager.Instance.SetState(NetworkState.ApiRequesting);

        Get("/profile");
    }

    void Post(string endpoint, object body)
    {
        string json = JsonSerializer.Serialize(body);

        string[] headers =
        {
            "Content-Type: application/json"
        };

        _http.Request(
            _baseUrl + endpoint,
            headers,
            HttpClient.Method.Post,
            json
        );

    }

    void Get(string endpoint)
    {
        string token = StateMachine.Instance.Context.Token;

        string[] headers =
        {
            "Content-Type: application/json",
            "Authorization: Bearer " + token
        };

        _http.Request(
            _baseUrl + endpoint,
            headers,
            HttpClient.Method.Get
        );
    }

    void OnResponse(long result, long code, string[] headers, byte[] body)
    {
        string text = Encoding.UTF8.GetString(body);
        NetworkManager.Instance.SetState(NetworkState.ApiRequestSucess);

        if (code != 200)
        {
            EventBus.Publish(new ApiErrorEvent
            {
                Code = (int)code,
                Message = text
            });
            return;
        }
        

        HandleResponse(text);
    }

    void HandleResponse(string json)
    {
        var baseRes = JsonSerializer.Deserialize<BaseResponse>(json);
        switch (baseRes.res_type)
        {
            case "login":
                var login = JsonSerializer.Deserialize<LoginResponse>(json);

                EventBus.Publish(new LoginSuccessEvent
                {
                    UserId = login.user_id,
                    Username = login.username,
                    Token = login.token
                });
                break;

            case "profile":
                var profile = JsonSerializer.Deserialize<ProfileResponse>(json);

                EventBus.Publish(new ProfileLoadedEvent
                {
                    UserId = profile.user_id,
                    Username = profile.username
                });
                break;
        }
    }
}