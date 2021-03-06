﻿using System;

namespace FileManager
{
    
    /// <summary>
    /// Интерфейс для реализации паттерна Command
    /// </summary>
    public interface ICommand <T>
    {

        /// <summary>
        /// Условие для выполнения действия
        /// </summary>
        /// <param name="value">Значение для сравнения</param>
        /// <returns>true or false</returns>
        public bool CanExecute (T value);

        /// <summary>
        /// Выполняемое действие
        /// </summary>
        /// <returns>Выход из программы</returns>
        public bool Execute();
    }
}
