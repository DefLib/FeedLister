using System.Windows;

namespace FeedLister.View
{
    /// <summary>
    /// SettingView.xaml の相互作用ロジック
    /// </summary>
    public partial class SettingView : Window
    {
        public SettingView()
        {
            InitializeComponent();
            InitializeSettingData();
        }

        private void InitializeSettingData()
        {
            TopMost.IsChecked = Properties.Settings.Default.TopMost;
            AutoUpdate.IsChecked = Properties.Settings.Default.AutoUpdate;
            sl_NowStatus.Value = Properties.Settings.Default.DefOpacity;
            sl_NowStatus.ValueChanged += sl_NowStatus_ValueChanged;
            tb_NowStatus.Text = "" + Properties.Settings.Default.DefOpacity;
        }

        private void UpdateConfiguration()
        {
            Properties.Settings.Default.TopMost = TopMost.IsChecked.Value;
            Properties.Settings.Default.AutoUpdate = AutoUpdate.IsChecked.Value;
            Properties.Settings.Default.DefOpacity = sl_NowStatus.Value;
        }

        private void SV_Apply_Click(object sender, RoutedEventArgs e)
        {
            UpdateConfiguration();
        }

        private void SV_OK_Click(object sender, RoutedEventArgs e)
        {
            UpdateConfiguration();

            Close();
        }

        private void SV_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void sl_NowStatus_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

            tb_NowStatus.Text = sl_NowStatus.Value.ToString();
        }
    }
}