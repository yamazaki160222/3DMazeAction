using System.Collections;
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

    // Start is called before the first frame update

   
    bool IsStun()
    {
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
        float acc = Input.GetAxis("Vertical");

        if (cc.isGrounded)
        {
            Debug.Log("isGrounded");
            float rot = Input.GetAxis("Horizontal");
            animator.SetBool("run",acc!=0f||rot!=0f);
            transform.Rotate(0, rot * rotSpeed * Time.deltaTime, 0);

            if (Input.GetButtonDown("Jump"))
            {
                Debug.Log("GetButtonDown_Jump");
                Jump();
            }
        }
        if (acc < 0)
        {
            acc *= backSpeed;
        }
        dir.y -= gravity * Time.deltaTime;
        cc.Move((transform.forward * acc * speed + dir) * Time.deltaTime);

        if (cc.isGrounded)
        {
            dir.y = 0;
        }
    }


    public void Jump()
    {
        if(IsStun()) return;
        if (cc.isGrounded)
        {
            dir.y = jumpPower;
            animator.SetTrigger("jump");
        }
    }

    
}
