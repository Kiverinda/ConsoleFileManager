using System;

namespace FileManager
{
    
    /// <summary>
    /// Интерфейс для реализации паттерна Command
    /// </summary>
    public interface ICommand
    {
        
        /// <summary>
        /// Условие для выполнения действия
        /// </summary>
        /// <param name="click">Информация о нажатой клавише</param>
        /// <returns>true or false</returns>
        public bool CanExecute(ConsoleKeyInfo click);

        /// <summary>
        /// Выполняемое действие
        /// </summary>
        /// <returns>Выход из программы</returns>
        public bool Execute();
    }
}
