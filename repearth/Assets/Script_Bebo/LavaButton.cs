using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LavaButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public ParticleSystem particleSys;
    [SerializeField] Animator volcanoAnimator;
    private ParticleSystem.EmissionModule particleEmission;
    private ParticleSystem.CollisionModule particleCollision;

    private bool canClick;
    private bool UIactive;

    private void Awake()
    {
        DialogueManager dm = FindObjectOfType<DialogueManager>();
        dm.OnOpenWindow += DisableInput;
        dm.OnCloseWindow += EnableInput;
        canClick = true;
        UIactive = false;
        particleEmission = particleSys.emission;
        particleCollision = particleSys.collision;
    }

    private void Update()
    {
        if (!UIactive) {
            canClick = particleSys.particleCount <= 0 ? true : false;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (canClick && !UIactive)
        {
            volcanoAnimator.SetBool("isErupting", true);
            particleEmission.enabled = true;
            particleCollision.dampen = 0.035f;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!UIactive) {
            canClick = false;
            volcanoAnimator.SetBool("isErupting", false);
            particleEmission.enabled = false;
            particleCollision.dampen = 0.2f;
        }
    }

    private void DisableInput()
    {
        Debug.Log("disable input");
        UIactive = true;
    }

    private void EnableInput()
    {
        UIactive = false;
    }
}
