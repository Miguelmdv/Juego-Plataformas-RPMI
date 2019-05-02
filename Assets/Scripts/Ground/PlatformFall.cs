using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformFall : MonoBehaviour {

    Rigidbody2D rb;
    BoxCollider2D col;

    public float fallDelay = 1f;
    public float respawnDelay = 5f;
    Vector3 posIni;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        posIni = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Invoke("Fall", fallDelay);
            Invoke("Respawn", fallDelay + respawnDelay);
        }
    }

    void Fall()
    {
        rb.isKinematic = false;  //la plataforma cae por fisicas
        col.isTrigger = true; //Cuando se activa trigger desactiva las colisiones
    }

    void Respawn() 
    {
        transform.position = posIni;
        rb.isKinematic = true;
        rb.velocity = Vector3.zero;
        col.isTrigger = false;
    }
}
