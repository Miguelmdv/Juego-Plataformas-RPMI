using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float maxSpeed = 4f;
    public float speed = 1f;
    public float jumpPower = 0.11f;
    [HideInInspector] public bool onGround;
    bool jump;
    bool dJump;

    float horizontal;

    Animator animCtr;
    Rigidbody2D rb;



	void Start () {
        rb = gameObject.GetComponent<Rigidbody2D>();
        animCtr = gameObject.GetComponent<Animator>();
    }
	

	void Update () {
        if (onGround) //Salto de precaucion al caer
        {
            dJump = true;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (onGround)
            {
                jump = true;
                dJump = true;
            }
            else if (dJump) //doble salto
            {
                jump = true;
                dJump = false;
            }
        }

        
    }

    private void FixedUpdate()
    {
        //evitar deslizamiento por el material {
        Vector3 fixedVel = rb.velocity;
        fixedVel.x *= 0.75f;
        if (onGround)
        {
            rb.velocity = fixedVel;
        } 
        //}


        horizontal = Input.GetAxis("Horizontal");
        rb.AddForce(Vector2.right * speed * horizontal, ForceMode2D.Force);

        SetWalkAnim();

        FlipSprite();

        ClampearVel();

        if(jump)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * jumpPower);
            jump = false;
            
        }
    }

    void SetWalkAnim()
    {
        if (horizontal != 0 && onGround)
        {
            animCtr.SetBool("Walking", true);
        }
        else
        {
            animCtr.SetBool("Walking", false);
        }
    }

    void FlipSprite()
    {
        if (horizontal > 0.1f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        if (horizontal < -0.1f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }

    void ClampearVel()
    {
        float speedClamped = Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed);
        rb.velocity = new Vector2(speedClamped, rb.velocity.y);
    }

    private void OnBecameInvisible()
    {
        transform.position = new Vector3(-5.69f, -1.36f, 0f);
    }
}
