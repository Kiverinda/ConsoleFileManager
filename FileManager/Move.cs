using System;
using System.Collections.Generic;

namespace FileManager
{
    
    /// <summary>
    /// Класс переноса файла или директории
    /// </summary>
    public class Move : ICommand<ConsoleKeyInfo>
    {

        /// <summary>
        /// Проверка условия для выполнения метода Execute
        /// </summary>
        /// <param name="click">Информация о нажатой клавише</param>
        /// <returns>true or false</returns>
        public bool CanExecute(ConsoleKeyInfo click)
        {
            return click.Key == ConsoleKey.F6;
        }

        /// <summary>
        /// Копирование файлов или директорий и последующее удаление старых обьектов 
        /// </summary>
        /// <returns>Выход из приложения</returns>
        public bool Execute()
        {
            int cursorPosition = Desktop.GetInstance().ActivePanel.AbsoluteCursorPosition;
            HashSet<int> bufer = Desktop.GetInstance().ActivePanel.BufferSelectedPositionCursor;
            
            new Copy().Execute();
            
            Desktop.GetInstance().ActivePanel.AbsoluteCursorPosition = cursorPosition;
            Desktop.GetInstance().ActivePanel.BufferSelectedPositionCursor = bufer;
            
            new Delete().Execute();
            
            return false;
        }
    }
}
