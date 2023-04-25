using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetIsHit : MonoBehaviour
{

    [SerializeField] bool isHit = false; //Playerに当たったかどうか

    public bool IsHit
    {
        get => this.isHit;
        set => this.isHit = value;
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
