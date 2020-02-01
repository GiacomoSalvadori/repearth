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

    private void Awake()
    {
        canClick = true;
        particleEmission = particleSys.emission;
        particleCollision = particleSys.collision;
    }

    private void Update()
    {
        canClick = particleSys.particleCount <= 0 ? true : false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (canClick)
        {
            volcanoAnimator.SetBool("isErupting", true);
            particleEmission.enabled = true;
            particleCollision.dampen = 0;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        canClick = false;
        volcanoAnimator.SetBool("isErupting", false);
        particleEmission.enabled = false;
        particleCollision.dampen = 0.2f;
    }
}
