using UnityEngine;
using System.IO;

public static class ReadFileSystem
{
    // Reads InfoFile that contains information for every level
    // Creates and reads SaveFiles
    private static readonly string SAVE_FOLDER = Application.dataPath + "/SaveFiles/";
    private static readonly string INFOFILE_FOLDER = Application.dataPath + "/InfoFiles/";

    public static void CreateSaveFile(string saveString)
    {
        if (!Directory.Exists(SAVE_FOLDER))
        {
            Directory.CreateDirectory(SAVE_FOLDER);
        }

        File.WriteAllText(SAVE_FOLDER + "save.txt", saveString);
    }

    public static string LoadSaveFile()
    {
        return LoadFile(SAVE_FOLDER, "save.txt");
    }

    public static string LoadLevelsInfoFile()
    {
        return LoadFile(INFOFILE_FOLDER, "levelsInfo.txt");
    }

    private static string LoadFile(string folderName, string fileName)
    {
        if (File.Exists(folderName + fileName))
        {
            string loadData = File.ReadAllText(folderName + fileName);
            return loadData;
        }
        else
        {
            return null;
        }
    }
}
