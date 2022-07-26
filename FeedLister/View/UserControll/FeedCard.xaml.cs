using System.Windows.Controls;

namespace FeedLister
{
    /// <summary>
    /// FeedCard.xaml の相互作用ロジック
    /// </summary>
    public partial class FeedCard : UserControl
    {
        public FeedCard()
        {
            InitializeComponent();
        }

        internal void SetCardContent(string title_str, string description_str, string create_at_str)
        {
            title.Text = title_str;
            description.Text = description_str;
            create_at.Text = create_at_str;
        }
    }
}
