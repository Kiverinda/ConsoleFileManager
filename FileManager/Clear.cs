using System;
using System.Linq;


namespace FileManager
{
    /// <summary>
    /// Класс для отрисовки рамок и пустых форм
    /// </summary>
    public class Clear
    {
        private int WindowHeight { get; set; }
        private int WindowWidth { get; set; }
        private int WidthPanel { get; set; }
        private int HightFilePanel { get; set; }
        private int RightBorder { get; set; }

        /// <summary>
        /// Конструктор класса 
        /// </summary>
        public Clear()
        {
            WindowHeight = Console.WindowHeight;
            WindowWidth = Console.WindowWidth;
            WidthPanel = WindowWidth / 2;
            HightFilePanel = WindowHeight - 5;
            RightBorder = WidthPanel - 2;
        }
        
        /// <summary>
        /// Отрисовка рамок одной файловой панели
        /// </summary>
        /// <param name="panel"></param>
        public void FPanel(FilesPanel panel)
        {
            ColorTextAndBackground.Base();
            Console.SetCursorPosition(MarginLeft(panel), 0);
            Console.Write("|");
            Console.Write(string.Concat(Enumerable.Repeat('*', RightBorder)));
            Console.Write("|");
            Console.SetCursorPosition(MarginLeft(panel), 1);
            Console.Write("|");
            Console.Write(string.Concat(Enumerable.Repeat(' ', RightBorder)));
            Console.Write("|");
            Console.SetCursorPosition(MarginLeft(panel), 2);
            Console.Write("|");
            Console.Write(string.Concat(Enumerable.Repeat('*', RightBorder)));
            Console.Write("|");
            for (int i = 3; i < HightFilePanel; i++)
            {
                Console.SetCursorPosition(MarginLeft(panel), i);
                Console.Write("|");
                Console.Write(string.Concat(Enumerable.Repeat(' ', RightBorder)));
                Console.Write("|");
            }

        }

        /// <summary>
        /// Отрисовка пустой формы для сообщений
        /// </summary>
        public void Message()
        {
            ColorTextAndBackground.InverseBase();
            Console.SetCursorPosition(WindowWidth / 3, WindowHeight / 3);
            Console.Write("|");
            Console.Write(string.Concat(Enumerable.Repeat('*', WindowWidth / 3)));
            Console.Write("|");
            for (int i = WindowHeight / 3 + 1; i < WindowHeight / 3 + 7; i++)
            {
                Console.SetCursorPosition(WindowWidth / 3, i);
                Console.Write("|");
                Console.Write(string.Concat(Enumerable.Repeat(' ', WindowWidth / 3)));
                Console.Write("|");
            }
            Console.SetCursorPosition(WindowWidth / 3, WindowHeight / 3 + 7);
            Console.Write("|");
            Console.Write(string.Concat(Enumerable.Repeat('*', WindowWidth / 3)));
            Console.Write("|");
            ColorTextAndBackground.Base();
        }

        /// <summary>
        /// Отрисовка рамок окна Help
        /// </summary>
        public void Help()
        {
            ColorTextAndBackground.InverseBase();
            Console.SetCursorPosition(WindowWidth / 4, WindowHeight / 5);
            Console.Write("|");
            Console.Write(string.Concat(Enumerable.Repeat('*', WindowWidth / 2)));
            Console.Write("|");
            for (int i = WindowHeight / 5 + 1; i < WindowHeight / 2 + 7; i++)
            {
                Console.SetCursorPosition(WindowWidth / 4, i);
                Console.Write("|");
                Console.Write(string.Concat(Enumerable.Repeat(' ', WindowWidth / 2)));
                Console.Write("|");
            }
            Console.SetCursorPosition(WindowWidth / 4, WindowHeight / 2 + 7);
            Console.Write("|");
            Console.Write(string.Concat(Enumerable.Repeat('*', WindowWidth / 2)));
            Console.Write("|");
            ColorTextAndBackground.Base();
        }

        /// <summary>
        /// Отрисовка рамок окна Copy
        /// </summary>
        public void Copy()
        {
            ColorTextAndBackground.InverseBase();
            Console.CursorVisible = false;
            Console.SetCursorPosition(WindowWidth / 4, WindowHeight / 3);
            Console.Write(string.Concat(Enumerable.Repeat('*', WindowWidth / 2)));
            Console.SetCursorPosition(WindowWidth / 4, WindowHeight / 3 + 8);
            Console.Write(string.Concat(Enumerable.Repeat('*', WindowWidth / 2)));

            for (int i = WindowHeight / 3; i < WindowHeight / 3 + 9; i++)
            {
                Console.SetCursorPosition(WindowWidth / 4, i);
                Console.Write("||");
                Console.SetCursorPosition(WindowWidth / 4 + WindowWidth / 2, i);
                Console.Write("||");
            }
            for (int i = WindowHeight / 3 + 1; i < WindowHeight / 3 + 8; i++)
            {
                Console.SetCursorPosition(WindowWidth / 4 + 2, i);
                Console.Write(string.Concat(Enumerable.Repeat(' ', WindowWidth / 2 - 2)));
            }
            ColorTextAndBackground.Base();
        }

        /// <summary>
        /// Отрисовка рамок командной строки
        /// </summary>
        public void CommandLine()
        {
            Console.SetCursorPosition(0, WindowHeight - 5);
            Console.Write(string.Concat(Enumerable.Repeat('*', WindowWidth)));
            Console.SetCursorPosition(0, WindowHeight - 4);
            Console.Write(string.Concat(Enumerable.Repeat(' ', WindowWidth)));
            Console.SetCursorPosition(0, WindowHeight - 3);
            Console.Write(string.Concat(Enumerable.Repeat('*', WindowWidth)));
        }

        /// <summary>
        /// Вычисление отступа слева
        /// </summary>
        /// <param name="panel">Текущая панель</param>
        /// <returns>Количество сомволов</returns>
        public int MarginLeft(FilesPanel panel)
        {
            int margin = 0;
            if (!panel.IsLeftPanel)
            {
                margin = WindowWidth / 2;
            }
            return margin;
        }
    }
}
