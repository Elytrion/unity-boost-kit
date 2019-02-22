using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSONExampleScript : MonoBehaviour
{
    private void Start()
    {
        JSONExampleDataPacket data1 = new JSONExampleDataPacket(1, "This is data packet 1");
        JSONExampleDataPacket data2 = new JSONExampleDataPacket(2, "This is data packet 2");
        JSONExampleDataPacket data3 = new JSONExampleDataPacket(3, "This is data packet 3");
        JSONExampleDataPacket[] datas = new JSONExampleDataPacket[]
        {
            new JSONExampleDataPacket(4, "Hi, I'm in an array"),
            new JSONExampleDataPacket(5, "Hey, I'm in an array too"),
            new JSONExampleDataPacket(6, "Hello, I'm here")
        };

        //creates a JSON file at Application.persistentDataPath from the DataPacket data1
        JSONFileManager.CreateJSONFile(data1, "/data1.json");

        //Returns true, as we just created the file above
        Debug.Log(JSONFileManager.DoesJSONFileExist("/data1.json"));

        //Returns false, as we have not created a file called "/data2.json"
        Debug.Log(JSONFileManager.DoesJSONFileExist("/data2.json"));

        //Displays "This is data packet 1" in the console, as we retrieved the data from the file
        //(Remember to specify the object you want back, in this case, JSONExampleDataPacket)
        Debug.Log(JSONFileManager.GetObjectFromJSON<JSONExampleDataPacket>("/data1.json").StringData);

        //Deletes the file "/data1.json" and outputs "DELETED /data1.json" in the console
        JSONFileManager.DeleteJSONFile("/data1.json");

        //creates a JSON file at Application.persistentDataPath from the DataPacket data2
        JSONFileManager.CreateJSONFile(data2, "/data2.json");

        //Adds DataPacket data3 to the JSON File we just created
        JSONFileManager.AddToJSONFile(data3, "/data2.json");

        //Outputs the IntData and StringData from the two data packets in the file
        //Example "3, This is data packet 3"
        foreach (JSONExampleDataPacket data in JSONFileManager.GetObjectsFromJSON<JSONExampleDataPacket>("/data2.json"))
        {
            Debug.Log(data.IntData + ", " + data.StringData);
        }

        //Deletes the file "/data2.json" and outputs "DELETED /data2.json" in the console
        JSONFileManager.DeleteJSONFile("/data2.json");

        //creates a JSON file at Application.persistentDataPath from the array of DataPackets datas
        JSONFileManager.CreateJSONFileFromArray(datas, "/dataArray.json");

        //Outputs the IntData and StringData from each DataPacket in the array to the console
        //Example "6, Hello, I'm here"
        foreach (JSONExampleDataPacket data in JSONFileManager.GetObjectsFromJSON<JSONExampleDataPacket>("/dataArray.json"))
        {
            Debug.Log(data.IntData + ", " + data.StringData);
        }

        //Deletes the file "/dataArray.json" and outputs "DELETED /dataArray.json" in the console
        JSONFileManager.DeleteJSONFile("/dataArray.json");

        /* Pay attention to how I've been writing the file names as /fileName.json instead of just fileName
         * This is because your file is saved as a file path, which requires the forward slash (/) and
         * file extension (.json) to be found and be a valid file path
         * (Note that JSONFileManager does automatically fix this issue, but it's still good
         * practice to write the file name as stated)
         */
    }
}

//Example object: This is the object type that will be saved for this example,
//although this can be changed for whatever you plan to use as your data container
//Note: The object MUST be serializable
[System.Serializable]
public struct JSONExampleDataPacket
{
    public int IntData;
    public string StringData;

    public JSONExampleDataPacket (int inInt, string inStringData)
    {
        IntData = inInt;
        StringData = inStringData;
    }
}

