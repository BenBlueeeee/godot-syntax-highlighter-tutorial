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
	
	public Color red = new(1, 0.2f, 0.2f);
	public Color blue = new(0.2f, 0.4f, 1);
	public Color green = new(1f, 0.4f, .2f);
	public Color unhighlightedColor = new(0.5f, 0.5f, 0.5f);
	
	public System.Collections.Generic.List<int> errorLines = [];
	public System.Collections.Generic.List<int> warningLines = [];
	public System.Collections.Generic.List<int> generalLines = [];
	
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
	
	public override Dictionary _GetLineSyntaxHighlighting(int lineNumber)
	{
		return errorLines.Contains(lineNumber) ? WholeLineDict(red, lineNumber)
			 : warningLines.Contains(lineNumber) ? WholeLineDict(blue, lineNumber)
			 : generalLines.Contains(lineNumber) ? WholeLineDict(green, lineNumber)
			 : null;
	}
}