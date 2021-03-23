using System;
using System.Globalization;
using System.Linq;
using System.Collections.Generic;

namespace FileManager
{
    /// <summary>
    /// Класс для вывода информации на экран
    /// </summary>
    public class View
    {
        /// <summary>
        /// Высота окна приложения
        /// </summary>
        public int WindowHeight { get; set; }

        /// <summary>
        /// Ширина окна приложения
        /// </summary>
        public int WindowWidth { get; set; }

        /// <summary>
        /// Отступ сверху до строки с путем к директории
        /// </summary>
        public int MarginTopForStringPath { get; set; }

        /// <summary>
        /// Отступ сверху до файловой панели
        /// </summary>
        public int MarginTopForWindowFiles { get; set; }

        /// <summary>
        /// Высота файловой панели
        /// </summary>
        public int WindowFileHeight { get; set; }

        /// <summary>
        /// Экзепляр класса с шаблонами окон
        /// </summary>
        public Clear CurrentClear { get; set; }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public View()
        {
            CurrentClear = new Clear();
            WindowHeight = Console.WindowHeight;
            WindowWidth = Console.WindowWidth;
            MarginTopForStringPath = 1;
            MarginTopForWindowFiles = 3;
            WindowFileHeight = WindowHeight - 9;
            Console.BufferHeight = Console.WindowHeight;
            Console.BufferWidth = Console.WindowWidth;
            Console.CursorVisible = false;
        }

