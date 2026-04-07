using Godot;
using System;
using System.Collections.Generic;

namespace SL.Scripts.Views;

public partial class Hand : Control
{

    public List<int> Cards { get; set; }


    public override void _Ready()
    {
        Cards = [1, 3, 15, 26, 36,18,29,16,32,48];
        Render();
    }


    public void Render()
    {
        foreach (var c in GetChildren())
        {
            c.QueueFree();
        }
            var spacing = 60;
            var start_x = -(Cards.Count * spacing) / 2;

            for (int i = 0; i < Cards.Count; i++)
            {
                var packedScene = GD.Load<PackedScene>("res://Scenes//Card.tscn");
                var card = packedScene.Instantiate<Card>();
                card.cardValue = Cards[i];
                card.Position = new (start_x + i * spacing, 0);

                AddChild(card);
            }
    }
}
