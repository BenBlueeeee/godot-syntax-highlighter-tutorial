using System;
using Godot;

// the different types of lines that we want to be able to print to our terminal
public enum LineType
{
	Error,
	Warning,
    General
}

public partial class Logger : TextEdit
{
	public LoggerSyntaxHighlighter loggerSyntaxHighlighter;
	
	public int LineNumber {get; private set;} = 0;
	
	public override void _Ready()
	{
		base._Ready();
		
		loggerSyntaxHighlighter = new(this);
		
		// write the initial line without any \n
		Text = "$   terminal initialised";
		LineNumber += 1;
		
		SyntaxHighlighter = loggerSyntaxHighlighter;
	}
	
	public string LineStart => $"\n{LineNumber} :   ";
	
	public void WriteLine(string phrase, LineType? lineType)
	{
		switch (lineType)
		{
			case LineType.Error:
			{
				loggerSyntaxHighlighter.errorLines.Add(LineNumber);
				break;
			}
			case LineType.Warning:
			{
				loggerSyntaxHighlighter.warningLines.Add(LineNumber);
				break;
			}
            case LineType.General:
            {
				loggerSyntaxHighlighter.generalLines.Add(LineNumber);
				break; 
            }
			default:
			{
				break;
			}
		}
		
		Text += $"{LineStart}{phrase}";
		LineNumber++;
		
        // force the most recently printed line to be visible
		ScrollVertical = float.MaxValue;
	}
}