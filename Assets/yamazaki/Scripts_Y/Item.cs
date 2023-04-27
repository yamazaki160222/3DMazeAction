using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] float rotSpeed = 1;
    [SerializeField] bool isHit = false;
    [SerializeField] int itemNo = 0;//アイテム効果　0:ライフ回復,　1:時間回復
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, rotSpeed, 0);
    }
    public bool IsHit
    {
        get => this.isHit;
        set => this.isHit = value;
    }
    public int ItemNo
    {
        get => this.itemNo;
    }

    void OnCollisionEnter(Collision collision)
    {
        isHit = (collision.gameObject.CompareTag("Player")) ? true : false;
    }
    public void ThisDestroy()
    {
        Destroy(this.gameObject);
    }
}
