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
    public float knockBack;
    public float stunDuration = 0.5f;

    [SerializeField] int defaultLife = 3;
    [SerializeField] float recoverTime = 0.0f;
    [SerializeField] bool isStun = false;
    [SerializeField] bool isGoal = false;
    [SerializeField] bool isGameOver = false;
    [SerializeField] int enemyNo;
    [SerializeField] int itemNo;

    [SerializeField] int life = 0;

    public int EnemyNo
    {
        get => this.enemyNo;
        set => this.enemyNo = value;
    }
    public int ItemNo
    {
        get => this.itemNo;
        set => this.itemNo = value;
    }

    // Start is called before the first frame update

    
    void Start()
    {
        cc = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (cc.isGrounded)
        {
            dir.y = 0;
        }
        */
        dir.y -= gravity * Time.deltaTime;
        Life();
        float acc = 0;
        if (!Stun() || !GetIsGoal())
        {
            acc = Input.GetAxis("Vertical");
        }

        if (Stun())
        {
            //Debug.Log("recoverTime:"+recoverTime);
            acc = 0;
            recoverTime -= Time.deltaTime;
        }
        if (!GetIsGoal())
        {
            Rotate(acc);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("GetButtonDown_Jump");
                Jump();
            }
            /*if (Input.GetKeyDown(KeyCode.G))//デバッグ用
            {
                SetIsGoal(true);
            }*/
            Move(acc);
        }
    }


    public void Move(float acc)
    {
        if (acc < 0)
        {
            acc *= backSpeed;
        }
        cc.Move((transform.forward * acc * speed + dir) * Time.deltaTime);

    }
    public void Rotate(float acc)
    {
        if (cc.isGrounded)
        {
            float rot = Input.GetAxis("Horizontal");
            animator.SetBool("run", acc != 0f || rot != 0f);
            transform.Rotate(0, rot * rotSpeed * Time.deltaTime, 0);
        }
    }

    public void Jump()
    {
        if (Stun()) return;
        if (cc.isGrounded)
        {
            dir.y = jumpPower;
            animator.SetTrigger("jump");
        }
    }
    public int Life()
    {
        string str = "Life:" + (defaultLife - life) + "/" + defaultLife;
        Debug.Log(str);
        return life;
    }
    public int DefaultLife()
    {
        return defaultLife;
    }
    public bool LifeUp()
    {
        if(life > 0 && life <= defaultLife)
        {
            life--;
            Debug.Log("LifeUp:true");
            Life();
            return true;
        }
        Debug.Log("LifeUp:false");
        return false;
    }

    public bool IsStun
    {
        get => this.isStun;
        set => this.isStun = value;
    }
    bool Stun()
    {
        if (recoverTime > 0.0f)
        {
            Debug.Log("IsStun:" + IsStun);
            return IsStun = true;
        }
        else
        {
            return IsStun = false;
        }
    }
    public bool GetIsGoal()
    {
        if (isGoal)
        {
            Debug.Log("isGoal:" + isGoal);
        }
        return isGoal;
    }
    public void SetIsGoal(bool b)
    {
        isGoal = b;
    }
    public bool GetIsGameOver()
    {
        return isGameOver;
    }
    public void SetIsGameOver(bool b)
    {
        isGameOver = b;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (Stun()) return;
        if (hit.gameObject.tag == "Enemy")
        {
            SetIsHit s = hit.gameObject.GetComponent<SetIsHit>();
            s.IsHit = false;
            this.EnemyNo = s.IdNo;
            HitAction();
        }

        if (hit.gameObject.tag == "Goal")
        {
            SetIsGoal(true);
            animator.SetBool("run", false);
        }
        if (hit.gameObject.tag == "Item")
        {
            Item s = hit.gameObject.GetComponent<Item>();
            s.IsHit = false;
            if (LifeUp())
            {
                this.ItemNo = s.IdNo;
            }
        }
    }
    public void HitAction()
    {
        if (life <= defaultLife)
        {
            life++;
            recoverTime = stunDuration;
            //cc.Move((transform.forward * -1 * speed + dir) * Time.deltaTime);
            animator.SetTrigger("damage");
        }
    }
    public void StartGoalAnim()
    {
        Debug.Log("StartGoalAnim");
        StartCoroutine("GoalAnim");
    }
    IEnumerator GoalAnim()
    {
        if (cc.isGrounded)
        {
            for(int i = 0; i < 3; i++)
            {
                dir.y -= gravity * Time.deltaTime;
                Debug.Log("dir.y:" + dir.y); 
                dir.y = jumpPower;
                animator.SetBool("goal", true);
                Debug.Log("dir.y:" + dir.y);
                Move(0);
                Debug.Log("GoalAnimCount:"+ i);
                yield return new WaitForSeconds(1f);
            }
        }
        animator.SetBool("goal", false);
        SetIsGoal(false);
    }
}
