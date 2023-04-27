using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalEffect : MonoBehaviour
{
    [SerializeField] GameObject particleObject;
    [SerializeField] float posiOffset_x;
    [SerializeField] float posiOffset_y;
    [SerializeField] float posiOffset_z;
    [SerializeField] float loteOffset_x;
    [SerializeField] float loteOffset_y;
    [SerializeField] float loteOffset_z;
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
        Vector3 posi = this.transform.position;
        Quaternion lote = Quaternion.Euler(0,0,0);

        posi.x += posiOffset_x;
        posi.y += posiOffset_y;
        posi.z += posiOffset_z;
        lote.x += loteOffset_x;
        lote.y += loteOffset_y;
        lote.z += loteOffset_z;
        GameObject g = Instantiate(particleObject,posi,lote);
        g.transform.parent = this.transform;

    }
}
