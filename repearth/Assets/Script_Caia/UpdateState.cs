using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UpdateState : MonoBehaviour
{
    private StateColor state;
    public GameObject backNode;
    public GameObject nextNode;
    public ColorRules rules;

    private SpriteRenderer sprite;
    private int waitTime;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        waitTime = 4;
    }
    
    // Update is called once per frame
    void Update()
    {
       StartCoroutine(UpdateColor(sprite.color));    
    }

    IEnumerator UpdateColor(Colors state)
    {
       setColor(state);
       yield return new WaitForSeconds(waitTime);

        if(checkIfUpdate(backNode))
            StartCoroutine(backNode.GetComponent<UpdateState>().UpdateColor(state));
        if (checkIfUpdate(nextNode))
            StartCoroutine(nextNode.GetComponent<UpdateState>().UpdateColor(state));
    }

    bool checkIfUpdate(GameObject node)
    {
        if(rules)
            return rules.CheckStrenght(state, node.GetComponent<UpdateState>().state);

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