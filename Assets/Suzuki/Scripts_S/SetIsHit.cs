using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetIsHit : MonoBehaviour
{

    [SerializeField] bool isHit = false; //Playerに当たったかどうか
    [SerializeField] int idNo;

    public bool IsHit
    {
        get => this.isHit;
        set => this.isHit = value;
    }
    public int IdNo
    {
        get => this.idNo;
        set => this.idNo = value;
    }

    void OnCollisionEnter(Collision collision)
    {
        isHit = (collision.gameObject.CompareTag("Player")) ? true : false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
