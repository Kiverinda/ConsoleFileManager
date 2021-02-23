using System;
using System.Linq;


namespace FileManager
{
    public class Clear
    {
        private int WindowHight { get; set; }
        private int WindowWidth { get; set; }
        private int WidthPanel { get; set; }
        private int HightFilePanel { get; set; }
        private int RightBorder { get; set; }

        public Clear()
        {
            WindowHight = Console.WindowHeight;
            WindowWidth = Console.WindowWidth;
            WidthPanel = WindowWidth / 2;
            HightFilePanel = WindowHight - 5;
            RightBorder = WidthPanel - 2;
        }
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

        public void Message()
        {
            ColorTextAndBackground.InverseBase();
            Console.SetCursorPosition(WindowWidth / 3, WindowHight / 3);
            Console.Write("|");
            Console.Write(string.Concat(Enumerable.Repeat('*', WindowWidth / 3)));
            Console.Write("|");
            for (int i = WindowHight / 3 + 1; i < WindowHight / 3 + 7; i++)
            {
                Console.SetCursorPosition(WindowWidth / 3, i);
                Console.Write("|");
                Console.Write(string.Concat(Enumerable.Repeat(' ', WindowWidth / 3)));
                Console.Write("|");
            }
            Console.SetCursorPosition(WindowWidth / 3, WindowHight / 3 + 7);
            Console.Write("|");
            Console.Write(string.Concat(Enumerable.Repeat('*', WindowWidth / 3)));
            Console.Write("|");
            ColorTextAndBackground.Base();
        }


        public void Copy()
        {
            ColorTextAndBackground.InverseBase();
            Console.CursorVisible = false;
            Console.SetCursorPosition(WindowWidth / 4, WindowHight / 3);
            Console.Write(string.Concat(Enumerable.Repeat('*', WindowWidth / 2)));
            Console.SetCursorPosition(WindowWidth / 4, WindowHight / 3 + 8);
            Console.Write(string.Concat(Enumerable.Repeat('*', WindowWidth / 2)));

            for (int i = WindowHight / 3; i < WindowHight / 3 + 9; i++)
            {
                Console.SetCursorPosition(WindowWidth / 4, i);
                Console.Write("||");
                Console.SetCursorPosition(WindowWidth / 4 + WindowWidth / 2, i);
                Console.Write("||");
            }
            for (int i = WindowHight / 3 + 1; i < WindowHight / 3 + 8; i++)
            {
                Console.SetCursorPosition(WindowWidth / 4 + 2, i);
                Console.Write(string.Concat(Enumerable.Repeat(' ', WindowWidth / 2 - 2)));
            }
            ColorTextAndBackground.Base();
        }

        public void CommandLine()
        {
            Console.SetCursorPosition(0, WindowHight - 5);
            Console.Write(string.Concat(Enumerable.Repeat('*', WindowWidth)));
            Console.SetCursorPosition(0, WindowHight - 4);
            Console.Write(string.Concat(Enumerable.Repeat(' ', WindowWidth)));
            Console.SetCursorPosition(0, WindowHight - 3);
            Console.Write(string.Concat(Enumerable.Repeat('*', WindowWidth)));
        }
        
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
