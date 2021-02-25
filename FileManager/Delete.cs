using System;
using System.IO;
using System.Collections.Generic;


namespace FileManager
{
    /// <summary>
    /// Класс для удаления файлов или директорий
    /// </summary>
    public class Delete : ICommand
    {
        /// <summary>
        /// Активная панель
        /// </summary>
        public FilesPanel ActivePanel { get; set; }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="filesPanel"></param>
        public Delete(FilesPanel filesPanel)
        {
            ActivePanel = filesPanel;
        }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public Delete()
        {

        }

        /// <summary>
        /// Проверка условия для выполнения метода Execute
        /// </summary>
        /// <param name="click">Информация о нажатой клавише</param>
        /// <returns>true or false</returns>
        public bool CanExecute(ConsoleKeyInfo click)
        {
            return click.Key == ConsoleKey.F8;
        }

        /// <summary>
        /// Запуск цепочки удаления и обновление окна программы после удаления
        /// </summary>
        /// <returns>true or false</returns>
        public bool Execute()
        {
            CheckBufferSelected();
            Desktop.GetInstance().Update();
            return false;
        }

        /// <summary>
        /// Проверяется наличие выделенных обьектов и если такие есть, то они поочередно отправляются на удаление.
        /// Если выделенных обьектов нет, то отправляется на проверку обьект, на котором находится курсор. 
        /// </summary>
        public void CheckBufferSelected()
        {
            ActivePanel = Desktop.GetInstance().ActivePanel;

            if (ActivePanel.BufferSelectedPositionCursor.Count == 0)
            {
                Check();
            }
            else
            {
                DeleteObjectFromToList();
            }
        }

        /// <summary>
        /// Валидация обьекта на котором находится курсор
        /// </summary>
        public void Check()
        {
            List<Attributes> currentList = ActivePanel.CurrentListDirAndFiles;
            if (currentList[ActivePanel.AbsoluteCursorPosition].Name == "[..]")
            {
                return;
            }
            DeleteFileOrDirectory(currentList[ActivePanel.AbsoluteCursorPosition]);
        }

        /// <summary>
        /// Создание списка обьектов для удаления и поочередная отправка их на удаление
        /// </summary>
        public void DeleteObjectFromToList()
        {
            List<Attributes> ListToDelete = new List<Attributes>();
            foreach (int i in ActivePanel.BufferSelectedPositionCursor)
            {
                ListToDelete.Add(ActivePanel.CurrentListDirAndFiles[i]);
            }
            foreach (Attributes i in ListToDelete)
            {
                DeleteFileOrDirectory(i);
            }
        }

        /// <summary>
        /// Удаление файла или директории
        /// </summary>
        /// <param name="attributes"></param>
        public void DeleteFileOrDirectory(Attributes attributes)
        {
            try
            {
                if (attributes.IsFile)
                {
                    if (Confirmation(attributes.Path)) File.Delete(attributes.Path);
                }
                else
                {
                    if (Confirmation(attributes.Path)) Directory.Delete(attributes.Path, true);
                }
            }
            catch(Exception ex)
            {
                new ErrorLog(this, ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// Подтверждение удаления от пользователя
        /// </summary>
        /// <param name="path">Полное имя файла или директории</param>
        /// <returns>true or false</returns>
        public bool Confirmation(string path)
        {
            View view = new View();
            view.Confirmation("Подтвердите удаление", path);
            
            ConsoleKeyInfo click;
            do
            {
                click = Console.ReadKey(true);
                if (click.Key == ConsoleKey.Y)
                {
                    return true;
                }
                else if (click.Key == ConsoleKey.N)
                {
                    return false;
                }

            } while (click.Key != ConsoleKey.Escape);
            
            return false;
        }

        
    }
}
