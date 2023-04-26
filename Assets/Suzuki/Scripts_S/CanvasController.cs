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
    public Text mainText;

    GameCon gameCon;
    CharCon_Y charCon_Y;
    bool a_flag;
    float a_color;

    // Start is called before the first frame update
    void Start()
    {
        gameCon = mainCam.GetComponent<GameCon>();
        player = mainCam.GetComponent<CameraCon>().charaLookAtPosition.gameObject;
        charCon_Y = player.GetComponent<CharCon_Y>();

        mainText.text = "Start!!";
        a_flag = true;
        a_color = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (charCon_Y.GetIsGoal() == true)
        {
            mainText.text = "StageClear!!\nTap to NextStage";
            mainText.color = new Color(0, 0, 0, 1);
        }
        else
        {
            TextClear(a_flag);
            GetGameTime();
            GetLife();
        }
        
    }

    void GetGameTime()//実時間取得
    {
        float time = gameCon.GameTime();
        float consoleTime = 0;
        if (time - consoleTime >= 1)
        {
            timeText.text = "Time:" + (int)time + "秒";
            consoleTime = time;
        }
    }
    void GetLife()
    {
        int defaultLife = charCon_Y.DefaultLife();
        int life = charCon_Y.Life();
        lifeText.text = "Life:" + life + "/" + defaultLife;
    }

    //Textを透明にしていく
    void TextClear(bool a_flag)
    {
        //a_flagがtrueの間実行する
        if (a_flag)
        {
            //テキストの透明度を変更する
            mainText.color = new Color(0, 0, 0, a_color);
            a_color -= Time.deltaTime / 2;
            //透明度が0になったら終了する。
            if (a_color < 0)
            {
                a_color = 0;
                a_flag = false;
            }
        }
    }

    /*
    //Textを透明から戻す
    void TextApper(bool a_flag)
    {
        //a_flagがfalseの間実行する
        if (!a_flag)
        {
            //テキストの透明度を変更する
            mainText.color = new Color(0, 0, 0, a_color);
            a_color += Time.deltaTime / 2;
            //透明度が1になったら終了する。
            if (a_color > 1)
            {
                a_color = 1;
                a_flag = true;
            }
        }
    }
    */

}
