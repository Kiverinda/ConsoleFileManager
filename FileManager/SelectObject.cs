using System;
using System.Collections.Generic;
using System.Text;

namespace FileManager
{
    
    /// <summary>
    /// Класс выбора обьекта
    /// </summary>
    public class SelectObject : ICommand
    {

        /// <summary>
        /// Проверка условия для выполнения метода Execute
        /// </summary>
        /// <param name="click">Информация о нажатой клавише</param>
        /// <returns>true or false</returns>
        public bool CanExecute(ConsoleKeyInfo click)
        {
            return click.Key == ConsoleKey.Spacebar;
        }

        /// <summary>
        /// Добавление абсолютной позиции курсора в буфер панели 
        /// </summary>
        /// <returns>Выход из программы</returns>
        public bool Execute()
        {
            View view = new View();
            int currentPositionCursor = Desktop.GetInstance().ActivePanel.AbsoluteCursorPosition;
            Desktop.GetInstance().ActivePanel.AddBufferSelectedPositionCursor(currentPositionCursor);
            view.SelectedItems(Desktop.GetInstance().ActivePanel);
            return false;
        }
    }
}
