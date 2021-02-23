using System;
using System.IO;
using System.Runtime.Serialization;
using System.Text.Json;


namespace FileManager
{
    public class UserData
    {
        private static UserData instance;
        public string LeftPanel_CurrentPath { get; set; }
        public int LeftPanel_FirstLineWhenScrolling { get; set; }
        public int LeftPanel_AbsoluteCursorPosition { get; set; }
        public int LeftPanel_RelativeCursorPosition { get; set; }
        public string RightPanel_CurrentPath { get; set; }
        public int RightPanel_FirstLineWhenScrolling { get; set; }
        public int RightPanel_AbsoluteCursorPosition { get; set; }
        public int RightPanel_RelativeCursorPosition { get; set; }
        public bool ActivePanel_IsLeftPanel { get; set; }

        private UserData()
        {

        }

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

        public void Deserialize()
        {
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

        public void Serialize()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

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
    }

}
