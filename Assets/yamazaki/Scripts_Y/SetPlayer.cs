using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayer : MonoBehaviour
{
    GameObject player;
    Transform tra;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public GameObject Player
    {
        get => this.player;
        set => this.player = value;
    }


    public Transform Tra
    {
        get { return tra; }
        set { tra = value; }
    }
    

}
