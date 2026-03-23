using System.IO;
using System.Reflection.Metadata.Ecma335;

namespace WpfApp1;

public class OsuParser
{
    public static List<string> GetHitObjects(string path)
    {
        List<string> objects = new List<string>();
        var lines = File.ReadAllLines(path);
        bool hitObject = false;

        foreach (var line in lines)
        {
            if (line.StartsWith("[HitObjects]"))
            {
                hitObject = true;
            }

            if (hitObject)
            {
                objects.Add(line);
            }
        }

        FileParser.DebugMessage("HitObjects successfully parsed", "info");
        return objects;
    }

    public static List<string> GetHitObjectTimes(List<string> hitObjects, int startTime, int endTime)
    {
        List<string> result = new List<string>();

        foreach (string line in hitObjects)
        {
            if (line.StartsWith("[HitObjects]"))
                continue;

            string[] parts = line.Split(',');

            if (parts.Length < 3)
                continue;

            if (int.TryParse(parts[2], out int time))
            {
                if (time >= startTime && time <= endTime)
                {
                    result.Add(line);
                }
            }
        }

        return result;
        FileParser.DebugMessage("All hitobjects within specified time range successfully parsed", "info");
    }

    public static void CreateNewDifficulty(
        string originalPath,
        string newPath,
        List<string> hitObjects,
        string newDiffName)
    {
        var lines = File.ReadAllLines(originalPath).ToList();

        bool inMetadata = false;
        for (int i = 0; i < lines.Count; i++)
        {
            if (lines[i].StartsWith("[Metadata]"))
            {
                inMetadata = true;
                continue;
            }

            if (inMetadata)
            {
                if (lines[i].StartsWith("Version:"))
                {
                    lines[i] = $"Version:{newDiffName}";
                    break; // only change the first Version line
                }

                // stop if we leave Metadata section
                if (lines[i].StartsWith("["))
                    break;
            }
        }

        int hitIndex = lines.FindIndex(line => line.StartsWith("[HitObjects]"));
        if (hitIndex == -1)
            return;

        var output = new List<string>();

        output.AddRange(lines.Take(hitIndex + 1));

        output.AddRange(hitObjects);

        File.WriteAllLines(newPath, output);

        FileParser.DebugMessage($"Created new difficulty: {newDiffName}", "info");
    }

    public static float GetHealthDrain(string filePath)
    {
        var lines = File.ReadAllLines(filePath);

        foreach (var line in lines)
        {
            if (line.Contains("HPDrainRate"))
            {
                // Split on ':' and parse the number
                var parts = line.Split(':');
                if (parts.Length > 1 && float.TryParse(parts[1].Trim(), out float settingVal))
                {
                    return settingVal;
                }
            }
        }

        return 0f;
    }
    
    public static float GetCircleSize(string filePath)
    {
        var lines = File.ReadAllLines(filePath);

        foreach (var line in lines)
        {
            if (line.Contains("CircleSize"))
            {
                // Split on ':' and parse the number
                var parts = line.Split(':');
                if (parts.Length > 1 && float.TryParse(parts[1].Trim(), out float settingVal))
                {
                    return settingVal;
                }
            }
        }

        return 0f;
    }
    
    public static float GetOverallDifficulty(string filePath)
    {
        var lines = File.ReadAllLines(filePath);

        foreach (var line in lines)
        {
            if (line.Contains("OverallDifficulty"))
            {
                // Split on ':' and parse the number
                var parts = line.Split(':');
                if (parts.Length > 1 && float.TryParse(parts[1].Trim(), out float settingVal))
                {
                    return settingVal;
                }
            }
        }

        return 0f;
    }
    
    public static float GetApproachRate(string filePath)
    {
        var lines = File.ReadAllLines(filePath);

        foreach (var line in lines)
        {
            if (line.Contains("ApproachRate"))
            {
                // Split on ':' and parse the number
                var parts = line.Split(':');
                if (parts.Length > 1 && float.TryParse(parts[1].Trim(), out float settingVal))
                {
                    return settingVal;
                }
            }
        }

        return 0f;
    }

    public static string GetDifficultyName(string filePath)
    {
        var lines = File.ReadAllLines(filePath);

        foreach (var line in lines)
        {
            if (line.Contains("Version:"))
            {
                var parts = line.Split(':');
                if (parts.Length > 1)
                {
                    return parts[1].Trim();
                }
            }
        }
        
        return string.Empty;
    }

    public static Metadata GetMetadata(string filePath)
    {
        var metadata = new Metadata();
        var lines = File.ReadAllLines(filePath);
        foreach (var line in lines)
        {
            if (line.StartsWith("Artist:"))
                metadata.Artist = line.Substring("Artist:".Length);

            if (line.StartsWith("Title:"))
                metadata.Title = line.Substring("Title:".Length);
            
            if (line.StartsWith("Creator:"))
                metadata.Creator = line.Substring("Creator:".Length);

            if (metadata.Artist != null && metadata.Title != null)
                break;
        }
        
        return metadata;
    }

    public static List<BeatmapSet> LoadBeatmaps(string path)
    {
        var folders = Directory.GetDirectories(path);
        List<BeatmapSet> sets = new List<BeatmapSet>();

        foreach (var folder in folders)
        {
            List<BeatmapDifficulty> difficulties = new List<BeatmapDifficulty>();
            
            var osuFiles = Directory.GetFiles(folder, "*.osu");
            
            if (osuFiles.Length == 0)
                continue;
            
            var meta = GetMetadata(osuFiles[0]);

            foreach (var file in osuFiles)
            {
                string version = GetDifficultyName(file);
                float cs = GetCircleSize(file);
                float od =  GetOverallDifficulty(file);
                float ar = GetApproachRate(file);
                float hp = GetHealthDrain(file);
                var diff = new BeatmapDifficulty( version, cs, ar, od, hp, file);
                difficulties.Add(diff);
            }
            
            
            BeatmapSet set = new BeatmapSet(folder, difficulties, meta.Artist, meta.Title);
            
            sets.Add(set);
        }

        return sets;
    }
}