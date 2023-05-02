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
    [SerializeField] List<GameObject> items;
    [SerializeField] int itemSet;//各アイテム生成数　基本１で
    [SerializeField] CameraCon cameraCon;
    [SerializeField] GoalEffect goalEffect;
    [SerializeField] MazeMake mazeMake;//マップ生成スクリプト
    [SerializeField] int mapSize;
    [SerializeField] int startPosiOffset_x = 1;
    [SerializeField] int startPosiOffset_z = 1;
    [SerializeField] float insPosiY_Player;
    [SerializeField] float insPosiY_Enemy;
    [SerializeField] float insPosiY_Goal;
    [SerializeField] float gameTime = 30;
    [SerializeField] float timePlus = 15;
    [SerializeField] bool isGoal = false;
    [SerializeField] GameObject playerSpawn;//プレイヤー位置0,0問題対応
    Transform pSpawn;

    int stageScore = 0;
    float time;
    float consoleTime = 0;//デバッグ用
    GameObject insPlayer;
    GameObject insGoal;
    [SerializeField] Dictionary<int, GameObject> insEnemyList;
    int enemyIdNo = 1;
    [SerializeField] Dictionary<int, GameObject> insItemList;
    int itemIdNo = 1;
    CharCon_Y  charCon;
    bool goalCheck;



    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start()stageScore:" + PlayerPrefs.GetInt("stageScore"));
        if (pSpawn != null)
        {
            pSpawn = playerSpawn.GetComponent<Transform>();
        }
        
        if(mazeMake != null)
        {
            Debug.Log("mapsize"+mazeMake.MapSize);
            //mapSize = mazeMake.MapSize;//マップ生成スクリプトからマップサイズを取得
        }
        else
        {
            Debug.Log("mazeMake null");
        }
        if(PlayerPrefs.GetInt("stageScore") != 0)
        {
            stageScore = PlayerPrefs.GetInt("stageScore");
            gameTime = PlayerPrefs.GetFloat("timeScore");

            if (gameTime < 30)
            {
                gameTime = 30;
            }
        }
        else
        {
            PlayerPrefs.SetInt("lifeScore", 0);
            PlayerPrefs.SetFloat("timeScore", 30);
        }
        InsPlayer();
        if (insPlayer.transform.position.x == 0 && insPlayer.transform.position.z == 0)
        {
            insPlayer.transform.position = new Vector3(1, 0, 1);
        }
        insEnemyList = new Dictionary<int, GameObject>();
        insItemList = new Dictionary<int, GameObject>();
        InsGoal();//ゴール生成
        InsEnemy();//エネミー生成
        InsItem(0, 0.5f);//(List items no,出現位置(高さ))
        InsItem(1, 0.5f);//(List items no,出現位置(高さ))

        /*for(int i = 0; i < 10000; i++)//デバッグ用
        {
            Debug.Log(insPosiCheck(0));
        }*/
        Debug.Log(insEnemyList);
        Debug.Log(insItemList);
        goalCheck = true;
        
        Debug.Log("Start()終了");

    }

    // Update is called once per frame
    void Update()
    {
        EnemyHit();
        ItemHit();
        RemoveCheck();
        Goal();
        //GameTime();
        if (!IsGoal)
        {
            time += Time.deltaTime;
            //ConsoleTime();//デバッグ用
            Debug.Log("Time" + GameTime() + "秒");
            GameOver();

        }
        //Debug.Log("highScore:"+PlayerPrefs.GetInt("highScore"));//デバッグ用
        //Debug.Log("stageScore:" + PlayerPrefs.GetInt("stageScore"));//デバッグ用
        //Debug.Log("lifeScore:" + PlayerPrefs.GetInt("lifeScore"));//デバッグ用
        //Debug.Log("timeScore:" + PlayerPrefs.GetFloat("timeScore"));//デバッグ用

    }

    Vector3 ObjectPosition(int mapSize,float y)//プレイヤー、エネミーのポジションを設定
    {
        int x = 0;
        int z = 0;
        while (true)
        {

            x = Random.Range(0, (mapSize + 1) / 2) * 2 + startPosiOffset_x;
            z = Random.Range(0, (mapSize + 1) / 2) * 2 + startPosiOffset_z;
            if (x != 0 && z != 0)
            {
                break;
            }
        }
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
            if (pSpawn != null)//プレイヤー生成
            {
                insPlayer = Instantiate(player, pSpawn);
                insPlayer.transform.position = pSpawn.transform.position;
            }
            else
            {
                insPlayer = Instantiate(player);
            }
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
    void InsGoal()
    {
        Vector3 posi = new Vector3();
        insGoal = InsObject(goal, insPosiY_Goal);
        posi = insGoal.transform.position;
        while (true)//プレイヤーとエネミーのポジションが被らなくなるまで繰り返す
        {
            posi = ObjectPosition(mapSize, insPosiY_Goal);
            if (posi.x > insPlayer.transform.position.x + 2 || posi.x < insPlayer.transform.position.x - 2)
            {
                if (posi.z > insPlayer.transform.position.z + 2 || posi.z < insPlayer.transform.position.z - 2)
                {
                    break;
                }
            }
        }
        insGoal.transform.position = posi;
    }
    void InsEnemy()
    {
        if (enemys.Count > 0)
        {
            for (int i = 0; i < enemysSet; i++)
            {
                GameObject e = InsObject(enemys[SetNo(enemys)], insPosiY_Enemy);
                e.transform.position = insPosiCheck(insEnemyList, insPosiY_Enemy);
                SetPlayer(e);
                SetIsHit s = e.GetComponent<SetIsHit>();
                s.IdNo = enemyIdNo;
                insEnemyList.Add(enemyIdNo, e);
                enemyIdNo++;
            }
        }
        else
        {
            Debug.Log("enemyが設定されていません");
        }
    }
    void InsItem(int i,float posiY)
    {
        if(items.Count >= i + 1)
        {
            for (int j = 0; j < itemSet; j++)
            {
                GameObject item = InsObject(items[i], posiY);
                item.transform.position = insPosiCheck(insItemList, posiY);
                Item it = item.GetComponent<Item>();
                insItemList.Add(itemIdNo, item);
                it.IdNo = itemIdNo;
                itemIdNo++;
            }

        }
    }
    GameObject InsObject(GameObject gameObject,float f)//オブジェクト（エネミー）をマップに追加
    {
        if (gameObject != null)
        {
            GameObject e = Instantiate(gameObject);
            e.transform.eulerAngles = ObjectRotation();
            return e;
        }
        else
        {
            Debug.Log("Object_null");
            return null;
        }
       
    }
    Vector3 insPosiCheck(Dictionary<int, GameObject> list,float f)
    {
        Vector3 posi = new Vector3();
        bool b = true;
        while (true)//プレイヤー、ゴール、とエネミーとのポジションが被らなくなるまで繰り返す
        {
            b = true;
            posi = ObjectPosition(mapSize, f);
            if ((posi.x != insPlayer.transform.position.x || posi.z != insPlayer.transform.position.z) &&
                (posi.x != insGoal.transform.position.x || posi.z != insGoal.transform.position.z))//プレイヤー、ゴールのポジションチェック
            {
                if (list.Count != 0)//生成済みエネミーがいないか確認
                {
                    foreach (int i in list.Keys)//eに生成済みのエネミーを代入
                    {
                        GameObject e = list[i];
                        Debug.Log(e.transform.position);
                        if (posi.x == e.transform.position.x || posi.z == e.transform.position.z)//生成済みエネミーのポジションチェック
                        {
                            b = false;//一致の場合false
                        }
                    }
                    if (b)//生成済みエネミーと一致しなかった場合break
                    {
                        break;
                    }
                }
                else//生成済みエネミーがいない場合break
                {
                    break;
                }
            }
        }
        return posi;
    }

    int SetNo(List<GameObject> list)
    {
        return Random.Range(0, list.Count);
    }

    
    void SetPlayer(GameObject e)
    {

        SetPlayer sP = e.GetComponent<SetPlayer>();
        sP.Player = insPlayer;
    }

    public float GameTime()//残時間
    {
        float f = gameTime - time;
        if (f < 0)
        {
            f = 0;
        }
        return f;
    }
    
    bool TimePlus()
    {
        if (GameTime() > 0)
        {
            gameTime += timePlus;
            return true;
        }
        return false;
    }
    void ConsoleTime()//Debug用　コンソールに経過時間表示
    {
        if (time - consoleTime >= 1)
        {
            consoleTime = time;
        }
    }
    void ReturnToTitle()
    {
        SceneManager.LoadScene("");
    }

    void EnemyHit()
    {
        if (insEnemyList != null && insEnemyList.Count != 0 && charCon.EnemyNo == 0)
        {
            int j = 0;
            bool b = false;
            foreach (int i in insEnemyList.Keys)
            {
                GameObject g = insEnemyList[i];
                Debug.Log("SetIsHit:" + g);
                SetIsHit h = g.GetComponent<SetIsHit>();
                if (h.IsHit == true)
                {
                    b = true;
                    j = i;
                }
            }
            if (b)
            {
                Debug.Log("EnemyIsHit");
                charCon.IsStun = true;
                charCon.HitAction();
                EnemyRemove(j);
            }
        }
    }
    public void ItemHit()
    {
        if (insItemList != null && insItemList.Count != 0 && charCon.ItemNo == 0)
        {
            Dictionary<int, GameObject> cpList = new Dictionary<int, GameObject>();
            foreach(int j in insItemList.Keys)
            {
                cpList.Add(j, insItemList[j]);
            }
            foreach (int j in cpList.Keys)
            {
                GameObject item = insItemList[j];
                Item i = item.GetComponent<Item>();
                if (i.IsHit == true)
                {
                    i.IsHit = false;
                    switch (i.ItemNo)
                    {
                        case 0:
                            if (charCon.LifeUp())
                            {
                                Debug.Log("ItemIsHit:LifeUp");
                                ItemRemove(j);
                                //i.ThisDestroy();
                            }
                            break;
                        case 1:
                            if (TimePlus())
                            {
                                Debug.Log("ItemIsHit:TimePlus");
                                ItemRemove(j);
                                //i.ThisDestroy();
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
    void RemoveCheck()
    {
        if (charCon.EnemyNo > 0)
        {
            EnemyRemove(charCon.EnemyNo);
            charCon.EnemyNo = 0;
        }
        if(charCon.ItemNo > 0)
        {
            ItemRemove(charCon.ItemNo);
            charCon.ItemNo = 0;
        }
    }
    void EnemyRemove(int i)
    {
        GameObject g = insEnemyList[i].gameObject;
        if (insEnemyList.Remove(i))
        {
            AudioSource.PlayClipAtPoint(g.GetComponent<AudioSource>().clip, g.transform.position);
            Destroy(g);
            Debug.Log("EnemyRemove:true");
        }
        else
        {
            Debug.Log("EnemyRemove:false");
        }
    }
    void ItemRemove(int i)
    {
        GameObject g = insItemList[i].gameObject;
        if (insItemList.Remove(i))
        {
            AudioSource.PlayClipAtPoint(g.GetComponent<AudioSource>().clip, g.transform.position);
            Destroy(g);
            Debug.Log("ItemRemove:true");
        }
        else
        {
            Debug.Log("ItemRemove:false");
        }

    }
    void AllRemove()
    {
        if(insEnemyList != null && insEnemyList.Count > 0)
        {
            List<int> list = new List<int>();
            foreach (int i in insEnemyList.Keys)
            {
                list.Add(i);
            }
            foreach(int i in list)
            {
                EnemyRemove(i);
            }
        }
        if(insItemList != null && insItemList.Count > 0)
        {
            List<int> list = new List<int>();
            foreach(int i in insItemList.Keys)
            {
                list.Add(i);
            }
            foreach(int i in list)
            {
                ItemRemove(i);
            }
        }
    }
    void Goal()
    {
        if (goalCheck && charCon.GetIsGoal())
        {
            stageScore++;
            PlayerPrefs.SetInt("stageScore", stageScore);
            if(PlayerPrefs.GetInt("stageScore") > PlayerPrefs.GetInt("highScore"))
            {
                PlayerPrefs.SetInt("highScore", stageScore);
            }
            PlayerPrefs.SetInt("lifeScore", charCon.CharLife);
            PlayerPrefs.SetFloat("timeScore", GameTime());
            IsGoal = true;
            Debug.Log("GameCon_Goal:" + charCon.GetIsGoal());
            AllRemove();
            goalEffect.OnEffect();
            charCon.StartGoalAnim();
            goalCheck = false;
        }
    }
    void GameOver()
    {
        if (charCon.Life() <= 0 || GameTime() <= 0)
        {
            PlayerPrefs.SetInt("stageScore", 0);
            PlayerPrefs.SetInt("lifeScore", 0);
            PlayerPrefs.SetFloat("timeScore", 30);
            charCon.IsGameOver = true;
            Debug.Log("GameCon_GameOver");
        }
    }
    public bool IsGoal
    {
        get => this.isGoal;
        set => this.isGoal = value;
    }
}
