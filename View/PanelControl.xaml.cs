using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TotalCommander.Model;

namespace TotalCommander.View
{
    /// <summary>
    /// Logika interakcji dla klasy PanelControl.xaml
    /// </summary>
    public partial class PanelControl : UserControl
    {
        public PanelControl()
        {
            InitializeComponent();
        }

        // ComboBoxSource

        public static readonly DependencyProperty ComboBoxSourceProperty =
            DependencyProperty.Register(
                nameof(ComboBoxSource),
                typeof(List<String>),
                typeof(PanelControl)
                );

        public string ComboBoxSource
        {
            set { SetValue(ComboBoxSourceProperty, value); }
        }

        // ComboBoxSelected

        public static readonly DependencyProperty ComboBoxSelectedProperty =
            DependencyProperty.Register(
                nameof(ComboBoxSelected),
                typeof(string),
                typeof(PanelControl)
                );

        public string ComboBoxSelected
        {
            get { return (string)GetValue(ComboBoxSelectedProperty); }
            set { SetValue(ComboBoxSelectedProperty, value); }
        }

        // Path

        public static readonly DependencyProperty PathProperty =
            DependencyProperty.Register(
                nameof(Path),
                typeof(String),
                typeof(PanelControl)
                );

        public string Path
        {
            set { SetValue(PathProperty, value); }
        }

        // ListViewSource

        public static readonly DependencyProperty ListViewSourceProperty =
            DependencyProperty.Register(
                nameof(ListViewSource),
                typeof(ObservableCollection<DirItem>),
                typeof(PanelControl)
                );

        public string ListViewSource
        {
            set { SetValue(ListViewSourceProperty, value); }
        }

        // ListViewSelection

        public static readonly DependencyProperty ListViewSelectionProperty =
            DependencyProperty.Register(
                nameof(ListViewSelection),
                typeof(DirItem),
                typeof(PanelControl)
                );

        public string ListViewSelection
        {
            set { SetValue(ListViewSelectionProperty, value); }
        }



        // zdarzenia

        //Metoda pomocnicza wywołująca zdarzenie
        //przy okazji metoda ta tworzy obiekt argument przekazywany przez to zdarzenie
        void RaiseMyEvent(RoutedEvent e)
        {
            //argument zdarzenia
            RoutedEventArgs newEventArgs =
                    new RoutedEventArgs(e);
            //wywołanie zdarzenia
            RaiseEvent(newEventArgs);
        }

        // ComboBoxOpened

        public static readonly RoutedEvent ComboOpenedEvent =
        EventManager.RegisterRoutedEvent(nameof(ComboOpened),
                     RoutingStrategy.Bubble, typeof(RoutedEventHandler),
                     typeof(PanelControl));

        public event RoutedEventHandler ComboOpened
        {
            add { AddHandler(ComboOpenedEvent, value); }
            remove { RemoveHandler(ComboOpenedEvent, value); }
        }

        private void ComboBox_DropDownOpened(object sender, EventArgs e)
        {
            RaiseMyEvent(PanelControl.ComboOpenedEvent);
        }

        // ListViewItemDoubleClick

        public static readonly RoutedEvent ListViewItemDoubleClickEvent =
        EventManager.RegisterRoutedEvent(nameof(ListViewItemDoubleClick),
                     RoutingStrategy.Bubble, typeof(RoutedEventHandler),
                     typeof(PanelControl));

        public event RoutedEventHandler ListViewItemDoubleClick
        {
            add { AddHandler(ListViewItemDoubleClickEvent, value); }
            remove { RemoveHandler(ListViewItemDoubleClickEvent, value); }
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            RaiseMyEvent(PanelControl.ListViewItemDoubleClickEvent);
        }
    }
}
