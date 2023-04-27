using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] float rotSpeed = 1;
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
    public int ItemNo
    {
        get => this.itemNo;
    }
    public void ThisDestroy()
    {
        Destroy(this.gameObject);
    }
}
