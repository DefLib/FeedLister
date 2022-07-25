using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;

namespace FeedLister.Code.Controller
{

    public class WindowsGadgetDB
    {
        public void CreateTable()
        {
            SQLiteConnection.CreateFile("WindowsGadget.db");
            SQLiteConnection sQLiteConnection = new SQLiteConnection("Data Source=WindowsGadget.db");
            sQLiteConnection.Open();

            string sql = "CREATE TABLE 'channel' ('id' INTEGER NOT NULL,'title' TEXT NOT NULL UNIQUE,'link' TEXT NOT NULL UNIQUE,'description' TEXT NOT NULL,PRIMARY KEY('id' AUTOINCREMENT))";
            SQLiteCommand cmd = new SQLiteCommand(sql,sQLiteConnection);
            cmd.ExecuteNonQuery();
            sql = "CREATE TABLE 'entry' ('id' INTEGER NOT NULL,'channel_id' INTEGER NOT NULL,'title' TEXT NOT NULL UNIQUE,'description' TEXT,'article_link' TEXT NOT NULL UNIQUE,'image_link' TEXT,'created_at' TEXT,PRIMARY KEY('id'),FOREIGN KEY('channel_id') REFERENCES 'channel'('id'))";
            cmd = new SQLiteCommand(sql, sQLiteConnection);
            cmd.ExecuteNonQuery();
            sQLiteConnection.Close();
        }
    }
}