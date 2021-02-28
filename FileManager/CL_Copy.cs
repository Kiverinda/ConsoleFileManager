using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace FileManager
{
    public class CL_Copy : ICommand<string>
    {
        public bool CanExecute(string value)
        {
            return value.ToLower() == "copy";
        }

        public bool Execute()
        {
            string currentPath = CommandLine.GetInstance().ListUserCommands[1];
            string targetPath = CommandLine.GetInstance().ListUserCommands[2];
            currentPath = currentPath.Remove(currentPath.LastIndexOf(@"\"), currentPath.Length - currentPath.LastIndexOf(@"\")) + @"\";
            targetPath = targetPath.Remove(targetPath.LastIndexOf(@"\"), targetPath.Length - targetPath.LastIndexOf(@"\")) + @"\";

            UpdateDesktop(currentPath, targetPath);

            Attributes newAttributes;
            if (Path.GetExtension(currentPath) != "")
            {
                string extension = Path.GetExtension(currentPath);
                long size = (currentPath).Length;
                string name = Path.GetFileName(currentPath);
                newAttributes = new Attributes(name, currentPath, size, true, extension);
            }
            else
            {
                newAttributes = new Attributes(Path.GetFileName(currentPath), currentPath);
            }

            Copy copy = new Copy(Desktop.GetInstance().ActivePanel);
            copy.CheckingExistenceObjectInDestinationFolder(newAttributes);
            return true;
        }

        public void UpdateDesktop(string current, string target)
        {
            Desktop.GetInstance().LeftPanel.UpdatePath(current);
            Desktop.GetInstance().RightPanel.UpdatePath(target);
            Desktop.GetInstance().ActivePanel = Desktop.GetInstance().LeftPanel;
            Desktop.GetInstance().ViewDesktop();
        }
    }
}
