using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
	public float totalFOV = 70.0f;
    public float rayRange = 10.0f;
	public GameObject enemy;
	public GameObject place;

    void Start()
    {
        
    }

    void Update()
    {
		if(Input.GetKeyDown(KeyCode.A))
		{
			GameObject go = GameObject.Instantiate(enemy, place.transform.position, Quaternion.identity) as GameObject;
			go.transform.LookAt(this.transform.position);
			Vector3 temp = go.transform.rotation.eulerAngles;
			Debug.Log(temp);
			//go.transform.rotation = new Quaternion(0, 0, x, 0);
		}
    }

	//void OnDrawGizmosSelected()
    //{
        
    //    float halfFOV = totalFOV / 2.0f;
    //    Quaternion leftRayRotation = Quaternion.AngleAxis(-halfFOV, Vector3.up);
    //    Quaternion rightRayRotation = Quaternion.AngleAxis(halfFOV, Vector3.up);
    //    Vector3 leftRayDirection = leftRayRotation * transform.forward;
    //    Vector3 rightRayDirection = rightRayRotation * transform.forward;
    //    Gizmos.DrawRay(transform.position, leftRayDirection * rayRange);
    //    Gizmos.DrawRay(transform.position, rightRayDirection * rayRange);
    //}
}