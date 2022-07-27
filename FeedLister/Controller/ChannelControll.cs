using FeedLister.FeedDecoder;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

namespace FeedLister.Controller
{
    class ChannelControll : WindowsGadgetDB
    {
        private static readonly string FileName = @"WindowsGadget.db";
        private readonly string ConnectionString = null;
        private readonly string DataBaseFilePath = System.AppDomain.CurrentDomain.BaseDirectory + FileName;

        public ChannelControll() // コネクション作成
        {
            if (!File.Exists(@"WindowsGadget.db"))
            {
                CreateTable();
            }

            var builder = new SQLiteConnectionStringBuilder()
            {
                DataSource = FileName
            };
            ConnectionString = builder.ToString();
        }

        public Channel SearchChannel(int id)
        {
            Channel c = null;
            using (var connection = new SQLiteConnection(ConnectionString))
            using (var command = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    command.CommandText = @"select * from channel where id='" + id + "'";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read() == true)
                        {
                            string title = reader["title"].ToString();
                            string link = reader["link"].ToString();
                            string feedLink = reader["feedLink"].ToString();
                            string description = reader["description"].ToString();
                            c = new Channel(id, title, link, feedLink, description);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return c;
        }

        public Channel SearchChannel(string title) // 配信サービスの名前からidを検索
        {
            Channel c = null;
            using (var connection = new SQLiteConnection(ConnectionString))
            using (var command = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    command.CommandText = @"select * from channel where title='" + title + "'";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read() == true)
                        {
                            int id = int.Parse(reader["id"].ToString());
                            string link = reader["link"].ToString();
                            string feedLink = reader["feedLink"].ToString();
                            string description = reader["description"].ToString();
                            c = new Channel(id, title, link, feedLink, description);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return c;
        }

        public List<Channel> GETChannel() // 配信サービス一覧を表示
        {
            var list = new List<Channel>();
            using (var connection = new SQLiteConnection(ConnectionString))
            using (var command = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    command.CommandText = @"select * from channel";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read() == true)
                        {
                            int id = Int32.Parse(reader["id"].ToString());
                            string title = reader["title"].ToString();
                            string link = reader["link"].ToString();
                            string feedLink = reader["feedLink"].ToString();
                            string description = reader["description"].ToString();
                            list.Add(new Channel(id, title, link, feedLink, description));
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }
            }
            return list;
        }

        public List<string> GetURLs()
        {
            var list = new List<string>();
            using (var connection = new SQLiteConnection(ConnectionString))
            using (var command = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    command.CommandText = @"select feedLink from channel";
                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {

                        }
                        else
                        {
                            foreach (var item in reader)
                            {
                                string link = reader["feedLink"].ToString();
                                list.Add(link);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }
            }
            return list;
        }

        private void AddChannel(Channel ch) // 新しい配信サービスの情報を追加する
        {
            string title = ch.title;
            string link = ch.link;
            string feedLink = ch.feedLink;
            string description = ch.description;

            using (var connection = new SQLiteConnection(ConnectionString))
            using (var command = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    command.CommandText = @"insert into channel (title,link,feedLink,description) values ('" + title + "','" + link + "','" + feedLink + "','" + description + "')";
                    command.ExecuteNonQuery();
                }
                catch (SQLiteException e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        internal bool AddChannel(string URL)
        {
            Channel ch = RSS2.ExtractChennelContent(FeedDownloader.GetxmlDoc(URL), URL);

            string title = ch.title;
            string link = ch.link;
            string feedLink = ch.feedLink;
            string description = ch.description;

            using (var connection = new SQLiteConnection(ConnectionString))
            using (var command = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    command.CommandText = @"insert into channel (title,link,feedLink,description) values ('" + title + "','" + link + "','" + feedLink + "','" + description + "')";
                    command.ExecuteNonQuery();
                }
                catch (SQLiteException e)
                {
                    Console.WriteLine(e.Message);
                    return false;
                }
                finally
                {
                    connection.Close();
                }
            }

            return true;
        }

        public void DeleteChannel(int id) // 配信サービス情報を削除する
        {
            new EntryControll().DeleteChannel(id);
            using (var connection = new SQLiteConnection(ConnectionString))
            using (var command = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    command.CommandText = @"delete from channel where id=@id";
                    command.Parameters.Add(new SQLiteParameter("@id", id));
                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }
            }

        }
    }
}
