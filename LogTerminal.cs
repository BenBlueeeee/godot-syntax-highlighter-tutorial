using System;
using Godot;

// the different types of lines that we want to be able to print to our terminal
public enum LineType
{
	Error,
	Warning,
    General
}

public partial class LogTerminal : TextEdit
{
	public LogSyntaxHighlighter loggerSyntaxHighlighter;
	
	public int CurrentLineNumber {get; private set;} = 0;
	
	public override void _Ready()
	{
		base._Ready();
		
		loggerSyntaxHighlighter = new(this);
		
		// write the initial line without any \n
		Text = "$   terminal initialised";
		CurrentLineNumber += 1;
		
		SyntaxHighlighter = loggerSyntaxHighlighter;
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
				loggerSyntaxHighlighter.errorLines.Add(CurrentLineNumber);
				break;
			}
			case LineType.Warning:
			{
				loggerSyntaxHighlighter.warningLines.Add(CurrentLineNumber);
				break;
			}
            case LineType.General:
            {
				loggerSyntaxHighlighter.generalLines.Add(CurrentLineNumber);
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