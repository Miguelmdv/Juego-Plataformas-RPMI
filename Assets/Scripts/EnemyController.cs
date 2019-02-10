using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public float maxSpeed = 3f;
    public float speed = 3f;

    Rigidbody2D rb;

    // Use this for initialization
    void Start () {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        rb.AddForce(Vector2.right * speed, ForceMode2D.Force);

        ClampearVel();

 
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        //Para gestionar la eliminacion del enemigo por caerle encima ademas de la funcion siguiente

        //if (col.gameObject.CompareTag("Player"))
        //{
        //    float yOffset = 0.8f;
        //    if (transform.position.y + yOffset < col.transform.position.y)
        //    {
        //        col.SendMessage("EnemyJump");
        //        Destroy(gameObject);
        //    }
        //    else
        //    {
        //        col.SendMessage("EnemyKnockBack", transform.position.x);
        //    }
        //}

        if (col.gameObject.CompareTag("Player"))
        {
            col.SendMessage("EnemyKnockBack", transform.position.x);
        }
    }

    void ClampearVel()
    {
        float speedClamped = Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed);
        rb.velocity = new Vector2(speedClamped, rb.velocity.y);

        if (rb.velocity.x > -0.01f && rb.velocity.x < 0.01f)
        {
            speed *= -1;
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
    }
}
