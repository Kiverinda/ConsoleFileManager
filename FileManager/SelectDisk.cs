using System;


namespace FileManager 
{
    
    /// <summary>
    /// Класс выбора диска
    /// </summary>
    public class SelectDisk : ICommand<ConsoleKeyInfo>
    {

        /// <summary>
        /// Проверка условия для выполнения метода Execute
        /// </summary>
        /// <param name="click">Информация о нажатой клавише</param>
        /// <returns>true or false</returns>
        public bool CanExecute(ConsoleKeyInfo click)
        {
            return ((click.Modifiers & ConsoleModifiers.Control) != 0) && (click.Key == ConsoleKey.F1);
        }

        /// <summary>
        /// Меню выбора диска с навигацией
        /// </summary>
        /// <returns>Выход из программы</returns>
        public bool Execute()
        {
            int positionCursor = 0;
            View view = new View();
            Attributes[] allDrives = new RequestToDisk().AllDrives();
            view.SelectDrive(Desktop.GetInstance().ActivePanel, positionCursor);
            while (true)
            {

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo userKey = Console.ReadKey(true);
                    switch (userKey.Key)
                    {
                        case ConsoleKey.Enter:
                            Desktop.GetInstance().ActivePanel.UpdatePath(allDrives[positionCursor].Name);
                            Desktop.GetInstance().Update();
                            return false;
                        case ConsoleKey.Escape:
                            Desktop.GetInstance().Update();
                            return false;
                        case ConsoleKey.DownArrow:
                            if (positionCursor < allDrives.Length - 1)
                            {
                                positionCursor++;
                            }
                            view.SelectDrive(Desktop.GetInstance().ActivePanel, positionCursor);
                            break;
                        case ConsoleKey.UpArrow:
                            if (positionCursor > 0)
                            {
                                positionCursor--;
                            }
                            view.SelectDrive(Desktop.GetInstance().ActivePanel, positionCursor);
                            break;
                        default:
                            break;
                    }
                }

            }
        }
    }
}
