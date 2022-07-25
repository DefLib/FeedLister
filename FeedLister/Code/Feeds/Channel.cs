public class Channel
{
    public int id { get; set; }
    public string title { get; set; }
    public string link { get; set; }
    public string description { get; set; }

    public Channel(string title, string link, string description)
    {
        this.title = title;
        this.link = link;
        this.description = description;
    }

    public Channel(int id, string title, string link, string description)
    {
        this.id = id;
        this.title = title;
        this.link = link;
        this.description = description;
    }
}