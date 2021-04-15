using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FirstTime
{
    private static Dictionary<string, bool> firstTimeDic = new Dictionary<string, bool>();

    public static bool IsFirstTime(string keyName, bool defaultValue = true)
    {
        string key = keyName;
        if (firstTimeDic.ContainsKey(key))
        {
            return firstTimeDic[keyName];
        }
        else
        {
            return defaultValue;
        }
    }

    public static void SetFirstTime(string keyName, bool value)
    {
        string key = keyName;
        if (firstTimeDic.ContainsKey(key))
        {
            firstTimeDic[key] = value;
        }
        else
        {
            firstTimeDic.Add(key, value);
        }
    }
}
