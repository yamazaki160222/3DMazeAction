using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    public GameObject titleNejiko;
    public GameObject titleWalls;
    AudioSource se;

    public Text leftText;
    public Text rightText;
    public Text scoreText;

    bool a_flag;

    // Start is called before the first frame update
    void Start()
    {
        GetChildren(titleWalls);

        BgmManager.Instance.GetComponent<BgmManager>().OnPlay = true;
        //BgmManager.Instance.SetActive(true);
        BgmManager.Instance.GetComponent<AudioSource>().Play();

        titleNejiko.GetComponent<Animator>().SetBool("gameOver", true);
        se = GetComponent<AudioSource>();
        scoreText.text = "Score : " + PlayerPrefs.GetInt("stageScore") + " Stage";

    }

    void GetChildren(GameObject obj)
    {
        Transform children = obj.GetComponentInChildren<Transform>();
        //子要素がいなければ終了
        if (children.childCount == 0)
        {
            return;
        }

        foreach (Transform ob in children)
        {
            if (ob.gameObject.GetComponent<FadeHidden>() != null)
            {
                ob.gameObject.GetComponent<FadeHidden>().enabled = false;
            }
            GetChildren(ob.gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            se.Play();
            SceneManager.LoadScene("Title");
        }
    }
}
