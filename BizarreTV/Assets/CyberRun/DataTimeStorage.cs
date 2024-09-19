using System;
using System.IO;
using UnityEngine;

public static class DataTimeStorage
{
    private static string filePath = Application.dataPath + "/bestTime.txt";

    public static void SaveTime(float time)
    {
        float bestTime = LoadBestTime();
        if (time < bestTime || bestTime == 0)
        {
            File.WriteAllText(filePath, time.ToString());
        }
    }

    public static float LoadBestTime()
    {
        if (File.Exists(filePath))
        {
            string fileContents = File.ReadAllText(filePath);
            if (float.TryParse(fileContents, out float bestTime))
            {
                return bestTime;
            }
        }
        return 0; // Если файла нет или произошла ошибка при чтении, возвращаем 0
    }
}