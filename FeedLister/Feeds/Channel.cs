public class Channel
{
    public int id { get; set; }
    public string title { get; set; }
    public string link { get; set; }
    public string feedLink { get; set; }
    public string description { get; set; }

    public Channel(string title, string link, string feedLink, string description)
    {
        this.title = title;
        this.link = link;
        this.feedLink = feedLink;
        this.description = description;
    }

    public Channel(int id, string title, string link, string feedLink, string description)
    {
        this.id = id;
        this.title = title;
        this.link = link;
        this.feedLink = feedLink;
        this.description = description;
    }
}