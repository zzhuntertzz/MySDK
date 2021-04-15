using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public static class ScreenShake
{
    public enum ShakeType
    {
        Weak, Normal, Strong,
    }
    
    public static void DoShake(ShakeType type)
    {
        DOTween.Kill("Camera");
        switch (type)
        {
            case ShakeType.Weak:
                FunctionCommon.mainCam.DOShakePosition(
                    .2f, Vector3.one * .05f, 5, 0)
                    .SetId("Camera").SetUpdate(true);
                break;
            
            case ShakeType.Normal:
                FunctionCommon.mainCam.DOShakePosition(
                    .4f, Vector3.one * .25f, 10, 90)
                    .SetId("Camera").SetUpdate(true);
                break;
            
            case ShakeType.Strong:
                FunctionCommon.mainCam.DOShakePosition(
                    .7f, Vector3.one * .6f, 10, 45)
                    .SetId("Camera").SetUpdate(true);
                break;
        }
    }
}
