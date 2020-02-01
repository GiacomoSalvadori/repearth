using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerSpawn : MonoBehaviour
{
    public GameObject enemy;
    public GameObject placePoint;
    public GameObject startPoint;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            GameObject go = GameObject.Instantiate(enemy, placePoint.transform.position, Quaternion.identity) as GameObject;
            //go.transform.LookAt(this.transform.position);
            float angle =  (Mathf.Atan2(placePoint.transform.position.y - startPoint.transform.position.y, placePoint.transform.position.x - startPoint.transform.position.x) * -180 / Mathf.PI  + 90) * -1;
            go.transform.localRotation = Quaternion.Euler(0, 0, angle);
        }

    }
}
