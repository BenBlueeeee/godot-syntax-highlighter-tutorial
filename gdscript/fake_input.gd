extends Node

@export var log_terminal : LogTerminal

func _unhandled_input(event):
    if Input.is_action_just_pressed("print_error"):
        log_terminal.write_line("This is an error", LogTerminal.LineType.Error)
    if Input.is_action_just_pressed("print_warning"):
        log_terminal.write_line("This is a warning", LogTerminal.LineType.Warning)
    if Input.is_action_just_pressed("print_generic"):
        log_terminal.write_line("This is a generic message", LogTerminal.LineType.Generic)