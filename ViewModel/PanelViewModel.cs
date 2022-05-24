using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Input;
using TotalCommander.Model;

namespace TotalCommander.ViewModel
{
    class PanelViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        Dir Dir = new Dir();

        public string CurrentPath
        {
            get => Dir.CurrentPath;
            set
            {
                Dir.CurrentPath = value;

                try
                {
                    Items = Dir.LoadItems();
                }
                catch (UnauthorizedAccessException)
                {
                    MessageBox.Show("Nie masz uprawnień dostępu do "+CurrentPath, "Brak dostępu", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    Items = new ObservableCollection<DirItem>() { new DirItem("..", ItemType.Dir) };
                }
                catch (DirectoryNotFoundException dirEx)
                {
                    MessageBox.Show("Nie znaleziono folderu " + dirEx.Message, "Folder nie istnieje", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    Items = new ObservableCollection<DirItem>() { new DirItem("..", ItemType.Dir) };
                }

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentPath)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentPathWithoutDrive)));
            }
        }

        public string CurrentPathWithoutDrive
        {
            get => CurrentPath?[(CurrentPath.IndexOf("\\"))..] ?? "";
        }

        public List<String> Drives
        {
            get => Dir.Drives;
            set
            {
                Dir.Drives = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Drives)));
            }
        }

        public String CurrentDrive
        {
            get => Dir.CurrentDrive;
            set
            {
                Dir.CurrentDrive = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentDrive)));

                CurrentPath = value;
            }
        }

        public ObservableCollection<DirItem> Items
        {
            get => Dir.Items;
            set
            {
                Dir.Items = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Items)));
            }
        }

        public DirItem SelectedItem
        {
            get => Dir.SelectedItem;
            set
            {
                Dir.SelectedItem = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedItem)));
            }
        }

        public void ReloadItems()
        {
            Items = Dir.LoadItems();
        }

        private ICommand loadDrives;
        public ICommand LoadDrives => loadDrives ?? (loadDrives = new RelayCommand(
            o =>
            {
                Drives = Dir.LoadDrives();
            },
            null
            ));

        private ICommand changePath;
        public ICommand ChangePath => changePath ?? (changePath = new RelayCommand(
            o =>
            {
                if(SelectedItem.Name == "..")
                {
                    CurrentPath = Dir.ParentDir();
                }
                else if(SelectedItem.Type == ItemType.Dir)
                {
                    CurrentPath += SelectedItem.Name + "\\";
                }
            },
            null
            ));
    }
}
