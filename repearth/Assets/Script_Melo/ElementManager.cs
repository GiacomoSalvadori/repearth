using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementManager : MonoBehaviour
{
    public float radius;

    public GameObject prefab;
    public int numberOfObjects = 20;


    // Start is called before the first frame update
    void Start()
    {
        GetPoints();
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
            Instantiate(prefab, pos, Quaternion.identity);
        }
    }
}
