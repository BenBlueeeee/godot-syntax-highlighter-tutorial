using Godot;
using System;

public partial class FakeInput : Node
{
    [Export] private LogTerminal logTerminal;

    public override void _Ready()
    {
        base._Ready();
        
        logTerminal.WriteLine("This is a fake error", LineType.Error);
        logTerminal.WriteLine("This is a fake warning", LineType.Warning);
        logTerminal.WriteLine("This is a fake general message", LineType.General);
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        base._UnhandledInput(@event);
        
        logTerminal.WriteLine("This is a fake error", LineType.Error);
    }
}
