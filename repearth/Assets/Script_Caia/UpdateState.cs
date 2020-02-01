using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateState : MonoBehaviour
{
    // 1 = nero; 2 = verde; 3 = grigio
    public GameObject backNode;
    public GameObject nextNode;

    private SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //StartCoroutine(UpdateColor(sprite.color));
    }

    IEnumerator UpdateColor(Color newColor)
    {
        sprite.color = newColor;

        yield return new WaitForSeconds(4);

        StartCoroutine(backNode.GetComponent<UpdateState>().UpdateColor(newColor));
        StartCoroutine(nextNode.GetComponent<UpdateState>().UpdateColor(newColor));
    }
}
