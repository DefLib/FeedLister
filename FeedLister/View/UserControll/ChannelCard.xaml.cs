using System.Windows.Controls;

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
