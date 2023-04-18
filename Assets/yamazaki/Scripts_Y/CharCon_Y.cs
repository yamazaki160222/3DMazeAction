﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharCon_Y : MonoBehaviour
{
    Animator animator;
    CharacterController cc;
    Vector3 dir = Vector3.zero;
    public float gravity = 20f;
    public float speed = 4f;
    public float rotSpeed = 300f;
    public float jumpPower = 8f;
    public float backSpeed = 0.5f;
    public float knockBack;

    const int defaultLife = 3;
    const float stunDuration = 0.5f;

    int life = defaultLife;
    float recoverTime = 0.0f;

    // Start is called before the first frame update

    public int Life()
    {
        Debug.Log("life:" + life);
        return life;
    }
    bool IsStun()
    {
        if(recoverTime > 0.0f || life <= 0)
        {
            Debug.Log("IsStun:true");
            return true;
        }
        Debug.Log("IsStun:false");
        return false;
    }
    void Start()
    {
        animator = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        dir.y -= gravity * Time.deltaTime;
        Life();
        float acc = Input.GetAxis("Vertical");

        if (IsStun())
        {
            dir.x = 0f;
            dir.z = 0f;
            recoverTime -= Time.deltaTime;
        }
        if (cc.isGrounded)
        {
            Debug.Log("isGrounded");
            float rot = Input.GetAxis("Horizontal");
            animator.SetBool("run",acc!=0f||rot!=0f);
            transform.Rotate(0, rot * rotSpeed * Time.deltaTime, 0);

            
        }
        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("GetButtonDown_Jump");
            Jump();
        }
        if (acc < 0)
        {
            acc *= backSpeed;
        }
        cc.Move((transform.forward * acc * speed + dir) * Time.deltaTime);


        /*
        if (cc.isGrounded)
        {
            dir.y = 0;
        }
        */
    }

    public void Jump()
    {
        if (IsStun()) return;
        if (cc.isGrounded)
        {
            dir.y = jumpPower;
            animator.SetTrigger("jump");
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (IsStun()) return;
        if (hit.gameObject.tag == "Enemy")
        {
            life--;
            recoverTime = stunDuration;
            //cc.Move((transform.forward * -1 * speed + dir) * Time.deltaTime);
            animator.SetTrigger("damage");

            //Destroy(hit.gameObject);
        }
    }
}
