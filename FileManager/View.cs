using System;
using System.Globalization;
using System.Linq;

namespace FileManager
{
    public class View
    {
        private int WindowHeight { get; set; }
        private int WindowWidth { get; set; }
        private int MarginTop_For_StringPath { get; set; }
        private int MarginTop_For_WindowFiles { get; set; }
        private int WindowFileHeight { get; set; }
        private Clear CurrentClear;


        public View()
        {
            CurrentClear = new Clear();
            WindowHeight = Console.WindowHeight;
            WindowWidth = Console.WindowWidth;
            MarginTop_For_StringPath = 1;
            MarginTop_For_WindowFiles = 3;
            WindowFileHeight = WindowHeight - 9;
            Console.BufferHeight = Console.WindowHeight;
            Console.BufferWidth = Console.WindowWidth;
            Console.CursorVisible = false;
        }
        
        public void FPanel(FilesPanel panel)
        {
            CurrentClear.FPanel(panel);
            Console.SetWindowPosition(0, 0);
            Console.SetCursorPosition(MarginLeft(panel), MarginTop_For_StringPath);
            Console.Write(panel.CurrentPath);
            int i = panel.FirstLineWhenScrolling;
            int positionInFilePanel = MarginTop_For_WindowFiles;
            while (i < WindowFileHeight + panel.FirstLineWhenScrolling + 1 && i < panel.CurrentListDirAndFiles.Count)
            {
                Console.SetCursorPosition(MarginLeft(panel), positionInFilePanel);
                if (panel.CurrentListDirAndFiles[i].IsFile)
                {
                    ColorTextAndBackground.ForFile();
                    Console.Write(panel.CurrentListDirAndFiles[i].Name);
                    Console.SetCursorPosition(MarginLeft(panel) + WindowWidth * 3 / 8, positionInFilePanel);
                    string size = (panel.CurrentListDirAndFiles[i].Size).ToString("#,#", CultureInfo.InvariantCulture);
                    Console.Write($"|  {size}");
                    ColorTextAndBackground.Base();
                }
                else
                {
                    Console.Write(panel.CurrentListDirAndFiles[i].Name);
                }
                positionInFilePanel++;
                i++;
            }
        }

        public void CurrentCursor(FilesPanel panel)
        {
            
            Console.CursorVisible = false;
            int startPositionFilePanel = 3;
            string nameFile = panel.CurrentListDirAndFiles[panel.AbsoluteCursorPosition].Name;
            Console.SetCursorPosition(MarginLeft(panel), panel.RelativeCursorPosition + startPositionFilePanel);

            if (panel.BufferSelectedPositionCursor.Contains(panel.AbsoluteCursorPosition))
            {
                ColorTextAndBackground.InverseSelect();
                Console.Write(nameFile);
                ColorTextAndBackground.ForFile();
            }
            else
            {
                if (panel.CurrentListDirAndFiles[panel.AbsoluteCursorPosition].IsFile)
                {
                    ColorTextAndBackground.InverseForFile();
                    Console.Write(nameFile);
                    ColorTextAndBackground.ForFile();
                }
                else
                {
                    ColorTextAndBackground.InverseBase();
                    Console.Write(nameFile);
                    ColorTextAndBackground.Base();
                }
            }

        }

        public void OldCursor(FilesPanel panel)
        {
            Console.CursorVisible = false;
            int positionInFilePanel = MarginTop_For_WindowFiles;
            string nameFile = panel.CurrentListDirAndFiles[panel.AbsoluteCursorPosition].Name;
            Console.SetCursorPosition(MarginLeft(panel), panel.RelativeCursorPosition + positionInFilePanel);
            if (panel.BufferSelectedPositionCursor.Contains(panel.AbsoluteCursorPosition))
            {
                ColorTextAndBackground.Select();
                Console.Write(nameFile);
                ColorTextAndBackground.ForFile();
            }
            else
            {
                if (panel.CurrentListDirAndFiles[panel.AbsoluteCursorPosition].IsFile)
                {
                    ColorTextAndBackground.ForFile();
                    Console.Write(nameFile);
                }
                else
                {
                    ColorTextAndBackground.Base();
                    Console.Write(nameFile);
                }
            }
        }

