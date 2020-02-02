using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementManager : MonoBehaviour
{
    public float radius;

    public GameObject prefab;
    public int numberOfObjects;

    public float centerX;
    public float centerY;

    public float scanTime;
    public float minPercentBlack;
    public float minPercentGreen;

    [Header("SPAWN MANAGER")]
    public float spawnTimeGreen;
    public float spawnTimeBlack;
    public float timeToGrey;

    private List<GameObject> nodes = new List<GameObject>();
    [Header("BLACK")]
    public int countBlack;
    public float percentBlack;
    [Header("GREEN")]
    public int countGreen;
    public float percentGreen;

    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        GetPoints();
        countBlack = 0;
        countGreen = 0;
        timer = scanTime - 0.3f;

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > scanTime)
        {
            timer = 0;
            if(countGreen == 0 || countBlack == 0)
            {
                //TODO: lose
            }

            if(percentBlack < minPercentBlack)
            {
                RandomSpawn(StateColor.CL_BLACK);
            }
            if(percentGreen < minPercentGreen)
            {
                RandomSpawn(StateColor.CL_GREEN);
            }
            CountBlackGreen();
        }
    }

    void RandomSpawn(StateColor colorToSpawn)
    {
        System.Random rnd = new System.Random(Guid.NewGuid().GetHashCode());
        int index = rnd.Next(0, nodes.Count);

        UpdateState state = null;
        if(nodes[index].GetComponent<UpdateState>())
        {
            state = nodes[index].GetComponent<UpdateState>();
        }

        switch (colorToSpawn)
        {
            case StateColor.CL_BLACK:
                if (state.state == StateColor.CL_GREEN)
                {
                    Debug.Log("SPAWN BLACK");
                    state.SetColor(colorToSpawn);
                }
                break;

            case StateColor.CL_GREEN:
                if (state.state == StateColor.CL_GREY)
                {
                    state.SetColor(colorToSpawn);
                }
                break;
        }
    }

    void GetPoints()
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            float angle = i * Mathf.PI * 2 / numberOfObjects;
            Vector3 pos = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
            GameObject go = GameObject.Instantiate(prefab, pos, Quaternion.identity);
            go.name = "SingleElement_"+i.ToString();
            float angleRotation = (Mathf.Atan2(pos.y - centerY, pos.x - centerX) * -180 / Mathf.PI + 90) * -1;
            go.transform.localRotation = Quaternion.Euler(0, 0, angleRotation);

            if(go.GetComponent<UpdateState>() != null)
            {
                go.GetComponent<UpdateState>().spawnTimeGreen = spawnTimeGreen;
                go.GetComponent<UpdateState>().spawnTimeBlack = spawnTimeBlack;
                go.GetComponent<UpdateState>().timeToGrey = timeToGrey;
            }

            nodes.Add(go);
        }

        SetupNode();
    }

    void SetupNode()
    {
        for (int i = 0; i < nodes.Count; i++)
        {
            UpdateState state = nodes[i].GetComponent<UpdateState>();
            if (i == 0)
            {
                state.backNode = nodes[nodes.Count - 1];
                state.nextNode = nodes[i + 1];
            }
            else if (i == nodes.Count - 1)
            {
                state.nextNode = nodes[0];
                state.backNode = nodes[i - 1];
            }
            else
            {
                state.backNode = nodes[i - 1];
                state.nextNode = nodes[i + 1];
            }
        }
    }

    void CountBlackGreen()
    {
        countBlack = 0;
        countGreen = 0;
        foreach(var n in nodes)
        {
            UpdateState elem;
            if(n.GetComponent<UpdateState>() != null)
            {
                elem = n.GetComponent<UpdateState>();
                if (elem.state == StateColor.CL_BLACK)
                {
                    countBlack++;
                }
                if (elem.state == StateColor.CL_GREEN)
                {
                    countGreen++;
                }
            }
        }

        percentBlack = ((float)countBlack / numberOfObjects) * 100;
        percentGreen = ((float)countGreen / numberOfObjects) * 100;

    }
}