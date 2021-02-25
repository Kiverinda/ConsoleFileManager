using System;
using System.Threading;

namespace FileManager
{
    /// <summary>
    /// Тип делегата для события Resize
    /// </summary>
    public delegate void ResizeWindow();
    /// <summary>
    /// Класс для отслеживания изменения размера окна приложения 
    /// </summary>
    public class ThreadControlSizeWindow
    {
        private static ThreadControlSizeWindow instance { get; set; }
        /// <summary>
        /// Ширина окна приложения
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// Высота окна приложения
        /// </summary>
        public int Height { get; set; }
        /// <summary>
        /// Условие закрытия потока
        /// </summary>
        public bool Close { get; set; }
        /// <summary>
        /// Событие, возникающее при изменении размеров окна 
        /// </summary>
        public event ResizeWindow Resize;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public ThreadControlSizeWindow()
        {
            Width = Console.WindowWidth;
            Height = Console.WindowHeight;
            Resize += delegate { };
        }
        /// <summary>
        /// Реализация паттерна singlton. 
        /// </summary>
        /// <returns>Экземпляр класса</returns>
        public static ThreadControlSizeWindow GetInstance()
        {
            if (instance == null)
            {
                    instance = new ThreadControlSizeWindow();
            }
            return instance;
        }
        /// <summary>
        /// Создание и запуск потока
        /// </summary>
        public void Start()
        {
            Thread controlSize = new Thread(Control);
            controlSize.Priority = ThreadPriority.BelowNormal;
            controlSize.Start();
        }
        /// <summary>
        /// Отслеживание изменений размеров окна приложения
        /// с генерацией события
        /// </summary>
        public void Control()
        {
            Close = false;
            while (!Close)
            {
                if (Console.WindowHeight != Height || Console.WindowWidth != Width)
                {
                    Height = Console.WindowHeight;
                    Width = Console.WindowWidth;
                    Resize();
                    Thread.Sleep(100);
                }
            }
        }
    }
}
