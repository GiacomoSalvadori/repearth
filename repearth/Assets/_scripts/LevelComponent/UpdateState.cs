using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class UpdateState : MonoBehaviour
{
    public StateColor state;
    public GameObject backNode;
    public GameObject nextNode;
    public ColorRules rules;

    [HideInInspector]
    public float spawnTimeGreen;
    [HideInInspector]
    public float spawnTimeBlack;
    [HideInInspector]
    public float timeToGrey;

    private SpriteRenderer sprite;

    private float timer;
    private bool startExpansion;

    public ParticleManager greenParticles;
    public ParticleManager blackParticles;


    public int rotationSpeed = 10;

    private FxManager fxManager;

    // Start is called before the first frame update
    void Start()
    {
        
        fxManager = transform.Find("FX").GetComponent<FxManager>();
        startExpansion = false;
        GameObject.FindObjectOfType<ElementManager>().OnGameReady += Go;
        GameObject.FindObjectOfType<ElementManager>().OnGameEnd += End;
        sprite = GetComponent<SpriteRenderer>();

        if(state != StateColor.CL_BLACK)
            sprite.color = rules.RetrieveHex(state);
        
        timer = 0;
    }


    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(new Vector3(0, 0, 0), Vector3.forward, rotationSpeed * Time.deltaTime);
        if (startExpansion)
        {
            timer += Time.deltaTime;

            switch (state)
            {
                case StateColor.CL_BLACK:
                    if (timer > spawnTimeBlack)
                    {
                        timer = 0;
                        StartCoroutine(UpdateColor(state));
                    }
                    break;
                case StateColor.CL_GREEN:
                    if (timer > spawnTimeGreen)
                    {
                        timer = 0;
                        StartCoroutine(UpdateColor(state));
                    }
                    break;
            }
        }
    }

    private void Go()
    {
        startExpansion = true;
    }

    private void End()
    {
        startExpansion = false;
    }



    IEnumerator UpdateColor(StateColor state)
    {
        SetColor(state);

        if(state == StateColor.CL_GREEN)
            yield return new WaitForSeconds(spawnTimeGreen);
        else
            yield return new WaitForSeconds(spawnTimeBlack);


        if (checkIfUpdate(backNode) && backNode.GetComponent<UpdateState>().state != state)
            backNode.GetComponent<UpdateState>().SetColor(state);
        //StartCoroutine(backNode.GetComponent<UpdateState>().SetColor(state));
        if (checkIfUpdate(nextNode) && nextNode.GetComponent<UpdateState>().state != state)
            nextNode.GetComponent<UpdateState>().SetColor(state);
            //StartCoroutine(nextNode.GetComponent<UpdateState>().SetColor(state));

    }

    bool checkIfUpdate(GameObject node)
    {
        if(rules)
        {
            bool r = rules.CheckStrenght(state, node.GetComponent<UpdateState>().state);
            return r;
        }

        return false;
    }

    public void SetColor(StateColor newState)
    {
        if(state == StateColor.CL_GREY && newState == StateColor.CL_GREEN)
            fxManager.SwitchFX(newState);
        else if((state == StateColor.CL_GREEN || state == StateColor.CL_GREY) && newState == StateColor.CL_BLACK)
            fxManager.SwitchFX(newState);
        else if(newState == StateColor.CL_GREY)
            fxManager.SwitchFX(newState);

        state = newState;
        //sprite.color = rules.RetrieveHex(newState);
        if (newState != StateColor.CL_RED)
            sprite.DOColor(rules.RetrieveHex(newState), 1.5f);
        else
            sprite.color = rules.RetrieveHex(newState);

    }


    private void OnParticleCollision(GameObject other)
    {
        if(other.CompareTag("Lava"))
        {
            if(state != StateColor.CL_RED)
            {
                SetColor(StateColor.CL_RED);
                sprite.DOColor(rules.RetrieveHex(StateColor.CL_GREY), timeToGrey).OnComplete(() => SetColor(StateColor.CL_GREY));
            }
        }
    }
}

public enum StateColor {
    CL_BLACK,
    CL_GREEN,
    CL_GREY,
    CL_RED
}
