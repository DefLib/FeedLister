using System;
using System.Collections.Generic;
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

namespace FeedLister
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void FeedList_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Switch to FeedDetails

        }

        private void FeedDetails_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Switch to FeedList

        }

        private void Menu_exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Menu_add_Click(object sender, RoutedEventArgs e)
        {
            new FeedAdditionView().Show();
        }

        private void Menu_delete_Click(object sender, RoutedEventArgs e)
        {
            new FeedDeleteView().Show();
        }

    }
}
