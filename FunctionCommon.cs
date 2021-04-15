using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Linq;
using Object = UnityEngine.Object;

public static class FunctionCommon
{
    static Camera _cam;
    public static Camera mainCam
    {
        get
        {
            if (_cam == null)
            {
                _cam = Camera.main;
            }
            return _cam;
        }
    }

    static float _ratio = 0;
    public static float newRatio
    {
        get
        {
            if (_ratio == 0)
            {
                float oldRatio = 1080f / 1920f;
                _ratio = (float) Screen.width / Screen.height;
                _ratio /= oldRatio; //depend on width
            }
            return _ratio;
        }
    }

    private static System.Random rng = new System.Random();

    public static IList<T> Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }

        return list;
    }

    public static int Random(int num1, int num2)
    {
        float val = Random((float) num1, num2);
        int result = Mathf.RoundToInt(val);
        return result;
    }
    
    public static float Random(float num1, float num2)
    {
        return UnityEngine.Random.Range(num1, num2);
    }

    public static Tweener ChangeValueFloat(float startValue, float endValue, float speed,
        Action<float> onUpdate)
    {
        return DOTween.To(() => startValue, x => startValue = x, endValue, speed).OnUpdate(delegate
        {
            onUpdate?.Invoke(startValue);
        });
    }

    public static Tweener ChangeValueInt(int startValue, int endValue, float speed,
        Action<int> onUpdate)
    {
        return DOTween.To(() => startValue, x => startValue = x, endValue, speed).OnUpdate(delegate
        {
            onUpdate?.Invoke(startValue);
        });
    }

    public static Tween DelayTime(float time, Action onDone)
    {
        return DOVirtual.DelayedCall(time, delegate
        {
            onDone();
        });
    }

    public static bool Between(float val, float min, float max)
    {
        return val >= min && val <= max;
    }

    public static void DeleteAllChild<T>(this T target, int start = 0, int end = 0) where T : Transform
    {
        for (int i = target.childCount - 1 - end; i >= start; i--)
        {
            UnityEngine.Object.DestroyImmediate(target.GetChild(i).gameObject);
        }
    }
    
    public static T LoadRandom<T>(this IList<T> list)
    {
        int rand = Random(0, list.Count - 1);
        return list[rand];
    }
    
    public static T LoadRandom<T>(this IList<T> list, Func<T, bool> predic)
    {
        list = list.Where(predic).ToList();
        if (list.Count == 0)
        {
            return default(T);
        }
        int rand = UnityEngine.Random.Range(0, list.Count - 1);
        return list[rand];
    }
    
    public static IList<int> ToListIndex<T>(this IList<T> lst)
    {
        List<int> indexs = new List<int>();
        for (int i = 0; i < lst.Count; i++)
        {
            indexs.Add(i);
        }
        return indexs;
    }

    public static List<T> RandomUnique<T>(this IList<T> lst, int take)
    {
        var result = new List<T>();
        var tmp = lst;
        for (int i = 0; i < take; i++)
        {
            int rand = UnityEngine.Random.Range(0, tmp.Count - 1);
            tmp.RemoveAt(rand);
            result.Add(tmp[rand]);
        }

        return result;
    }
    
    public static List<T> RandomUnique<T>(this IList<T> lst, int take, Func<T, bool> predic)
    {
        lst = lst.Where(predic).ToList();
        if (lst.Count == 0)
        {
            return default(List<T>);
        }

        return RandomUnique(lst, take);
    }

    public static List<T> ConvertToList<T>(this T item)
    {
        var result = new List<T>();
        result.Add(item);
        return result;
    }
    
    public static Vector2 RadianToVector2(this float radian)
    {
        return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
    }
      
    public static Vector2 DegreeToVector2(this float degree)
    {
        return RadianToVector2(degree * Mathf.Deg2Rad);
    }
    
    /// <summary>
    /// Make 3D gameobject x axis look at target in 2D (with object has default rotation like in 3D).
    /// </summary>
    /// <param name="trans">Trans.</param>
    /// <param name="targetTrans">Target trans.</param>
    public static void LookAtAxisX2D(this Transform trans, Transform targetTrans)
    {
        LookAtAxisX2D(trans, targetTrans.position);
    }
    /// <summary>
    /// Make 3D gameobject x axis look at target in 2D (with object has default rotation like in 3D).
    /// </summary>
    /// <param name="trans">Trans.</param>
    /// <param name="targetPosition">Target position.</param>
    public static void LookAtAxisX2D(this Transform trans, Vector3 targetPosition)
    {
        // It's important to know rotating direction (clock-wise or counter clock-wise)
        // If target is above of gameobject (has y value higher) then rotate counter clock-wise and vice versa
        bool isAboveOfXAxis = targetPosition.y > trans.position.y;
        float angle = (isAboveOfXAxis ? 1 : -1) * Vector3.Angle(Vector3.right, targetPosition - trans.position);
//        trans.localRotation = Quaternion.identity;
        trans.localRotation = Quaternion.Euler(Vector3.forward * angle);
    }
    /// <summary>
    /// Make 3D gameobject y axis look at target in 2D (with object has default rotation like in 3D).
    /// </summary>
    /// <param name="trans">Trans.</param>
    /// <param name="targetTrans">Target trans.</param>
    public static void LookAtAxisY2D(this Transform trans, Transform targetTrans)
    {
        LookAtAxisY2D(trans, targetTrans.position);
    }
    /// <summary>
    /// Make 3D gameobject y axis look at target in 2D (with object has default rotation like in 3D).
    /// </summary>
    /// <param name="trans">Trans.</param>
    /// <param name="targetPosition">Target position.</param>
    public static void LookAtAxisY2D(this Transform trans, Vector3 targetPosition)
    {
        var position = trans.position;
        bool isLeftOfYAxis = targetPosition.x < position.y;
        float angle = (isLeftOfYAxis ? 1 : -1) * Vector3.Angle(Vector3.up, targetPosition - position);
//        trans.localRotation = Quaternion.identity;
        trans.localRotation = Quaternion.Euler(Vector3.forward * angle);
    }
    /// 
    /// This is a 2D version of Quaternion.LookAt; it returns a quaternion
    /// that makes the local +X axis point in the given forward direction.
    /// 
    /// forward direction
    /// Quaternion that rotates +X to align with forward
    static void LookAt2D(this Transform transform, Vector2 forward)
    {
        transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(forward.y, forward.x) * Mathf.Rad2Deg);
    }

    public static string ToHexString(this Color c)
    {
        return $"#{ColorUtility.ToHtmlStringRGB(c)}";
    }
    public static Color ToColor(this string hex)
    {
        Color color = Color.white;
        ColorUtility.TryParseHtmlString(hex, out color);
        return color;
    }
}

public static class LoadAssets<T> where T : Object
{
    public static T Load(string path)
    {
        T result = default(T);
#if UNITY_EDITOR
        result = UnityEditor.AssetDatabase.LoadAssetAtPath<T>($"{path}");
#endif
        return result;
    }
    public static List<string> LoadAllName(string path, string extension)
    {
        List<string> lstName = Directory.GetFiles(path).Where(x => x.EndsWith(extension))
            .Select(x => Path.GetFileName(x)).ToList();
        return lstName;
    }
}