        public void SelectDrive(FilesPanel panel, int positionCursor)
        {
            FileAttributes[] allDrives = new RequestToDisk().AllDrives();
            int count = 0;
            ColorTextAndBackground.InverseBase();
            Console.CursorVisible = false;
            Console.SetCursorPosition(MarginLeft(panel) + 25, MarginTop_For_WindowFiles + 3);
            Console.Write(string.Concat(Enumerable.Repeat('*', MarginTop_For_WindowFiles + 8)));

            while (count < allDrives.Length)
            {
                Console.SetCursorPosition(MarginLeft(panel) + 25, MarginTop_For_WindowFiles + 4 + count);
                Console.Write("||");
                Console.Write(string.Concat(Enumerable.Repeat(' ', MarginTop_For_WindowFiles / 2)));
                Console.Write($" {allDrives[count].Name} ");
                Console.Write(string.Concat(Enumerable.Repeat(' ', MarginTop_For_WindowFiles / 2)));
                Console.Write("||");
                count++;
            }
            Console.SetCursorPosition(MarginLeft(panel) + 25, MarginTop_For_WindowFiles + 4 + count);
            Console.Write(string.Concat(Enumerable.Repeat('*', MarginTop_For_WindowFiles + 8)));

            Console.SetCursorPosition(MarginLeft(panel) + 29, MarginTop_For_WindowFiles + 4 + positionCursor);
            ColorTextAndBackground.Base();
            Console.Write($"{allDrives[positionCursor].Name}");

            ColorTextAndBackground.Base();
        }

        public void SelectedItems(FilesPanel panel)
        {
            Console.CursorVisible = false;
            int startPositionFilePanel = 3;
            foreach(int item in panel.BufferSelectedPositionCursor)
            {
                if (item >= panel.FirstLineWhenScrolling)
                {
                    string nameFile = panel.CurrentListDirAndFiles[item].Name;
                    Console.SetCursorPosition(MarginLeft(panel), item - panel.FirstLineWhenScrolling + startPositionFilePanel);
                    if (panel.CurrentListDirAndFiles[item].IsFile)
                    {
                        ColorTextAndBackground.Select();
                        Console.Write(nameFile);
                        ColorTextAndBackground.ForFile();
                    }
                    else
                    {
                        ColorTextAndBackground.Select();
                        Console.Write(nameFile);
                        ColorTextAndBackground.Base();
                    }
                }
            }
            CurrentCursor(panel);
        }

        public string MessageCreate(string message)
        {
            CurrentClear.Message();
            ColorTextAndBackground.InverseBase();
            Console.SetCursorPosition(WindowWidth / 3 + 5, WindowHeight / 3 + 2);
            Console.Write(message);
            Console.SetCursorPosition(WindowWidth / 3 + 5, WindowHeight / 3 + 4);
            Console.CursorVisible = true;
            string nameFile = Console.ReadLine();
            ColorTextAndBackground.Base();
            Console.CursorVisible = false;
            return nameFile;
        }
        public void Message(string message)
        {
            CurrentClear.Message();
            ColorTextAndBackground.InverseBase();
            Console.SetCursorPosition(WindowWidth / 3 + 5, WindowHeight / 3 + 2);
            Console.Write(message);
            ColorTextAndBackground.Base();
        }

        public void Confirmation(string message, string path)
        { 
            CurrentClear.Message();
            ColorTextAndBackground.InverseBase();
            Console.SetCursorPosition(WindowWidth / 3 + 5, WindowHeight / 3 + 2);
            Console.Write(message);
            Console.SetCursorPosition(WindowWidth / 3 + 5, WindowHeight / 3 + 3);
            Console.Write(path);
            Console.SetCursorPosition(WindowWidth / 3 + 15, WindowHeight / 3 + 5);
            Console.Write("Y  Да               N Нет");
            ColorTextAndBackground.Base();
        }

