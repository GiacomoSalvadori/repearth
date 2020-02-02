using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class JumpEfx : MonoBehaviour
{
    #region Values
    public float jumpForce = 1.0f;
    public float jumpTime = 1.0f;
    public RectTransform target;
    RectTransform rt;
    #endregion

    private void Awake()
    {
        rt = GetComponent<RectTransform>();
        Debug.Log("rect "+rt.position);
    }

    private void Start()
    {
        //rt.DOJumpAnchorPos(target.position, jumpForce, 1, jumpTime).SetLoops(-1, LoopType.Yoyo);
        rt.DOAnchorPosY(180.0f, 1.0f, false).SetLoops(-1, LoopType.Yoyo);
    }
}
