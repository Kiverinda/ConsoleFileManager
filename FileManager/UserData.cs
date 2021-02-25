using System;
using System.IO;
using System.Runtime.Serialization;
using System.Text.Json;


namespace FileManager
{
    /// <summary>
    /// Класс с данными о приложении, которые сохраняются на диске
    /// </summary>
    public class UserData
    {
        private static UserData instance;
        
        /// <summary>
        /// Текущий путь для левой панели
        /// </summary>
        public string LeftPanel_CurrentPath { get; set; }
        
        /// <summary>
        /// Первая отображаемая строка при скролинге для левой панели
        /// </summary>
        public int LeftPanel_FirstLineWhenScrolling { get; set; }
        
        /// <summary>
        /// Абсолютная позиция курсора для левой панели
        /// </summary>
        public int LeftPanel_AbsoluteCursorPosition { get; set; }

        /// <summary>
        /// Позиция курсора относительно окна для левой панели
        /// </summary>
        public int LeftPanel_RelativeCursorPosition { get; set; }

        /// <summary>
        /// Текущий путь для правой панели
        /// </summary>
        public string RightPanel_CurrentPath { get; set; }

        /// <summary>
        /// Первая отображаемая строка при скролинге для левой панели
        /// </summary>
        public int RightPanel_FirstLineWhenScrolling { get; set; }

        /// <summary>
        /// Абсолютная позиция курсора для левой панели
        /// </summary>
        public int RightPanel_AbsoluteCursorPosition { get; set; }

        /// <summary>
        /// Позиция курсора относительно окна для левой панели
        /// </summary>
        public int RightPanel_RelativeCursorPosition { get; set; }

        /// <summary>
        /// Является ли активная панель левой
        /// </summary>
        public bool ActivePanel_IsLeftPanel { get; set; }

        /// <summary>
        /// Ширина окна приложения
        /// </summary>
        public int WindowWidth { get; set; }

        /// <summary>
        /// Высота окна приложения
        /// </summary>
        public int WindowHeight { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        private UserData()
        {

        }

        /// <summary>
        /// Реализация паттерна singlton
        /// </summary>
        /// <returns></returns>
        public static UserData GetInstance()
        {
            if (instance == null)
            {
                if (File.Exists("config.json"))
                {
                    string jsonString = File.ReadAllText("config.json");
                    instance = JsonSerializer.Deserialize<UserData>(jsonString);
                }
                else
                {
                    instance = new UserData();
                }
            }

            return instance;
        }

        /// <summary>
        /// Серилизация данных в формат json
        /// </summary>
        public void Serialize()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            WindowWidth = Console.WindowWidth;
            WindowHeight = Console.WindowHeight;
            ActivePanel_IsLeftPanel = Desktop.GetInstance().ActivePanel.IsLeftPanel;
            LeftPanel_CurrentPath = Desktop.GetInstance().LeftPanel.CurrentPath;
            LeftPanel_FirstLineWhenScrolling = Desktop.GetInstance().LeftPanel.FirstLineWhenScrolling;
            LeftPanel_AbsoluteCursorPosition = Desktop.GetInstance().LeftPanel.AbsoluteCursorPosition;
            LeftPanel_RelativeCursorPosition = Desktop.GetInstance().LeftPanel.RelativeCursorPosition;
            RightPanel_CurrentPath = Desktop.GetInstance().RightPanel.CurrentPath;
            RightPanel_FirstLineWhenScrolling = Desktop.GetInstance().RightPanel.FirstLineWhenScrolling;
            RightPanel_AbsoluteCursorPosition = Desktop.GetInstance().RightPanel.AbsoluteCursorPosition;
            RightPanel_RelativeCursorPosition = Desktop.GetInstance().RightPanel.RelativeCursorPosition;

            string jsonString = JsonSerializer.Serialize<UserData>(UserData.GetInstance(), options);
            File.WriteAllText("config.json", jsonString);
        }

        /// <summary>
        /// Десирилизация данных из json
        /// </summary>
        public void Deserialize()
        {
            Console.WindowWidth = WindowWidth;
            Console.WindowHeight = WindowHeight;

            Desktop.GetInstance().LeftPanel.UpdatePath(LeftPanel_CurrentPath);
            Desktop.GetInstance().RightPanel.UpdatePath(RightPanel_CurrentPath);

            Desktop.GetInstance().LeftPanel.FirstLineWhenScrolling = LeftPanel_FirstLineWhenScrolling;
            Desktop.GetInstance().LeftPanel.AbsoluteCursorPosition = LeftPanel_AbsoluteCursorPosition;
            Desktop.GetInstance().LeftPanel.RelativeCursorPosition = LeftPanel_RelativeCursorPosition;

            Desktop.GetInstance().RightPanel.FirstLineWhenScrolling = RightPanel_FirstLineWhenScrolling;
            Desktop.GetInstance().RightPanel.AbsoluteCursorPosition = RightPanel_AbsoluteCursorPosition;
            Desktop.GetInstance().RightPanel.RelativeCursorPosition = RightPanel_RelativeCursorPosition;

            if (ActivePanel_IsLeftPanel)
            {
                Desktop.GetInstance().ActivePanel = Desktop.GetInstance().LeftPanel;
                Desktop.GetInstance().ActivePanel.IsLeftPanel = true;
            }
            else
            {
                Desktop.GetInstance().ActivePanel = Desktop.GetInstance().RightPanel;
                Desktop.GetInstance().ActivePanel.IsLeftPanel = false;
            }
        }
    }

}
