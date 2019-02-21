using System.IO;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public static class JSONFileManager
{
    //Change this to change the directory where saved files will go
    public static string DEFAULT_FILE_PATH = Application.persistentDataPath;

    /// <summary>
    /// Determines if the specified file exists
    /// </summary>
    public static bool DoesJSONFileExist(string inFileName)
    {
        string fullFilePath = GetFullFilePath(inFileName);
        return File.Exists(fullFilePath);
    }

    /// <summary>
    /// Deletes the specified file
    /// </summary>
    public static void DeleteJSONFile(string inFileName)
    {
        if (DoesJSONFileExist(inFileName))
        {
            File.Delete(GetFullFilePath(inFileName));
            Debug.Log("DELETED " + inFileName);
        }
        else
        {
            Debug.LogError(inFileName + " NOT FOUND");
        }
    }

    /// <summary>
    /// Creates a JSON file from the specified object and overrides any JSON file with the same name
    /// </summary>
    public static void CreateJSONFile<T>(T inObjectToSave, string inFileName)
    {
        string jsonText = JSONUtils.ToJSON(inObjectToSave, true);

        WriteTextToFile(jsonText, inFileName);
    }

    /// <summary>
    /// Creates a JSON file from an array of objects and overrides any JSON file with the same name
    /// </summary>
    public static void CreateJSONFileFromArray<T>(T[] inObjectsToSave, string inFileName)
    {
        foreach (T objectToSave in inObjectsToSave)
        {
            AddToJSONFile(objectToSave, inFileName);
        }
    }

    /// <summary>
    /// Adds the specified object to a prexisting JSON file and creates a JSON file if the file does not exist
    /// </summary>
    public static void AddToJSONFile<T>(T inObjectToSave, string inFileName)
    {
        if (GetJSONText(inFileName) == null)
        {
            CreateJSONFile(inObjectToSave, inFileName);

            string initialJSONText = JSONUtils.ToJSON(inObjectToSave);
            WriteTextToFile(initialJSONText, inFileName);

            return;
        }

        List<T> jsonObjects = GetObjectsFromJSON<T>(inFileName).OfType<T>().ToList();

        jsonObjects.Add(inObjectToSave);

        string newJSONText = JSONUtils.ToJSON(jsonObjects.ToArray());

        WriteTextToFile(newJSONText, inFileName);
    }

    /// <summary>
    /// Returns a specified object from a JSON file
    /// </summary>
    public static T GetObjectFromJSON<T>(string inFileName)
    {
        return JSONUtils.FromJSON<T>(GetJSONText(inFileName))[0];
    }

    /// <summary>
    /// Returns an array of specified objects from a JSON file
    /// </summary>
    public static T[] GetObjectsFromJSON<T>(string inFileName)
    {
        return JSONUtils.FromJSON<T>(GetJSONText(inFileName));
    }

    /// <summary>
    /// Gets the raw JSON data within the JSON file
    /// </summary>
    private static string GetJSONText(string inFileName)
    {
        if (!DoesJSONFileExist(inFileName))
            return null;

        return File.ReadAllText(GetFullFilePath(inFileName));
    }

    /// <summary>
    /// Writes the raw JSON data into the specified file
    /// </summary>
    private static void WriteTextToFile(this string inJSONText, string inFileName)
    {
        File.WriteAllText(GetFullFilePath(inFileName), inJSONText);
    }

    /// <summary>
    /// Gets the full path of a file
    /// </summary>
    private static string GetFullFilePath(string inFileName)
    {
        if (inFileName.Contains("/"))
        {
            if (inFileName.Contains(".json"))
            {
                return DEFAULT_FILE_PATH + inFileName;
            }
            else
            {
                string editedFileName = inFileName + ".json";
                return DEFAULT_FILE_PATH + editedFileName;
            }
        }
        else
        {
            if (inFileName.Contains(".json"))
            {
                return DEFAULT_FILE_PATH + "/" + inFileName;
            }
            else
            {
                string editedFileName = "/" + inFileName + ".json";
                return DEFAULT_FILE_PATH + editedFileName;
            }
        }
    }
}
