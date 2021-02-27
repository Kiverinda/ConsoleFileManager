using System;

namespace FileManager
{
    
    /// <summary>
    /// Интерфейс для реализации паттерна Command для командной строки
    /// </summary>
    public interface ICommandForCLine<T>
    {

        /// <summary>
        /// Условие для выполнения действия
        /// </summary>
        /// <param name="value">Значение для сравнения</param>
        /// <returns>true or false</returns>
        public bool CanExecuteForCLine (T value);

        /// <summary>
        /// Выполняемое действие
        /// </summary>
        /// <returns>Выход из программы</returns>
        public bool ExecuteForCLine();
    }
}
