using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DailyCode
{
    /// <summary>
    /// Interaction logic for Tasks.xaml
    /// </summary>
    public partial class Tasks : Page
    {
        public static ObservableCollection<ListBoxItem> taskMap = new ObservableCollection<ListBoxItem>();


        public Tasks()
        {
            InitializeComponent();
            Loaded += Tasks_Loaded;

        }

        private void ButtonHandlers()
        {
            TrashPath.MouseEnter += (s, e) => TrashPath.Fill = Brushes.Red;
            TrashPath.MouseLeave += (s, e) => TrashPath.Fill = Brushes.CornflowerBlue;
        }


        private void Tasks_Loaded(object sender, RoutedEventArgs e)
        {
            ButtonHandlers();
            TaskList.ItemsSource = taskMap;
        }

        private void ResetPage()
        {
            TrashBtn.Visibility = Visibility.Hidden;
            TaskList.UnselectAll();
        }

        private void CodeButton_Click(object sender, RoutedEventArgs e)
        {
            ResetPage();
            NavigationService.Navigate(new Code());
        }
        private void AddItem(object sender, RoutedEventArgs e)
        {
            var text = TaskText.Text;
            if (string.IsNullOrWhiteSpace(text))
            {
                MessageBox.Show("Please enter a task");
                return;
            }

            var item = new ListBoxItem { Content = text, Height = 40, Background = Brushes.LightGray };
            taskMap.Add(item);
            TaskText.Text = "";
            TaskText.Focus();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var item = (ListBoxItem)TaskList.SelectedItem;
            taskMap.Remove(item);

        }

        private void TaskList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (TaskList.SelectedIndex == -1)
                TrashBtn.Visibility = Visibility.Hidden;
            else
            {
                TrashBtn.Visibility = Visibility.Visible;
                if (TaskList.SelectedItem is ListBoxItem selectedItem)
                    TaskText.Text = selectedItem.Content.ToString();
                
            }

        }



    }
}
