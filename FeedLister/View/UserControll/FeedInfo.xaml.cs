using System.Windows.Controls;

namespace FeedLister
{
    /// <summary>
    /// FeedInfo.xaml の相互作用ロジック
    /// </summary>
    public partial class FeedInfo : UserControl
    {
        public FeedInfo()
        {
            InitializeComponent();
        }

        internal void setFeedInfo(string title_str, string description_str, string link_str, object image_str)
        {
            title.Text = title_str;
            description.Text = description_str;
            link.Text = link_str;
        }
    }
}
