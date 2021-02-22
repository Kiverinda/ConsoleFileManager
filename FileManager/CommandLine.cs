using System;
using System.IO;
using System.Collections.Generic;


namespace FileManager
{
    public class CommandLine
    {
        public FilesPanel CurrentPanel { get; set; }
        public View CommandView { get; set; }
        public Stack<String> StackString { get; set; }

        public CommandLine(FilesPanel currentPanel)
        {
            CurrentPanel = currentPanel;
            CommandView = new View();
            StackString = new Stack<string>();
        }

        public void Parse()
        {
            string commandString = "";
            ConsoleKeyInfo key;
            do
            {
                key = CommandView.CommandLine(CurrentPanel.CurrentPath, commandString);
                if (key.Key == ConsoleKey.Escape)
                {
                    return;
                }
                if (key.Key == ConsoleKey.Enter)
                {
                    StackString.Push(commandString);
                    string[] command = commandString.Split(' ');
                    CheckCommand(command);
                    Desktop.Update();
                    CommandView.OldCursor(CurrentPanel);
                    commandString = "";
                    continue;
                }
                if (key.Key == ConsoleKey.UpArrow)
                {
                    commandString += CurrentPanel.CurrentPath;
                    continue;
                }
                if (key.Key == ConsoleKey.DownArrow)
                {
                    if(StackString.Count != 0)
                    {
                        commandString = StackString.Pop();
                    }
                    continue;
                }
                if (key.Key == ConsoleKey.LeftArrow)
                {
                    continue;
                }
                if (key.Key == ConsoleKey.RightArrow)
                {
                    continue;
                }
                if (key.Key == ConsoleKey.Backspace)
                {
                    if (commandString.Length > 0)
                    {
                        commandString = commandString.Remove(commandString.Length - 1);
                    }
                    else
                    {
                        continue;
                    }
                }
                else
                {
                    commandString += key.KeyChar;
                }
            } while (true);
        }

        public void CheckCommand(string[] command)
        {
            switch (command[0])
            {
                case "cd":
                    if (Directory.Exists(command[1]))
                    {
                        Desktop.ActivePanel.UpdatePath(command[1]);
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
                        Action.Tree(command[1]);
                        Console.ReadLine();
                    }
                    break;
                default:
                    return;
            }
        }
        
    }
}
