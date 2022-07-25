public class Entry
{
    public int id { get; set; }
    public int channel_ID { get; set; }
    public string title { get; set; }
    public string description { get; set; }
    public string article_link { get; set; }
    public string image_link { get; set; }
    public string created_at { get; set; }

    public Entry(string title, string description, string article_link, string image_link, string created_at)
    {
        this.title = title;
        this.description = description;
        this.article_link = article_link;
        this.image_link = image_link;
        this.created_at = created_at;
    }
    public Entry(int channel_id,string title, string description, string article_link, string image_link, string created_at)
    {
        this.channel_ID = channel_id;
        this.title = title;
        this.description = description;
        this.article_link = article_link;
        this.image_link = image_link;
        this.created_at = created_at;
    }

    public Entry(int id, int channel_ID, string title, string description, string article_link, string image_link, string created_at)
    {
        this.id = id;
        this.channel_ID = channel_ID;
        this.title = title;
        this.description = description;
        this.article_link = article_link;
        this.image_link = image_link;
        this.created_at = created_at;
    }
}
