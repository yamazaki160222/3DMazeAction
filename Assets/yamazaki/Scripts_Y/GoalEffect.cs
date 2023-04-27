using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalEffect : MonoBehaviour
{
    [SerializeField] GameObject particleObject;
    [SerializeField] float posi_x;
    [SerializeField] float posi_y;
    [SerializeField] float posi_z;
    [SerializeField] float lote_x;
    [SerializeField] float lote_y;
    [SerializeField] float lote_z;
    // Start is called before the first frame update
    void Start()
    {
        //OnEffect();//デバッグ用
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnEffect()
    {
        GameObject g = Instantiate(particleObject,
            this.transform.localPosition,
            this.transform.localRotation);
        g.transform.parent = this.transform;
        
        Vector3 posi =g.transform.localPosition;
        posi.x = posi_x;
        posi.y = posi_y;
        posi.z = posi_z;
        g.transform.localPosition = posi;
        Quaternion lote = this.transform.localRotation;
        lote.x = lote_x;
        lote.y = lote_y;
        lote.z = lote_z;
        g.transform.localRotation = Quaternion.Euler(lote_x,lote_y,lote_z);
        g.SetActive(true);

    }
}
