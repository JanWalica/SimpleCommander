using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Input;
using TotalCommander.Model;

namespace TotalCommander.ViewModel
{
    class MainViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public PanelViewModel PanelA { get; set; }
        public PanelViewModel PanelB { get; set; }

        public MainViewModel()
        {
            PanelA = new PanelViewModel();
            PanelB = new PanelViewModel();
        }

        private ICommand copyItem;
        public ICommand CopyItem => copyItem ?? (copyItem = new RelayCommand(
            o =>
            {
                if (PanelA.SelectedItem.Type == ItemType.File)
                {
                    String sourceFile = PanelA.CurrentPath + PanelA.SelectedItem.Name;
                    String destFile = PanelB.CurrentPath + PanelA.SelectedItem.Name;

                    try
                    {
                        Model.Manipulation.CopyFile(sourceFile, destFile);
                    }
                    catch(UnauthorizedAccessException)
                    {
                        MessageBox.Show("Nie masz wymaganych uprawnień.", "Brak dostępu", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                    catch(Exception e)
                    {
                        MessageBox.Show(e.Message, "Błąd", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }

                    PanelB.ReloadItems();
                }
                else
                {
                    String sourceDir = PanelA.CurrentPath + PanelA.SelectedItem.Name;
                    String destDir = PanelB.CurrentPath + PanelA.SelectedItem.Name;

                    try
                    {
                        Model.Manipulation.CopyDir(sourceDir, destDir);
                    }
                    catch (UnauthorizedAccessException)
                    {
                        MessageBox.Show("Nie masz uprawnień dostępu do " + PanelB.CurrentPath, "Brak dostępu", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message, "Błąd", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }

                    PanelB.ReloadItems();
                }
            },
            o =>
            {
                if(PanelB.CurrentPath is null)
                {
                    return false;
                }

                if (PanelA.SelectedItem is null)
                {
                    return false;
                }
                
                if (PanelA.SelectedItem.Name == "..")
                {
                    return false;
                }
                
                return true;
            }
            ));

        private ICommand reloadPanels;
        public ICommand ReloadPanels => reloadPanels ?? (reloadPanels = new RelayCommand(
            o =>
            {
                PanelA.ReloadItems();
                PanelB.ReloadItems();
            },
            o =>
            {
                if (PanelA.CurrentPath is null || PanelB.CurrentPath is null)
                {
                    return false;
                }
                return true;
            }
            ));
    }
}
