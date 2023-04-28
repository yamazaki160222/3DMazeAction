using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeManager : MonoBehaviour
{
    [SerializeField] bool playFlg;

    AudioSource se;
    GameObject parent;
    SetIsHit eneSc;
    Goal goalSc;

    // Start is called before the first frame update
    void Start()
    {
        se = GetComponent<AudioSource>();

        parent = transform.parent.gameObject;
        if (parent.CompareTag("Enemy"))
        {
            eneSc = parent.GetComponent<SetIsHit>();
            Debug.Log("eneScセット");
            goalSc = null;
            //playFlg = eneSc.IsHit;
        }
        else if(parent.CompareTag("Goal"))
        {
            goalSc = parent.GetComponent<Goal>();
            Debug.Log("goalScセット");
            eneSc = null;
            //playFlg = goalSc.endFlg;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if(eneSc.IsHit || goalSc.endFlg)
        /*
        if (eneSc.IsHit)
        {
            playFlg = true;
        }
        */

        if (goalSc.endFlg)
        {
            playFlg = true;
            se.Play();
        }
        /*
        if (playFlg)
        {
            se.Play();
        }
        */
    }
}
