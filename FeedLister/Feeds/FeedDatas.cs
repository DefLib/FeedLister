using System.Collections.Generic;

public class FeedData
{

    // 全てのFeedに共通する値

    public Channel channel { get; set; }

    public List<Entry> Len { get; set; }

    public FeedData(Channel channel, List<Entry> Len)
    {
        this.channel = channel;
        this.Len = Len;
    }
}
