using Godot;
using System.Threading.Tasks;
namespace SL.Scripts.Views;

public partial class Transition : CanvasLayer
{
    private ColorRect _rect;

    public override void _Ready()
    {
        _rect = GetNode<ColorRect>("ColorRect");
    }

    public async Task FadeIn(float duration = 0.3f)
    {
        _rect.MouseFilter = Control.MouseFilterEnum.Stop;
        var tween = CreateTween();
        tween.TweenProperty(_rect, "modulate:a", 1.0f, duration);
        await ToSignal(tween, "finished");
    }

    public async Task FadeOut(float duration = 0.3f)
    {

        var tween = CreateTween();
        tween.TweenProperty(_rect, "modulate:a", 0.0f, duration);
        await ToSignal(tween, "finished");
        _rect.MouseFilter = Control.MouseFilterEnum.Ignore;

    }
}