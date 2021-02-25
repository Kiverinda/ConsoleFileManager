using System.IO;
using System.Collections.Generic;

namespace FileManager
{
    /// <summary>
    /// Класс файловой панели
    /// </summary>
    public class FilesPanel
    {
        /// <summary>
        /// Является панел левой
        /// </summary>
        public bool IsLeftPanel { get; set; }
        
        /// <summary>
        /// Путь к теущей директории
        /// </summary>
        public string CurrentPath { get; set; }

        /// <summary>
        /// Список файлов и директорий в текущей директории
        /// </summary>
        public List<Attributes> CurrentListDirAndFiles { get; set; }

        /// <summary>
        /// Первая отображаемая линия при скроллинге
        /// </summary>
        public int FirstLineWhenScrolling { get; set; }

        /// <summary>
        /// Абсолютная позиция курсора
        /// </summary>
        public int AbsoluteCursorPosition { get; set; }

        /// <summary>
        /// Позиция курсора относительно окна панели при скроллинге
        /// </summary>
        public int RelativeCursorPosition { get; set; }

        /// <summary>
        /// Список выделенных абсолютных позиций курсора
        /// </summary>
        public HashSet<int> BufferSelectedPositionCursor { get; set; }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="isLeftPanel">Является ли создаваемая панель левой</param>
        public FilesPanel(bool isLeftPanel)
        {
            IsLeftPanel = isLeftPanel;
            CurrentPath = DriveInfo.GetDrives()[0].Name;
            CurrentListDirAndFiles = new RequestToDisk(CurrentPath).GetListCurrentDirectory();
            BufferSelectedPositionCursor = new HashSet<int>();
        }

        /// <summary>
        /// Добавление и удаление позиции курсора в буфер для осуществления 
        /// общего действия с группой
        /// </summary>
        /// <param name="item"></param>
        public void AddBufferSelectedPositionCursor(int item)
        {
            if (!BufferSelectedPositionCursor.Add(item)) BufferSelectedPositionCursor.Remove(item);
        }

        /// <summary>
        /// Одновление данных при изменении текущей директории
        /// </summary>
        /// <param name="path"></param>
        public void UpdatePath(string path)
        {
            RequestToDisk request = new RequestToDisk(path);
            CurrentPath = path;
            CurrentListDirAndFiles = request.GetListCurrentDirectory();
            AbsoluteCursorPosition = 0;
            RelativeCursorPosition = 0;
            FirstLineWhenScrolling = 0;
            BufferSelectedPositionCursor = new HashSet<int>();
        }
    }
}
