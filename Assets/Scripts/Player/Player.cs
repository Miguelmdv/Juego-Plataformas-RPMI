using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    CharacterController2D charMovement;
    Animator animCtrl;

    float horizontal;
    float vertical;
    float jump2;
    bool crouch;
    bool jump;

    private void Awake()
    {
        charMovement = GetComponent<CharacterController2D>();
        animCtrl = GetComponent<Animator>();
    }

    void Start () {
		
	}
	

	void Update () {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        jump2 = Input.GetAxisRaw("Jump");
        if(jump2 > 0)
        {
            jump = true;
            
        }
        if( vertical < 0)
        {
            crouch = true;
        }
        else
        {
            crouch = false;
        }

        Animaciones();

	}

    private void FixedUpdate()
    {
        charMovement.Move(horizontal * 50 * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }

    void Animaciones()
    {
        if (horizontal != 0)
        {
            animCtrl.SetBool("Walking", true);
        }
        else
        {
            animCtrl.SetBool("Walking", false);

        }

        //if (jump)
        //{
        //    animCtrl.SetTrigger("Jump");
        //}

    }
}
