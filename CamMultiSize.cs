using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMultiSize : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Camera>().orthographicSize /= FunctionCommon.newRatio;
    }
}