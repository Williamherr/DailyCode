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
    /// Interaction logic for Code.xaml
    /// </summary>
    public partial class Code : Page
    {
        public static ObservableCollection<ListBoxItem> codeMap = new ObservableCollection<ListBoxItem>();
        public Code()
        {
            InitializeComponent();
            Loaded += Tasks_Loaded;
        }
        private void TaskButton_Click(object sender, RoutedEventArgs e)
        {
            ResetPage();
            NavigationService.Navigate(new Tasks());
        }


        private void ButtonHandlers()
        {
            TrashPath.MouseEnter += (s, e) => TrashPath.Fill = Brushes.Red;
            TrashPath.MouseLeave += (s, e) => TrashPath.Fill = Brushes.CornflowerBlue;
        }

        private void Tasks_Loaded(object sender, RoutedEventArgs e)
        {
            ButtonHandlers();
            TaskList.ItemsSource = codeMap;
        }

        private void ResetPage()
        {
            TrashBtn.Visibility = Visibility.Hidden;
            TaskList.UnselectAll();
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
            codeMap.Add(item);
            TaskText.Text = "";
            TaskText.Focus();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var item = (ListBoxItem)TaskList.SelectedItem;
            codeMap.Remove(item);
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
