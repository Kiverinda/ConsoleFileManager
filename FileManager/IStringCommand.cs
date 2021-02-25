using System;

namespace FileManager
{

    /// <summary>
    /// Интерфейс для реализации паттерна Command
    /// </summary>
    public interface IStringCommand
    {

        /// <summary>
        /// Условие для выполнения действия
        /// </summary>
        /// <param name="click">Информация о нажатой клавише</param>
        /// <returns>true or false</returns>
        public bool CanExecute(string command);

        /// <summary>
        /// Выполняемое действие
        /// </summary>
        public void Execute(string[] enteredData);
    }
}
