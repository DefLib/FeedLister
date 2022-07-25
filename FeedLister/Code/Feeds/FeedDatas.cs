using System;

public class FeedData
{

    // 全てのFeedに共通する値

    public Channel channel { get; set; }

    public Entry entry { get; set; }

    public FeedData(Channel channel, Entry entry)
    {
        this.channel = channel;
        this.entry = entry;
    }
}