        public ConsoleKeyInfo CommandLine(string path, string command)
        {
            CurrentClear.CommandLine();
            Console.CursorVisible = true;
            Console.SetCursorPosition(2, WindowHeight - 4);
            Console.Write($"{path} ");
            Console.Write(command);
            ConsoleKeyInfo key = Console.ReadKey(true);
            Console.CursorVisible = false;
            return key;
        }

        public void Footer()
        {
            CurrentClear.CommandLine();
            Menu();
        }

        public void Menu() { 
            ColorTextAndBackground.Base();
            Console.SetCursorPosition(0, WindowHeight - 2);
            Console.Write("|  F1 ПОМОЩЬ  ");
            Console.Write("|  F2 ФАЙЛ  ");
            Console.Write("|  F3 РЕДАКТИРОВАНИЕ  ");
            Console.Write("|  F4 ПЕРЕИМЕНОВАТЬ  ");
            Console.Write("|  F5 КОПИРОВАНИЕ  ");
            Console.Write("|  F6 ПЕРЕМЕЩЕНИЕ  ");
            Console.Write("|  F7 КАТАЛОГ   ");
            Console.Write("|  F8 УДАЛЕНИЕ  ");
            Console.Write("|  F9 ДЕРЕВО   ");
            Console.Write("|  ESC ВЫХОД   ");
            Console.Write("|");
            Console.SetCursorPosition(0, WindowHeight - 1);
            Console.Write(string.Concat(Enumerable.Repeat('*', WindowWidth - 1)));
            Console.SetCursorPosition(0, 0);
        }

        public void Copy(string currentPath, string targetPath)
        {
            CurrentClear.Copy();
            ColorTextAndBackground.InverseBase();
            Console.CursorVisible = false;
            Console.SetCursorPosition(WindowWidth / 4 + 5, WindowHeight / 3 + 1);
            Console.Write("КОПИРОВАНИЕ: ");
            Console.SetCursorPosition(WindowWidth / 4 + 5, WindowHeight / 3 + 2);
            Console.Write(currentPath + " -> ");
            Console.SetCursorPosition(WindowWidth / 4 + 5, WindowHeight / 3 + 3);
            Console.Write(targetPath);
            ColorTextAndBackground.Base();
        }

        public void CopyPersentage(double persentage)
        {
            ColorTextAndBackground.InverseBase();
            Console.SetCursorPosition(WindowWidth / 4 + 5, WindowHeight / 3 + 5);
            Console.Write($"Выполненно: {(int)persentage} %");
            ColorTextAndBackground.Base();
            CopyLinePersentage(persentage);
        }

        public void CopyLinePersentage(double persentage)
        {
            Console.SetCursorPosition(WindowWidth / 4 + 5, WindowHeight / 3 + 7);
            Console.Write("[");
            Console.Write(string.Concat(Enumerable.Repeat('-', WindowWidth / 2 - 10)));
            Console.Write("]");
            Console.SetCursorPosition(WindowWidth / 4 + 6, WindowHeight / 3 + 7);
            ColorTextAndBackground.Base();
            int viewPersentage = (int)((WindowWidth / 2 - 10) * persentage / 100);
            Console.Write(string.Concat(Enumerable.Repeat(' ', viewPersentage)));
            Console.CursorVisible = false;
        }

        public void Tree(FilesPanel panel, string path, int count)
        {
            ////CurrentClear.FPanel(panel);
            //Console.SetCursorPosition(MarginLeft(panel), MarginTop_For_WindowFiles);
            //Console.Write(path);
            if (count < WindowFileHeight)
            {
                Console.SetCursorPosition(MarginLeft(panel), MarginTop_For_WindowFiles + count);
                Console.Write(path);
            }
        }
        
        public int MarginLeft(FilesPanel panel)
        {
            int margin;
            if (panel.IsLeftPanel)
            {
                margin = 2;
            }
            else
            {
                margin = WindowWidth / 2 + 2;
            }
            return margin;
        }

    }
}
