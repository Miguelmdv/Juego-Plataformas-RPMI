using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfGround : MonoBehaviour {

    PlayerController player;
    Rigidbody2D rb;

	void Start () {
        player = GetComponent<PlayerController>();
        rb = GetComponentInParent<Rigidbody2D>();
	}

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Platform"))
        {
            rb.velocity = new Vector2(0f, 0f);  //evitar bug de movimiento cuando caes en una plataforma que baja
            player.transform.parent = col.transform; //mover pj con la plataforma
            player.onGround = true;
        }
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            player.onGround = true;
        }
        if (col.gameObject.CompareTag("Platform"))
        {
            player.transform.parent = col.transform; //mover pj con la plataforma
            player.onGround = true;            
        }
    }
    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            player.onGround = false;
        }
        if (col.gameObject.CompareTag("Platform"))
        {
            player.transform.parent = null;
            player.onGround = false;         
        }
    }
}
