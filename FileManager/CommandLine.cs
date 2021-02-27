using System;
using System.IO;
using System.Collections.Generic;


namespace FileManager
{
    public class CommandLine
    {
        public FilesPanel CurrentPanel { get; set; }
        public View CommandView { get; set; }
        public int SizeBuffer { get; set; }
        public List<String> BufferCommands { get; set; }

        public CommandLine(FilesPanel currentPanel)
        {
            CurrentPanel = currentPanel;
            CommandView = new View();
            SizeBuffer = 5;
            BufferCommands = new List<string>(SizeBuffer);
        }

        public void Management()
        {
            string commandLine = "";
            int commandNumberInBuffer = 0;
            int cursorPositionInLine = 0;

            ConsoleKeyInfo click;
            do
            {
                
                CommandView.CommandLine(CurrentPanel.CurrentPath, commandLine, cursorPositionInLine);
                click = Console.ReadKey(true);
                if (click.Key == ConsoleKey.Escape)
                {
                    return;
                }
                else if (click.Key == ConsoleKey.Enter)
                {
                    if(BufferCommands.Count >= SizeBuffer)
                    {
                        BufferCommands.RemoveAt(0);
                    }   
                    BufferCommands.Add(commandLine);
                    commandNumberInBuffer = BufferCommands.Count;
                    List<string> command = CustomMethods.SplitString(" ", commandLine);
                    CheckCommand(command);
                    commandLine = "";
                    cursorPositionInLine = commandLine.Length;
                    Desktop.GetInstance().Update();
                    CommandView.OldCursor(CurrentPanel);
                    commandLine = "";
                    continue;
                }
                else if(click.Key == ConsoleKey.P && (click.Modifiers & ConsoleModifiers.Control) != 0)
                {
                    commandLine += CurrentPanel.CurrentPath;
                    cursorPositionInLine = commandLine.Length;
                    continue;
                }
                else if(click.Key == ConsoleKey.DownArrow)
                {
                    if(BufferCommands.Count != 0)
                    {
                        if (commandNumberInBuffer > 0)
                        {
                            commandNumberInBuffer--;
                            commandLine = BufferCommands[commandNumberInBuffer];
                            cursorPositionInLine = commandLine.Length;
                        }
                    }
                    continue;
                }
                else if(click.Key == ConsoleKey.UpArrow)
                {
                    if (BufferCommands.Count != 0)
                    {
                        if (commandNumberInBuffer < BufferCommands.Count - 1)
                        {
                            commandNumberInBuffer++;
                            commandLine = BufferCommands[commandNumberInBuffer];
                            cursorPositionInLine = commandLine.Length;
                        }
                    }
                    continue;
                }
                else if(click.Key == ConsoleKey.LeftArrow)
                {
                    if(commandLine.Length > 0 && cursorPositionInLine > 0)
                    {
                        cursorPositionInLine -= 1;
                    }
                    continue;
                }
                else if(click.Key == ConsoleKey.RightArrow)
                {
                    if (cursorPositionInLine < commandLine.Length)
                    {
                        cursorPositionInLine += 1;
                    }
                    continue;
                }
                else if(click.Key == ConsoleKey.Backspace)
                {
                    if (commandLine.Length > 0 && cursorPositionInLine > 0)
                    {
                        commandLine = commandLine.Remove(cursorPositionInLine - 1, 1);
                        cursorPositionInLine -= 1;
                    }
                    else
                    {
                        continue;
                    }
                }
                else
                {
                    if(cursorPositionInLine == commandLine.Length)
                    {
                        commandLine += click.KeyChar;
                        cursorPositionInLine += 1;
                    }
                    else if(cursorPositionInLine < commandLine.Length)
                    {
                        commandLine = commandLine.Insert(cursorPositionInLine, click.KeyChar.ToString());
                        cursorPositionInLine += 1;
                    }
                }
            } while (true);
        }

        public void CheckCommand(List<string> command)
        {
            try
            {
                switch (command[0])
                {
                    case "cd":
                        if (Directory.Exists(command[1]))
                        {
                            Desktop.GetInstance().ActivePanel.UpdatePath(command[1]);
                        }
                        break;
                    case "copy":
                        CommandLineAction claCopy = new CommandLineAction(command[1], command[2]);
                        claCopy.CL_Copy();
                        break;
                    case "rename":
                        CommandLineAction claRename = new CommandLineAction(command[1]);
                        claRename.CL_Rename();
                        break;
                    case "delete":
                        CommandLineAction claDelete = new CommandLineAction(command[1]);
                        claDelete.CL_Delete();
                        break;
                    case "tree":
                        if (Directory.Exists(command[1]))
                        {
                            new Tree().TreeFilesAndDirectory(command[1]);
                            Desktop.GetInstance().Update();
                        }
                        break;
                    default:
                        return;
                }
            }
            catch(Exception ex)
            {
                new ErrorLog(this, ex.Message, ex.StackTrace);
            }
            
        }
        
    }
}
