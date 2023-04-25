using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCon : MonoBehaviour
{
    [SerializeField] GameObject player;
    //[SerializeField] GameObject enemy;
    [SerializeField] List<GameObject> enemys;
    [SerializeField] List<int> enemysSetPlayerNo;
    [SerializeField] GameObject goal;
    [SerializeField] CameraCon cameraCon;
    //[SerializeField] MazeMake mazeMake;//マップ生成スクリプト
    [SerializeField] int mapSize;
    [SerializeField] int startPosiOffset_x = 1;
    [SerializeField] int startPosiOffset_z = 1;
    public float insPosiY_Player;
    public float insPosiY_Enemy;
    public float insPosiY_Goal;
    float time;
    float consoleTime = 0;//デバッグ用
    GameObject insPlayer;
    CharCon_Y  charCon;
    SetPlayer sP;



    // Start is called before the first frame update
    void Start()
    {

        //mapSize = mazeMake.getMapSize();//マップ生成スクリプトからマップサイズを取得

        insPlayer = Instantiate(player);//プレイヤー生成
        insPlayer.transform.position = ObjectPosition(mapSize, insPosiY_Player);//プレイヤーポジション設定
        cameraCon.setTransform(insPlayer.transform);//メインカメラにプレイヤーポジションをセット
        charCon = insPlayer.GetComponent<CharCon_Y>();//charConにキャラクターコントローラーを代入
        InsObject(enemys[SetEneNo()],insPosiY_Enemy);//エネミー生成
        InsObject(goal,insPosiY_Goal);//ゴール生成

        for(int i = 0; i < 10000; i++)
        {
            Debug.Log(insPosiCheck(0));
        }

    }

    // Update is called once per frame
    void Update()
    {
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
    void InsObject(GameObject gameObject,float f)//オブジェクト（エネミー）をマップに追加
    {
        if (gameObject != null)
        {
            GameObject e = Instantiate(gameObject);
            e.transform.position = insPosiCheck(f);
            SetPlayer(e);
        }
        else
        {
            Debug.Log("insObject_null");
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
        sP = e.GetComponent<SetPlayer>();
        sP.Player = insPlayer;
    }

    /* //Instantiateしたオブジェクトは（クローン）になるため比較できない
    bool EnemyCheck(GameObject e)
    {
        foreach (int i in enemysSetPlayerNo)
        {
            Debug.Log(e +":"+enemys[i]);
            if(e.Equals(enemys[i]))
            {
                return true;
            }
        }
        return false;
    }
    */
    float GameTime()//実時間取得
    {
        time += Time.deltaTime;
        return time;
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
