public class TagService
{
    public List<string> Tags { get; private set; } = new List<string> { "Food", "Transport", "Utilities", "Entertainment" };

    public void AddTag(string tag)
    {
        if (!string.IsNullOrWhiteSpace(tag) && !Tags.Contains(tag))
        {
            Tags.Add(tag);
        }
    }

    public void RemoveTag(string tag)
    {
        Tags.Remove(tag);
    }
}