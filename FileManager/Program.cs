using System;
using System.Collections.Generic;


namespace FileManager
{
    
    /// <summary>
    /// Главный метод приложения
    /// </summary>
    public class Program
    {
        
        /// <summary>
        /// Точка входа в программу
        /// </summary>
        public static void Main()
        {
            Desktop desktop = Desktop.GetInstance();
            
            // Запуск потока, контролируещего размер окна приложения
            ThreadControlSizeWindow Control = ThreadControlSizeWindow.GetInstance();
            Control.Resize += ControlSize;
            Control.Start();

            // Запуск приложения
            desktop.Run();
        }

        /// <summary>
        /// Метод, вызываемый при изменении размеров окна приложения
        /// </summary>
        public static void ControlSize()
        {
            Desktop.GetInstance().ViewDesktop();
        }
    }
}  
