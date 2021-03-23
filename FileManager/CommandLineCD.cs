using System;
using System.IO;

namespace FileManager
{
    /// <summary>
    /// Класс для перехода в указанную директорию
    /// </summary>
    public class CommandLineCD : ICommand<string>
    {
        
        /// <summary>
        /// Сравнение входящей строки с контрольной строкой
        /// </summary>
        /// <param name="value">Код горячей клавиши</param>
        /// <returns>true or false</returns>
        public bool CanExecute(string value)
        {
            return value.ToLower() == "cd";
        }

        /// <summary>
        /// Переход в указанную директорию
        /// </summary>
        public bool Execute()
        {
            CommandLine commandLine = CommandLine.GetInstance();
            try
            {
                if (Directory.Exists(commandLine.ListUserCommands[1]))
                {
                    Desktop.GetInstance().ActivePanel.UpdatePath(commandLine.ListUserCommands[1]);
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
