using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Colors { Green = "ACF243", Black = "000000", Grey = "808080"}
public enum BlackStrengths { Green = "ACF243", Grey = "808080" }
public enum GreenStrengths { Grey = "808080" }

public class UpdateState : MonoBehaviour
{
    private Colors state;

    public GameObject backNode;
    public GameObject nextNode;

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
        switch (state)
        {
            case Colors.Black:
                node.getState()
            
            case Colors.Green:
                sprite.color = Colors.Green;
        }

        
    }

    public getState()
    {
        return state;
    }


    public setColor(Colors newState)
    {
        state = newState;

        switch(state)
        {
            case Colors.Black :
                sprite.color = Colors.Black;

            case Colors.Green:
                sprite.color = Colors.Green;

            case Colors.Grey:
                sprite.color = Colors.Grey;
        }

    }
}

public enum StateColor {
    CL_BLACK,
    CL_GREEN,
    CL_GREY
}