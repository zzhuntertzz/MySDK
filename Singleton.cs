using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T i;
    public static T I
    {
        get
        {
            if (i == null)
            {
                i = FindObjectOfType<T>();
            }
            return i;
        }
    }
}