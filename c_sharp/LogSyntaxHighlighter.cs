using System;
using Godot;
using Godot.Collections;

public partial class LogSyntaxHighlighter(LogTerminal inputVirtualTerminal) : SyntaxHighlighter
{
	private LogTerminal VirtualTerminal = inputVirtualTerminal;

	public int HighlightingStartColumn(int lineNumber)
	{
		return VirtualTerminal.LineStart(lineNumber).Length - 1;
	}

	public int HighlightingEndColumn = int.MaxValue;
	
	public Color red = new(1, 0.0f, 0.0f);
	public Color orange = new(1.0f, 0.5f, 0);
	public Color green = new(0.196f, .545f, .133f);
	public Color unhighlightedColor = new(0.5f, 0.5f, 0.5f);
	
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
		return errorLines.Contains(line) ? WholeLineDict(red, line)
			 : warningLines.Contains(line) ? WholeLineDict(orange, line)
			 : genericLines.Contains(line) ? WholeLineDict(green, line)
			 : null;
	}
}