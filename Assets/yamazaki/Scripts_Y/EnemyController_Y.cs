using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController_Y : MonoBehaviour
{
    [SerializeField] GameObject player;//追跡対象
    [SerializeField] Transform plaPosi;//追跡対象座標
    public float traceDist = 100.0f;//追跡半径
    NavMeshAgent nav;
    bool EnemyEye;//視界方向にいるか
    public float chasetimeLimit = 2.0f;//目標を見失ってから追跡を続ける時間
    float chesetime;//目標を見失ってからの経過時間


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("robo instantiate");
        nav = GetComponent<NavMeshAgent>();
        GetPlayer();
        //GetPlaPosi();
        StartCoroutine(CheckDist());
        nav.isStopped = true;//追跡無効の状態でスタート

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void GetPlayer()
    {
        SetPlayer sP = GetComponent<SetPlayer>();
        //Debug.Log("SetPlayer:" + sP);//デバッグ用
        player = sP.Player;
        
        //Debug.Log("player:" + player);//デバッグ用
    }
    void GetPlaPosi()
    {
        SetPlayer sP = GetComponent<SetPlayer>();
        plaPosi = sP.Tra;
    }

    void OnTriggerEnter(Collider other)//視界方向コライダー
    {
        EnemyEye = true;//視界方向にいるとき
    }
    void OnTriggerExit(Collider other)
    {
        EnemyEye = false;//視界方向にいないとき
    }
    IEnumerator CheckDist()//コルーチン処理
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);//0.2秒毎
            //Debug.Log(chesetime);
            float dist = Vector3.Distance(player.transform.position, transform.position);//プレイヤーとの距離
            Vector3 nejiko = (player.transform.position);//ネジコの座標
            Vector3 nejikocenter = new Vector3(nejiko.x, nejiko.y + 0.1f, nejiko.z);//ネジコの体の中央
            Vector3 vec = (nejikocenter - transform.position).normalized;//レイのベクトル生成
            Ray ray = new Ray(transform.position, vec);//視線の生成
                                                       //Debug.Log(EnemyEye);

            Quaternion quaternion = Quaternion.LookRotation(vec);//ネジコのいる方向

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {

            }



            if (dist < traceDist && hit.collider.gameObject.tag == "Player" && EnemyEye)
            //(ネジコは追跡範囲か&&視線Rayが衝突したオブジェクトのタグは"Playerか&&視界コライダーにネジコは入っているか")
            {
                chesetime = 0;//視認追跡

                //プレイヤーの位置を目的地に設定
                nav.SetDestination(player.transform.position);//標的の設定
                this.transform.rotation = quaternion; //標的へ視界を向ける

                //追跡
                nav.isStopped = false;
                Debug.DrawRay(transform.position, vec * dist, Color.red, 0.2f);//デバッグ用：追跡フラグ満たしているとき赤線表示
            }
            else
            {
                Debug.DrawRay(transform.position, vec * dist, Color.blue, 0.2f);//デバッグ用：追跡フラグ満たしていないとき青線表示

                if (nav.isStopped == false)//追跡中に追跡条件を満たさなくなった場合
                {
                    nav.SetDestination(player.transform.position);
                    this.transform.rotation = quaternion;
                    chesetime++;//見失ってからの追跡カウント開始
                    if (chesetime > chasetimeLimit * 5)//指定秒数見失ったら追跡を終了する※５倍はコルーチン関数の秒数を変えるとき変更して下さい
                    {
                        nav.isStopped = true;
                    }
                }
            }
        }
    }
}
