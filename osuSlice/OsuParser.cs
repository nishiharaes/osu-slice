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
                Console.WriteLine("Added hit object: " + line);
                objects.Add(line);
            }
        }
        Console.WriteLine("All objects successfully parsed");
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
                    Console.WriteLine("HitObject " + line + "added to output between  " + startTime + " and " + endTime);
                    result.Add(line);
                }
            }
        }
        Console.WriteLine("All objects successfully parsed");
        return result;
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
        
        Console.WriteLine($"Created new difficulty: {newDiffName}");
    }
}