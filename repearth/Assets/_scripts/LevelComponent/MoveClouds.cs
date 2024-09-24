using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveClouds : MonoBehaviour
{
    #region Properties
    public float roundTime = 15.0f;
    public bool clockwise = true;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        MoveCloud();
    }

    private void MoveCloud()
    {
        Vector3 rot = new Vector3(0.0f, 0.0f, 360.0f);
        if (clockwise) {
            rot *= -1.0f;
        }
        transform.DORotate(rot, roundTime, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
    }
}
