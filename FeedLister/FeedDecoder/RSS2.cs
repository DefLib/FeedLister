using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace FeedLister.FeedDecoder
{
    internal class RSS2
    {
        public static Channel ExtractChennelContent(XElement xmlDoc,string url)
        {
            Channel ch = null;

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
                ch = new Channel(siteTitle, siteLink, url, siteDescription);
            }
            catch (Exception e)
            {

            }

            return ch;
        }

        public static FeedData ExtractALLContent(XElement xmlDoc,string url)
        {
            // RSS2用解析メソッド

            Channel ch = null;
            var Len = new List<Entry>();

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
                ch = new Channel(siteTitle, siteLink, url, siteDescription);

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

                return new FeedData(ch, Len);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
    }
}
