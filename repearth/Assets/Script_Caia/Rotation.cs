using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public float rotationSpeed = 10;
    private float angle = 90;
    public Vector3 center;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(new Vector3(0,0,0), Vector3.forward, rotationSpeed * Time.deltaTime);
    }

}