namespace WpfApp1;

public class BeatmapDifficulty
{
    private string Version;
    private string AudioFile;
    private string backgroundFile;
    private int cs;
    private int od;
    private int ar;
    private int hp;
    private string filePath;

    public BeatmapDifficulty(string ver, string aFile, string bgFile, int cs, int od, int ar, int hp, string fPath)
    {
        this.Version = ver;
        this.AudioFile = aFile;
        this.backgroundFile = bgFile;
        this.cs = cs;
        this.od = od;
        this.ar = ar;
        this.hp = hp;
        this.filePath = fPath;
    }
}