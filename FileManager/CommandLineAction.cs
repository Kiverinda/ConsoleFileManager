using System.IO;
using System.Collections.Generic;
using System.Text;

namespace FileManager
{
    class CommandLineAction 
    {
        public string CurrentPath { get; set; }
        public string TargetPath { get; set; }
        public Stack<string> StackString { get; set; }

        public CommandLineAction(string currentPath, string targetPath)
        {
            CurrentPath = currentPath;
            TargetPath = targetPath;
        }

        public CommandLineAction(string currentPath)
        {
            CurrentPath = currentPath;
            TargetPath = "";
        }

        public void CL_Copy()
        {
            string current = CurrentPath.Remove(CurrentPath.LastIndexOf(@"\"), CurrentPath.Length - CurrentPath.LastIndexOf(@"\"));
            string target = TargetPath.Remove(TargetPath.LastIndexOf(@"\"), TargetPath.Length - TargetPath.LastIndexOf(@"\"));
            Desktop.GetInstance().LeftPanel.UpdatePath(current);
            Desktop.GetInstance().RightPanel.UpdatePath(target);
            Desktop.GetInstance().ActivePanel = Desktop.GetInstance().LeftPanel;
            Desktop.GetInstance().ViewDesktop();
            Attributes newAttributes;
            if (Path.GetExtension(CurrentPath) != "")
            {
                string extension = Path.GetExtension(CurrentPath);
                long size = (CurrentPath).Length;
                string name = Path.GetFileName(CurrentPath);
                newAttributes = new Attributes(name, CurrentPath, size, true, extension);
            }
            else
            {
                newAttributes = new Attributes(Path.GetFileName(CurrentPath), CurrentPath);
            }

            Copy copy = new Copy(Desktop.GetInstance().ActivePanel);
            copy.CheckingExistenceObjectInDestinationFolder(newAttributes);
        }

        public void CL_Rename()
        {
            View view = new View();
            string newName = view.MessageCreate("ВВЕДИТЕ НОВОЕ ИМЯ");
            string oldName = Path.GetFileName(CurrentPath);
            if (File.Exists(CurrentPath))
            {
                File.Move(CurrentPath, CurrentPath.Replace(oldName, newName));
            } 
            else if (Directory.Exists(CurrentPath))
            {
                Directory.Move(CurrentPath, CurrentPath.Replace(oldName, newName));
            }
            else
            {
                view.Message("ОБЪЕКТ НЕ НАЙДЕН");
            }
        }

        public void CL_Delete()
        {
            Delete delete = new Delete(Desktop.GetInstance().ActivePanel);
            string name = Path.GetFileName(CurrentPath);
            bool isFile = false;
            if (File.Exists(CurrentPath)) isFile = true;
            Attributes attributes = new Attributes(name, CurrentPath, 0, isFile, "");
            delete.DeleteFileOrDirectory(attributes);
        }
    }
}
