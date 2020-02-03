using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class FxManager : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] List<GameCharacter> fx = new List<GameCharacter>();

    void Awake()
    {
        //transform.localScale = new Vector3(1, 0, 1);
        spriteRenderer = GetComponent<SpriteRenderer>();
        Hide();
    }

    public void Hide()
    {
        spriteRenderer.enabled = false;
    }

    public void Show()
    {
        spriteRenderer.enabled = true;
    }

    public void Popup()
    {
        Show();
        transform.DOScaleY(1, 2f).SetEase(Ease.InBounce);
    }
    
    public void Popout()
    {
        transform.DOScaleY(0, 1f).SetEase(Ease.OutBounce);
        Hide();
    }

    public void SwitchFX(StateColor color)
    {
        Popout();
        var go = fx.Find(x => x.chName == color.ToString());
        if (go)
        {
            spriteRenderer.sprite = go.img;
        }
        else
        {
            spriteRenderer.sprite = null;
        }

        Popup();
    }
}
