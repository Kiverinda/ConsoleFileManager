using System;
using System.Collections.Generic;
using System.Text;

namespace FileManager
{
    /// <summary>
    /// Класс для перемещения курсора на одну позицию вверх
    /// </summary>
    public class UpArrow : ICommand
    {
        /// <summary>
        /// Проверка условия для выполнения метода Execute
        /// </summary>
        /// <param name="click">Информация о нажатой клавише</param>
        /// <returns>true or false</returns>
        public bool CanExecute(ConsoleKeyInfo click)
        {
            return click.Key == ConsoleKey.UpArrow;
        }

        /// <summary>
        /// Перемещение курсора на одну позицию вверх
        /// </summary>
        /// <returns>true or false</returns>
        public bool Execute()
        {
            FilesPanel panel = Desktop.GetInstance().ActivePanel;
            View view = new View();
            Clear clear = new Clear();
            if (panel.AbsoluteCursorPosition == 0)
            {
                return false;
            }
            else if (panel.RelativeCursorPosition > 0)
            {
                view.OldCursor(panel);
                panel.AbsoluteCursorPosition--;
                panel.RelativeCursorPosition--;
                view.CurrentCursor(panel);
            }
            else if (panel.RelativeCursorPosition <= 0)
            {
                panel.AbsoluteCursorPosition--;
                panel.FirstLineWhenScrolling--;
                clear.FPanel(panel);
                view.FPanel(panel);
                view.CurrentCursor(panel);
            }
            return false;
        }
    }
}
