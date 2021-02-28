using System;
using System.Collections.Generic;
using System.Text;


namespace FileManager
{
    class CL_Enter : ICommand<ConsoleKeyInfo>                           
    {
        public bool CanExecute(ConsoleKeyInfo click)
        {
            return click.Key == ConsoleKey.Enter;
        }

        public bool Execute()
        {
            if (CommandLine.GetInstance().BufferCommands.Count >= CommandLine.GetInstance().SizeBuffer)
            {
                CommandLine.GetInstance().BufferCommands.RemoveAt(0);
            }
            CommandLine.GetInstance().BufferCommands.Add(CommandLine.GetInstance().Line);
            CommandLine.GetInstance().CommandNumberInBuffer = CommandLine.GetInstance().BufferCommands.Count;
            CommandLine.GetInstance().ListUserCommands = CustomMethods.SplitString(" ", CommandLine.GetInstance().Line);
            CommandLine.GetInstance().Action();
            CommandLine.GetInstance().Line = "";
            CommandLine.GetInstance().CursorPositionInLine = CommandLine.GetInstance().Line.Length;
            Desktop.GetInstance().Update();
            CommandLine.GetInstance().CommandView.OldCursor(CommandLine.GetInstance().CurrentPanel);
            return false;
            //commandLine = "";
        }
    }
}
