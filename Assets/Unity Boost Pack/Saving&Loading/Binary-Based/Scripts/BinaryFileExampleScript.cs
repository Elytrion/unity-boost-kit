using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinaryFileExampleScript : MonoBehaviour
{
    private void Start()
    {
        BinaryExampleDataPacket data1 = new BinaryExampleDataPacket(1, "This is data packet 1");

        //Returns false, as we have not created a binary file called "binaryData" with the extension ".custom"
        Debug.Log(BinaryFileManager.DoesBinaryFileExist("/binaryData", ".custom"));

        //Creates a ".custom" binary file at Application.persistentDataPath from the DataPacket data1
        BinaryFileManager.CreateBinaryFile(data1, "/binaryData", ".custom");

        //Returns true, as we just created the file above
        Debug.Log(BinaryFileManager.DoesBinaryFileExist("/binaryData", ".custom"));

        //Displays "This is data packet 1" in the console, as we retrieved the data from the file
        //(Remember to specify the object you want back, in this case, BinaryExampleDataPacket)
        Debug.Log(BinaryFileManager.GetObjectFromBinaryFile<BinaryExampleDataPacket>("/binaryData", ".custom").StringData);

        //Deletes the file "/binaryData.custom" and outputs "DELETED /binaryData.custom" in the console
        BinaryFileManager.DeleteBinaryFile("/binaryData", ".custom");

    }
}

//Example object: This is the object type that will be saved for this example,
//although this can be changed for whatever you plan to use as your data container
//Note: The object MUST be serializable
[System.Serializable]
public struct BinaryExampleDataPacket
{
    public int IntData;
    public string StringData;

    public BinaryExampleDataPacket(int inInt, string inStringData)
    {
        IntData = inInt;
        StringData = inStringData;
    }
}
