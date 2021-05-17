using UnityEngine;

public class SingletonDontDestroy<T> : MonoBehaviour where T : SingletonDontDestroy<T>
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

    private void Awake()
    {
        if (I != null)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
}