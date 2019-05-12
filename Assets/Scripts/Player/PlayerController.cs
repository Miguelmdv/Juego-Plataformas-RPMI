using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerController : MonoBehaviour {

    float health = 100;
    public float maxSpeed = 4f;
    public float speed = 1f;
    public float jumpPower = 0.11f;
    [HideInInspector] public bool onGround;
    bool jump;
    bool dJump;

    float horizontal;

    Animator animCtr;
    Rigidbody2D rb;
    SpriteRenderer spr;
    [SerializeField] BoxCollider2D collAttack;


    public bool pause = false;
    public GameObject retry, pauseMenu;
    [SerializeField] AudioMixerSnapshot[] snapshot;




    void Start () {
        rb = GetComponent<Rigidbody2D>();
        animCtr = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
    }

    void CheckGameOver()
    {
        if (health <= 0)
        {
            //Destroy(spawnEnDestroyer);
            retry.SetActive(true);
            Destroy(gameObject);
        }
    }
	

	void Update () {

        horizontal = Input.GetAxis("Horizontal");

        Jump();

        SetJumpAnim();
        SetWalkAnim();

        FlipSprite();
        SetAttack();

        if (Input.GetKeyDown(KeyCode.Escape)) // menu de pausa
        {
            pause = !pause;
            pauseMenu.SetActive(pause);
            if (pause)
            {
                Time.timeScale = 0;
                snapshot[1].TransitionTo(0);
            }
            else
            {
                Time.timeScale = 1;
                snapshot[0].TransitionTo(0);
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

        rb.AddForce(Vector2.right * speed * horizontal, ForceMode2D.Force);

        ClampearVel();

        if(jump)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * jumpPower);
            jump = false;
            
        }
    }


    void Jump()
    {
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


    void SetWalkAnim() //Animacion de andar
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

    void SetJumpAnim(){  //Animacion de salto y caida
        animCtr.SetFloat("Jump", rb.velocity.y);
        if (onGround)
            animCtr.SetBool("Ground", true);
        else
            animCtr.SetBool("Ground", false);
    }


    void SetAttack() // animacion de ataque
    {
        if (Input.GetMouseButtonDown(0))
        {
            animCtr.SetTrigger("Attack");
            collAttack.enabled = true;
        }
    }


    void FinishAttack()
    {
        collAttack.enabled = false;
    }




    void FlipSprite() //Dar la vuelta al sprite segun la direccion
    {
        if (horizontal > 0.1f)
        {
            transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        }
        else if (horizontal < -0.1f)
        {
            transform.localScale = new Vector3(-0.2f, 0.2f, 0.2f);
        }
    }


    void ClampearVel()  //limitar velocidad
    {
        float speedClamped = Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed);
        rb.velocity = new Vector2(speedClamped, rb.velocity.y);
    }

    private void OnBecameInvisible() //fuera de la camara
    {
        transform.position = new Vector3(-5.69f, -1.36f, 0f);
    }


    public void EnemyJump()
    {
        jump = true;
    }


    public void EnemyKnockBack(float enemyPosX)
    {
        jump = true;

        float side = Mathf.Sign(enemyPosX - transform.position.x);
        rb.AddForce(Vector2.left * jumpPower * side * 2, ForceMode2D.Impulse);

        Invoke("EnableColor", 1f);

        Color miColor = new Color(255/255f, 106/255f, 0/255f); //color personalizado rgb pero se necesitan valores tanto por 1.
        spr.color = miColor;
    }


    void EnableColor()
    {
        spr.color = Color.white;
    }
}
