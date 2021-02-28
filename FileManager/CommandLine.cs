using System;
using System.IO;
using System.Collections.Generic;


namespace FileManager
{
    public class CommandLine
    {
        private static CommandLine instance;
        public FilesPanel CurrentPanel { get; set; }
        public View CommandView { get; set; }
        public readonly int SizeBuffer = 5;
        public List<String> BufferCommands { get; set; }
        public List<String> ListUserCommands { get; set; }
        public List<ICommand<ConsoleKeyInfo>> KeyManagement { get; set; }
        public List<ICommand<String>> Commands { get; set; }
        public string Line { get; set; }
        public int CommandNumberInBuffer { get; set; }
        public int CursorPositionInLine { get; set; }

        public static CommandLine GetInstance()
        {
            if (instance == null)
            {
                instance = new CommandLine();
            }
            return instance;
        }

        public void ClearData()
        {
            CurrentPanel = Desktop.GetInstance().ActivePanel;
            BufferCommands.Clear();
            CommandNumberInBuffer = 0;
            CursorPositionInLine = 0;
            Line = "";
            ListUserCommands.Clear();
        }

        public CommandLine()
        {
            CurrentPanel = Desktop.GetInstance().ActivePanel;
            CommandView = new View();
            SizeBuffer = 5;
            BufferCommands = new List<string>(SizeBuffer);
            CommandNumberInBuffer = 0;
            CursorPositionInLine = 0;
            Line = "";
            ListUserCommands = new List<string>();
            KeyManagement = new List<ICommand<ConsoleKeyInfo>>()
            {
                new CL_Escape(),
                new CL_AddPath(),
                new CL_Enter(),
                new CL_DownArrow(),
                new CL_UpArrow(),
                new CL_LeftArrow(),
                new CL_RightArrow(),
                new CL_Backspace(),
                new CL_AddChar()

            };
            Commands = new List<ICommand<String>>()
            {

            };

        }

        public void Management()
        {
            ConsoleKeyInfo click;
            bool quit = false;
            Line = "";
            CursorPositionInLine = Line.Length;
            CommandNumberInBuffer = BufferCommands.Count - 1;
            while (!quit)
            {
                CommandView.CommandLine(CurrentPanel.CurrentPath, Line, CursorPositionInLine);
                click = Console.ReadKey(true);
                foreach (ICommand<ConsoleKeyInfo> command in KeyManagement)
                {
                    if (command.CanExecute(click))
                    {
                        quit = command.Execute();
                        break;
                    }
                }
            }
        }

        public void Action()
        {
            try
            {
                foreach (ICommand<String> command in Commands)
                {
                    if (command.CanExecute(ListUserCommands[0]))
                    {
                        command.Execute();
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                new ErrorLog(this, ex.Message, ex.StackTrace);
            }
        }

        //public void CheckCommand(List<string> command)
        //{
        //    try
        //    {
        //        switch (command[0])
        //        {
        //            case "cd":
        //                if (Directory.Exists(command[1]))
        //                {
        //                    Desktop.GetInstance().ActivePanel.UpdatePath(command[1]);
        //                }
        //                break;
        //            case "copy":
        //                CommandLineAction claCopy = new CommandLineAction(command[1], command[2]);
        //                claCopy.CL_Copy();
        //                break;
        //            case "rename":
        //                CommandLineAction claRename = new CommandLineAction(command[1]);
        //                claRename.CL_Rename();
        //                break;
        //            case "delete":
        //                CommandLineAction claDelete = new CommandLineAction(command[1]);
        //                claDelete.CL_Delete();
        //                break;
        //            case "tree":
        //                if (Directory.Exists(command[1]))
        //                {
        //                    new Tree().TreeFilesAndDirectory(command[1]);
        //                    Desktop.GetInstance().Update();
        //                }
        //                break;
        //            default:
        //                return;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        new ErrorLog(this, ex.Message, ex.StackTrace);
        //    }

        //}

    }
}
