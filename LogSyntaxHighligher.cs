using System;
using Godot;
using Godot.Collections;

public partial class LoggerSyntaxHighlighter(Logger inputVirtualTerminal) : SyntaxHighlighter
{
	private Logger VirtualTerminal = inputVirtualTerminal;

	public int HighlightingStartColumn => VirtualTerminal.LineStart.Length - 1;
	public int HighlightingEndColumn = int.MaxValue;
	
	public Color red = new(1, 0.2f, 0.2f);
	public Color blue = new(0.2f, 0.4f, 1);
	public Color green = new(1f, 0.4f, .2f);
	public Color unhighlightedColor = new(0.5f, 0.5f, 0.5f);
	
	public System.Collections.Generic.List<int> errorLines = [];
	public System.Collections.Generic.List<int> warningLines = [];
	public System.Collections.Generic.List<int> generalLines = [];
	
	private Dictionary WholeLineDict(Color color)
	{
		return new()
		{
			[HighlightingStartColumn] = new Dictionary<Variant, Color>
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
		return errorLines.Contains(line) ? WholeLineDict(red)
			 : warningLines.Contains(line) ? WholeLineDict(blue)
			 : generalLines.Contains(line) ? WholeLineDict(green)
			 : null;
	}
}