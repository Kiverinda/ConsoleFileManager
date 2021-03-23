using System;
using System.Collections.Generic;


namespace FileManager
{
    /// <summary>
    /// Класс реализующий командную строку
    /// </summary>
    public class CommandLine
    {
        private static CommandLine instance;

        /// <summary>
        /// Текущий путь
        /// </summary>
        public FilesPanel CurrentPanel { get; set; }

        /// <summary>
        /// Размер буфера для введенных команд
        /// </summary>
        public readonly int SizeBuffer = 5;

        /// <summary>
        /// Буфер введенных комманд
        /// </summary>
        public List<String> BufferCommands { get; set; }

        /// <summary>
        /// Номер команды в буфере
        /// </summary>
        public int CommandNumberInBuffer { get; set; }

        /// <summary>
        /// Строка, введенная пользователем в командной строке
        /// </summary>
        public List<String> ListUserCommands { get; set; }

        /// <summary>
        /// Список для метода KeyManagement
        /// </summary>
        public List<ICommand<ConsoleKeyInfo>> KeyManagement { get; set; }

        /// <summary>
        /// Список для метода Comands
        /// </summary>
        public List<ICommand<String>> Comands { get; set; }

        /// <summary>
        /// Введенная пользователем строка
        /// </summary>
        public string Line { get; set; }

        /// <summary>
        /// Позиция курсора
        /// </summary>
        public int CursorPositionInLine { get; set; }

        /// <summary>
        /// Реализация шаблона singlton
        /// </summary>
        /// <returns></returns>
        public static CommandLine GetInstance()
        {
            if (instance == null)
            {
                instance = new CommandLine();
            }
            return instance;
        }

        /// <summary>
        /// Конструктор класса
        /// </summary>
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
                new CommandLineEscape(),
                new CommandLineAddPath(),
                new CommandLineEnter(),
                new CommandLineDownArrow(),
                new CommandLineUpArrow(),
                new CommandLineLeftArrow(),
                new CommandLineRightArrow(),
                new CommandLineBackspace(),
                new CommandLineAddChar()

            };
            Comands = new List<ICommand<String>>()
            {
                new CommandLineCD(),
                new CommandLineCopy(),
                new CommandLineRename(),
                new CommandLineDelete()
            };

        }

        /// <summary>
        /// Управление в командной строке
        /// </summary>
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

        /// <summary>
        /// Выбор метода в зависимости от введенной команды
        /// </summary>
        public void Action()
        {
            try
            {
                foreach (ICommand<String> command in Comands)
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
