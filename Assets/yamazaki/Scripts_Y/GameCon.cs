using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCon : MonoBehaviour
{
    [SerializeField] GameObject player;
    //[SerializeField] GameObject enemy;
    [SerializeField] List<GameObject> enemys;
    [SerializeField] int enemysSet;
    [SerializeField] GameObject goal;
    [SerializeField] CameraCon cameraCon;
    //[SerializeField] MazeMake mazeMake;//マップ生成スクリプト
    [SerializeField] int mapSize;
    [SerializeField] int startPosiOffset_x = 1;
    [SerializeField] int startPosiOffset_z = 1;
    [SerializeField] float insPosiY_Player;
    [SerializeField] float insPosiY_Enemy;
    [SerializeField] float insPosiY_Goal;
    float time;
    float consoleTime = 0;//デバッグ用
    GameObject insPlayer;
    CharCon_Y  charCon;
    List<GameObject> insObjList;



    // Start is called before the first frame update
    void Start()
    {

        //mapSize = mazeMake.getMapSize();//マップ生成スクリプトからマップサイズを取得

        InsPlayer();
        insObjList = new List<GameObject>();
        InsEnemy();//エネミー生成
        InsObject(goal,insPosiY_Goal);//ゴール生成

        /*for(int i = 0; i < 10000; i++)//デバッグ用
        {
            Debug.Log(insPosiCheck(0));
        }*/
        Debug.Log("Start()終了");

    }

    // Update is called once per frame
    void Update()
    {
        EnemyHit();
        //GameTime();
        //ConsoleTime();//デバッグ用
        //Goal();
    }

    Vector3 ObjectPosition(int mapSize,float y)//プレイヤー、エネミーのポジションを設定
    {
        int x = Random.Range(0, (mapSize + 1) / 2) * 2 + startPosiOffset_x;
        int z = Random.Range(0, (mapSize + 1) / 2) * 2 + startPosiOffset_z;
        Vector3 posi = new Vector3(x, y, z);
        return posi;
    }
    Vector3 ObjectRotation()
    {
        int i = Random.Range(0, 3);
        float y = 0;
        switch (i){
            case 0:
                y = 0;
                break;
            case 1:
                y = 90;
                break;
            case 2:
                y = 180;
                break;
            case 3:
                y = -90;
                break;
            default:
                break;
        }
        Vector3 rot = new Vector3(0, y, 0);
        return rot;
    }

    void InsPlayer()
    {
        if (player != null)
        {
            insPlayer = Instantiate(player);//プレイヤー生成
            insPlayer.transform.position = ObjectPosition(mapSize, insPosiY_Player);//プレイヤーポジション設定
            insPlayer.transform.eulerAngles = ObjectRotation();
            cameraCon.setTransform(insPlayer.transform);//メインカメラにプレイヤーポジションをセット
            charCon = insPlayer.GetComponent<CharCon_Y>();//charConにキャラクターコントローラーを代入
        }
        else
        {
            Debug.Log("playerが設定されていません");
        }
    }
    GameObject InsObject(GameObject gameObject,float f)//オブジェクト（エネミー）をマップに追加
    {
        if (gameObject != null)
        {
            GameObject e = Instantiate(gameObject);
            e.transform.position = insPosiCheck(f);
            e.transform.eulerAngles = ObjectRotation();
            return e;
        }
        else
        {
            Debug.Log("Object_null");
            return null;
        }
       
    }
    Vector3 insPosiCheck(float f)
    {
        Vector3 posi = new Vector3();
        while (true)//プレイヤーとエネミーのポジションが被らなくなるまで繰り返す
        {
            posi = ObjectPosition(mapSize, f);
            if (posi.x != insPlayer.transform.position.x || posi.y != insPlayer.transform.position.y)
            {
                break;
            }
        }
        return posi;
    }

    int SetEneNo()
    {
        return Random.Range(0, enemys.Count);
    }

    
    void SetPlayer(GameObject e)
    {

        SetPlayer sP = e.GetComponent<SetPlayer>();
        sP.Player = insPlayer;
    }

    public float GameTime()//実時間取得
    {
        time += Time.deltaTime;
        return time;
    }
    void InsEnemy()
    {
        if(enemys.Count > 0)
        {
            for (int i = 0; i < enemysSet; i++)
            {
                GameObject e = InsObject(enemys[SetEneNo()], insPosiY_Enemy);
                SetPlayer(e);
                insObjList.Add(e);
            }
        }
        else
        {
            Debug.Log("enemyが設定されていません");
        }
        
    }
    void ConsoleTime()//Debug用　コンソールに経過時間表示
    {
        if (time - consoleTime >= 1)
        {
            Debug.Log(time + "秒");
            consoleTime = time;
        }
    }
    void ReturnToTitle()
    {
        SceneManager.LoadScene("");
    }

    void EnemyHit()
    {
        foreach(GameObject g in insObjList)
        {
            SetIsHit h = g.GetComponent<SetIsHit>();
            if (h.IsHit == true)
            {
                Debug.Log("IsHit");
                charCon.IsStun = true;
                charCon.HitAction();
                h.IsHit = false;
            }
        }
    }
    void Goal()
    {
        if (charCon.GetIsGoal())
        {
            Debug.Log("GameCon_Goal:" + charCon.GetIsGoal());
            charCon.StartGoalAnim();
        }
    }
    void GameOver()
    {
        if (charCon.Life() >= charCon.DefaultLife())
        {
            Debug.Log("GameCon_GameOver");
        }
    }
}
