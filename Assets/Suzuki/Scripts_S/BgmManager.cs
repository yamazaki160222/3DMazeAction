using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BgmManager : MonoBehaviour
{
    static GameObject mInstance;
    [SerializeField] bool onPlay;

    public static GameObject Instance
    {
        get
        {
            return mInstance;
        }
    }

    public bool OnPlay{
        get => this.onPlay;
        set => this.onPlay = value;
    }

    void Awake()
    {
        if (mInstance == null)
        {
            DontDestroyOnLoad(gameObject);
            mInstance = gameObject;
            onPlay = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /*
    void Start()
    {
        mInstance = gameObject;
        DontDestroyOnLoad(gameObject);
        onPlay = true;
    }
    */

    // Update is called once per frame
    void Update()
    {
        if(!onPlay)     //GameConから干渉
        {
            gameObject.SetActive(false);
            mInstance = null;
        }
    }

}
  
