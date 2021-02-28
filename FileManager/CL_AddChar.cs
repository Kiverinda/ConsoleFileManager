using System;
using System.Collections.Generic;
using System.Text;

namespace FileManager
{
    class CL_AddChar : ICommand<ConsoleKeyInfo>
    {
        ConsoleKeyInfo Click { get; set; }
        
        public bool CanExecute(ConsoleKeyInfo click)
        {
            Click = click;
            
            return true;
        }

        public bool Execute()
        {
            if (CommandLine.GetInstance().CursorPositionInLine == CommandLine.GetInstance().Line.Length)
            {
                CommandLine.GetInstance().Line += Click.KeyChar;
                CommandLine.GetInstance().CursorPositionInLine += 1;
            }
            else if (CommandLine.GetInstance().CursorPositionInLine < CommandLine.GetInstance().Line.Length)
            {
                CommandLine.GetInstance().Line = CommandLine.GetInstance().Line.Insert(CommandLine.GetInstance().CursorPositionInLine,
                    Click.KeyChar.ToString());
                CommandLine.GetInstance().CursorPositionInLine += 1;
            }

            return false;
        }
    }
}
