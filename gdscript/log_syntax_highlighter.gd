class_name LogSyntaxHighlighter extends SyntaxHighlighter

var log_terminal : LogTerminal

# godot doesnt provide a max int const, so use a large number
var FAKE_MAX_INT : int = 100_000
var highlighting_end_column : int = FAKE_MAX_INT

@export var error_color : Color = Color(1, 0, 0)
@export var warning_color : Color = Color(1, 0.5, 0)
@export var generic_color : Color = Color(0.196, .545, .133)

@export var unhighlighted_color : Color = Color(.5, .5, .5)

var error_lines : Array[int] = []
var warning_lines : Array[int] = []
var generic_lines : Array[int] = []

func _init(inputLogTerminal : LogTerminal):
    log_terminal = inputLogTerminal

# returns the column which contains the first character of the printed text (excludes the printed line number)
func highlighting_start_column(line_number : int):
    return log_terminal.line_start(line_number).length() - 1

func whole_line_dict(color : Color, line_number : int):
    return {
        highlighting_start_column(line_number) : {
            "color" = color,
        },
        highlighting_end_column : {
            "color" = unhighlighted_color
        }
    }

func _get_line_syntax_highlighting(line: int) -> Dictionary:
    if error_lines.has(line):
        return whole_line_dict(error_color, line)
    elif warning_lines.has(line):
        return whole_line_dict(warning_color, line)
    elif generic_lines.has(line):
        return whole_line_dict(generic_color, line)
    else:
        return Dictionary() # cannot return null in gdscript, so return an empty dict instead
