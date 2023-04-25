using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController_S : MonoBehaviour
{
    //public float walkSpeed;
    //public int moveRange;       //原点からの移動範囲（前後）

    GameObject enemy;
    //Vector3 origin;     //初期位置
    Animator animator;
    bool isOpen;        //進行方向が開けているかどうか

    // Start is called before the first frame update
    void Start()
    {
        enemy = transform.parent.gameObject;
        animator = enemy.GetComponent<Animator>();
        animator.SetTrigger("walk");

        //origin = enemy.transform.position;

        isOpen = true;
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("衝突検知");
        if(other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Goal") || other.gameObject.CompareTag("Enemy"))
        {
            //Debug.Log("これ以上進めない");
            isOpen = false;
        }
    }
    
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Goal") || other.gameObject.CompareTag("Enemy"))
        {
            //Debug.Log("回転します");
            Transform eneTra = enemy.transform;

            int GetArg(int r) => r == 1 ? 45 : -45;
            Quaternion toRoll = Quaternion.AngleAxis(GetArg(Random.Range(0, 2)), Vector3.up);
            eneTra.rotation *= toRoll;

            /*
            if (Random.Range(0, 2) == 1) 
            {
                //Debug.Log("右回転");
                Quaternion toRoll = Quaternion.AngleAxis(45, Vector3.up);
                eneTra.rotation *= toRoll;
            }
            else
            {
                //Debug.Log("左回転");
                Quaternion toRoll = Quaternion.AngleAxis(-45, Vector3.up);
                eneTra.rotation *= toRoll;
            }
            */
        }
    }
    
    
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Goal") || other.gameObject.CompareTag("Enemy"))
        {
            //Debug.Log("前進できる");
            isOpen = true;
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        Transform eneTra = enemy.transform;
        //int count = moveRange;
        if (isOpen)
        {
            eneTra.position += eneTra.forward / 100;
        }
        else
        {
            /*
            Debug.Log("回転します");

            //Transform eneTra = enemy.transform;
            Quaternion toRoll = Quaternion.AngleAxis(45, Vector3.up);
            //eneTra.rotation = Quaternion.Lerp(eneTra.rotation, toRoll, Time.deltaTime * 20);
            eneTra.rotation *= toRoll;
            */
            /*
            // 現在の自信の回転の情報を取得する。
            Quaternion q = eneTra.rotation;
            // 合成して、自身に設定
            eneTra.rotation = q * toRoll;
            */

        }
 
    }
}
