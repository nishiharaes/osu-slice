namespace WpfApp1;

public class BeatmapDifficulty
{
    private string Version;
    private string AudioFile;
    private string backgroundFile;
    private float cs;
    private float od;
    private float ar;
    private float hp;
    private string filePath;

    public BeatmapDifficulty(string ver, string aFile, string bgFile, float cs, float od, float ar, float hp, string fPath)
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