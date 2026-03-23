namespace WpfApp1;

public class BeatmapDifficulty
{
    public string Version { get; private set; }
    public float cs { get; private set; }
    public float od { get; private set; }
    public float ar { get; private set; }
    public float hp { get; private set; }
    public string filePath { get; private set; }

    public BeatmapDifficulty(string ver, float cs, float od, float ar, float hp, string fPath)
    {
        this.Version = ver;
        this.cs = cs;
        this.od = od;
        this.ar = ar;
        this.hp = hp;
        this.filePath = fPath;
    }
}