using UnityEngine;

public class SingletonScriptableObject<T> : ScriptableObject where T : SingletonScriptableObject<T>
{
    private static T i;
    public static T I
    {
        get
        {
            if (i == null)
            {
                i = Resources.Load<T>(typeof(T).Name);
            }
            return i;
        }
    }
}