using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementManager : MonoBehaviour
{
    public float radius;

    public GameObject prefab;
    public int numberOfObjects = 20;

    public float centerX;
    public float centerY;

    private List<GameObject> nodes = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        GetPoints();
        centerY = 0;
        centerX = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GetPoints()
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            float angle = i * Mathf.PI * 2 / numberOfObjects;
            Vector3 pos = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
            GameObject go = GameObject.Instantiate(prefab, pos, Quaternion.identity);
            float angleRotation = (Mathf.Atan2(pos.y - centerY, pos.x - centerX) * -180 / Mathf.PI + 90) * -1;
            go.transform.localRotation = Quaternion.Euler(0, 0, angleRotation);
            go.AddComponent<UpdateState>();
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
}