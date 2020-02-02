using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FadeSplash : MonoBehaviour
{
    #region Values
    public CanvasGroup panel;
    #endregion

    void Awake()
    {
        panel.alpha = 1.0f;
        DOVirtual.DelayedCall(1.0f, () => panel.DOFade(0.0f, 1.5f).OnComplete(() => gameObject.SetActive(false)));
    }
}
