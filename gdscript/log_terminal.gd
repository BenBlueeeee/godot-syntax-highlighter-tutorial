class_name LogTerminal extends TextEdit

enum LineType
{
    Error,
	Warning,
    Generic
}

var log_syntax_highlighter : LogSyntaxHighlighter
var current_line_number : int = 0

# godot doesnt provide a max float const, so use a large number
var FAKE_MAX_FLOAT : float = 100_000.0
var highlighting_end_column : float = FAKE_MAX_FLOAT

func _ready():
    log_syntax_highlighter = LogSyntaxHighlighter.new(self)

    text = "$   " + Time.get_time_string_from_system() + " : terminal initialised"

    current_line_number += 1

    syntax_highlighter = log_syntax_highlighter

func line_start(line_number : int) -> String:
    return "\n" + str(line_number) + " :    "

func write_line(phrase : String, line_type : LineType) -> void:
    match line_type:
        LineType.Error:
            log_syntax_highlighter.error_lines.append(current_line_number)
        LineType.Warning:
            log_syntax_highlighter.warning_lines.append(current_line_number)
        LineType.Generic:
            log_syntax_highlighter.generic_lines.append(current_line_number)
    
    text += line_start(current_line_number) + phrase
    current_line_number += 1

    scroll_vertical = FAKE_MAX_FLOAT
        
