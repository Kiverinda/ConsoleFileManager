using System;
using System.IO;

namespace FileManager
{
    class CL_CD : ICommand<string>
    {
        public bool CanExecute(string value)
        {
            return value.ToLower() == "cd";
        }

        public bool Execute()
        {
            try
            {
                if (Directory.Exists(CommandLine.GetInstance().ListUserCommands[1]))
                {
                    Desktop.GetInstance().ActivePanel.UpdatePath(CommandLine.GetInstance().ListUserCommands[1]);
                }
            }
            catch (Exception ex)
            {
                new ErrorLog(this, ex.Message, ex.StackTrace);
            }
            return true;
        }
    }
}
