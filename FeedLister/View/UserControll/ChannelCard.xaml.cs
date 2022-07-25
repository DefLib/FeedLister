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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FeedLister
{
    /// <summary>
    /// ChannelCard.xaml の相互作用ロジック
    /// </summary>
    public partial class ChannelCard : UserControl
    {
        public ChannelCard()
        {
            InitializeComponent();
        }

        public void SetCardContent(string title_str, string description_str, string link_str)
        {
            this.title.Text = title_str;
            this.description.Text = description_str;
            this.link.Text = link_str;
        }
    }
}
