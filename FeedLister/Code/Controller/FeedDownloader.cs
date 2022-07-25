using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace FeedLister.Code.Controller
{

    /// <summary>
    /// インスタンスに結果を格納する
    /// </summary>
    internal class FeedDownloader
    {

        private List<string> URLs;

        private List<XElement> LxmlDoc;

        public HashSet<Channel> Lch { get; set; }

        public HashSet<Entry> Len { get; set; }

        public FeedDownloader()
        {
            URLs = new List<string>();

            LxmlDoc = new List<XElement>();

            Lch = new HashSet<Channel>();

            Len = new HashSet<Entry>();
        }

        public bool FeedDownload(List<string> URLs)
        {
            LoadURLs(URLs);

            DownLoadFeeds();

            return true;
        }

        /// <summary>
        /// URLの有効性確認のための単体利用可
        /// </summary>
        /// <param name="url"></param>
        public void LoadURLs(string url)
        {
            try
            {
                Console.WriteLine("Start Download Next URL " + url + " ...");

                XElement xmlDoc = XElement.Load(url);
                LxmlDoc.Add(xmlDoc);
                URLs.Add(url);

                Console.WriteLine("Download is Complete!!");
                return;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 内部呼び出し対応
        /// </summary>
        /// <param name="urls"></param>
        private void LoadURLs(List<string> urls)
        {
            foreach (string url in urls)
            {
                LoadURLs(url);
            }
        }

        /// <summary>
        /// 配信サイトからロードする
        /// </summary>
        private void DownLoadFeeds()
        {
            foreach(XElement xmlDoc in LxmlDoc)
            {
                int cf = CheckFeeds(xmlDoc);
                switch (cf)
                {
                    case 0:
                        ForRSS1(xmlDoc);
                        break;

                    case 1:
                        ForRSS2(xmlDoc);
                        break;

                    case 2:
                        ForAtom(xmlDoc);
                        break;
                }
            }
        }

        /// <summary>
        /// 外部からの呼び出し対応
        /// </summary>
        /// <param name="URL"></param>
        /// <returns></returns>
        public static int CheckFeeds(string URL)
        {
            // TODO xpathによる判定の実装

            // if RSS1 => 0 else if RSS2 => 1 else if Atom => 2 else -999

            // XElement xmlDoc.Nameで出せた！

            XElement xmlDoc = XElement.Load(URL);
            string name = xmlDoc.Name.LocalName;
            if (name.Equals("rdf:RDF"))
            {
                Console.WriteLine(URL + " is RSS1!");
                return 0;
            }
            else if (name.Equals("rss"))
            {
                Console.WriteLine(URL + " is RSS2!");
                return 1;
            }
            else if (name.Equals("rdf:RDF"))
            {
                Console.WriteLine(URL + " is Atom!");
                return 2;
            }
            else
            {
                return -999;
            }
        }

        /// <summary>
        /// Feedの種類を確認する
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <returns>
        /// 0 = RSS1
        /// 1 = RSS2
        /// 2 = Atom
        /// </returns>
        private int CheckFeeds(XElement xmlDoc)
        {
            // TODO xpathによる判定の実装

            // if RSS1 => 0 else if RSS2 => 1 else if Atom => 2 else -999

            // XElement xmlDoc.Nameで出せた！

            string name = xmlDoc.Name.LocalName;
            if (name.Equals("rdf:RDF"))
            {
                return 0;
            }
            else if (name.Equals("rss"))
            {
                return 1;
            }
            else if (name.Equals("rdf:RDF"))
            {
                return 2;
            }
            else
            {
                return -999;
            }
        }

        /// <summary>
        /// RSS1用解析メソッド
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <exception cref="Exception"></exception>
        private void ForRSS1(XElement xmlDoc)
        {
            // TODO RSS1用解析メソッド
            throw new Exception();
        }

        /// <summary>
        /// RSS2用解析メソッド
        /// </summary>
        /// <param name="xmlDoc"></param>
        private void ForRSS2(XElement xmlDoc) 
        {
            // RSS2用解析メソッド

            string siteTitle, siteLink, siteDescription;

            try
            {
                XElement channel = xmlDoc.Element("channel");

                try { siteTitle = channel.Element("title").Value; }
                catch (NullReferenceException e)
                {
                    Console.WriteLine(e.Message);
                    siteTitle = "empty";
                }

                try { siteLink = channel.Element("link").Value; }
                catch (NullReferenceException e)
                {
                    Console.WriteLine(e.Message);
                    siteLink = "empty";
                }

                try { siteDescription = channel.Element("description").Value; }
                catch (NullReferenceException e)
                {
                    Console.WriteLine(e.Message);
                    siteDescription = "empty";
                }
                Channel c = new Channel(siteTitle, siteLink, siteDescription);
                Lch.Add(c);

                IEnumerable<XElement> items = channel.Elements("item");
                foreach (XElement item in items)
                {
                    string title = "";
                    string article_link = "";
                    string description = "";
                    string created_at = "";

                    try { title = item.Element("title").Value; }
                    catch (NullReferenceException e)
                    {
                        Console.WriteLine(e.Message);
                        title = "empty";
                    }

                    try { article_link = item.Element("link").Value; }
                    catch (NullReferenceException e)
                    {
                        Console.WriteLine(e.Message);
                        article_link = "empty";
                    }

                    try { description = item.Element("description").Value; }
                    catch (NullReferenceException e)
                    {
                        Console.WriteLine(e.Message);
                        description = "empty";
                    }

                    try { created_at = item.Element("pubDate").Value; }
                    catch (NullReferenceException e)
                    {
                        Console.WriteLine(e.Message);
                        created_at = "empty";
                    }

                    Entry fd = new Entry(
                        int.MaxValue, title, description, article_link, null, created_at
                    );

                    Len.Add(fd);
                }

                return;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {

            }

            return;
        }

        /// <summary>
        /// Atom用解析メソッド
        /// </summary>
        /// <param name="feed"></param>
        /// <returns></returns>
        private List<Entry> ForAtom(XElement feed) 
        {
            // TODO Atom用解析メソッド
            List<Entry> Entry = new List<Entry>();

            int siteId;
            string siteTitle, siteLink, siteDescription;

            siteId = int.Parse(feed.Element("id").Value);
            siteTitle = feed.Element("title").Value;
            siteLink = feed.Element("link").Value;

            return null;
        }
    }
}