using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeManager : MonoBehaviour
{
    [SerializeField] bool playFlg;
    [SerializeField] bool isEneSe;
    [SerializeField] bool isGoalSe;

    //AudioSource se;
    AudioClip se;
    GameObject parent;
    SetIsHit eneSc;
    Goal goalSc;

    // Start is called before the first frame update
    void Start()
    {
        //se = GetComponent<AudioSource>();
        se = GetComponent<AudioSource>().clip;

        parent = transform.parent.gameObject;
        /*
        if (parent.CompareTag("Enemy"))
        {
            isEneSe = true;
            eneSc = parent.GetComponent<SetIsHit>();
            Debug.Log("eneScセット");
            //goalSc = null;
            //playFlg = eneSc.IsHit;
        }
        else*/ if (parent.CompareTag("Goal"))
        {
            isGoalSe = true;
            goalSc = parent.GetComponent<Goal>();
            Debug.Log("goalScセット");
            //eneSc = null;
            //playFlg = goalSc.endFlg;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isEneSe)
        {
            if (eneSc.IsHit)
            {
                playFlg = true;
                //if (playFlg)
                //{
                Debug.Log("再生します");
                AudioSource.PlayClipAtPoint(se, transform.position);
                playFlg = false;
                //} 
            }
        }
        else if (isGoalSe)
        {
            if (goalSc.endFlg)
            {
                playFlg = true;
                //if (playFlg)
                //{
                Debug.Log("再生します");
                //GetComponent<AudioSource>().Play();
                AudioSource.PlayClipAtPoint(se, transform.position);
                playFlg = false;
                Destroy(gameObject);
                //}
            }
        }
    }
}