using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCon : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject camera;
    [SerializeField] List<GameObject> enemys = new List<GameObject>();
    [SerializeField] GameObject wall;
    [SerializeField] GameObject Ground;
    [SerializeField] float limitTime;

    GameObject insPlay;
    GameObject insCame;
    

    
    // Start is called before the first frame update
    void Start()
    {
        insPlay = Instantiate(player);
        insCame = Instantiate(camera);
        insPlay.transform.position = new Vector3(0, 0.57f, 0);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
