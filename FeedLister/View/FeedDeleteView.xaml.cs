using FeedLister.Controller;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace FeedLister
{
    /// <summary>
    /// FeedDeleteView.xaml の相互作用ロジック
    /// </summary>
    public partial class FeedDeleteView : Window
    {
        private string nowSelected = "";

        public FeedDeleteView()
        {
            InitializeComponent();
            InitializeChannelList();
        }

        private void InitializeChannelList()
        {
            List<Channel> Lch = new ChannelControll().GETChannel();
            foreach(Channel ch in Lch)
            {
                CreateChannelCard(ch);
            }
        }

        private RadioButton CreateChannelCard(Channel ch)
        {
            ChannelCard cc = new ChannelCard();
            cc.SetCardContent(ch.title, ch.description, ch.link);
            cc.MinWidth = 320;
            cc.MinHeight = 100;

            RadioButton rb = new RadioButton();
            rb.Margin = new Thickness(10, 10, 10, 10);
            rb.MinWidth = 350;
            rb.MinHeight = 100;
            rb.Name = "id_" + ch.id;
            rb.GroupName = "A";
            rb.Content = cc;
            rb.Checked += Rb_Checked;
            rb.Unchecked += Rb_Unchecked;

            ChannelList.Children.Add(rb);

            return rb;
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

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            //ToDo 削除処理追加
            if(nowSelected.Length != 0)
            {
                if (MessageBox.Show("Delete Next Channel " + new ChannelControll().SearchChannel(int.Parse(nowSelected)).title + " ?", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    new ChannelControll().DeleteChannel(int.Parse(nowSelected));
                    MessageBox.Show("Deleted is Success !!");
                }
                else
                {
                    return;
                }
            }
            else
            {
                MessageBox.Show("please Select Feed Data !!","Error!");
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
