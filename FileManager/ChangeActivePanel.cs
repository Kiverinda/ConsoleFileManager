using System;

namespace FileManager
{
    /// <summary>
    /// Класс, осуществляющий выбор активной файловой панели.
    /// Реализует паттерн Command
    /// </summary>
    public class ChangeActivePanel : ICommand
    {
        /// <summary>
        /// Проверка условия для выполнения метода Execute
        /// </summary>
        /// <param name="click">Информация о нажатой клавише</param>
        /// <returns>true or false</returns>
        public bool CanExecute(ConsoleKeyInfo click)
        {
            return click.Key == ConsoleKey.Tab;
        }
        /// <summary>
        /// Переключение активной файловой панели
        /// </summary>
        /// <returns>Выход из программы</returns>
        public bool Execute()
        {
            View view = new View();
            view.OldCursor(Desktop.GetInstance().ActivePanel);
            if (Desktop.GetInstance().ActivePanel == Desktop.GetInstance().LeftPanel)
            {
                Desktop.GetInstance().ActivePanel = Desktop.GetInstance().RightPanel;
            }
            else
            {
                Desktop.GetInstance().ActivePanel = Desktop.GetInstance().LeftPanel;
            }
            view.CurrentCursor(Desktop.GetInstance().ActivePanel);

            return false;
        }
        
    }
}
