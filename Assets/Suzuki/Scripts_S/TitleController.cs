﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour
{
    public GameObject titleNejiko;
    public GameObject titleWalls;
    
    AudioSource se;

    // Start is called before the first frame update
    void Start()
    {
        BgmManager.Instance.GetComponent<BgmManager>().OnPlay = true;
        BgmManager.Instance.GetComponent<AudioSource>().Play();

        GetChildren(titleWalls);
        se = GetComponent<AudioSource>();
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

    public void OnStartButtonClicked()
    {
        se.Play();
        Invoke("LoadScene", 0.5f);

    }

    void LoadScene()
    {
        BgmManager.Instance.GetComponent<BgmManager>().OnPlay = false;
        BgmManager.Instance.SetActive(false);
        SceneManager.LoadScene("Stage1Kai");
    }

        // Update is called once per frame
        void Update()
    {
        
    }
}
