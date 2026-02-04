using Godot;
using System;

public partial class FakeInput : Node
{
    [Export] private LogTerminal logTerminal;

    public override void _UnhandledInput(InputEvent @event)
    {
        base._UnhandledInput(@event);

        if (Input.IsActionJustPressed("print_error"))
        {
            logTerminal.WriteLine("This is an error", LineType.Error);
        }
        else if (Input.IsActionJustPressed("print_warning"))
        {
            logTerminal.WriteLine("This is a warning", LineType.Warning);
        }
        else if (Input.IsActionJustPressed("print_generic"))
        {
            logTerminal.WriteLine("This is a generic message", LineType.Generic);
        }
    }
}
