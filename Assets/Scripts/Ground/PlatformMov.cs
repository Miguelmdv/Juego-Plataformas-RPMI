using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMov : MonoBehaviour {

    public Transform target;
    public float speed;

    Vector3 start, end;

	// Use this for initialization
	void Start () {
		if(target != null)
        {
            target.parent = null;
            start = transform.position;
            end = target.position;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        if (target != null)
        {
            float fixedSpeed = speed * Time.fixedDeltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, fixedSpeed);
        }

        if(transform.position == target.position)
        {
            //target.position = (target.position == start) ? end : start;
            if(target.position == start)
            { 
                target.position = end;
            }
            else
            {
                target.position = start;
            }

        }
    }
}
