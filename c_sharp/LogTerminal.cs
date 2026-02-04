using System;
using Godot;

// the different types of lines that we want to be able to print to our terminal
public enum LineType
{
	Error,
	Warning,
    Generic
}

public partial class LogTerminal : TextEdit
{
	public LogSyntaxHighlighter logSyntaxHighlighter;
	
	public int CurrentLineNumber {get; private set;} = 0;
	
	public override void _Ready()
	{
		base._Ready();
		
		logSyntaxHighlighter = new(this);
		
		// write the initial line without any \n
		Text = $"$	{DateTime.Now:HH:mm:ss} : terminal initialised";
		CurrentLineNumber += 1;
		
		SyntaxHighlighter = logSyntaxHighlighter;
	}
	
	public string LineStart(int lineNumber)
	{
		return $"\n{lineNumber} :   ";
	}
	
	public void WriteLine(string phrase, LineType? lineType)
	{
		switch (lineType)
		{
			case LineType.Error:
			{
				logSyntaxHighlighter.errorLines.Add(CurrentLineNumber);
				break;
			}
			case LineType.Warning:
			{
				logSyntaxHighlighter.warningLines.Add(CurrentLineNumber);
				break;
			}
            case LineType.Generic:
            {
				logSyntaxHighlighter.genericLines.Add(CurrentLineNumber);
				break; 
            }
			default:
			{
				break;
			}
		}
		
		Text += $"{LineStart(CurrentLineNumber)}{phrase}";
		CurrentLineNumber++;
		
        // force the most recently printed line to be visible
		ScrollVertical = float.MaxValue;
	}
}