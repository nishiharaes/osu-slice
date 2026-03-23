namespace WpfApp1;

public class BeatmapSet
{
    public string Artist { get; private set; }
    public string Title { get; private set; }
    public List<BeatmapDifficulty> Difficulties { get; private set; }
    public string FolderPath { get; private set; }
    
    public BeatmapSet(string path, List<BeatmapDifficulty> difficulties,  string artist, string title)
    {
        this.Difficulties = difficulties;
        this.Artist = artist;
        this.Title = title;
        this.FolderPath = path;
    }
}