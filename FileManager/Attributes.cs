using System;

namespace FileManager
{
    /// <summary>
    /// Тип данных для хранения информации об обьектах файловой системы
    /// </summary>
    public class Attributes
    {
        /// <summary>
        /// Имя обьекта
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Полный путь к обьекту
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Размер обьекта
        /// </summary>
        public long Size { get; set; }

        /// <summary>
        /// Является ли обьект файлом
        /// </summary>
        public bool IsFile { get; set; }

        /// <summary>
        /// Расширение обьекта
        /// </summary>
        public string Extension { get; set; }

        /// <summary>
        /// Дата и время редактирования обьекта
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="name">Имя обьекта</param>
        /// <param name="path">Путь к обьекту</param>
        public Attributes(string name, string path)
        {
            Name = name;
            Path = path;
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="name">Имя обьекта</param>
        /// <param name="path">Путь к обьекту</param>
        /// <param name="size">Размер обьекта</param>
        /// <param name="isFile">Является ли обьект файлом</param>
        /// <param name="extension">Расширение обьекта</param>
        public Attributes(string name, string path, long size, bool isFile, string extension)
        {
            Name = name;
            Path = path;
            Size = size;
            IsFile = isFile;
            Extension = extension;
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="name">Имя обьекта</param>
        /// <param name="path">Путь к обьекту</param>
        /// <param name="size">Размер обьекта</param>
        /// <param name="isFile">Является ли обьект файлом</param>
        /// <param name="extension">Расширение обьекта</param>
        /// <param name="date">Дата последнего редактирования</param>
        public Attributes(string name, string path, long size, bool isFile, string extension, DateTime date)
        {
            Name = name;
            Path = path;
            Size = size;
            IsFile = isFile;
            Extension = extension;
            Date = date;
        }
    }
}
