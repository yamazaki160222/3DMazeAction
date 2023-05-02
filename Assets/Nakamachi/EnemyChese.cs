using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChese : MonoBehaviour
{
    public GameObject player;//追跡対象
    public float traceDist = 100.0f;//追跡半径
    NavMeshAgent nav;
    //bool EnemyEye;//視界方向にいるか
    bool cheseFlag;
    public float chasetimeLimit = 2.0f;//目標を見失ってから追跡を続ける時間
    float chesetime;//目標を見失ってからの経過時間

    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        GetPlayer();
        StartCoroutine(CheckDist());
        nav.isStopped = false;//追跡有効の状態でスタート

    }
    void GetPlayer()
    {
        SetPlayer sP = GetComponent<SetPlayer>();
        Debug.Log("SetPlayer:" + sP);//デバッグ用
        player = sP.Player;
        cheseFlag = false;

    }

    /*void OnCollisionEnter(Collision collision)//ネジコと衝突したときの処理
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);//仮で消しています
        }
    }*/
    /*void OnTriggerEnter(Collider other)//ゴールに侵入したときの処理
    {
        if (other.gameObject.tag == "Goal")
        {
            Destroy(this.gameObject);//仮で消しています
        }
    }*/



    IEnumerator CheckDist()//コルーチン処理
    {
        Vector3 startPosition = this.transform.position;
        Vector3 startAngle = this.transform.forward;
        nav.SetDestination(startPosition);

        for (int i = 0; i < 4; i++)
        {
            Vector3 center = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
            Ray ray1 = new Ray(center, transform.forward);
            RaycastHit hit1;
            if (Physics.Raycast(ray1, out hit1, 1.0f))
            {
                string name = hit1.collider.gameObject.tag;
                if (name == "Wall") { transform.Rotate(0, 90.0f, 0, Space.World); }
            }
        }
        yield return new WaitForSeconds(3.0f);

        while (true)
        {

            yield return new WaitForSeconds(0.2f);//0.2秒毎
            //Debug.Log(chesetime);
            float dist = Vector3.Distance(player.transform.position, transform.position);//プレイヤーとの距離
            Vector3 nejiko = (player.transform.position);//ネジコの座標
            Vector3 enemy = this.transform.position;
            Vector3 nejikocenter = new Vector3(nejiko.x, nejiko.y + 0.2f, nejiko.z);//ネジコの体の中央
            Vector3 enemycenter = new Vector3(enemy.x, enemy.y + 0.2f, enemy.z);
            Vector3 vec = (nejikocenter - enemycenter).normalized;//レイのベクトル生成
            Ray ray = new Ray(enemycenter, vec);//視線の生成

            Quaternion quaternion = Quaternion.LookRotation(vec);//ネジコのいる方向
            Vector3 rotationAnglesNejiko = quaternion.eulerAngles;


            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))//レイの生成
            {

            }

            float angle = Vector3.Angle(this.transform.forward, player.transform.position - this.transform.position);//敵から見たネジコの方向


            if (dist < traceDist && hit.collider.gameObject.tag == "Player" && angle < 90)
            //(ネジコは追跡範囲か&&視線Rayが衝突したオブジェクトのタグは"Playerか&&視界左右90度以内か")
            {
                cheseFlag = true;
                chesetime = 0;//視認追跡

                //プレイヤーの位置を目的地に設定
                nav.SetDestination(player.transform.position);//標的の設定
                this.transform.rotation = quaternion; //標的へ視界を向ける
            }
            else
            {
                if (cheseFlag)
                {
                    nav.SetDestination(player.transform.position);
                    this.transform.rotation = quaternion;
                    chesetime++;//見失ってからの追跡カウント開始
                    if (chesetime > chasetimeLimit * 5)//指定秒数見失ったら追跡を終了する※５倍はコルーチン関数の秒数を変えるとき変更して下さい
                    {
                        nav.SetDestination(startPosition);
                        this.transform.forward = startAngle;
                    }
                }
            }
        }
    }
}