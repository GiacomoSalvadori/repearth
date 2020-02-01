using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UpdateState : MonoBehaviour
{
    public StateColor state;
    public GameObject backNode;
    public GameObject nextNode;
    public ColorRules rules;

    private SpriteRenderer sprite;
    private int waitTime;
    private bool isCall;
    public ParticleManager greenParticles;
    public ParticleManager blackParticles;


    // Start is called before the first frame update
    void Start()
    {
        
        sprite = GetComponent<SpriteRenderer>();
        waitTime = 20;
        isCall = false;
        SetColor(StateColor.CL_GREEN);
        //child = this.gameObject.transform.GetComponentInChildren<ParticleManager>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (!isCall && state!=StateColor.CL_GREY)
        {
            isCall = true;
            StartCoroutine(UpdateColor(state));
        }
         
    }

    private void TouchedByLava(){
        Debug.Log("piece "+name);
    }

    IEnumerator UpdateColor(StateColor state)
    {
        SetColor(state);

        yield return new WaitForSeconds(waitTime);
       

        if(checkIfUpdate(backNode))
            StartCoroutine(backNode.GetComponent<UpdateState>().UpdateColor(state));
        if (checkIfUpdate(nextNode))
            StartCoroutine(nextNode.GetComponent<UpdateState>().UpdateColor(state));

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

        if(newState == StateColor.CL_GREEN)
        {
            blackParticles.gameObject.SetActive(false);
            greenParticles.gameObject.SetActive(true);
        }
        else if(newState == StateColor.CL_BLACK)
        {
            greenParticles.gameObject.SetActive(false);
            blackParticles.gameObject.SetActive(true);
        }
        else
        {
            greenParticles.gameObject.SetActive(false);
            blackParticles.gameObject.SetActive(false);
        }
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