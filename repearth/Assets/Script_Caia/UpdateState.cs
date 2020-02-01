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

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        waitTime = 4;
        isCall = false;
        SetColor(StateColor.CL_GREEN);
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
    }
}

public enum StateColor {
    CL_BLACK,
    CL_GREEN,
    CL_GREY
}