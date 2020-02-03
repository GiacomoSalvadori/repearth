using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class JumpEfx : MonoBehaviour
{
    #region Values
    public float jumpForce = 1.0f;
    public float jumpTime = 1.0f;
    public float targetMobile = 180.0f;
    public float targetDesktop = 190.0f;
    RectTransform rt;
    #endregion

    private void Awake()
    {
        rt = GetComponent<RectTransform>();
    }

    private void Start()
    {
        float target = PlatformInstance.IsMobile() ? targetMobile : targetDesktop;
        Debug.Log("target "+target);
        rt.DOAnchorPosY(target, 1.0f, false).SetLoops(-1, LoopType.Yoyo);
    }
}
