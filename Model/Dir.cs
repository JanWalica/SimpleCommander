using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace TotalCommander.Model
{
    class Dir
    {
        public List<String> Drives { get; set; } = new List<String>();
        public String CurrentDrive { get; set; }
        public String CurrentPath { get; set; }
        public ObservableCollection<DirItem> Items { get; set; } = new ObservableCollection<DirItem>();
        public DirItem SelectedItem { get; set; }
        public List<String> LoadDrives()
        {
            return Directory.GetLogicalDrives().ToList();
        }

        public String ParentDir()
        {
            String tmp = Directory.GetParent(Directory.GetParent(CurrentPath).FullName).FullName + "\\";

            if (Drives.Contains(tmp[..(tmp.LastIndexOf("\\"))]))
            {
                tmp = tmp[..(tmp.LastIndexOf("\\"))];
            }

            return tmp;
        }

        public ObservableCollection<DirItem> LoadItems()
        {
            ObservableCollection<DirItem> listTmp = new ObservableCollection<DirItem>() { new DirItem("..", ItemType.Dir) };

            String[] dirsTmp = Directory.GetDirectories(CurrentPath);

            foreach (String dir in dirsTmp)
            {
                string name = dir.Substring(dir.LastIndexOf('\\') + 1);
                DirItem fileTmp = new DirItem(name, ItemType.Dir);
                listTmp.Add(fileTmp);
            }

            String[] filesTmp = Directory.GetFiles(CurrentPath);

            foreach(String file in filesTmp)
            {
                string name = file.Substring(file.LastIndexOf('\\') + 1);
                DirItem fileTmp = new DirItem(name, ItemType.File);
                listTmp.Add(fileTmp);
            }

            if (Drives.Contains(CurrentPath))
            {
                listTmp.RemoveAt(0);
            }

            return listTmp;
        }
    }
}
