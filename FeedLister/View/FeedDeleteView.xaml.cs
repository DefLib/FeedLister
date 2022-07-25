using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FeedLister
{
    /// <summary>
    /// FeedDeleteView.xaml の相互作用ロジック
    /// </summary>
    public partial class FeedDeleteView : Window
    {
        public FeedDeleteView()
        {
            InitializeComponent();
        }

        private RadioButton CreateChannelCard()
        {
            ChannelCard cc = new ChannelCard();
            cc.SetCardContent("Test", "Description", "Link");
            cc.Width = 320;
            cc.Height = 150;

            RadioButton rb = new RadioButton();
            rb.Margin = new Thickness(10, 10, 10, 10);
            rb.Width = 350;
            rb.Height = 150;
            rb.Content = cc;

            return rb;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            //ToDo 削除処理追加
            MessageBox.Show("please Select Feed Data !!");
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
