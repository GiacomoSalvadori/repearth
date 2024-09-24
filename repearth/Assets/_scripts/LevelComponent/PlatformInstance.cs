using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformInstance
{
    public static bool IsMobile()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android) {
            return true;
        } else {
            return false;
        }
    }
}
