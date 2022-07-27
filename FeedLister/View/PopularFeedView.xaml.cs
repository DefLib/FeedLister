using FeedLister.Controller;
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

namespace FeedLister.View
{
    /// <summary>
    /// PopularFeedView.xaml の相互作用ロジック
    /// </summary>
    public partial class PopularFeedView : Window
    {
        private string nowSelected;

        private List<Channel> Lch;

        public PopularFeedView()
        {
            InitializeComponent();
            InitializeChannelList();
        }

        private void InitializeChannelList()
        {
            int id = 0;
            Lch = new APIControll().GetPopChannelList();
            foreach (Channel ch in Lch)
            {
                CreateChannelCard(ch,id++);
            }
        }

        private RadioButton CreateChannelCard(Channel ch, int id)
        {
            ChannelCard cc = new ChannelCard();
            cc.SetCardContent(ch.title, ch.description, ch.link);
            cc.MinWidth = 320;
            cc.MinHeight = 100;

            RadioButton rb = new RadioButton();
            rb.Margin = new Thickness(10, 10, 10, 10);
            rb.MinWidth = 350;
            rb.MinHeight = 100;
            rb.Name = "id_" + id;
            rb.GroupName = "A";
            rb.Content = cc;
            rb.Checked += Rb_Checked;
            rb.Unchecked += Rb_Unchecked;

            ChannelList.Children.Add(rb);

            return rb;
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

        private void Rb_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = e.Source as RadioButton;
            nowSelected = rb.Name.Replace("id_", "");
        }

        private void Rb_Unchecked(object sender, RoutedEventArgs e)
        {
            nowSelected = "";
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            string url = Lch[int.Parse(nowSelected.Replace("id_", ""))].feedLink;
            if (url.Length == 0)
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

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