        /// <summary>
        /// Вывод файловой панели на экран
        /// </summary>
        /// <param name="panel"></param>
        public void FPanel(FilesPanel panel)
        {
            CurrentClear.FPanel(panel);
            Console.SetWindowPosition(0, 0);
            Console.SetCursorPosition(MarginLeft(panel), MarginTopForStringPath);
            Console.Write(panel.CurrentPath);
            int i = panel.FirstLineWhenScrolling;
            int positionInFilePanel = MarginTopForWindowFiles;
            while (i < WindowFileHeight + panel.FirstLineWhenScrolling + 1 && i < panel.CurrentListDirAndFiles.Count)
            {
                Console.SetCursorPosition(MarginLeft(panel), positionInFilePanel);
                if (panel.CurrentListDirAndFiles[i].IsFile)
                {
                    ColorTextAndBackground.ForFile();
                    Console.Write(Substring(panel.CurrentListDirAndFiles[i].Name));
                    Console.SetCursorPosition(MarginLeft(panel) + WindowWidth * 1 / 3, positionInFilePanel);
                    Console.Write($"| {panel.CurrentListDirAndFiles[i].Extension}  ");
                    Console.SetCursorPosition(MarginLeft(panel) + WindowWidth * 5 / 13, positionInFilePanel);
                    string size = (panel.CurrentListDirAndFiles[i].Size).ToString("#,#", CultureInfo.InvariantCulture);
                    Console.Write($"| {size}  ");
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

        /// <summary>
        /// Вывод на экран текущего положения курсора
        /// </summary>
        /// <param name="panel"></param>
        public void CurrentCursor(FilesPanel panel)
        {

            Console.CursorVisible = false;
            int startPositionFilePanel = 3;
            string name = Substring(panel.CurrentListDirAndFiles[panel.AbsoluteCursorPosition].Name);
            string extension = panel.CurrentListDirAndFiles[panel.AbsoluteCursorPosition].Extension;
            string size = (panel.CurrentListDirAndFiles[panel.AbsoluteCursorPosition].Size).ToString("#,#", CultureInfo.InvariantCulture);
            if (panel.BufferSelectedPositionCursor.Contains(panel.AbsoluteCursorPosition))
            {
                Console.SetCursorPosition(MarginLeft(panel), panel.RelativeCursorPosition + startPositionFilePanel);
                ColorTextAndBackground.InverseSelect();
                Console.Write(name);
                ColorTextAndBackground.ForFile();
            }
            else
            {
                if (panel.CurrentListDirAndFiles[panel.AbsoluteCursorPosition].IsFile)
                {
                    Console.SetCursorPosition(MarginLeft(panel), panel.RelativeCursorPosition + startPositionFilePanel);
                    ColorTextAndBackground.InverseForFile();
                    Console.Write(name);
                    Console.SetCursorPosition(MarginLeft(panel) + WindowWidth * 1 / 3, panel.RelativeCursorPosition + startPositionFilePanel);
                    Console.Write($"| {extension}   ");
                    Console.SetCursorPosition(MarginLeft(panel) + WindowWidth * 5 / 13, panel.RelativeCursorPosition + startPositionFilePanel);
                    Console.Write($"| {size}");
                    ColorTextAndBackground.ForFile();
                }
                else
                {
                    Console.SetCursorPosition(MarginLeft(panel), panel.RelativeCursorPosition + startPositionFilePanel);
                    ColorTextAndBackground.InverseBase();
                    Console.Write(name);
                    ColorTextAndBackground.Base();
                }
            }

        }

        /// <summary>
        /// Вывод на экран старого положения курсора
        /// </summary>
        /// <param name="panel"></param>
        public void OldCursor(FilesPanel panel)
        {
            Console.CursorVisible = false;
            int positionInFilePanel = MarginTopForWindowFiles;
            string name = Substring(panel.CurrentListDirAndFiles[panel.AbsoluteCursorPosition].Name);
            string extension = panel.CurrentListDirAndFiles[panel.AbsoluteCursorPosition].Extension;
            string size = (panel.CurrentListDirAndFiles[panel.AbsoluteCursorPosition].Size).ToString("#,#", CultureInfo.InvariantCulture);
            if (panel.BufferSelectedPositionCursor.Contains(panel.AbsoluteCursorPosition))
            {
                ColorTextAndBackground.Select();
                Console.SetCursorPosition(MarginLeft(panel), panel.RelativeCursorPosition + positionInFilePanel);
                Console.Write(name);
                ColorTextAndBackground.ForFile();
            }
            else
            {
                if (panel.CurrentListDirAndFiles[panel.AbsoluteCursorPosition].IsFile)
                {
                    ColorTextAndBackground.ForFile();
                    Console.SetCursorPosition(MarginLeft(panel), panel.RelativeCursorPosition + positionInFilePanel);
                    Console.Write(name);
                    Console.SetCursorPosition(MarginLeft(panel) + WindowWidth * 1 / 3, panel.RelativeCursorPosition + positionInFilePanel);
                    Console.Write($"| {extension}   ");
                    Console.SetCursorPosition(MarginLeft(panel) + WindowWidth * 5 / 13, panel.RelativeCursorPosition + positionInFilePanel);
                    Console.Write($"| {size}");
                }
                else
                {
                    ColorTextAndBackground.Base();
                    Console.SetCursorPosition(MarginLeft(panel), panel.RelativeCursorPosition + positionInFilePanel);
                    Console.Write(name);
                }
            }
        }

        /// <summary>
        /// Окно выбора диска
        /// </summary>
        /// <param name="panel"></param>
        /// <param name="positionCursor"></param>
        public void SelectDrive(FilesPanel panel, int positionCursor)
        {
            Attributes[] allDrives = new RequestToDisk().AllDrives();
            int count = 0;
            ColorTextAndBackground.InverseBase();
            Console.CursorVisible = false;
            Console.SetCursorPosition(MarginLeft(panel) + 25, MarginTopForWindowFiles + 3);
            Console.Write(string.Concat(Enumerable.Repeat('*', MarginTopForWindowFiles + 8)));

            while (count < allDrives.Length)
            {
                Console.SetCursorPosition(MarginLeft(panel) + 25, MarginTopForWindowFiles + 4 + count);
                Console.Write("||");
                Console.Write(string.Concat(Enumerable.Repeat(' ', MarginTopForWindowFiles / 2)));
                Console.Write($" {allDrives[count].Name} ");
                Console.Write(string.Concat(Enumerable.Repeat(' ', MarginTopForWindowFiles / 2)));
                Console.Write("||");
                count++;
            }
            Console.SetCursorPosition(MarginLeft(panel) + 25, MarginTopForWindowFiles + 4 + count);
            Console.Write(string.Concat(Enumerable.Repeat('*', MarginTopForWindowFiles + 8)));

            Console.SetCursorPosition(MarginLeft(panel) + 29, MarginTopForWindowFiles + 4 + positionCursor);
            ColorTextAndBackground.Base();
            Console.Write($"{allDrives[positionCursor].Name}");

            ColorTextAndBackground.Base();
        }

        /// <summary>
        /// Отображение выбранного элемента файловой панели
        /// </summary>
        /// <param name="panel"></param>
        public void SelectedItems(FilesPanel panel)
        {
            Console.CursorVisible = false;
            int startPositionFilePanel = 3;
            foreach (int item in panel.BufferSelectedPositionCursor)
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

        /// <summary>
        /// Вывод на экран окна создания файла или директории и получение имени от пользователя 
        /// </summary>
        /// <param name="message"></param>
        /// <returns>Имя файла или директории</returns>
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

        /// <summary>
        /// Вывод на экран окна с сообщением
        /// </summary>
        /// <param name="message"></param>
        public void Message(string message)
        {
            CurrentClear.Message();
            ColorTextAndBackground.InverseBase();
            Console.SetCursorPosition(WindowWidth / 3 + 5, WindowHeight / 3 + 2);
            Console.Write(message);
            ColorTextAndBackground.Base();
        }

        /// <summary>
        /// Окно подтверждения удаления
        /// </summary>
        /// <param name="message">Текст сообщения</param>
        /// <param name="path">Информация о нажатой клавише</param>
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

        /// <summary>
        /// Вывод текущего пути и команды в командную строку
        /// </summary>
        /// <param name="path">Текущий путь</param>
        /// <param name="command">Команда</param>
        /// <param name="positionCursor">Позиция курсора в командной строке</param>
        
        public void CommandLine(string path, string command, int positionCursor)
        {
            CurrentClear.CommandLine();
            Console.CursorVisible = true;
            Console.SetCursorPosition(2, WindowHeight - 4);
            Console.Write($"{path} ");
            Console.Write(command);
            Console.SetCursorPosition(2 + path.Length + positionCursor + 1, WindowHeight - 4);
        }

        /// <summary>
        /// Вывод нижней части окна приложения
        /// </summary>
        public void Footer()
        {
            CurrentClear.CommandLine();
            Menu();
        }

        /// <summary>
        /// Вывод меню
        /// </summary>
        public void Menu()
        {
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

        /// <summary>
        /// Вывод на экран окна копирования
        /// </summary>
        /// <param name="currentPath"></param>
        /// <param name="targetPath"></param>
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

        /// <summary>
        /// Вывод на экран числового представления прогресса копирования
        /// </summary>
        /// <param name="persentage">Проценты выполнения</param>
        public void CopyPersentage(double persentage)
        {
            ColorTextAndBackground.InverseBase();
            Console.SetCursorPosition(WindowWidth / 4 + 5, WindowHeight / 3 + 5);
            Console.Write($"Выполненно: {(int)persentage} %");
            ColorTextAndBackground.Base();
            CopyLinePersentage(persentage);
        }

        /// <summary>
        /// Вывод на экран прогрессбара копирования
        /// </summary>
        /// <param name="persentage">Проценты выполнения</param>
        public void CopyLinePersentage(double persentage)
        {
            ColorTextAndBackground.InverseBase();
            Console.SetCursorPosition(WindowWidth / 4 + 5, WindowHeight / 3 + 7);
            Console.Write("[");
            Console.Write(string.Concat(Enumerable.Repeat(' ', WindowWidth / 2 - 10)));
            Console.Write("]");
            Console.SetCursorPosition(WindowWidth / 4 + 6, WindowHeight / 3 + 7);
            ColorTextAndBackground.Base();
            int viewPersentage = (int)((WindowWidth / 2 - 10) * persentage / 100);
            Console.Write(string.Concat(Enumerable.Repeat(' ', viewPersentage)));
            Console.CursorVisible = false;
        }

        /// <summary>
        /// Вывод дерева файлов и директорий
        /// </summary>
        /// <param name="panel">Панель для отрисовки дерева</param>
        /// <param name="source">Список файлов и директорий</param>
        /// <param name="count">Количество элементов для отображения</param>
        public void Tree(FilesPanel panel, List<Attributes> source, int count)
        {
            for (int i = 0; i < WindowFileHeight; i++)
            {
                if(source.Count > (count + i))
                {
                    Console.SetCursorPosition(MarginLeft(panel), MarginTopForWindowFiles + i);
                    Console.Write(source[count + i].Path);
                }
            }

        }

        /// <summary>
        /// Вывод окна со справкой по приложению
        /// </summary>
        public void Help()
        {
            new Clear().Help();
        }

        /// <summary>
        /// Вычисление отступа слева
        /// </summary>
        /// <param name="panel">Файловая панель</param>
        /// <returns></returns>
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

        /// <summary>
        /// Сокращение длинных имен файлов и директорий
        /// </summary>
        /// <param name="str">Текущее имя</param>
        /// <returns>Сокращенное имя</returns>
        public string Substring(string str)
        {
            int length = WindowWidth / 4;
            if (str.Length > length)
            {
                return str.Substring(0, length) + "~~";
            }
            return str;
        }

    }
}
