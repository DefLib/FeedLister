using FeedLister.FeedDecoder;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace FeedLister.Controller
{
    class APIControll
    {
        internal List<Channel> GetPopChannelList()
        {
            StreamReader sr = new StreamReader(new WebClient().OpenRead(@"http://localhost:8000"));
            string urlsoruse = sr.ReadToEnd().Replace("\"","");
            string[] urls = urlsoruse.Split(",");

            var lch = new List<Channel>();
            foreach(string url in urls)
            {
                switch (FeedDownloader.CheckFeeds(url))
                {
                    case 0:
                        break;
                    case 1:
                        lch.Add(RSS2.ExtractChennelContent(FeedDownloader.GetxmlDoc(url), url));
                        break;
                    case 2:
                        break;
                    default:
                        break;
                }
            }
            return lch;
        }
    }
}
