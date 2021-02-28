using System;


namespace FileManager
{
    class CL_UpArrow : ICommand<ConsoleKeyInfo>
    {
        public bool CanExecute(ConsoleKeyInfo click)
        {
            return click.Key == ConsoleKey.UpArrow;
        }

        public bool Execute()
        {
            if (CommandLine.GetInstance().BufferCommands.Count != 0)
            {
                if (CommandLine.GetInstance().CommandNumberInBuffer < CommandLine.GetInstance().BufferCommands.Count - 1)
                {
                    CommandLine.GetInstance().CommandNumberInBuffer++;
                    CommandLine.GetInstance().Line = CommandLine.GetInstance().BufferCommands[CommandLine.GetInstance().CommandNumberInBuffer];
                    CommandLine.GetInstance().CursorPositionInLine = CommandLine.GetInstance().Line.Length;
                }
            }

            return false;
        }
    }
}