namespace WpfApp1;

public class BeatmapSet
{
    private string Artist;
    private string Title;
    List<BeatmapDifficulty> Difficulties;
    
    public BeatmapSet(List<BeatmapDifficulty> difficulties,  string artist, string title)
    {
        this.Difficulties = difficulties;
        this.Artist = artist;
        this.Title = title;
    }
}