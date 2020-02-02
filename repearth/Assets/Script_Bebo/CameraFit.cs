using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFit : MonoBehaviour
{
    [Range(0, 10)]
    public float offset;
    [SerializeField] SpriteRenderer rink;

    private void Start()
    {
        //float orthoSize = rink.bounds.size.x * Screen.height / Screen.width * 0.5f;
        float screenRatio = (float)Screen.width / (float)Screen.height;
        float targetRatio = rink.bounds.size.x + offset / rink.bounds.size.y + offset;

        if (screenRatio >= targetRatio)
        {
            Camera.main.orthographicSize = rink.bounds.size.y + offset / 2;
        }
        else
        {
            float differenceInSize = targetRatio / screenRatio;
            Camera.main.orthographicSize = rink.bounds.size.y + offset / 2 * differenceInSize;
        }

        //Camera.main.orthographicSize = orthoSize;
    }
}
