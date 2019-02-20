using System;
using UnityEngine;

public static class JSONUtils
{
    public static T[] FromJSON<T>(string inJSON)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(inJSON);

        if (wrapper == null)
            return null;

        return wrapper.Items;
    }

    public static string ToJSON<T>(T inItem, bool inPrettyPrint = true)
    {
        Wrapper<T> wrapper = new Wrapper<T>
        {
            Items = new T[] { inItem }
        };

        return JsonUtility.ToJson(wrapper, inPrettyPrint);
    }

    public static string ToJSON<T>(T[] inArray, bool inPrettyPrint = true)
    {
        Wrapper<T> wrapper = new Wrapper<T>
        {
            Items = inArray
        };

        return JsonUtility.ToJson(wrapper, inPrettyPrint);
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}
