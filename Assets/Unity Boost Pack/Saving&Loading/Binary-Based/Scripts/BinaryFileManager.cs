using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class BinaryFileManager
{
    //Change this to change the directory where saved files will go
    public static string DEFAULT_FILE_PATH = Application.persistentDataPath;

    public const string DEFAULT_FILE_EXTENSION = ".custom";

    /// <summary>
    /// Determines if the specified file exists
    /// </summary>
    public static bool DoesBinaryFileExist(string inFileName, string inFileExtension = DEFAULT_FILE_EXTENSION)
    {
        return File.Exists(GetFullFilePath(inFileName, inFileExtension));
    }

    /// <summary>
    /// Deletes the specified file
    /// </summary>
    public static void DeleteBinaryFile(string inFileName, string inFileExtension = DEFAULT_FILE_EXTENSION)
    {
        if (DoesBinaryFileExist(inFileName, inFileExtension))
        {
            File.Delete(GetFullFilePath(inFileName, inFileExtension));
            Debug.Log("DELETED " + inFileName);
        }
        else
        {
            Debug.LogError(inFileName + " NOT FOUND");
        }
    }

    /// <summary>
    /// Creates a binary file with a specified extension from a specified object
    /// </summary>
    public static void CreateBinaryFile<T>(T inObjectToSave, string inFileName, string inFileExtension = DEFAULT_FILE_EXTENSION)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(GetFullFilePath(inFileName, inFileExtension), FileMode.Create);
        bf.Serialize(stream, inObjectToSave);
        stream.Close();
    }

    /// <summary>
    /// Adds a specified object to a specified binary file
    /// </summary>
    public static void AddToBinaryFile<T>(T inObjectToSave, string inFileName, string inFileExtension = DEFAULT_FILE_EXTENSION)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(GetFullFilePath(inFileName, inFileExtension), FileMode.Append);
        bf.Serialize(stream, inObjectToSave);
        stream.Close();
    }

    /// <summary>
    /// Returns a specified object from a binary file
    /// </summary>
    public static T GetObjectFromBinaryFile<T>(string inFileName, string inFileExtension = DEFAULT_FILE_EXTENSION)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(GetFullFilePath(inFileName, inFileExtension), FileMode.Open);
        T objectFromFile = (T)bf.Deserialize(stream);
        stream.Close();
        return objectFromFile;
    }

    private static string GetFullFilePath(string inFileName, string inFileExtension)
    {
        if (!inFileExtension.Contains("."))
        {
            inFileExtension = "." + inFileExtension;
        }

        if (inFileName.Contains("."))
        {
            return DEFAULT_FILE_PATH + inFileName;
        }
        else
        {
            if (inFileName.Contains("/"))
            {
                return DEFAULT_FILE_PATH + inFileName + inFileExtension;
            }
            else
            {
                return DEFAULT_FILE_PATH + "/" + inFileExtension + inFileExtension;
            }
        }
    }
}
