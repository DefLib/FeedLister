using FeedLister.Controller;
using FeedLister.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

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
            InitializeEntryData();
        }

        /// <summary>
        /// List<Entry>を取得する。存在しない場合は
        /// </summary>
        private void InitializeEntryData()
        {
            SetStatusMessage("Updating...");

            FeedList.Children.Clear();

            new FeedDownloader().FeedDownload(new ChannelControll().GetURLs());
            List<Entry> len = new List<Entry>();
            foreach(Channel ch in new ChannelControll().GETChannel())
            {
                len.AddRange(new EntryControll().GETEntry(ch.id));
            }

            if (len.Count == 0)
            {
                infoMessage.Visibility = Visibility.Visible;
                infoMessage.IsEnabled = true;

                FeedList.Opacity = 0.00d;
                FeedList.IsEnabled = false;

                FeedDetails.IsEnabled = false;
                FeedDetails.Visibility = Visibility.Hidden;
            }
            else
            {
                foreach (Entry en in len)
                {
                    FeedCard fc = new FeedCard();
                    fc.SetCardContent(en.title, en.description, en.created_at);
                    fc.Name = "id_" + en.id.ToString();
                    FeedList.Children.Add(fc);
                }

                infoMessage.Visibility = Visibility.Hidden;
                infoMessage.IsEnabled = false;

                FeedDetails.IsEnabled = false;
                FeedDetails.Visibility = Visibility.Hidden;

                Details.setFeedInfo("" + "", "", "", null);

                FeedList.Opacity = 1.0d;
                FeedList.IsEnabled = true;

                SetUpdateTime(DateTime.Now.ToString());
            }

            SetStatusMessage("Updated");
        }

        private void FeedList_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Switch to FeedDetails
            FeedCard fc = e.Source as FeedCard;
            string name = fc.Name;

            // Insert DB IO
            Details.setFeedInfo(fc.title.Text, fc.description.Text, fc.create_at.Text, null);
            Details.Name = name;

            infoMessage.Visibility = Visibility.Hidden;
            infoMessage.IsEnabled = false;

            FeedList.Opacity = 0.25d;
            FeedList.IsEnabled = false;

            FeedDetails.IsEnabled = true;
            FeedDetails.Visibility = Visibility.Visible;
        }

        private void FeedDetails_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Switch to FeedList
            infoMessage.Visibility = Visibility.Hidden;
            infoMessage.IsEnabled = false;

            FeedDetails.IsEnabled = false;
            FeedDetails.Visibility = Visibility.Hidden;

            Details.setFeedInfo("" + "", "", "", null);

            FeedList.Opacity = 1.0d;
            FeedList.IsEnabled = true;
        }

        private void FeedDetails_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            int id = int.Parse(Details.Name.Replace("id_", ""));
            string url = new EntryControll().SearchEntry(id).article_link;
            try
            {
                Process.Start(url);
                return;
            }
            catch (Win32Exception nobrowser)
            {
                MessageBox.Show(nobrowser.Message + "\n" + "Selected url is " + url,"noBlowser");
            }
            catch (Exception other)
            {
                MessageBox.Show(other.Message,"unknown Exp");
            }
            finally
            {
                if(MessageBox.Show("Copy URL " + url, "Copy?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    Clipboard.SetText(url);
                    MessageBox.Show("Copy is Sucsess !!", "Copyed");
                } 
            }
        }

        private void Menu_Setting_Click(object sender, RoutedEventArgs e)
        {
            // new SettingView().Show();
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

        private void Menu_Update_Click(object sender, RoutedEventArgs e)
        {
            IsEnabled = false;

            InitializeEntryData();

            IsEnabled = true;
        }

        private void SetStatusMessage(string msg)
        {
            tb_Now_Status.Text = "Status:" + msg;
        }

        private void SetUpdateTime(string msg)
        {
            tb_Last_Update_Time.Text = "Last Update:" + msg;
        }
    }
}