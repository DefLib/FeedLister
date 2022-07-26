using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

namespace FeedLister.Controller
{
    class EntryControll : WindowsGadgetDB
    {
        private static readonly string FileName = @"WindowsGadget.db";
        private readonly string ConnectionString = null;
        private readonly string DataBaseFilePath = System.AppDomain.CurrentDomain.BaseDirectory + FileName;

        public EntryControll()
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

        public List<Entry> GETEntry(int channel_id)
        {
            var list = new List<Entry>();
            using (var connection = new SQLiteConnection(ConnectionString))
            using (var command = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    command.CommandText = @"select * from entry order by id desc";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read() == true)
                        {
                            int id = Int32.Parse(reader["id"].ToString());
                            string title = reader["title"].ToString();
                            string description = reader["description"].ToString();
                            string article_link = reader["article_link"].ToString();
                            string image_link = reader["image_link"].ToString();
                            string create_at = reader["created_at"].ToString();

                            list.Add(new Entry(id, channel_id, title, description, article_link, image_link, create_at));
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

        public Entry SearchEntry(int id)
        {
            Entry en = null;
            using (var connection = new SQLiteConnection(ConnectionString))
            using (var command = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    command.CommandText = @"select * from entry";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read() == true)
                        {
                            int channel_id = Int32.Parse(reader["channel_id"].ToString());
                            string title = reader["title"].ToString();
                            string description = reader["description"].ToString();
                            string article_link = reader["article_link"].ToString();
                            string image_link = reader["image_link"].ToString();
                            string create_at = reader["created_at"].ToString();

                            en = new Entry(channel_id,title,description,article_link,image_link,create_at);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }
            }
            return en;
        }

        public void AddEntry(Entry ch)
        {
            int channel_id = ch.channel_ID;
            string title = ch.title;
            string description = ch.description;
            string article_link = ch.article_link;
            string image_link = ch.image_link;
            string created_at = ch.created_at;

            using (var connection = new SQLiteConnection(ConnectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    try
                    {
                        connection.Open();
                        command.CommandText = @"insert into entry (channel_ID,title,description,article_link,image_link,created_at) values (" + channel_id + ",'" + title + "','" + description + "','" + article_link + "','" + image_link + "','" + created_at + "')";
                        command.Parameters.Add(new SQLiteParameter("@channel_id", channel_id));
                        command.Parameters.Add(new SQLiteParameter("@title", title));
                        command.Parameters.Add(new SQLiteParameter("@description", description));
                        command.Parameters.Add(new SQLiteParameter("@article_link", article_link));
                        command.Parameters.Add(new SQLiteParameter("@image_link", image_link));
                        command.Parameters.Add(new SQLiteParameter("@created_at", created_at));
                        command.ExecuteNonQuery();
                    }
                    catch (SQLiteException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// Downloaderからの呼び出し対応用
        /// </summary>
        /// <param name="lfd"></param>
        internal void AddEntry(HashSet<FeedData> lfd)
        {
            foreach (FeedData fd in lfd)
            {
                int channel_id = new ChannelControll().SearchChannel(fd.ch.title).id;
                fd.ch.id = channel_id;
                foreach(Entry en in fd.Len)
                {
                    AddEntry(en);
                }
            }
        }

        public void DeleteEntry(int id)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    try
                    {
                        connection.Open();
                        command.CommandText = @"delete from entry where id=@id";
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
}
