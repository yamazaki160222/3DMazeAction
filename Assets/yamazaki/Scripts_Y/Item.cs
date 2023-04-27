using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] float rotSpeed = 1;
    [SerializeField] int itemNo = 0;//アイテム効果　0:ライフ回復,　1:時間回復
    [SerializeField] bool isHit = false;
    [SerializeField] int idNo;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, rotSpeed, 0);
    }
    public int ItemNo
    {
        get => this.itemNo;
    }
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
    public void ThisDestroy()
    {
        Destroy(this.gameObject);
    }
    public void OnTriggerEnter(Collider other)
    {
        isHit = (other.gameObject.CompareTag("Player")) ? true : false;
    }
}
