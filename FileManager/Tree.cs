using System;
using System.Collections.Generic;

namespace FileManager
{

    /// <summary>
    ///  Класс построения дерева файлов и дирректорий
    /// </summary>
    public class Tree : ICommand
    {

        /// <summary>
        /// Проверка условия для выполнения метода Execute
        /// </summary>
        /// <param name="click">Информация о нажатой клавише</param>
        /// <returns>true or false</returns>
        public bool CanExecute(ConsoleKeyInfo click)
        {
            return click.Key == ConsoleKey.F9;
        }

        /// <summary>
        /// Получение пути к целевой директории  
        /// </summary>
        /// <returns></returns>
        public bool Execute()
        {
            int cursorPosition = Desktop.GetInstance().ActivePanel.AbsoluteCursorPosition;
            string path = Desktop.GetInstance().ActivePanel.CurrentListDirAndFiles[cursorPosition].Path;
            TreeFilesAndDirectory(path);
            Desktop.GetInstance().Update();
            return false;
        }

        /// <summary>
        /// Получение данных о целевой директории
        /// </summary>
        /// <param name="path"></param>
        public void TreeFilesAndDirectory(string path)
        {
            RequestToDisk request = new RequestToDisk(path);
            View view = new View();
            view.Message("ИДЕТ ПОСТРОЕНИЕ ДЕРЕВА");
            List<Attributes> source = request.GetListDirectoryAndFiles();
            view.FPanel(Desktop.GetInstance().ActivePanel);

            Navigation(GetPanelToView(), source);
        }

        /// <summary>
        /// Файловая панель, вместо которой отрисовывается дерево
        /// </summary>
        /// <returns>Файловая панель</returns>
        public FilesPanel GetPanelToView()
        {
            if (Desktop.GetInstance().ActivePanel.IsLeftPanel)
            {
                return Desktop.GetInstance().RightPanel;
            }
            else
            {
                return Desktop.GetInstance().LeftPanel;
            }
        }

        /// <summary>
        /// Постраничная отрисовка дерева с навигацией по страницам
        /// </summary>
        /// <param name="viewPanel">Файловая панель для отрисовки</param>
        /// <param name="source">Список файлов и директорий</param>
        public void Navigation(FilesPanel viewPanel, List<Attributes> source)
        {
            int countView = Console.WindowHeight - 9;
            int startPosition = 0;
            ConsoleKeyInfo click;
            Clear clear = new Clear();

            while (true)
            {
                clear.FPanel(viewPanel);
                new View().Tree(viewPanel, source, startPosition);
                click = Console.ReadKey();
                if (click.Key == ConsoleKey.Escape)
                {
                    return;
                }
                if (click.Key == ConsoleKey.DownArrow)
                {
                    if (startPosition + countView < source.Count)
                    {
                        startPosition += countView;
                    }
                }
                if (click.Key == ConsoleKey.UpArrow)
                {
                    if (startPosition >= countView)
                    {
                        startPosition -= countView;
                    }
                }

            }
        }

    }
}
