using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController_Y : MonoBehaviour
{
    public GameObject player;//追跡対象
    public float traceDist = 100.0f;//追跡半径
    NavMeshAgent nav;
    bool EnemyEye;//視界方向にいるか
    public float chasetimeLimit = 2.0f;//目標を見失ってから追跡を続ける時間
    float chesetime;//目標を見失ってからの経過時間


    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
