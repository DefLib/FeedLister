using FeedLister.Controller;
using FeedLister.FeedDecoder;
using System;
using System.Collections.Generic;
using System.Windows;

namespace FeedLister
{
    /// <summary>
    /// FeedAdditionView.xaml の相互作用ロジック
    /// </summary>
    public partial class FeedAdditionView : Window
    {
        public FeedAdditionView()
        {
            InitializeComponent();
        }

        private void ChannelAdd(string URL)
        {
            if (new ChannelControll().AddChannel(URL))
            {
                MessageBox.Show("登録しました");
                Close();
            }
            else
            {
                MessageBox.Show("既に登録されているURLです");
            }
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            string url = URL.Text;
            if(url.Length == 0)
            {
                MessageBox.Show("URLを入力してください");
                return;
            }

            try
            {
                int flag = FeedDownloader.CheckFeeds(url);
                if (flag == -999)
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("無効なURLです");
                return;
            }

            ChannelAdd(url);
        }
    }
}
