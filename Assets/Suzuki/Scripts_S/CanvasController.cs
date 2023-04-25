using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    public Camera mainCam;
    public GameObject player;
    public Text timeText;
    public Text lifeText;

    GameCon gameCon;
    CharCon_Y charCon_Y;

    // Start is called before the first frame update
    void Start()
    {
        gameCon = mainCam.GetComponent<GameCon>();
        player = mainCam.GetComponent<CameraCon>().charaLookAtPosition.gameObject;
        charCon_Y = player.GetComponent<CharCon_Y>();
    }

    // Update is called once per frame
    void Update()
    {
        getGameTime();
        getLife();
    }

    void getGameTime()//実時間取得
    {
        //float time = gameCon.GameTime();
        //以下2行、代理文
        float time = 0;
        time += Time.deltaTime;

        float consoleTime = 0;
        if (time - consoleTime >= 1)
        {
            timeText.text = "Time:" + (int)time + "秒";
            consoleTime = time;
        }
    }
    void getLife()
    {
        int defaultLife = charCon_Y.DefaultLife();
        int life = charCon_Y.Life();
        lifeText.text = "Life:" + life + "/" + defaultLife;
    }

}
