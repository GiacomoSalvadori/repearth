using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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


    private SpriteRenderer sprite;
    private bool isCall;

    private float timer;

    public ParticleManager greenParticles;
    public ParticleManager blackParticles;


    public int rotationSpeed = 10;

    // Start is called before the first frame update
    void Start()
    {
        
        sprite = GetComponent<SpriteRenderer>();
        isCall = false;
        SetColor(StateColor.CL_GREEN);
        timer = 0;
        //child = this.gameObject.transform.GetComponentInChildren<ParticleManager>();
    }
    
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        transform.RotateAround(new Vector3(0,0,0), Vector3.forward, rotationSpeed * Time.deltaTime);

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

        isCall = false;
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
        sprite.color = rules.RetrieveHex(newState);
    }

    private void OnParticleCollision(GameObject other)
    {
        if(other.CompareTag("Lava"))
        {
            SetColor(StateColor.CL_GREY);
        }
    }
}

public enum StateColor {
    CL_BLACK,
    CL_GREEN,
    CL_GREY
}