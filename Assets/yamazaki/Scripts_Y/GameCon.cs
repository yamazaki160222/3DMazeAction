using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCon : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject Enemy;
    [SerializeField] CameraCon cameraCon;
    //[SerializeField] MazeMake mazeMake;//マップ生成スクリプト
    [SerializeField] int mapSize;
    public float insPosiY;
    float time;
    float consoleTime = 0;//デバッグ用
    GameObject insPlayer;



    // Start is called before the first frame update
    void Start()
    {
        insPlayer = Instantiate(player);
        cameraCon.setTransform(insPlayer.transform);
        //mapSize = mazeMake.getMapSize();//マップ生成スクリプトからマップサイズを取得
        insPlayer.transform.position = ObjectPosition(mapSize);
        //SetObject(Enemy);//エネミー生成

    }

    // Update is called once per frame
    void Update()
    {
        GameTime();
        ConsoleTime();
    }

    Vector3 ObjectPosition(int mapSize)//プレイヤー、エネミーのポジションを設定
    {
        int x = Random.Range(0, ((mapSize + 1) / 2) * 2);
        int z = Random.Range(0, ((mapSize + 1) / 2) * 2); ;
        Vector3 posi = new Vector3(x, insPosiY, z);
        return posi;
    }
    void SetObject(GameObject gameObject)//オブジェクト（エネミー）をマップに追加
    {
        Vector3 posi = new Vector3();
        while (true)//プレイヤーとエネミーのポジションが被らなくなるまで繰り返す
        {
            posi = ObjectPosition(mapSize);
            if (posi.x != insPlayer.transform.position.x &&
                posi.z != insPlayer.transform.position.z)
            {
                break;
            }
        }
        GameObject e = Instantiate(gameObject);
        e.transform.position = posi;
    }
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
}
