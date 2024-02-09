public class TodoItem
{
    public long UserId { get; set; }
    public long Id { get; set; }
    public string? Title { get; set; }
    public bool Completed { get; set; }
}

public class Posts
{
    public long UserId { get; set; }
    public long Id { get; set; }
    public string? Title { get; set; }
    public string? Body { get; set; }
}