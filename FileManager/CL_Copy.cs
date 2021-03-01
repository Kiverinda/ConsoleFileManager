using System;
using System.IO;

namespace FileManager
{
    /// <summary>
    /// Класс для копирования файла и директории
    /// </summary>
    public class CL_Copy : ICommand<string>
    {
        /// <summary>
        /// Сравнение входящей строки с контрольной строкой
        /// </summary>
        /// <param name="value">Входящая строка</param>
        /// <returns>true or false</returns>
        public bool CanExecute(string value)
        {
            return value.ToLower() == "copy";
        }
        /// <summary>
        /// Копирование обьекта при помощи класса Copy.
        /// </summary>
        public bool Execute()
        {
            Attributes newAttributes = SetObjectAttributes();
            Copy copy = new Copy(Desktop.GetInstance().ActivePanel);
            copy.CheckingExistenceObjectInDestinationFolder(newAttributes);
            return true;
        }
        /// <summary>
        /// Обновление файловых панелей
        /// </summary>
        /// <param name="current">Путь текущего расположения обьекта</param>
        /// <param name="target">Путь назначения</param>
        public void UpdateDesktop(string current, string target)
        {
            Desktop.GetInstance().LeftPanel.UpdatePath(current);
            Desktop.GetInstance().RightPanel.UpdatePath(target);
            Desktop.GetInstance().ActivePanel = Desktop.GetInstance().LeftPanel;
            Desktop.GetInstance().ViewDesktop();
        }
        /// <summary>
        /// Создание аттрибутов для обьекта копирования.
        /// </summary>
        /// <returns>Аттрибуты копируемого обьекта</returns>
        public Attributes SetObjectAttributes()
        {
            string currentPath = CommandLine.GetInstance().ListUserCommands[1];
            string targetPath = CommandLine.GetInstance().ListUserCommands[2];
            currentPath = currentPath.Remove(currentPath.LastIndexOf(@"\"), currentPath.Length - currentPath.LastIndexOf(@"\")) + @"\";
            targetPath = targetPath.Remove(targetPath.LastIndexOf(@"\"), targetPath.Length - targetPath.LastIndexOf(@"\")) + @"\";

            UpdateDesktop(currentPath, targetPath);

            if (Path.GetExtension(currentPath) != "")
            {
                string extension = Path.GetExtension(currentPath);
                long size = (currentPath).Length;
                string name = Path.GetFileName(currentPath);
                return new Attributes(name, currentPath, size, true, extension);
            }
            else
            {
                return new Attributes(Path.GetFileName(currentPath), currentPath);
            }
        }
    }
}
