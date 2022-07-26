using FeedLister.Exceptions;
using FeedLister.FeedDecoder;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace FeedLister.Controller
{

    /// <summary>
    /// インスタンスに結果を格納する
    /// </summary>
    internal class FeedDownloader
    {

        private List<string> URLs;

        public HashSet<Channel> Lch { get; set; }

        public HashSet<Entry> Len { get; set; }

        public HashSet<FeedData> Lfd { get; set; }

        public FeedDownloader()
        {
            URLs = new List<string>();

            Lch = new HashSet<Channel>();

            Len = new HashSet<Entry>();

            Lfd = new HashSet<FeedData>();
        }

        public void FeedDownload(List<string> URLs)
        {
            LoadURLs(URLs);

            foreach(string URL in this.URLs)
            {
                DownLoadFeeds(URL);
            }

            new EntryControll().AddEntry(Lfd);

            return;
        }

        /// <summary>
        /// URLの有効性確認のための単体利用可
        /// </summary>
        /// <param name="url"></param>
        public void LoadURLs(string url)
        {
            try
            {
                Console.WriteLine("Check Next URL " + url + " ...");

                _ = XElement.Load(url);
                URLs.Add(url);

                Console.WriteLine("Check is Complete!!");
                return;
            }
            catch (Exception)
            {
                return;
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
        private void DownLoadFeeds(string URL)
        {
            Console.WriteLine("Start Download Next URL " + URL + " ...");

            XElement xmlDoc = XElement.Load(URL);

            Console.WriteLine("Download is Complete!!");

            FeedData fd = null;
            int cf = CheckFeeds(xmlDoc);
            switch (cf)
            {
                // This Case Is Not Setting channel_id;
                case 0:
                    fd = RSS1.ExtractALLContent(xmlDoc, URL);
                    break;

                case 1:
                    fd = RSS2.ExtractALLContent(xmlDoc, URL);
                    break;

                case 2:
                    fd = Atom.ExtractALLContent(xmlDoc, URL);
                    break;
            }

            Lfd.Add(fd);
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

        internal static XElement GetxmlDoc(string url)
        {
            return XElement.Load(url);
        }
    }
}