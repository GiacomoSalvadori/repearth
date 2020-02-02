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

    // Start is called before the first frame update
    void Start()
    {
        startExpansion = false;
        GameObject.FindObjectOfType<ElementManager>().OnGameReady += Go;
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

    IEnumerator UpdateColor(StateColor state)
    {
        SetColor(state);

        if(state == StateColor.CL_GREEN)
            yield return new WaitForSeconds(spawnTimeGreen);
        else
            yield return new WaitForSeconds(spawnTimeBlack);


        if (checkIfUpdate(backNode))
            backNode.GetComponent<UpdateState>().SetColor(state);
        //StartCoroutine(backNode.GetComponent<UpdateState>().SetColor(state));
        if (checkIfUpdate(nextNode))
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