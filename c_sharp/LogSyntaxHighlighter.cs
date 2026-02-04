using System;
using Godot;
using Godot.Collections;

public partial class LogSyntaxHighlighter(LogTerminal inputLogTerminal) : SyntaxHighlighter
{
	private LogTerminal logTerminal = inputLogTerminal;

	public int HighlightingStartColumn(int lineNumber)
	{
		return logTerminal.LineStart(lineNumber).Length - 1;
	}

	public int HighlightingEndColumn = int.MaxValue;
	
	[Export] public Color errorColor = new(1, 0.0f, 0.0f);
	[Export] Color warningColor = new(1.0f, 0.5f, 0);
	[Export] Color genericColor = new(0.196f, .545f, .133f);
	[Export] Color unhighlightedColor = new(0.5f, 0.5f, 0.5f);
	
	public System.Collections.Generic.List<int> errorLines = [];
	public System.Collections.Generic.List<int> warningLines = [];
	public System.Collections.Generic.List<int> genericLines = [];
	
	private Dictionary WholeLineDict(Color color, int lineNumber)
	{
		return new()
		{
			[HighlightingStartColumn(lineNumber)] = new Dictionary<Variant, Color>
			{
				["color"] = color
			},
			[HighlightingEndColumn] = new Dictionary<Variant, Color>
			{
				["color"] = unhighlightedColor
			}
		};
	}
	
	public override Dictionary _GetLineSyntaxHighlighting(int line)
	{
		return errorLines.Contains(line) ? WholeLineDict(errorColor, line)
			 : warningLines.Contains(line) ? WholeLineDict(warningColor, line)
			 : genericLines.Contains(line) ? WholeLineDict(genericColor, line)
			 : null;
	}
}