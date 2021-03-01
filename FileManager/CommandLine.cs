using System;
using System.Collections.Generic;


namespace FileManager
{
    public class CommandLine
    {
        private static CommandLine instance;
        public FilesPanel CurrentPanel { get; set; }
        public readonly int SizeBuffer = 5;
        public List<String> BufferCommands { get; set; }
        public int CommandNumberInBuffer { get; set; }
        public List<String> ListUserCommands { get; set; }
        public List<ICommand<ConsoleKeyInfo>> KeyManagement { get; set; }
        public List<ICommand<String>> Commands { get; set; }
        public string Line { get; set; }
        public int CursorPositionInLine { get; set; }

        public static CommandLine GetInstance()
        {
            if (instance == null)
            {
                instance = new CommandLine();
            }
            return instance;
        }

        public CommandLine()
        {
            CurrentPanel = Desktop.GetInstance().ActivePanel;
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
                new CL_CD(),
                new CL_Copy(),
                new CL_Rename(),
                new CL_Delete()
            };

        }

        public void Management()
        {
            ConsoleKeyInfo click;
            bool quit = false;
            Line = "";
            CurrentPanel = Desktop.GetInstance().ActivePanel;
            CursorPositionInLine = Line.Length;
            CommandNumberInBuffer = BufferCommands.Count - 1;
            while (!quit)
            {
                new View().CommandLine(CurrentPanel.CurrentPath, Line, CursorPositionInLine);
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
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                new ErrorLog(this, ex.Message, ex.StackTrace);
            }
        }
    }
}
