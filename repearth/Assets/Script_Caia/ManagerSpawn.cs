using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject enemy;
    public GameObject place;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            GameObject go = GameObject.Instantiate(enemy, place.transform.position, Quaternion.identity) as GameObject;
            go.transform.LookAt(this.transform.position);
            Vector3 temp = go.transform.rotation.eulerAngles;
            Debug.Log(temp);
            //go.transform.rotation = new Quaternion(0, 0, x, 0);


        }
    }
}
