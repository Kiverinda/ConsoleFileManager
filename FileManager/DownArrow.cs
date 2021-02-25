using System;
using System.Collections.Generic;
using System.Text;

namespace FileManager
{
    /// <summary>
    /// Класс для перемещения курсора на одну позицию вниз
    /// </summary>
    public class DownArrow : ICommand
    {
        /// <summary>
        /// Проверка условия для выполнения метода Execute
        /// </summary>
        /// <param name="click">Информация о нажатой клавише</param>
        /// <returns>true or false</returns>
        public bool CanExecute(ConsoleKeyInfo click)
        {
            return click.Key == ConsoleKey.DownArrow;
        }

        /// <summary>
        /// Перемещение курсора на одну позицию вниз
        /// </summary>
        /// <returns>true or false</returns>
        public bool Execute()
        {
            FilesPanel panel = Desktop.GetInstance().ActivePanel;
            View view = new View();
            Clear clear = new Clear();
            int windowFileHight = Console.WindowHeight - 9;

            if (panel.AbsoluteCursorPosition >= Desktop.GetInstance().ActivePanel.CurrentListDirAndFiles.Count - 1)
            {
                return false;
            }
            else if (panel.AbsoluteCursorPosition < windowFileHight + panel.FirstLineWhenScrolling)
            {
                view.OldCursor(panel);
                panel.AbsoluteCursorPosition++;
                panel.RelativeCursorPosition++;
                view.CurrentCursor(panel);
            }
            else if (panel.AbsoluteCursorPosition >= windowFileHight + panel.FirstLineWhenScrolling)
            {
                panel.AbsoluteCursorPosition++;
                panel.FirstLineWhenScrolling++;
                clear.FPanel(panel);
                view.FPanel(panel);
                view.CurrentCursor(panel);
            }
            return false;
        }
    }
}
